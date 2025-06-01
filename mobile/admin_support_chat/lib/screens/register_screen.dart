import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/auth_provider.dart';
import '../services/auth_service.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();

  final firstNameController = TextEditingController();
  final lastNameController = TextEditingController();
  final birthDateController = TextEditingController();
  final emailController = TextEditingController();
  final passwordController = TextEditingController();
  final confirmPasswordController = TextEditingController();

  String? selectedGender;
  bool isLoading = false;

  @override
  void dispose() {
    firstNameController.dispose();
    lastNameController.dispose();
    birthDateController.dispose();
    emailController.dispose();
    passwordController.dispose();
    confirmPasswordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        title: const Text('Регистрация'),
        backgroundColor: Colors.white,
        foregroundColor: Colors.black,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(20),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              _buildTextField(controller: firstNameController, label: 'Имя'),
              const SizedBox(height: 20),
              _buildTextField(controller: lastNameController, label: 'Фамилия'),
              const SizedBox(height: 20),
              _buildTextField(
                  controller: birthDateController,
                  label: 'Дата рождения',
                  hint: 'дд.мм.гггг'),
              const SizedBox(height: 20),
              _buildDropdownGender(),
              const SizedBox(height: 20),
              _buildTextField(controller: emailController, label: 'Email'),
              const SizedBox(height: 20),
              _buildTextField(controller: passwordController, label: 'Пароль', obscure: true),
              const SizedBox(height: 20),
              _buildTextField(controller: confirmPasswordController, label: 'Подтвердите пароль', obscure: true),
              const SizedBox(height: 30),
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  onPressed: isLoading ? null : _register,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.blue,
                    foregroundColor: Colors.white,
                    padding: const EdgeInsets.symmetric(vertical: 15),
                    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                  ),
                  child: isLoading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text('Зарегистрироваться', style: TextStyle(fontSize: 16)),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildTextField({
    required TextEditingController controller,
    required String label,
    bool obscure = false,
    String? hint,
  }) {
    return TextFormField(
      controller: controller,
      obscureText: obscure,
      decoration: InputDecoration(
        labelText: label,
        hintText: hint,
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) return 'Поле "$label" обязательно';
        return null;
      },
    );
  }

  Widget _buildDropdownGender() {
    return DropdownButtonFormField<String>(
      decoration: InputDecoration(
        labelText: 'Пол',
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
      ),
      value: selectedGender,
      items: ['Мужской', 'Женский']
          .map((gender) => DropdownMenuItem(value: gender, child: Text(gender)))
          .toList(),
      onChanged: (value) => setState(() => selectedGender = value),
      validator: (value) => value == null ? 'Выберите пол' : null,
    );
  }

  Future<void> _register() async {
    if (!_formKey.currentState!.validate()) return;
    if (passwordController.text != confirmPasswordController.text) {
      _showError('Пароли не совпадают');
      return;
    }

    setState(() => isLoading = true);

    try {
      final authService = AuthService();
      final response = await authService.register(
        email: emailController.text,
        password: passwordController.text,
        name: firstNameController.text,
        surname: lastNameController.text,
        gender: selectedGender!,
        dateOfBirth: birthDateController.text,
        country: 'Россия',
      );

      await Provider.of<AuthProvider>(context, listen: false).registerWithUserId(
        response.userId,
      );

      Navigator.pop(context);
    } catch (e) {
      _showError('Ошибка: ${e.toString()}');
    } finally {
      setState(() => isLoading = false);
    }
  }

  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(message)));
  }
}
