import 'package:graphql/client.dart';

class UserService {
  static const String _endpoint = 'http://localhost:8070/graphql';

  final GraphQLClient _client;

  UserService()
      : _client = GraphQLClient(
    link: HttpLink(_endpoint),
    cache: GraphQLCache(),
  );

  Future<Map<String, dynamic>> fetchUser(String userId) async {
    const query = r'''
      query User($id: ID!) {
        user(id: $id) {
          isSuccessfully
          message
          name
          surName
          email
          userName
          isPremium
          channelId
        }
      }
    ''';

    final options = QueryOptions(
      document: gql(query),
      variables: {'id': userId},
    );

    final result = await _client.query(options);

    if (result.hasException) {
      throw Exception('GraphQL error: ${result.exception.toString()}');
    }

    final user = result.data?['user'];
    if (user == null || user['isSuccessfully'] != true) {
      throw Exception(user?['message'] ?? 'Failed to fetch user data');
    }

    return {
      'name': user['name'],
      'surName': user['surName'],
      'email': user['email'],
      'userName': user['userName'],
      'isPremium': user['isPremium'],
      'channelId': user['channelId'],
    };
  }
}
