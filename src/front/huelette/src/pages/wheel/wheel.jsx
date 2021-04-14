import React from 'react'
import './wheel.css';
import BasicLayout from '../../core/BasicLayout';

const Wheel = () => {
    // TODO - 
    // Get coins from user
    // Don't show coins/bet input when user isn't logged
    // Add people who bet
    // Add input validator
    // Add proper wheel
    // Add History 
    // Count of users betting on color

    return (
        <div className="main__wrapper">
            <div className="game__container">
                <div className="place-bet__container">
                    <div className="place-bet">
                        <div className="bet-coins">
                            <i class="fas fa-coins"></i> 192 294 422
                        </div>
                        <div className="bets-values">
                            <div className="bets-values-item">Clear</div>
                            <div className="bets-values-item">+10</div>
                            <div className="bets-values-item">+100</div>
                            <div className="bets-values-item">+1k</div>
                            <div className="bets-values-item">+10k</div>
                            <div className="bets-values-item">1/2</div>
                            <div className="bets-values-item">2x</div>
                            <div className="bets-values-item">Max</div>
                        </div>
                        <input type="number" placeholder="enter your bet" />
                    </div>
                </div>
                <div className="wheel__container">
                    <div className="wheel">
                        It is 100% working wheel
                        </div>
                </div>
                <div className="history">

                </div>
            </div>
            <div className="bets__container">
                <div className="bets">
                    <div className="bet-button x2">x2</div>
                    <div className="bet-people">We</div>
                </div>
                <div className="bets">
                    <div className="bet-button x3">x3</div>
                    <div className="bet-people">Are</div>
                </div>
                <div className="bets">
                    <div className="bet-button x5">x5</div>
                    <div className="bet-people">Some</div>
                </div>
                <div className="bets">
                    <div className="bet-button x50">x50</div>
                    <div className="bet-people">Betters?</div>
                </div>
            </div>
        </div>
    )
}

export default Wheel;