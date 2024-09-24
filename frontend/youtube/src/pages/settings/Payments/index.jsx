import React from 'react';
import './style.css';

const Payments = () => {
    return (
        <div className="payments-block">
            <h2 className="block-title">Расчеты и платежи</h2>

            <div className="payments-info">
                <p className="info-header">Укажите, как вы будете совершать покупки на YouTube</p>
            </div>
            <hr className="divider" />
            <h2 className="block-title">Купленные ранее подписки:</h2>
            <table className="subscription-table">
                <thead>
                    <tr>
                        <th className="column-header">Название подписки</th>
                        <th className="column-header">Дата начала</th>
                        <th className="column-header">Дата конца</th>
                        <th className="column-header">Стоимость</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Premium</td>
                        <td>22.03.2024</td>
                        <td>23.04.2023</td>
                        <td>499 руб.</td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};

export default Payments;