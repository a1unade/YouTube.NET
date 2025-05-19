import 'package:graphql_flutter/graphql_flutter.dart';
import '../models/video_model.dart';

class VideoService {
  final HttpLink httpLink = HttpLink(
    'http://localhost:8070/graphql',
    defaultHeaders: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    },
  );

  Future<List<Video>> fetchVideos({int page = 1, int size = 10}) async {
    final client = GraphQLClient(
      link: httpLink,
      cache: GraphQLCache(),
    );

    const query = r'''
      query VideoPagination($page: Int!, $size: Int!) {
        videoPagination(page: $page, size: $size) {
          isSuccessfully
          message
          videos {
            previewUrl
            channelImageUrl
            videoName
            views
            releaseDate
            videoId
            channelId
          }
        }
      }
    ''';

    final result = await client.query(QueryOptions(
      document: gql(query),
      variables: {'page': page, 'size': size},
    ));

    if (result.hasException) {
      throw Exception('GraphQL Error: ${result.exception.toString()}');
    }

    final data = result.data?['videoPagination'];
    if (data == null || !(data['isSuccessfully'] as bool)) {
      throw Exception('Server returned unsuccessful result');
    }

    final videosJson = data['videos'] as List<dynamic>;
    return videosJson.map((json) => Video.fromJson(json)).toList();
  }
}
