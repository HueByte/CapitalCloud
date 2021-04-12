import React from 'react'
import './wheel.css';
import BasicLayout from '../../core/BasicLayout';

const Wheel = () => {
    return (
        <>
            <div className="main__wrapper">
                <div className="game__container">
                    <div className="place-bet">
                        <input type="number" placeholder="I'm your inpuuut"/>
                    </div>
                    <div className="wheel__container">
                        <div className="wheel">
                            It is 100% working wheel
                        </div>
                    </div>
                    <div className="history">
                        hiiiii
                    </div>
                </div>
                <div className="bets__container">
                    <div className="bets">
                        <div className="bet-button">x2</div>
                        <div className="bet-people">We</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x3</div>
                        <div className="bet-people">Are</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x5</div>
                        <div className="bet-people">Some</div>
                    </div>
                    <div className="bets">
                        <div className="bet-button">x50</div>
                        <div className="bet-people">Betters?</div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Wheel;