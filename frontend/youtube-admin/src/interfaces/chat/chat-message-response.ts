export interface ChatMessageResponse {
  chatId: string;
  message: string;
  attachment: {
    contentType: string;
    fileId: string;
  };
  userId: string;
  messageId: string;
  time: string;
  isRead: boolean;
  date: string;
}
