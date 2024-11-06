import { ChatMessage } from "./chat-message.ts";

export interface ChatHistoryResponse {
  chatMessages: ChatMessage[];
  pageCount: number;
  isSuccessfully: boolean;
  message: string | null;
}
