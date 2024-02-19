import React, {useState} from 'react';

export const PortsEdit = (props) => {

  // State
  const defaultPorts = [
    {type: "USB 2.0", quantity: 0},
    {type: "USB 3.0", quantity: 0},
    {type: "USB C", quantity: 0}
  ];

  const [editedPorts, setEditedPorts] = useState(props.ports ?? defaultPorts);

  // Handlers
  const handlePortChanged = (portType, e) => {
    // Update the edited port to have the desired quantity
    const newPorts = [...editedPorts];
    const index = newPorts.findIndex(p => p.type === portType);
    newPorts[index].quantity = e.currentTarget.value;
    setEditedPorts(newPorts);

    props.onPortsEdited(newPorts);
  }

  const sortedPorts = editedPorts.sort((a, b) => a.type > b.type ? 1 : (a.type < b.type ? -1 : 0));

  return (
    <div>
     {sortedPorts.map(p => 
        <div>
          <input type="number" value={p.quantity} onChange={(e) => handlePortChanged(p.type, e)} />
          <span>x ${p.type}`</span>
        </div>
      )}
    </div>
   
  );
}