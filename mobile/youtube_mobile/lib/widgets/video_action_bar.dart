import 'package:flutter/material.dart';

class VideoActionBar extends StatefulWidget {
  @override
  State<VideoActionBar> createState() => _VideoActionBarState();
}

class _VideoActionBarState extends State<VideoActionBar> {
  bool isLiked = false;
  bool isDisliked = false;
  int likeCount = 256; // Пример

  void toggleLike() {
    setState(() {
      if (isLiked) {
        likeCount--;
      } else {
        likeCount++;
        if (isDisliked) isDisliked = false;
      }
      isLiked = !isLiked;
    });
  }

  void toggleDislike() {
    setState(() {
      if (isDisliked) {
        isDisliked = false;
      } else {
        isDisliked = true;
        if (isLiked) {
          likeCount--;
          isLiked = false;
        }
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: [
        _buildAction(Icons.thumb_up, isLiked ? '$likeCount' : '$likeCount',
            toggleLike, isLiked ? Colors.blue : Colors.white),
        _buildAction(Icons.thumb_down, 'Не нравится', toggleDislike,
            isDisliked ? Colors.blue : Colors.white),
        _buildAction(Icons.share, 'Поделиться', () {}, Colors.white),
        _buildAction(Icons.movie_creation, 'Ремикс', () {}, Colors.white),
        _buildAction(Icons.download, 'Скачать', () {}, Colors.white),
      ],
    );
  }

  Widget _buildAction(
      IconData icon, String label, VoidCallback onTap, Color color) {
    return InkWell(
      onTap: onTap,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Icon(icon, color: color),
          SizedBox(height: 4),
          Text(label, style: TextStyle(color: Colors.white, fontSize: 12)),
        ],
      ),
    );
  }
}
