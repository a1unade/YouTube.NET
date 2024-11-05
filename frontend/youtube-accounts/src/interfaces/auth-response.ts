export interface AuthResponse {
  userId: string;
  token: string;
  isSuccessfully: boolean;
  message: string | null;
}
