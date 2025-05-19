import 'package:flutter/material.dart';

class ChannelInfoRow extends StatelessWidget {
  final Map<String, dynamic> channelData;

  const ChannelInfoRow({super.key, required this.channelData});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(12.0),
      child: Row(
        children: [
          CircleAvatar(
            backgroundImage: NetworkImage('http://localhost:8080/File/GetFileStream/${channelData['mainImage']}'),
          ),
          SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(channelData['name'] ?? '', style: TextStyle(fontWeight: FontWeight.bold)),
                Text('${channelData['subscribers'] ?? 0} подписчиков', style: TextStyle(color: Colors.grey, fontSize: 12)),
              ],
            ),
          ),
          TextButton(
            style: TextButton.styleFrom(
              backgroundColor: Colors.red,
            ),
            onPressed: () {  },
            child: Text(
              'Подписаться',
              style: TextStyle(
                color: Colors.white,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
        ],
      ),
    );
  }
}
