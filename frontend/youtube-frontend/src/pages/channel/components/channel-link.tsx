import { useEffect, useState } from 'react';

const ChannelLink = (props: { link: string }) => {
  const { link } = props;
  const [icon, setIcon] = useState<string>(
    'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcQ_Hj2m1Axyy6URWznezjuUGTVuNrHHBdMcIE9nkMU6Xr9uYtJealIFTREjelj3mhyQHZVzectRvH1N0Ho',
  );
  const [title, setTitle] = useState<string>('');

  useEffect(() => {
    if (link.includes('vk.com')) {
      setIcon(
        'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcS1-VPo2Z7lAOL4BfCyh5lBecVKGmQkzxMydD8aROfe00oEECTF6vlVRFDmchhBQd3hr9gvFAjO-oXk0JEb0ezhuWiJ2qi-muZJUi0',
      );
      setTitle('VK');
    }

    if (link.includes('instagram.com')) {
      setIcon(
        'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcQqzHpvesYBsmdBWDYRugCp85G91_-8BqpRJCqkxzl3sWgZa49u-jBCd5bzJhXJYOZjTQYo5t8mhvsLis-Syy1nIqFtNrFoovCj3bE34cmrKiRjMopfxQ',
      );
      setTitle('Instagram');
    }

    if (link.includes('steamcommunity.com') || link.includes('steam.com')) {
      setIcon(
        'https://encrypted-tbn2.gstatic.com/favicon-tbn?q=tbn:ANd9GcTcAkmqa2y6HbgT3tVJfh8DSlPS_677TnjHl0Y-hpg0cEwzKzWyhrADCcQEAeWvmikWzdxoFTIcbHPEDJ7Wk8VEdOOURSp2xwT3EqNzmifIrrezWIihDw',
      );
      setTitle('Steam');
    }

    if (link.includes('twitter.com')) {
      setIcon(
        'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcSIGOvV0HrRKAe84vUZ6ERueQkPeWvMeXh3GnPbDURoLwDZKgKa8UmHT8gaeKuE-Xc9u9GcWApNuAUb3HMDYQ7VlMk74e03fpyx3clY0_lb0g',
      );
      setTitle('Twitter');
    }

    if (link.includes('tiktok.com')) {
      setIcon(
        'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcTcBLjXcdPdrvf7vm3krYSrE5nTsulUW2d7SiAXUyOmJ3k7LmJ3ismlN7eU4GinNoUQc6_0xtDkc8rWHFdDmQzlI6UOKmQQscwltJ46aBUE8zThNA',
      );
      setTitle('TikTok');
    }

    if (link.includes('twitch.com')) {
      setIcon(
        'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcS5Im5cduSWmAQu5hA19JhpStaFN4bu-W6z9WejTaLN7sTmmiFn6jpZ-LH5D2FbnNry71xS4dqLRfAWlnyY6UiZMJbNQnC-jw3z--FvIcTPw_hk',
      );
      setTitle('Twitch');
    }

    if (link.includes('t.me')) {
      setIcon(
        'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcRCYEQiHIZLqxVS7H7Zny-ZPp2ZSkTGwtpZGTC2UQwiitNrmeo_NRhSCYSq5q1OVn-2ZozC9R4dl45nfn-ytcGd6lPp6uBXFmcO',
      );
      setTitle('Telegram');
    }
  }, [link, icon]);

  return (
    <div className="channel-link">
      <div className="channel-link-icon">
        <img src={icon} alt="" />
      </div>
      <div className="channel-link-detailed">
        <span>{title}</span>
        <a href={link}>{link}</a>
      </div>
    </div>
  );
};

export default ChannelLink;
