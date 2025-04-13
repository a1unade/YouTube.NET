import React, { useEffect, useState } from 'react';
import apiClient from '../../utils/apiClient.ts';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';
import { PaymentResponse } from '../../interfaces/payment/payment-response.ts';

type Plan = 'month' | 'half' | 'year' | '';

const mapPrice: Record<Plan, number> = {
  month: 249,
  half: 1349,
  year: 2249,
  '': 0,
};

const PaymentModal = ({
  active,
  setActive,
  userId,
  balanceId,
}: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
  userId: string | null;
  balanceId: string;
}) => {
  const [cardNumber, setCardNumber] = useState('1234 5678 9012 3456');
  const [ownerName, setOwnerName] = useState('ИМЯ ВЛАДЕЛЬЦА');
  const [expiryDate, setExpiryDate] = useState('12/29');
  const [balance, setBalance] = useState('');
  const [selectedPlan, setSelectedPlan] = useState<Plan>('');
  const { addAlert } = useAlerts();

  useEffect(() => {
    if (!userId || userId === '') setActive(false);

    document.body.style.overflow = active ? 'hidden' : '';
    setCardNumber('1234 5678 9012 3456');
    setOwnerName('ИМЯ ВЛАДЕЛЬЦА');
    setExpiryDate('12/29');

    apiClient.get<PaymentResponse>(`Payment/GetBalance/Id=${balanceId}`).then((res) => {
      setBalance(res.data.message);
    });
    return () => {
      document.body.style.overflow = '';
    };
  }, [active]);

  const processPayment = () => {
    apiClient
      .post<PaymentResponse>('Payment/ProcessPayment', {
        id: userId,
        price: mapPrice[selectedPlan],
      })
      .then((res) => {
        if (res.status === 200) {
          addAlert(res.data.message);
        } else {
          addAlert('Произошла ошибка');
        }
      });
  };

  const handleCardNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    let value = e.target.value.replace(/\D/g, '');
    if (value.length > 16) value = value.slice(0, 16);
    value = value.replace(/(\d{4})(?=\d)/g, '$1 ');
    setCardNumber(value);
  };

  const handleOwnerNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    let value = e.target.value.replace(/[^A-Za-zА-Яа-яЁё\s]/g, '');
    if (value.length > 20) value = value.slice(0, 20);
    setOwnerName(value.toUpperCase());
  };

  const handleExpiryDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    let value = e.target.value.replace(/\D/g, '');
    if (value.length > 4) value = value.slice(0, 4);
    if (value.length >= 3) value = value.slice(0, 2) + '/' + value.slice(2, 4);
    setExpiryDate(value);
  };

  const handleSubscriptionChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedPlan(e.target.value as Plan);
  };

  return (
    <div>
      <div
        className={`modal-overlay ${active ? 'active' : ''}`}
        onClick={() => setActive(false)}
        role="dialog"
      >
        <div className="modal-content" style={{ width: 550 }} onClick={(e) => e.stopPropagation()}>
          <div className="video-modal-window" style={{ padding: 20 }}>
            <h1>Остался один шаг</h1>
            <div style={{ fontSize: 18, marginBottom: 20 }}>Выберите тип подписки</div>

            <div className="premium-selector">
              <select value={selectedPlan} onChange={handleSubscriptionChange}>
                <option value="" disabled={true} hidden={true}>
                  Тип подписки
                </option>
                <option value="month">1 месяц — {mapPrice['month']}₽</option>
                <option value="half">6 месяцев — {mapPrice['half']}₽</option>
                <option value="year">12 месяцев — {mapPrice['year']}₽</option>
              </select>
              <label>Тип подписки</label>
            </div>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                flexDirection: 'row',
                width: 440,
                gap: 40,
                justifyContent: 'right',
                marginTop: 40,
              }}
            >
              <h3 style={{ marginBottom: 20, marginTop: 20 }}>Ваш баланс: </h3>
              <span>{balance}₽</span>
            </div>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                flexDirection: 'row',
                width: 440,
                gap: 40,
                justifyContent: 'right',
                marginTop: 40,
              }}
            >
              <h3 style={{ marginBottom: 20, marginTop: 20 }}>К оплате: </h3>
              <span>{mapPrice[selectedPlan]}₽</span>
            </div>
            <div style={{ marginBottom: 20 }}>
              <svg
                width="100%"
                height="180"
                viewBox="0 0 400 180"
                xmlns="http://www.w3.org/2000/svg"
              >
                <rect x="0" y="0" width="400" height="180" rx="16" fill="#1E1E1E" />

                <foreignObject x="20" y="30" width="360" height="60">
                  <input
                    type="text"
                    placeholder={cardNumber}
                    value={cardNumber}
                    onChange={handleCardNumberChange}
                    style={{
                      width: '100%',
                      height: '100%',
                      fontSize: '20px',
                      color: 'white',
                      backgroundColor: 'transparent',
                      border: 'none',
                      outline: 'none',
                    }}
                  />
                </foreignObject>

                <foreignObject x="20" y="90" width="360" height="40">
                  <input
                    type="text"
                    placeholder={ownerName}
                    value={ownerName}
                    onChange={handleOwnerNameChange}
                    style={{
                      width: '100%',
                      height: '100%',
                      fontSize: '14px',
                      color: 'white',
                      backgroundColor: 'transparent',
                      border: 'none',
                      outline: 'none',
                    }}
                  />
                </foreignObject>

                <foreignObject x="320" y="90" width="60" height="40">
                  <input
                    type="text"
                    value={expiryDate}
                    placeholder={expiryDate}
                    onChange={handleExpiryDateChange}
                    style={{
                      width: '100%',
                      height: '100%',
                      fontSize: '14px',
                      color: 'white',
                      backgroundColor: 'transparent',
                      border: 'none',
                      outline: 'none',
                    }}
                  />
                </foreignObject>
              </svg>
            </div>
            <div className="report-buttons">
              <button
                onClick={() => {
                  setActive(false);
                }}
              >
                Отмена
              </button>
              <button
                onClick={() => {
                  processPayment();
                  setActive(false);
                }}
                className="active"
              >
                Оплатить
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PaymentModal;
