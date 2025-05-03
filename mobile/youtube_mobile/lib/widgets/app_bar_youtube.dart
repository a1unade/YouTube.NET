import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class YouTubeAppBar extends StatelessWidget implements PreferredSizeWidget {
  @override
  Widget build(BuildContext context) {
    return AppBar(
      backgroundColor: Colors.black,
      title: SvgPicture.network(
        'https://upload.wikimedia.org/wikipedia/commons/b/b8/YouTube_Logo_2017.svg',
        height: 24,
      ),
      actions: [
        IconButton(
          icon: Icon(Icons.notifications_none_outlined),
          onPressed: () {},
        ),
        IconButton(
          icon: Icon(Icons.search),
          onPressed: () {},
        ),
      ],
    );
  }

  @override
  Size get preferredSize => Size.fromHeight(kToolbarHeight);
}
