import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:youtube_mobile/screens/premium_page.dart';
import '../providers/auth_provider.dart';
import 'login_screen.dart';

class ProfileScreen extends StatelessWidget {
  const ProfileScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final auth = Provider.of<AuthProvider>(context);

    return Scaffold(
      backgroundColor: Colors.white,
      body: auth.isLoggedIn ? _buildProfileContent(context, auth) : _buildLoginPrompt(context),
    );
  }

  Widget _buildLoginPrompt(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text('Вы не вошли в аккаунт', style: TextStyle(fontSize: 18)),
          SizedBox(height: 20),
          SizedBox(
            width: 200,
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (_) => LoginScreen()),
                );
              },
              style: ElevatedButton.styleFrom(
                foregroundColor: Colors.white,
                padding: EdgeInsets.symmetric(vertical: 15),
                backgroundColor: Colors.blue,
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
              ),
              child: Text('Войти', style: TextStyle(fontSize: 16)),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildProfileContent(BuildContext context, AuthProvider auth) {
    return ListView(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 20),
      children: [
        Row(
          children: [
            CircleAvatar(
              radius: 30,
              backgroundImage: NetworkImage('https://i.pravatar.cc/150?img=3'),
            ),
            SizedBox(width: 12),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: const [
                Text("Имя", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                Text("example@gmail.com", style: TextStyle(color: Colors.grey)),
              ],
            ),
            Spacer(),
            Icon(Icons.keyboard_arrow_right),
          ],
        ),
        SizedBox(height: 30),
        _buildTile(Icons.person, "Мой канал"),
        _buildTile(Icons.play_circle_outline, "Ваши видео"),
        _buildTile(Icons.download_outlined, "Загрузки"),
        _buildTile(Icons.workspace_premium, "YouTube Premium", () {
          Navigator.push(
            context,
            MaterialPageRoute(builder: (_) => PremiumPage(userId: 'user123', balanceId: 'balance456')),
          );
        }),
        Divider(height: 40),
        _buildTile(Icons.settings, "Настройки"),
        _buildTile(Icons.help_outline, "Справка и отзыв"),
        ListTile(
          leading: Icon(Icons.logout),
          title: Text("Выйти"),
          onTap: () => auth.logOut(),
        ),
      ],
    );
  }

  Widget _buildTile(IconData icon, String title, [VoidCallback? onTap]) {
    return ListTile(
      leading: Icon(icon),
      title: Text(title),
      trailing: Icon(Icons.keyboard_arrow_right),
      onTap: onTap,
    );
  }
}