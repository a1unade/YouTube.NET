import 'package:graphql_flutter/graphql_flutter.dart';
import '../models/player_video_model.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

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

  Future<bool> incrementView(String videoId) async {
    final res = await http.post(
      Uri.parse('http://localhost:8080/Video/IncrementView'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'videoId': formatGuidWithDashes(videoId)}),
    );

    return res.statusCode >= 200 && res.statusCode < 300;
  }

  String formatGuidWithDashes(String guidWithoutDashes) {
    final regex = RegExp(r'^(.{8})(.{4})(.{4})(.{4})(.{12})$');
    final match = regex.firstMatch(guidWithoutDashes);
    if (match == null) {
      throw FormatException('Invalid GUID format');
    }
    return '${match[1]}-${match[2]}-${match[3]}-${match[4]}-${match[5]}';
  }
}
