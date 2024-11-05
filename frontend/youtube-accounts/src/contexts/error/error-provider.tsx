import React, { createContext, useState } from 'react';
import { ErrorContextType } from '../../types/error/error-context-type';
import { useNavigate } from 'react-router-dom';

export const ErrorContext = createContext<ErrorContextType | undefined>(undefined);

export const ErrorProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const setErrorAndRedirect = (message: string | null) => {
    setError(message);
    navigate('/error');
  };

  const clearError = () => {
    setError(null);
    navigate('/');
  };

  return (
    <ErrorContext.Provider value={{ setErrorAndRedirect, clearError, error }}>
      {children}
    </ErrorContext.Provider>
  );
};
