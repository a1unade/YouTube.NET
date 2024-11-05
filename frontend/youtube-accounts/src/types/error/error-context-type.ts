export type ErrorContextType = {
  setErrorAndRedirect: (message: string | null) => void;
  clearError: () => void;
  error: string | null;
};
