import 'package:flutter/material.dart';

class FeatureBlock extends StatelessWidget {
  final String title;
  final String description;

  const FeatureBlock({
    super.key,
    required this.title,
    required this.description, required bool reverse,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Row(
        children: [
          const Icon(Icons.play_circle_fill, size: 40),
          const SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(title, style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
                Text(description),
              ],
            ),
          )
        ],
      ),
    );
  }
}
