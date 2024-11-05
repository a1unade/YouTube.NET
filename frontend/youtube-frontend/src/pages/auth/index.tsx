import React, { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

const AuthRedirect: React.FC = () => {
  const { userId } = useParams<{ userId: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    if (userId) {
      document.cookie = `userId=${userId}; path=/;`;
      navigate('/');
    }
  }, [userId, navigate]);

  return <div>Redirecting...</div>;
};

export default AuthRedirect;
