/* istanbul ignore file */

import { useState } from 'react';
import Email from './components/Email';
import Password from './components/Password';
import apiClient from '../../utils/api-client.ts';
import { useErrors } from '../../hooks/error/use-errors.ts';
import { jwtDecode } from 'jwt-decode';
import { JWTTokenDecoded } from '../../interfaces/jwt-token-decoded.ts';
import { AuthResponse } from '../../interfaces/auth-response.ts';
import Confirmation from './components/Confirmation.tsx';

const Sign = () => {
  const [containerContent, setContainerContent] = useState(0);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { setErrorAndRedirect } = useErrors();

  const processLogin = () => {
    apiClient
      .post<AuthResponse>('Auth/Login', {
        password: password,
        email: email,
      })
      .then((response) => {
        if (response.status === 200) {
          setContainerContent(containerContent + 1);
          const decodedJWT = jwtDecode<JWTTokenDecoded>(response.data.token);
          if (decodedJWT.Role === 'User') {
            // Редирект на ютуб
            window.location.href = `http://localhost:5172/auth/${decodedJWT.Id}`;
          } else {
            // Редирект на админку
            window.location.href = `http://localhost:5174/auth/${decodedJWT.Id}`;
          }
        } else {
          setErrorAndRedirect(response.data.message);
        }
      })
      .catch((error) => {
        const errorMessage = error.response?.data.Error || null;
        setErrorAndRedirect(errorMessage);
      });
  };

  const renderContainerContent = () => {
    switch (containerContent) {
      case 0:
        return (
          <Email
            setEmail={setEmail}
            email={email}
            setContainerContent={setContainerContent}
            containerContent={containerContent}
          />
        );
      case 1:
        return (
          <Password
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            setPassword={setPassword}
            email={email}
            password={password}
            processLogin={processLogin}
          />
        );
      case 2:
        return <Confirmation email={email} />;
      default:
        return null;
    }
  };

  return <>{renderContainerContent()}</>;
};

export default Sign;
