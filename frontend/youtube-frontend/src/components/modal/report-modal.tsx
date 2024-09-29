import React, { useEffect, useState } from 'react';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';

const ReportModal = (props: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { active, setActive } = props;
  const { addAlert } = useAlerts();
  const [selectedReason, setSelectedReason] = useState<string | null>(null);

  useEffect(() => {
    if (active) {
      document.body.style.overflow = 'hidden';
    } else {
      document.body.style.overflow = '';
    }

    return () => {
      document.body.style.overflow = '';
    };
  }, [active]);

  const handleReasonSelect = (reason: string) => {
    setSelectedReason(reason);
  };

  return (
    <div>
      <div
        className={`modal-overlay ${active ? 'active' : ''}`}
        onClick={() => setActive(false)}
        role="dialog"
      >
        <div className="modal-content" style={{ width: 450 }} onClick={(e) => e.stopPropagation()}>
          <div className="video-modal-window" style={{ padding: 20 }}>
            <div className="reports-block">
              <span style={{ fontSize: 16, marginLeft: 5 }}>Причина жалобы</span>
              <div
                className="report-block-content"
                onClick={() => handleReasonSelect('Содержавние сексуального характера')}
              >
                <input
                  type="radio"
                  name="report-reason"
                  checked={selectedReason === 'Содержавние сексуального характера'}
                  readOnly
                />
                <span>Содержавние сексуального характера</span>
              </div>
              <div
                className="report-block-content"
                onClick={() => handleReasonSelect('Жестокое или отталкивающее содержание')}
              >
                <input
                  type="radio"
                  name="report-reason"
                  checked={selectedReason === 'Жестокое или отталкивающее содержание'}
                  readOnly
                />
                <span>Жестокое или отталкивающее содержание</span>
              </div>
              <div
                className="report-block-content"
                onClick={() => handleReasonSelect('Дискриминационные высказывания и оскорбления')}
              >
                <input
                  type="radio"
                  name="report-reason"
                  checked={selectedReason === 'Дискриминационные высказывания и оскорбления'}
                  readOnly
                />
                <span>Дискриминационные высказывания и оскорбления</span>
              </div>
              <div
                className="report-block-content"
                onClick={() => handleReasonSelect('Вредные или опасные действия')}
              >
                <input
                  type="radio"
                  name="report-reason"
                  checked={selectedReason === 'Вредные или опасные действия'}
                  readOnly
                />
                <span>Вредные или опасные действия</span>
              </div>
              <div className="report-block-content" onClick={() => handleReasonSelect('Спам')}>
                <input
                  type="radio"
                  name="report-reason"
                  checked={selectedReason === 'Спам'}
                  readOnly
                />
                <span>Спам</span>
              </div>
            </div>
            <div className="report-buttons">
              <button
                onClick={() => {
                  setActive(false);
                  setSelectedReason(null);
                }}
              >
                Отмена
              </button>
              <button
                onClick={() => {
                  setActive(false);
                  addAlert('Жалоба отправлена.');
                  setSelectedReason(null);
                }}
                disabled={!selectedReason}
                className={selectedReason ? 'active' : 'disabled'}
              >
                Пожаловаться
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ReportModal;
