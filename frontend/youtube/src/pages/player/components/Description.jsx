// eslint-disable-next-line react/prop-types
const Description = ({ description, onViewLocationClick }) => {
  const convertLinks = (description) => {
    const urlRegex = /(https?:\/\/[^\s]+)/g;
    // eslint-disable-next-line react/prop-types
    return description.replace(urlRegex, '<a href="$1" target="_blank">$1</a>');
  };

  return (
    <>
      <div dangerouslySetInnerHTML={{ __html: convertLinks(description) }} />
      <a href="#" onClick={onViewLocationClick}>
        Посмотреть локацию
      </a>
    </>
  );
};

export default Description;
