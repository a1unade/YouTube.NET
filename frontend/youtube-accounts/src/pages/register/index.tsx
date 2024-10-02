import { useState } from 'react';
import Name from './components/Name';
import Common from './components/Common';
import Email from './components/Email';
import Confirmation from './components/Confirmation';
import Password from './components/Password';
import Check from './components/Check';
import Terms from './components/Terms';

const Register = () => {
  const [containerContent, setContainerContent] = useState(0);
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [gender, setGender] = useState('');

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
          <Password setContainerContent={setContainerContent} containerContent={containerContent} />
        );
      case 4:
        return (
          <Confirmation
            email={email}
            setContainerContent={setContainerContent}
            containerContent={containerContent}
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
