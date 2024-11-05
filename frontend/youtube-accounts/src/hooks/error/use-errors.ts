import { useContext } from 'react';
import { ErrorContextType } from '../../types/error/error-context-type';
import { ErrorContext } from '../../contexts/error/error-provider.tsx';

export const useErrors = (): ErrorContextType => {
  const context = useContext(ErrorContext);
  if (!context) {
    throw new Error('useErrors must be used within an ErrorProvider');
  }
  return context;
};
