import 'package:flutter_bloc/flutter_bloc.dart';
import 'user_event.dart';
import 'user_state.dart';
import '../../services/user_service.dart';

class UserBloc extends Bloc<UserEvent, UserState> {
  final UserService userService;

  UserBloc(this.userService) : super(UserInitial()) {
    on<LoadUser>((event, emit) async {
      emit(UserLoading());
      try {
        final userData = await userService.fetchUser(event.userId);
        emit(UserLoaded(userData));
      } catch (e) {
        emit(UserError(e.toString()));
      }
    });
  }
}
