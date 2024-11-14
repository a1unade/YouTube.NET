export type PostItemType = {
  id: string;
  text: string;
  attachment: {
    type: string;
    file: string;
  } | null;
  likeCount: number;
  dislikeCount: number;
  commentCount: number;
  date: string;
};
