import { CommentType } from '../../types/comment/comment-type.ts';
import { useState } from 'react';
import {
  ButtonDislikeIcon,
  ButtonDislikeIconFilled,
  ButtonLikeIcon,
  ButtonLikeIconFilled,
} from '../../assets/icons.tsx';

const Comment = (props: { comment: CommentType }) => {
  const { comment } = props;
  const [liked, setLiked] = useState(false);
  const [disliked, setDisliked] = useState(false);

  return (
    <div className="player-comment">
      <div className="main-video-info" style={{ marginRight: 20, gap: 20, width: '100%' }}>
        <div className="author-image">
          <img src={comment.authorProfileImageUrl} alt="" />
        </div>
        <div className="main-video-details" style={{ width: '100%' }}>
          <div className="main-video-name" style={{ fontSize: 12 }}>
            <span>
              <b>{comment.authorDisplayName}</b>
            </span>
          </div>
          <div
            dangerouslySetInnerHTML={{
              __html: comment.textDisplay,
            }}
          />
          <div className="comment-footer">
            <div
              onClick={() => {
                disliked ? setDisliked(false) : null;
                setLiked(!liked);
              }}
            >
              {liked ? <ButtonLikeIconFilled /> : <ButtonLikeIcon />}
            </div>
            <div
              onClick={() => {
                liked ? setLiked(false) : null;
                setDisliked(!disliked);
              }}
            >
              {disliked ? <ButtonDislikeIconFilled /> : <ButtonDislikeIcon />}
            </div>
            <button>Ответить</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Comment;
