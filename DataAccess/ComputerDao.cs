using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using thesis_exercise.DataAccess.Dtos;

namespace thesis_exercise.DataAccess;

public interface IComputerDao {
    Task<GetComputersQueryData> GetComputers();
    Task<Guid?> AddComputer(ComputerData data);
    Task UpdateComputer(ComputerData data);
    Task DeleteComputer(Guid computerId);
    Task<List<ComputerData>> SearchComputers(SearchComputersData data);
}

public class ComputerDao : IComputerDao {

    private readonly IConfiguration configuration;

    public ComputerDao(IConfiguration configuration)
    {
        this.configuration = configuration;
    }


    public async Task<GetComputersQueryData> GetComputers()
    {
        GetComputersQueryData result;

        using(var conn = new SqlConnection(configuration.GetConnectionString("db"))) 
        {
            var computers = (await conn.QueryAsync<ComputerData>(@"SELECT * FROM Computers")).ToList();
            var ports = (await conn.QueryAsync<PortData>(@"SELECT * FROM Ports")).ToList();
            
            result = new GetComputersQueryData() {
                Computers = computers,
                Ports = ports
            };
        }

        return result;
    }

    public async Task<Guid?> AddComputer(ComputerData data) {
        
        Guid? newId = null;
        using(var conn = new SqlConnection(configuration.GetConnectionString("db")))
        {
            var sql = @"
                INSERT INTO Computers 
                (
                    RAM,
                    DiskSpace,
                    DiskType,
                    GraphicsCard,
                    Weight,
                    Power,
                    Processor
                )
                OUTPUT INSERTED.Id
                VALUES
                (
                    @RAM,
                    @DiskSpace,
                    @DiskType,
                    @GraphicsCard,
                    @Weight,
                    @Power,
                    @Processor
                )
            ";

            object parameters = new {
                Ram = data.RAM,
                data.DiskSpace,
                data.DiskType,
                data.GraphicsCard,
                data.Weight,
                data.Power,
                data.Processor
            };

            newId = await conn.QuerySingleAsync<Guid>(sql, parameters);

            // Insert any defined ports
            data.Ports.ForEach(p => p.ComputerId = newId.Value);
            await conn.ExecuteAsync("INSERT INTO Ports (ComputerId, Type, Quantity) VALUES (@ComputerId, @Type, @Quantity)", data.Ports);
        }

        return newId;
    }

    public async Task UpdateComputer(ComputerData data)
    {
        using(var conn = new SqlConnection(configuration.GetConnectionString("db")))
        {
            var updateComputerSql = @"
                UPDATE
                    Computers
                SET
                    RAM = @Ram,
                    DiskSpace = @DiskSpace,
                    DiskType = @DiskType,
                    GraphicsCard = @GraphicsCard,
                    Weight = @Weight,
                    Power = @Power,
                    Processor = @Processor
                WHERE
                    Id = @Id
            ";

            object[] updateComputerParameters = {
                new {
                    Ram = data.RAM,
                    data.DiskSpace,
                    data.DiskType,
                    data.GraphicsCard,
                    data.Weight,
                    data.Power,
                    data.Processor,
                    data.Id
                }
            };

            await conn.ExecuteAsync(updateComputerSql, updateComputerParameters);

            // Replace the ports
            await conn.ExecuteAsync("DELETE FROM Ports WHERE ComputerId = @ComputerId", new { ComputerId = data.Id});
            await conn.ExecuteAsync("INSERT INTO Ports (ComputerId, Type, Quantity) VALUES (@ComputerId, @Type, @Quantity)", data.Ports);
        }
    }

    public async Task DeleteComputer(Guid computerId){

        using (var conn = new SqlConnection(configuration.GetConnectionString("db"))) 
        {
            await conn.ExecuteAsync("DELETE FROM Ports WHERE ComputerId = @Id", new { Id = computerId});
            await conn.ExecuteAsync("DELETE FROM Computers WHERE id = @Id", new { Id = computerId});
        }
    }

    public async Task<List<ComputerData>> SearchComputers(SearchComputersData data)
    {
        var results = new List<ComputerData>();

        using (var conn = new SqlConnection(configuration.GetConnectionString("db"))) 
        {   
            var sql = new StringBuilder();
            sql.Append("SELECT * FROM Computers WHERE 1=1");
            if(!string.IsNullOrWhiteSpace(data.RAM)) sql.Append(" AND RAM = @RAM");
            if(!string.IsNullOrWhiteSpace(data.DiskSpace)) sql.Append(" AND DiskSpace = @DiskSpace");
            if(!string.IsNullOrWhiteSpace(data.DiskType)) sql.Append(" AND DiskType LIKE @DiskType");
            if(!string.IsNullOrWhiteSpace(data.GraphicsCard)) sql.Append(" AND GraphicsCard LIKE @GraphicsCard");
            if(!string.IsNullOrWhiteSpace(data.Weight)) sql.Append(" AND Weight = @Weight");
            if(!string.IsNullOrWhiteSpace(data.Power)) sql.Append(" AND Power = @Power");
            if(!string.IsNullOrWhiteSpace(data.Processor)) sql.Append(" AND Processor LIKE @Processor");
            
            object parameters = new {
                RAM = string.IsNullOrWhiteSpace(data.RAM) ? 0 : Convert.ToInt32(data.RAM),
                DiskSpace = string.IsNullOrWhiteSpace(data.DiskSpace) ? 0 : Convert.ToInt32(data.DiskSpace),
                DiskType = $"%{data.DiskType}%",
                GraphicsCard = $"%{data.GraphicsCard}%",
                Weight = string.IsNullOrWhiteSpace(data.Weight) ? 0m : Convert.ToDecimal(data.Weight),
                Power = string.IsNullOrWhiteSpace(data.Power) ? 0 : Convert.ToInt32(data.Power),
                Processor = $"%{data.Processor}%"
            };

            results = (await conn.QueryAsync<ComputerData>(sql.ToString(), parameters)).ToList();
        }

        return results;
    }
}
