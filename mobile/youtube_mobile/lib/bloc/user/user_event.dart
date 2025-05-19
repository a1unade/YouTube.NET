abstract class UserEvent {}

class LoadUser extends UserEvent {
  final String userId;

  LoadUser(this.userId);
}
