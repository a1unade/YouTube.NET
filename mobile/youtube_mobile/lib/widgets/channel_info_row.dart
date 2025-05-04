import 'package:flutter/material.dart';
import '../modals/video_model.dart';

class ChannelInfoRow extends StatefulWidget {
  final VideoModel video;

  const ChannelInfoRow({required this.video});

  @override
  State<ChannelInfoRow> createState() => _ChannelInfoRowState();
}

class _ChannelInfoRowState extends State<ChannelInfoRow> {
  bool isSubscribed = false;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(12.0),
      child: Row(
        children: [
          CircleAvatar(
            backgroundImage: NetworkImage(widget.video.avatarUrl),
          ),
          SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(widget.video.channelName,
                    style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold)),
                Text('1 млн подписчиков', style: TextStyle(color: Colors.grey, fontSize: 12)),
              ],
            ),
          ),
          TextButton(
            onPressed: () {
              setState(() => isSubscribed = !isSubscribed);
            },
            child: Text(
              isSubscribed ? 'Вы подписаны' : 'Подписаться',
              style: TextStyle(
                color: isSubscribed ? Colors.grey : Colors.white,
                fontWeight: FontWeight.bold,
              ),
            ),
            style: TextButton.styleFrom(
              backgroundColor: isSubscribed ? Colors.grey.shade800 : Colors.red,
            ),
          ),
        ],
      ),
    );
  }
}
