class WalletResponse {
  final bool isSuccessfully;
  final String? message;
  final String? entityId;

  WalletResponse({
    required this.isSuccessfully,
    this.message,
    this.entityId,
  });

  factory WalletResponse.fromJson(Map<String, dynamic> json) {
    return WalletResponse(
      isSuccessfully: json['isSuccessfully'] ?? false,
      message: json['message'],
      entityId: json['entityId'],
    );
  }
}