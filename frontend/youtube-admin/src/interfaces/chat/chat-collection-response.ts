import { ChatSingleItem } from "./chat-single-item.ts";

export interface ChatCollectionResponse {
  chatCardDtos: ChatSingleItem[];
  isSuccessfully: boolean;
  message: string | null;
}
