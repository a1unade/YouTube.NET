import { useState } from 'react';
import Name from './components/Name';
import Common from './components/Common';
import Email from './components/Email';
import Confirmation from './components/Confirmation';
import Password from './components/Password';
import Check from './components/Check';
import Terms from './components/Terms';
import apiClient from '../../utils/api-client.ts';
import { useNavigate } from 'react-router-dom';
import { AuthResponse } from '../../interfaces/auth-response.ts';

const Register = () => {
  const [containerContent, setContainerContent] = useState(0);
  const [email, setEmail] = useState('');
  const [userId, setUserId] = useState('');
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [gender, setGender] = useState('');
  const [password, setPassword] = useState('');
  const [date, setDate] = useState('');
  const navigate = useNavigate();

  const processAuth = () => {
    apiClient
      .post<AuthResponse>('Auth/Auth', {
        password: password,
        email: email,
        name: name,
        surname: surname,
        gender: gender,
        dateOfBirth: date,
        country: 'Россия',
      })
      .then((response) => {
        if (response.status === 200) {
          setContainerContent(containerContent + 1);
          setUserId(response.data.userId);
        } else {
          navigate('/error');
        }
      });
  };

  const renderContainerContent = () => {
    switch (containerContent) {
      case 0:
        return (
          <Name
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            name={name}
            setName={setName}
            surname={surname}
            setSurname={setSurname}
          />
        );
      case 1:
        return (
          <Common
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            gender={gender}
            setGender={setGender}
            setDate={setDate}
          />
        );
      case 2:
        return (
          <Email
            email={email}
            setEmail={setEmail}
            setContainerContent={setContainerContent}
            containerContent={containerContent}
          />
        );
      case 3:
        return (
          <Password
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            password={password}
            setPassword={setPassword}
            processAuth={processAuth}
          />
        );
      case 4:
        return (
          <Confirmation
            email={email}
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            userId={userId}
          />
        );
      case 5:
        return (
          <Check
            setContainerContent={setContainerContent}
            containerContent={containerContent}
            name={name}
            email={email}
          />
        );
      case 6:
        return <Terms setContainerContent={setContainerContent} />;
      default:
        return null;
    }
  };

  return <>{renderContainerContent()}</>;
};

export default Register;
