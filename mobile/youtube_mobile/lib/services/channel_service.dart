import 'package:graphql_flutter/graphql_flutter.dart';

class ChannelService {
  final HttpLink httpLink = HttpLink(
    'http://localhost:8070/graphql',
    defaultHeaders: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    },
  );

  Future<Map<String, dynamic>> fetchChannelById(String id) async {
    final client = GraphQLClient(
      link: httpLink,
      cache: GraphQLCache(),
    );

    final query = r'''
      query ChannelQuery($id: ID!) {
        channel(id: $id) {
          channel {
            id
            name
            subscribers
            videoCount
            description
            mainImage
            bannerImage
          }
        }
      }
    ''';

    final result = await client.query(QueryOptions(
      document: gql(query),
      variables: {'id': id},
    ));

    if (result.hasException) {
      throw Exception('GraphQL Error: ${result.exception.toString()}');
    }

    final channelData = result.data?['channel']?['channel'];
    if (channelData == null) {
      throw Exception('Channel data not found');
    }

    return channelData as Map<String, dynamic>;
  }
}
