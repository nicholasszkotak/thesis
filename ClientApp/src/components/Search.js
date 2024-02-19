import React, { useState } from 'react';
import { ComputerService } from '../services/computerService';

export const Search = (props) => {

  // State
  const [searchForm, setSearchForm] = useState({});
  const handleFieldChanged = (propName, e) => {
    setSearchForm({
      ...searchForm,
      [propName]: e.currentTarget.value
    });
  }

  // Props
  const computerService = props.computerService ?? ComputerService;

  // Handlers

  const handleSearchClicked = async () => {
    var results = await computerService.searchComputers(searchForm)
    await props.onSearchCompleted(results);
  }

  return (
      <table>
        <thead>
          <tr>
            <th>RAM</th>
            <th>Disk Space</th>
            <th>Disk Type</th>
            <th>Graphics Card</th>
            <th>Weight</th>
            <th>Processor</th>
            <th>Power</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <input type="number" name="ram" value={searchForm.ram} onChange={(e) => handleFieldChanged("ram", e)} />
            </td>
            <td>
              <input type="number" name="diskSpace" value={searchForm.diskSpace} onChange={(e) => handleFieldChanged("diskSpace", e)} />
            </td>
            <td>
              <input name="diskType" value={searchForm.diskType} onChange={(e) => handleFieldChanged("diskType", e)} />
            </td>
            <td>
              <input name="graphicsCard" value={searchForm.graphicsCard} onChange={(e) => handleFieldChanged("graphicsCard", e)} />
            </td>
            <td>
              <input name="weight" value={searchForm.weight} onChange={(e) => handleFieldChanged("weight", e)} />
            </td>
            <td>
              <input name="processor" value={searchForm.processor} onChange={(e) => handleFieldChanged("processor", e)} />
            </td>
            <td>
              <input type="number" name="power" value={searchForm.power} onChange={(e) => handleFieldChanged("power", e)} />
            </td>
            <td>
              <button onClick={handleSearchClicked}>Search</button>
            </td>
          </tr>
        </tbody>
      </table>
      
  );
}