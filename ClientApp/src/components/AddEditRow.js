import React, { useState } from 'react';
import { ComputerService } from '../services/computerService';
import { PortsEdit } from './PortsEdit';

export const AddEditRow = (props) => {

  // State
  const [editedComputer, setEditedComputer] = useState({...props.computer});

  // Handlers
  const handleFieldChanged = (propName, e) => {
    setEditedComputer({
      ...editedComputer,
      [propName]: e.currentTarget.value
    });
  }
  const handlePortsEdited = (newPorts) => {
    setEditedComputer({
      ...editedComputer,
      ports: newPorts
    });
  }

  // Props
  const computerService = props.computerService ?? ComputerService;

  // Handlers

  const handleSaveClicked = async () => {
    await computerService.saveComputer(editedComputer)
    await props.onSaveComplete(editedComputer);
  }

  const handleCancelClicked = () => props.onCancel();

  return (
      <tr key={editedComputer.id}>
        <td>
          <input type="number" name="ram" value={editedComputer.ram} onChange={(e) => handleFieldChanged("ram", e)} />
        </td>
        <td>
          <PortsEdit ports={editedComputer.ports} onPortsEdited={handlePortsEdited} />
        </td>
        <td>
          <input type="number" name="diskSpace" value={editedComputer.diskSpace} onChange={(e) => handleFieldChanged("diskSpace", e)} />
        </td>
        <td>
          <input name="diskType" value={editedComputer.diskType} onChange={(e) => handleFieldChanged("diskType", e)} />
        </td>
        <td>
          <input name="graphicsCard" value={editedComputer.graphicsCard} onChange={(e) => handleFieldChanged("graphicsCard", e)} />
        </td>
        <td>
          <input name="weight" value={editedComputer.weight} onChange={(e) => handleFieldChanged("weight", e)} />
        </td>
        <td>
          <input name="processor" value={editedComputer.processor} onChange={(e) => handleFieldChanged("processor", e)} />
        </td>
        <td>
          <input type="number" name="power" value={editedComputer.power} onChange={(e) => handleFieldChanged("power", e)} />
        </td>
          <td>
            <button onClick={handleSaveClicked}>Save</button>
            { editedComputer.id && <button onClick={handleCancelClicked}>Cancel</button> }
          </td>
      </tr>
  );
}