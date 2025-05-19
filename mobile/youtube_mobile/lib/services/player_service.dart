import 'package:graphql_flutter/graphql_flutter.dart';
import '../models/player_video_model.dart';

class PlayerService {
  final HttpLink httpLink = HttpLink(
    'http://localhost:8070/graphql',
    defaultHeaders: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    },
  );

  Future<PlayerVideo> fetchVideoById(String videoId) async {
    final client = GraphQLClient(
      link: httpLink,
      cache: GraphQLCache(),
    );

    const query = r'''
    query VideoById($id: ID!) {
      video(id: $id) {
        isSuccessfully
        channelId
        videoId
        video {
          videoFileId
          previewId
          viewCount
          name
          realiseDate
          channelName
        }
      }
    }
  ''';

    final result = await client.query(QueryOptions(
      document: gql(query),
      variables: {'id': videoId},
    ));

    if (result.hasException) {
      throw Exception('GraphQL Error: ${result.exception.toString()}');
    }

    final data = result.data?['video'];
    if (data == null || !(data['isSuccessfully'] as bool)) {
      throw Exception('Server returned unsuccessful result');
    }

    return PlayerVideo.fromJson(data['video']);
  }
}
