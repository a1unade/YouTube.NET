export interface ChatMessage {
  message: string;
  author: string;
  attachment: {
    type: string;
    file: string;
    link: string;
  } | null;
  time: string;
  is_read: boolean;
}
