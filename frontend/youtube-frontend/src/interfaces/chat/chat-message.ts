export interface ChatMessage {
  messageId: string;
  message: string;
  senderId: string;
  attachment: {
    contentType: string;
    fileId: string;
  };
  time: string;
  isRead: boolean;
  date: string;
}
