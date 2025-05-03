import 'package:flutter/material.dart';

class PlanCard extends StatelessWidget {
  final String title;
  final String price;
  final String note;
  final List<String> benefits;

  const PlanCard({
    super.key,
    required this.title,
    required this.price,
    required this.note,
    required this.benefits,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 3,
      margin: const EdgeInsets.symmetric(vertical: 10),
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Row(
              children: [
                const Icon(Icons.account_circle),
                const SizedBox(width: 8),
                Text(title, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
              ],
            ),
            const SizedBox(height: 8),
            Text(price, style: const TextStyle(fontSize: 16)),
            Text(note, style: const TextStyle(color: Colors.grey)),
            const SizedBox(height: 8),
            ...benefits.map((b) => Row(
                  children: [
                    const Icon(Icons.check, color: Colors.green, size: 18),
                    const SizedBox(width: 4),
                    Text(b),
                  ],
                )),
          ],
        ),
      ),
    );
  }
}
