import { useErrors } from '../../hooks/error/use-errors.ts';

const Error = () => {
  const { error, clearError } = useErrors();

  return (
    <>
      <div className="header">
        <h1 style={{ maxWidth: 300 }}>Ошибка</h1>
        <div className="notice" style={{ marginLeft: 0, marginTop: 30, maxWidth: 350 }}>
          <span style={{ fontSize: 14, lineHeight: 1.5 }}>
            {error ? <span>{error}</span> : <span>Произошла ошибка, повторите попытку позже.</span>}
          </span>
        </div>
      </div>
      <div
        style={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          placeSelf: 'end',
          width: '100%',
          marginTop: 200,
        }}
      >
        <button className="right-button" onClick={() => clearError()}>
          Назад
        </button>
      </div>
    </>
  );
};

export default Error;
