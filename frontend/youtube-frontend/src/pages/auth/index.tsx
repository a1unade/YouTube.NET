import React, { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

const AuthRedirect: React.FC = () => {
  const { userId, balanceId } = useParams<{ userId: string; balanceId?: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    if (userId) {
      document.cookie = `userId=${userId}; path=/;`;
    }

    if (balanceId) {
      document.cookie = `balanceId=${balanceId}; path=/;`;
    }

    if (userId) {
      navigate('/');
    }
  }, [userId, balanceId, navigate]);

  return <div>Redirecting...</div>;
};

export default AuthRedirect;
