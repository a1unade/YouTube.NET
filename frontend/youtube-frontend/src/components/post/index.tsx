import { PostItemType } from '../../types/post/post-item-type.ts';
import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import {
  ButtonDislikeIcon,
  ButtonDislikeIconFilled,
  ButtonLikeIcon,
  ButtonLikeIconFilled,
  ShareIcon,
  ShowMoreIcon,
} from '../../assets/icons.tsx';
import React, { useRef, useState } from 'react';
import ActionsModal from '../modal/actions-modal.tsx';

const PostSingleItem = (props: {
  post: PostItemType;
  channel: ChannelShortType;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
  setSaveActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { post, channel, setReportVideoActive, setSaveActive } = props;

  const [liked, setLiked] = useState(false);
  const [disliked, setDisliked] = useState(false);
  const buttonRef = useRef<HTMLButtonElement>(null);
  const [actionsModal, setActionsModal] = useState(false);

  const toggleModal = () => {
    setActionsModal(!actionsModal);
  };

  return (
    <>
      <div className="post-single-item-layout">
        <img
          className="circular-avatar"
          style={{ width: 48, height: 48 }}
          src={channel.image}
          alt="avatar"
        />
        <div className="post-info-layout">
          <div className="post-detailed-info">
            <p>{channel.name}</p>
            <span>{post.date}</span>
          </div>
          <span>{post.text}</span>
          {post.attachment !== null ? <img src={post.attachment!.file} alt="attachment" /> : null}
          <div className="post-actions-section">
            <button
              className="post-actions-button"
              onClick={() => {
                disliked ? setDisliked(false) : null;
                setLiked(!liked);
              }}
            >
              {liked ? <ButtonLikeIconFilled /> : <ButtonLikeIcon />}
            </button>
            <button
              className="post-actions-button"
              onClick={() => {
                liked ? setLiked(false) : null;
                setDisliked(!disliked);
              }}
            >
              {disliked ? <ButtonDislikeIconFilled /> : <ButtonDislikeIcon />}
            </button>
            <button className="post-actions-button">
              <ShareIcon />
            </button>
          </div>
        </div>
        <div
          style={{ alignItems: 'start', height: '100%', position: 'relative' }}
          className="post-actions-section"
        >
          <button className="post-actions-button" ref={buttonRef} onClick={toggleModal}>
            <ShowMoreIcon />
          </button>
          <ActionsModal
            active={actionsModal}
            setActive={setActionsModal}
            setReportVideoActive={setReportVideoActive}
            setSaveActive={setSaveActive}
            buttonRef={buttonRef}
          />
        </div>
      </div>
    </>
  );
};

export default PostSingleItem;
