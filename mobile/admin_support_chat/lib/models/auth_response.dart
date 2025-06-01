class AuthResponse {
  final String userId;
  final bool isSuccessfully;
  final String? message;
  final String? entityId;

  AuthResponse({
    required this.userId,
    required this.isSuccessfully,
    this.message,
    this.entityId,
  });

  factory AuthResponse.fromJson(Map<String, dynamic> json) {
    return AuthResponse(
      userId: json['userId'] ?? '',
      isSuccessfully: json['isSuccessfully'] ?? false,
      message: json['message'],
      entityId: json['entityId'],
    );
  }
}
