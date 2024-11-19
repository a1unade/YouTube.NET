import { useEffect, useState } from 'react';

/**
 * Компонент уведомления.
 *
 * Отображает сообщение и автоматически скрывается через 2 секунды.
 *
 * @param {Object} props - Свойства компонента.
 * @param {string} props.message - Текст сообщения, отображаемого в уведомлении.
 * @param {() => void} props.onClose - Функция, вызываемая при закрытии уведомления.
 *
 * @returns {JSX.Element} Возвращает элемент интерфейса уведомления.
 *
 * @example Пример использования:
 *   <Alert message="Успешно сохранено!" onClose={() => console.log('Закрыто')} />
 */

const Alert = (props: { message: string; onClose: () => void }): JSX.Element => {
  const { message, onClose } = props;
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const showTimer = setTimeout(() => {
      setVisible(true);
    }, 10);

    const hideTimer = setTimeout(() => {
      setVisible(false);
    }, 2000);

    return () => {
      clearTimeout(showTimer);
      clearTimeout(hideTimer);
    };
  }, []);

  useEffect(() => {
    if (!visible) {
      const removeTimer = setTimeout(() => {
        onClose();
      }, 500);

      return () => clearTimeout(removeTimer);
    }
  }, [visible, onClose]);

  return <div className={`alert ${visible ? 'alert-show' : 'alert-hide'}`}>{message}</div>;
};

export default Alert;
