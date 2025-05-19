class PlayerVideo {
  final String videoFileId;
  final String previewId;
  final int viewCount;
  final String name;
  final String realiseDate;
  final String channelName;

  PlayerVideo({
    required this.videoFileId,
    required this.previewId,
    required this.viewCount,
    required this.name,
    required this.realiseDate,
    required this.channelName,
  });

  factory PlayerVideo.fromJson(Map<String, dynamic> json) {
    return PlayerVideo(
      videoFileId: json['videoFileId'] ?? '',
      previewId: json['previewId'] ?? '',
      viewCount: json['viewCount'] ?? 0,
      name: json['name'] ?? '',
      realiseDate: json['realiseDate'] ?? '',
      channelName: json['channelName'] ?? '',
    );
  }
}
