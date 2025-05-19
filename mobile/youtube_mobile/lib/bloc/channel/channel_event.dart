abstract class ChannelEvent {}

class LoadChannel extends ChannelEvent {
  final String channelId;
  LoadChannel(this.channelId);
}