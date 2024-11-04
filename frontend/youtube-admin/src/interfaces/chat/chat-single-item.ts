export interface ChatSingleItem {
  userName: string;
  avatarUrl: string | null;
  lastMessage: {
    senderId: string;
    messageId: string;
    message: string;
    time: string;
    isRead: boolean;
  } | null;
  chatId: string;
}
