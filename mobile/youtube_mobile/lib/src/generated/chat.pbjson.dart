//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 3.3

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names
// ignore_for_file: curly_braces_in_flow_control_structures
// ignore_for_file: deprecated_member_use_from_same_package, library_prefixes
// ignore_for_file: non_constant_identifier_names

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use joinChatRequestDescriptor instead')
const JoinChatRequest$json = {
  '1': 'JoinChatRequest',
  '2': [
    {'1': 'user_id', '3': 1, '4': 1, '5': 9, '10': 'userId'},
    {'1': 'chat_id', '3': 2, '4': 1, '5': 9, '9': 0, '10': 'chatId', '17': true},
  ],
  '8': [
    {'1': '_chat_id'},
  ],
};

/// Descriptor for `JoinChatRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List joinChatRequestDescriptor = $convert.base64Decode(
    'Cg9Kb2luQ2hhdFJlcXVlc3QSFwoHdXNlcl9pZBgBIAEoCVIGdXNlcklkEhwKB2NoYXRfaWQYAi'
    'ABKAlIAFIGY2hhdElkiAEBQgoKCF9jaGF0X2lk');

@$core.Deprecated('Use joinChatResponseDescriptor instead')
const JoinChatResponse$json = {
  '1': 'JoinChatResponse',
  '2': [
    {'1': 'user_id', '3': 1, '4': 1, '5': 9, '10': 'userId'},
    {'1': 'chat_id', '3': 2, '4': 1, '5': 9, '10': 'chatId'},
  ],
};

/// Descriptor for `JoinChatResponse`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List joinChatResponseDescriptor = $convert.base64Decode(
    'ChBKb2luQ2hhdFJlc3BvbnNlEhcKB3VzZXJfaWQYASABKAlSBnVzZXJJZBIXCgdjaGF0X2lkGA'
    'IgASgJUgZjaGF0SWQ=');

@$core.Deprecated('Use sendMessageRequestDescriptor instead')
const SendMessageRequest$json = {
  '1': 'SendMessageRequest',
  '2': [
    {'1': 'chat_id', '3': 1, '4': 1, '5': 9, '10': 'chatId'},
    {'1': 'user_id', '3': 2, '4': 1, '5': 9, '10': 'userId'},
    {'1': 'message', '3': 3, '4': 1, '5': 9, '10': 'message'},
  ],
};

/// Descriptor for `SendMessageRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sendMessageRequestDescriptor = $convert.base64Decode(
    'ChJTZW5kTWVzc2FnZVJlcXVlc3QSFwoHY2hhdF9pZBgBIAEoCVIGY2hhdElkEhcKB3VzZXJfaW'
    'QYAiABKAlSBnVzZXJJZBIYCgdtZXNzYWdlGAMgASgJUgdtZXNzYWdl');

@$core.Deprecated('Use sendMessageResponseDescriptor instead')
const SendMessageResponse$json = {
  '1': 'SendMessageResponse',
  '2': [
    {'1': 'success', '3': 1, '4': 1, '5': 8, '10': 'success'},
  ],
};

/// Descriptor for `SendMessageResponse`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sendMessageResponseDescriptor = $convert.base64Decode(
    'ChNTZW5kTWVzc2FnZVJlc3BvbnNlEhgKB3N1Y2Nlc3MYASABKAhSB3N1Y2Nlc3M=');

@$core.Deprecated('Use chatMessageResponseDescriptor instead')
const ChatMessageResponse$json = {
  '1': 'ChatMessageResponse',
  '2': [
    {'1': 'message_id', '3': 1, '4': 1, '5': 9, '10': 'messageId'},
    {'1': 'user_id', '3': 2, '4': 1, '5': 9, '10': 'userId'},
    {'1': 'message', '3': 3, '4': 1, '5': 9, '10': 'message'},
    {'1': 'date', '3': 4, '4': 1, '5': 9, '10': 'date'},
    {'1': 'time', '3': 5, '4': 1, '5': 9, '10': 'time'},
    {'1': 'is_read', '3': 6, '4': 1, '5': 8, '10': 'isRead'},
  ],
};

/// Descriptor for `ChatMessageResponse`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List chatMessageResponseDescriptor = $convert.base64Decode(
    'ChNDaGF0TWVzc2FnZVJlc3BvbnNlEh0KCm1lc3NhZ2VfaWQYASABKAlSCW1lc3NhZ2VJZBIXCg'
    'd1c2VyX2lkGAIgASgJUgZ1c2VySWQSGAoHbWVzc2FnZRgDIAEoCVIHbWVzc2FnZRISCgRkYXRl'
    'GAQgASgJUgRkYXRlEhIKBHRpbWUYBSABKAlSBHRpbWUSFwoHaXNfcmVhZBgGIAEoCFIGaXNSZW'
    'Fk');

