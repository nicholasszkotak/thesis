import React from 'react';

export const PortsView = (props) => {

  let {ports} = props;
  ports = ports.sort((a, b) => a.type > b.type ? 1 : (a.type < b.type ? -1 : 0));

  return (
    <div>
        {ports.map(p => 
          <div>{`${p.quantity} x ${p.type}`}</div>
        )}
    </div>
  );
}