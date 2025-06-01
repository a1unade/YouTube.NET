class LoginResponse {
  final String userId;

  LoginResponse({required this.userId});

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      userId: json['userId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'userId': userId,
    };
  }
}
