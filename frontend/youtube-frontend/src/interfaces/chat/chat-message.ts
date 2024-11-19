/**
 * Интерфейс, представляющий сообщение в чате.
 *
 * Используется для передачи данных о сообщении, включая текст,
 * время отправки, отправителя, статус прочтения и возможные вложения.
 *
 * @interface ChatMessage
 * @property {string} messageId - Идентификатор сообщения.
 * @property {string} message - Текст сообщения.
 * @property {string} time - Время отправки сообщения в формате HH:mm.
 * @property {string} senderId - Идентификатор отправителя сообщения.
 * @property {boolean} isRead - Статус прочтения сообщения: `true` - прочитано, `false` - не прочитано.
 * @property {Object | null} [attachment] - Информация о вложении, если оно присутствует.
 * @property {'image' | 'file'} attachment.type - Тип вложения, может быть `image` или `file`.
 * @property {string} attachment.link - Ссылка на вложение.
 * @property {string} [attachment.file] - Имя файла для загрузки; доступно только при типе вложения `file`.
 * @property {string} date - Дата отправки сообщения в формате YYYY-MM-DD.
 */
export interface ChatMessage {
  messageId: string;
  message: string;
  time: string;
  senderId: string;
  isRead: boolean;
  attachment?: {
    type: 'image' | 'file';
    link: string;
    file?: string;
  } | null;
  date: string;
}
