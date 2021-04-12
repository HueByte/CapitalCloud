import React from 'react'
import './wheel.css';
import BasicLayout from '../../core/BasicLayout';

const Wheel = () => {
    return (
        <>
            <div className="main__wrapepr">
                <div className="game__container">
                    <div className="place-bet">
                        xxx
                    </div>
                    <div className="wheel__container">
                        <div className="wheel">
                            ama wheel icon
                        </div>
                    </div>
                    <div className="history">
                        hiiiii
                    </div>
                </div>
                <div className="bets__container">
                    <div className="bets">
                        <div className="bet-button">x2</div>
                        <div className="bet-people">x</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x3</div>
                        <div className="bet-people">x</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x5</div>
                        <div className="bet-people">x</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x50</div>
                        <div className="bet-people">x</div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Wheel;