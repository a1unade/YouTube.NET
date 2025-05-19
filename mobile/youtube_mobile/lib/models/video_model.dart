class Video {
  final String previewUrl;
  final String channelImageUrl;
  final String videoName;
  final int views;
  final String releaseDate;
  final String videoId;
  final String channelId;

  Video({
    required this.previewUrl,
    required this.channelImageUrl,
    required this.videoName,
    required this.views,
    required this.releaseDate,
    required this.videoId,
    required this.channelId,
  });

  factory Video.fromJson(Map<String, dynamic> json) {
    return Video(
      previewUrl: json['previewUrl'],
      channelImageUrl: json['channelImageUrl'],
      videoName: json['videoName'],
      views: json['views'],
      releaseDate: json['releaseDate'],
      videoId: json['videoId'],
      channelId: json['channelId'],
    );
  }
}
