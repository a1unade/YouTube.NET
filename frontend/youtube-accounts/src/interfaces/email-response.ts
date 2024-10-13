export interface EmailResponse {
  newUser: boolean;
  confirmation: boolean;
  error: string | undefined;
}
