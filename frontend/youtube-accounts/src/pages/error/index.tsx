const Error = () => {
  return (
    <div className="header">
      <h1 style={{ maxWidth: 300 }}>Ошибка</h1>
      <div className="notice" style={{ marginLeft: 0, marginTop: 30, maxWidth: 350 }}>
        <span style={{ fontSize: 14, lineHeight: 1.5 }}>
          Произошла ошибка, повторите попытку позже
        </span>
      </div>
    </div>
  );
};

export default Error;
