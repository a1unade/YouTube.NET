import 'package:flutter/material.dart';

void showPaymentModal(BuildContext context, String? userId, String balanceId) {
  showDialog(
    context: context,
    builder: (_) => AlertDialog(
      title: const Text('Оформить подписку'),
      content: Text('ID пользователя: $userId\nБаланс: $balanceId'),
      actions: [
        TextButton(onPressed: () => Navigator.pop(context), child: const Text('Закрыть')),
      ],
    ),
  );
}
