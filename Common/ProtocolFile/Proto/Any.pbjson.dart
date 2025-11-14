//
//  Generated code. Do not modify.
//  source: Proto/Any.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use packetIDDescriptor instead')
const PacketID$json = {
  '1': 'PacketID',
  '2': [
    {'1': 'NONE', '2': 0},
    {'1': 'PacketID_NotifyServerInfo', '2': 1},
    {'1': 'PacketID_NotifyClientInfo', '2': 2},
  ],
};

/// Descriptor for `PacketID`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List packetIDDescriptor = $convert.base64Decode(
    'CghQYWNrZXRJRBIICgROT05FEAASHQoZUGFja2V0SURfTm90aWZ5U2VydmVySW5mbxABEh0KGV'
    'BhY2tldElEX05vdGlmeUNsaWVudEluZm8QAg==');

@$core.Deprecated('Use notifyServerInfoDescriptor instead')
const NotifyServerInfo$json = {
  '1': 'NotifyServerInfo',
  '2': [
    {'1': 'serverKey', '3': 1, '4': 1, '5': 12, '10': 'serverKey'},
  ],
};

/// Descriptor for `NotifyServerInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List notifyServerInfoDescriptor = $convert.base64Decode(
    'ChBOb3RpZnlTZXJ2ZXJJbmZvEhwKCXNlcnZlcktleRgBIAEoDFIJc2VydmVyS2V5');

@$core.Deprecated('Use notifyClientInfoDescriptor instead')
const NotifyClientInfo$json = {
  '1': 'NotifyClientInfo',
  '2': [
    {'1': 'clientKey', '3': 1, '4': 1, '5': 12, '10': 'clientKey'},
  ],
};

/// Descriptor for `NotifyClientInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List notifyClientInfoDescriptor = $convert.base64Decode(
    'ChBOb3RpZnlDbGllbnRJbmZvEhwKCWNsaWVudEtleRgBIAEoDFIJY2xpZW50S2V5');

@$core.Deprecated('Use anyDescriptor instead')
const Any$json = {
  '1': 'Any',
  '2': [
    {'1': 'type_url', '3': 1, '4': 1, '5': 9, '10': 'typeUrl'},
    {'1': 'value', '3': 2, '4': 1, '5': 12, '10': 'value'},
  ],
};

/// Descriptor for `Any`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List anyDescriptor = $convert.base64Decode(
    'CgNBbnkSGQoIdHlwZV91cmwYASABKAlSB3R5cGVVcmwSFAoFdmFsdWUYAiABKAxSBXZhbHVl');

