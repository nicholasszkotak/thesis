import React, { useEffect, useState } from 'react';
import { AddEditRow } from './AddEditRow';
import { ComputerService } from "../services/computerService";
import { Search } from './Search';
import { PortsView } from './PortsView';

export const Home = (props) => {

  // State
  const [loading, setLoading] = useState(true);
  const [computers, setComputers] = useState({});
  const [editId, setEditId] = useState("");

  // Props
  const computerService = props.computerService ?? ComputerService;

  // Initial data load
  useEffect(() =>
  {
    const loadData = async () => {
      const computers = await computerService.getComputers();
      setLoading(false);
      setComputers(computers);
    }

    loadData()
      .catch(console.error);
    
  }, []);

  // Handlers
  const handleEditClicked = (computerId) => setEditId(computerId);
  const handleCancelClicked = () => setEditId("");
  const handleDeleteClicked = async (computerId) => {
    try
    {
      await computerService.deleteComputer(computerId);
      setComputers(computers.filter(c => c.id !== computerId));
    }
    catch(e) {
      console.error(e);
    }
  }

  const handleSaveComplete = async () => {

    // Reload the grid - since there aren't a lot of computers in the database, this simplifies the re-render logic.
    setLoading(true);
    const computers = await computerService.getComputers();
    setLoading(false);
    setComputers(computers);
    setEditId("");
  }

  const handleSearchCompleted = (results) => {
    setComputers(results);
  };

  // Return loading state 
  if(loading) 
    return (<p><em>Loading...</em></p>);

  return (
      <div>
        <h1 id="tableLabel">Computers</h1>
        <Search onSearchCompleted={handleSearchCompleted} />
        <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>RAM (MB)</th>
            <th>Ports</th>
            <th>Disk Space (GB)</th>
            <th>Disk Type</th>
            <th>Graphics Card</th>
            <th>Weight (kg)</th>
            <th>Processor</th>
            <th>Power (W)</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {computers.map(c => {
              if(c.id === editId) return <AddEditRow key={c.id} computer={c} onCancel={handleCancelClicked} onSaveComplete={handleSaveComplete} />;
              return (
              <tr key={c.id}>
                <td>{c.ram}</td>
                <td>
                  <PortsView ports={c.ports} />
                </td>
                <td>{c.diskSpace}</td>
                <td>{c.diskType}</td>
                <td>{c.graphicsCard}</td>
                <td>{c.weight}</td>
                <td>{c.processor}</td>
                <td>{c.power}</td>
                <td>
                  <button onClick={() => handleEditClicked(c.id)}>Edit</button>
                  <button onClick={() => handleDeleteClicked(c.id)}>Delete</button>
                </td>
              </tr>
            )
          })}

          <AddEditRow computer={{}} onSaveComplete={handleSaveComplete} />
        </tbody>
      </table>
      </div>
    );
}