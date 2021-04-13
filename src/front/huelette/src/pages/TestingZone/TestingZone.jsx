import React from 'react';
import './TestingZone.css';

const TestingZone = () => {
    return (
        //Working 100%
        // <div className="container-test-absolute">
        //     <div className="shark">
        //         <h1>I'm shark</h1>
        //     </div>
        // </div>

        //Shitty on phones
        <div className="container-100v">
            <div className="shark">
                <h1>I'm shark<br></br>{window.innerWidth} - {window.innerHeight}</h1>
            </div>
        </div>
    )
}

export default TestingZone;