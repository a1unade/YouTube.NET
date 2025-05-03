// premium_page.dart

import 'package:flutter/material.dart';
import '../modals/payment_modal.dart';
import '../modals/add_funds_modal.dart';
import '../widgets/plan_card.dart';
import '../widgets/feature_block.dart';

class PremiumPage extends StatelessWidget {
  final String? userId;
  final String balanceId;

  const PremiumPage({super.key, required this.userId, required this.balanceId});

  @override
  Widget build(BuildContext context) {
    final TextStyle h1Style = const TextStyle(fontSize: 60, fontWeight: FontWeight.bold);
    final TextStyle h3Style = const TextStyle(fontSize: 18, fontWeight: FontWeight.w300);

    return Scaffold(
      body: SingleChildScrollView(
        child: Column(
          children: [
            // Header Layout
            Container(
              height: 800,
              width: double.infinity,
              padding: const EdgeInsets.symmetric(horizontal: 16),
              decoration: const BoxDecoration(
                image: DecorationImage(
                  image: NetworkImage(
                    'https://www.gstatic.com/youtube/img/promos/growth/ytp_lp2_background_web_4098x2304.jpg',
                  ),
                  fit: BoxFit.cover,
                ),
              ),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Image.network(
                    'https://www.gstatic.com/youtube/img/promos/growth/e7c0850faf8290ead635fb188d20df5095ecb6d518a7d3fbabb42b51e7302330_573x93.png',
                    width: 250,
                  ),
                  const SizedBox(height: 30),
                  Text('YouTube нон-стоп', style: h1Style),
                  const SizedBox(height: 10),
                  Text('Смотрите без рекламы, слушайте в фоне и скачивайте видео офлайн', style: h3Style),
                  const SizedBox(height: 30),
                  Text('1 месяц всего за 249 ₽ • Отменить можно в любой момент', style: h3Style),
                  const SizedBox(height: 20),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      ElevatedButton(
                        onPressed: () => showPaymentModal(context, userId, balanceId),
                        style: ElevatedButton.styleFrom(
                          backgroundColor: const Color(0xFF065FD4),
                          padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 14),
                          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(30)),
                          textStyle: const TextStyle(fontWeight: FontWeight.w800, color: Colors.white),
                        ),
                        child: const Text('Попробовать', style: TextStyle(color: Colors.white)),
                      ),
                      const SizedBox(width: 20),
                      ElevatedButton(
                        onPressed: () => showAddFundsModal(context, userId, balanceId),
                        style: ElevatedButton.styleFrom(
                          backgroundColor: const Color(0xFF065FD4),
                          padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 14),
                          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(30)),
                          textStyle: const TextStyle(fontWeight: FontWeight.w800, color: Colors.white),
                        ),
                        child: const Text('Пополнить', style: TextStyle(color: Colors.white)),
                      ),
                    ],
                  ),
                  const SizedBox(height: 20),
                  Text('Или выберите более выгодный план — 6 месяцев или год', style: h3Style),
                  const SizedBox(height: 10),
                  const Text(
                    'За 7 дней до окончания пробного периода мы напомним. Деньги спишутся автоматически.',
                    style: TextStyle(fontSize: 11, color: Color.fromRGBO(0, 0, 0, 0.4)),
                    textAlign: TextAlign.center,
                  ),
                ],
              ),
            ),

            const SizedBox(height: 50),
            const Text('Выберите подходящий тариф', style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold)),
            const SizedBox(height: 30),

            // Тарифы
            Wrap(
              spacing: 30,
              runSpacing: 30,
              alignment: WrapAlignment.center,
              children: const [
                PlanCard(
                  title: '1 месяц',
                  price: '249₽ за месяц',
                  note: 'Автопродление включено',
                  benefits: ['Без рекламы', 'Фоновое воспроизведение', 'Загрузка видео для просмотра офлайн'],
                ),
                PlanCard(
                  title: '6 месяцев',
                  price: '1349₽ единоразово',
                  note: 'Экономия 10%',
                  benefits: ['Больше за меньшие деньги', 'Те же Premium-привилегии', 'Удобно в подарок'],
                ),
                PlanCard(
                  title: '1 год',
                  price: '2249₽',
                  note: 'Экономия 25%',
                  benefits: ['Максимальная выгода', 'Год без рекламы', 'Все функции YouTube Premium'],
                ),
              ],
            ),

            const SizedBox(height: 60),

            // Фичи
            const FeatureBlock(
              title: 'Смотрите без рекламы',
              description: 'Получайте максимум от видео без перерывов и рекламы.',
              reverse: false,
            ),
            const FeatureBlock(
              title: 'Фоновое воспроизведение',
              description: 'Видео продолжается даже при сворачивании приложения.',
              reverse: true,
            ),
            const FeatureBlock(
              title: 'Скачивайте и смотрите офлайн',
              description: 'Загружайте видео и смотрите без интернета.',
              reverse: false,
            ),
            const FeatureBlock(
              title: 'Режим "Картинка в картинке"',
              description: 'Смотрите видео поверх других приложений.',
              reverse: true,
            ),

            const SizedBox(height: 60),
          ],
        ),
      ),
    );
  }
}
