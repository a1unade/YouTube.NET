abstract class ChannelState {}

class ChannelInitial extends ChannelState {}

class ChannelLoading extends ChannelState {}

class ChannelLoaded extends ChannelState {
  final Map<String, dynamic> channelData;
  ChannelLoaded(this.channelData);
}

class ChannelError extends ChannelState {
  final String message;
  ChannelError(this.message);
}