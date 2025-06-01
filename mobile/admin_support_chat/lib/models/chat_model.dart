class ChatSummary {
  final String chatId;
  final String userName;
  final String? avatarUrl;
  final String? lastMessage;

  ChatSummary({
    required this.chatId,
    required this.userName,
    this.avatarUrl,
    this.lastMessage,
  });

  factory ChatSummary.fromJson(Map<String, dynamic> json) {
    return ChatSummary(
      chatId: json['chatId'],
      userName: json['userName'],
      avatarUrl: json['avatarUrl'],
      lastMessage: json['lastMessage'],
    );
  }
}