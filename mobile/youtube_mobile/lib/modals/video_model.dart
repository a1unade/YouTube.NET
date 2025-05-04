class VideoModel {
  final String title;
  final String channelName;
  final String views;
  final String timeAgo;
  final String thumbnailUrl;
  final String avatarUrl;
  final String videoUrl;

  VideoModel({
    required this.title,
    required this.channelName,
    required this.views,
    required this.timeAgo,
    required this.thumbnailUrl,
    required this.avatarUrl,
    required this.videoUrl,
  });
}
