export interface ChatMessage {
  messageId: string;
  message: string;
  senderId: string;
  attachment: {
    type: string;
    file: string;
    link: string;
  } | null;
  time: string;
  isRead: boolean;
  date: string;
}
