import 'package:flutter/material.dart';

class SubscriptionsScreen extends StatelessWidget {
  const SubscriptionsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final channels = [
      {
        'name': 'Channel',
        'avatar': 'https://i.pravatar.cc/150?img=1',
      },
      {
        'name': 'Channel',
        'avatar': 'https://i.pravatar.cc/150?img=5',
      },
      {
        'name': 'Channel',
        'avatar': 'https://i.pravatar.cc/150?img=12',
      },
      {
        'name': 'Channel',
        'avatar': 'https://i.pravatar.cc/150?img=20',
      },
    ];

    return Scaffold(
      backgroundColor: Colors.white,
      body: ListView.separated(
        itemCount: channels.length,
        padding: const EdgeInsets.all(16),
        separatorBuilder: (_, __) => Divider(height: 24),
        itemBuilder: (context, index) {
          final channel = channels[index];
          return ListTile(
            leading: CircleAvatar(
              backgroundImage: NetworkImage(channel['avatar']!),
              radius: 25,
            ),
            title: Text(
              channel['name']!,
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.w500),
            ),
            trailing: Icon(Icons.keyboard_arrow_right),
            onTap: () {},
          );
        },
      ),
    );
  }
}
