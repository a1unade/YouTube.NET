import { ChatMessage } from './chat-message.ts';

export interface ChatHistoryResponse {
  chatMessages: ChatMessage[];
  isSuccessfully: boolean;
  message: string | null;
}
