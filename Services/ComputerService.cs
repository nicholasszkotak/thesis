using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thesis_exercise.DataAccess;
using thesis_exercise.DataAccess.Dtos;
using thesis_exercise.Services.Models;

namespace thesis_exercise.Services
{
    public interface IComputerService {
        
        Task<List<ComputerModel>> GetComputers();

        Task<Guid?> AddComputer(ComputerModel model);
        Task UpdateComputer(ComputerModel model);
        Task DeleteComputer(Guid computerId);

        Task<List<ComputerModel>> SearchComputers(SearchComputersModel model);
    }

    public class ComputerService : IComputerService
    {

        private readonly IComputerDao computerDao;

        public ComputerService(IComputerDao computerDao) {
            this.computerDao = computerDao;
        }
        
         public async Task<List<ComputerModel>> GetComputers() {

            var queryData = await computerDao.GetComputers();
            var portTypes = new List<string>() {"USB 2.0", "USB 3.0", "USB C"};

            // Build a dictionary of ports. Fill in any "empty" port types with a "0" value
            var portsDictionary = queryData.Ports
                .Select(p => new PortModel(
                    p.Id,
                    p.ComputerId,
                    p.Type,
                    p.Quantity
                )).ToList()
                .GroupBy(p => p.ComputerId)
                .ToDictionary(g => g.Key, g => g.ToList());
            
            foreach(var item in portsDictionary)
            {
                var computerId = item.Key;
                var ports = item.Value;

                portTypes.ForEach(type => {
                    if(!ports.Exists(p => p.Type == type))
                        ports.Add(new PortModel(null, computerId, type, 0));
                });
            }

            // Also add ports for any computer that does not have port records
            foreach(var computerId in queryData.Computers.Select(c => c.Id))
            {
                if(!portsDictionary.ContainsKey(computerId.Value))
                {
                    var ports = new List<PortModel>();
                    portTypes.ForEach(type => {
                        ports.Add(new PortModel(null, computerId.Value, type, 0));
                    });
                    portsDictionary[computerId.Value] = ports;
                }
            }

            // Build the computer models by connecting the computers to their ports
            return queryData.Computers.Select(d => new ComputerModel(
                d.Id,
                d.RAM,
                d.DiskSpace,
                d.DiskType,
                d.GraphicsCard,
                d.Weight,
                d.Power,
                d.Processor,
                portsDictionary[d.Id.Value]
            )).ToList();
        }

        public async Task<Guid?> AddComputer(ComputerModel model) {
            return await computerDao.AddComputer(new ComputerData() {
                RAM = model.RAM,
                DiskSpace = model.DiskSpace,
                DiskType = model.DiskType,
                GraphicsCard = model.GraphicsCard,
                Weight = model.Weight,
                Power = model.Power,
                Processor = model.Processor,
                Ports = model.Ports.Select(p => new PortData()
                {
                    Id = p.Id,
                    ComputerId = p.ComputerId,
                    Type = p.Type,
                    Quantity = p.Quantity
                }).ToList()
            });
        }

        public async Task UpdateComputer(ComputerModel model) {
            await computerDao.UpdateComputer(new ComputerData() {
                Id = model.ID,
                RAM = model.RAM,
                DiskSpace = model.DiskSpace,
                DiskType = model.DiskType,
                GraphicsCard = model.GraphicsCard,
                Weight = model.Weight,
                Power = model.Power,
                Processor = model.Processor,
                Ports = model.Ports.Select(p => new PortData()
                {
                    Id = p.Id,
                    ComputerId = p.ComputerId,
                    Type = p.Type,
                    Quantity = p.Quantity
                }).ToList()
            });
        }

        public async Task DeleteComputer(Guid computerId) {
            await computerDao.DeleteComputer(computerId);
        }

        public async Task<List<ComputerModel>> SearchComputers(SearchComputersModel model) {
            
            var data = await computerDao.SearchComputers(new SearchComputersData() {
                RAM = model.RAM,
                DiskType = model.DiskType,
                DiskSpace = model.DiskSpace,
                GraphicsCard = model.GraphicsCard,
                Weight = model.Weight,
                Power = model.Power,
                Processor = model.Processor
            });

             return data.Select(d => new ComputerModel(
                d.Id,
                d.RAM,
                d.DiskSpace,
                d.DiskType,
                d.GraphicsCard,
                d.Weight,
                d.Power,
                d.Processor,
                new List<PortModel>()
            )).ToList();
        }
    }
}