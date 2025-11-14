//
//  Generated code. Do not modify.
//  source: Proto/dartany.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use errorCodeDescriptor instead')
const ErrorCode$json = {
  '1': 'ErrorCode',
  '2': [
    {'1': 'None', '2': 0},
    {'1': 'Success', '2': 1},
    {'1': 'Failed', '2': 2},
  ],
};

/// Descriptor for `ErrorCode`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List errorCodeDescriptor = $convert.base64Decode(
    'CglFcnJvckNvZGUSCAoETm9uZRAAEgsKB1N1Y2Nlc3MQARIKCgZGYWlsZWQQAg==');

@$core.Deprecated('Use ePacketIDDescriptor instead')
const ePacketID$json = {
  '1': 'ePacketID',
  '2': [
    {'1': 'NONE', '2': 0},
    {'1': 'eReqLoginInfo', '2': 1},
    {'1': 'eResLoginInfo', '2': 2},
    {'1': 'eReqFolderInfo', '2': 3},
    {'1': 'eResFolderInfoBegin', '2': 4},
    {'1': 'eNotifyFolderInfo', '2': 5},
    {'1': 'eNotifyFileInfo', '2': 6},
    {'1': 'eResFolderInfoEnd', '2': 7},
    {'1': 'eReqDuplicateCheckBegin', '2': 8},
    {'1': 'eResDuplicateCheckBegin', '2': 9},
    {'1': 'eReqDuplicateCheck', '2': 10},
    {'1': 'eResDuplicateCheck', '2': 11},
    {'1': 'eReqDuplicateCheckEnd', '2': 12},
    {'1': 'eResDuplicateCheckEnd', '2': 13},
    {'1': 'eReqFileSendBegin', '2': 14},
    {'1': 'eResFileSendBegin', '2': 15},
    {'1': 'eReqFileOverwriteCheck', '2': 16},
    {'1': 'eResFileOverwriteCheck', '2': 17},
    {'1': 'eReqFileContentsSend', '2': 18},
    {'1': 'eResFileContentsSend', '2': 19},
    {'1': 'eReqFileSendEnd', '2': 20},
    {'1': 'eResFileSendEnd', '2': 21},
    {'1': 'eReqFileNameChange', '2': 22},
    {'1': 'eResFileNameChange', '2': 23},
    {'1': 'eReqFileDelete', '2': 24},
    {'1': 'eResFileDelete', '2': 25},
    {'1': 'eReqServiceList', '2': 26},
    {'1': 'eResServiceList', '2': 27},
    {'1': 'eReqServiceStart', '2': 28},
    {'1': 'eResServiceStart', '2': 29},
    {'1': 'eReqServiceStop', '2': 30},
    {'1': 'eResServiceStop', '2': 31},
    {'1': 'eReqFileReceiveBegin', '2': 32},
    {'1': 'eResFileReceiveBegin', '2': 33},
    {'1': 'eReqFileReceiveConents', '2': 34},
    {'1': 'eResFileReceiveConents', '2': 35},
    {'1': 'eNotifyFileReceiveEnd', '2': 36},
    {'1': 'eReqChatBot', '2': 37},
    {'1': 'eResChatBot', '2': 38},
    {'1': 'MAX', '2': 39},
  ],
};

/// Descriptor for `ePacketID`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List ePacketIDDescriptor = $convert.base64Decode(
    'CgllUGFja2V0SUQSCAoETk9ORRAAEhEKDWVSZXFMb2dpbkluZm8QARIRCg1lUmVzTG9naW5Jbm'
    'ZvEAISEgoOZVJlcUZvbGRlckluZm8QAxIXChNlUmVzRm9sZGVySW5mb0JlZ2luEAQSFQoRZU5v'
    'dGlmeUZvbGRlckluZm8QBRITCg9lTm90aWZ5RmlsZUluZm8QBhIVChFlUmVzRm9sZGVySW5mb0'
    'VuZBAHEhsKF2VSZXFEdXBsaWNhdGVDaGVja0JlZ2luEAgSGwoXZVJlc0R1cGxpY2F0ZUNoZWNr'
    'QmVnaW4QCRIWChJlUmVxRHVwbGljYXRlQ2hlY2sQChIWChJlUmVzRHVwbGljYXRlQ2hlY2sQCx'
    'IZChVlUmVxRHVwbGljYXRlQ2hlY2tFbmQQDBIZChVlUmVzRHVwbGljYXRlQ2hlY2tFbmQQDRIV'
    'ChFlUmVxRmlsZVNlbmRCZWdpbhAOEhUKEWVSZXNGaWxlU2VuZEJlZ2luEA8SGgoWZVJlcUZpbG'
    'VPdmVyd3JpdGVDaGVjaxAQEhoKFmVSZXNGaWxlT3ZlcndyaXRlQ2hlY2sQERIYChRlUmVxRmls'
    'ZUNvbnRlbnRzU2VuZBASEhgKFGVSZXNGaWxlQ29udGVudHNTZW5kEBMSEwoPZVJlcUZpbGVTZW'
    '5kRW5kEBQSEwoPZVJlc0ZpbGVTZW5kRW5kEBUSFgoSZVJlcUZpbGVOYW1lQ2hhbmdlEBYSFgoS'
    'ZVJlc0ZpbGVOYW1lQ2hhbmdlEBcSEgoOZVJlcUZpbGVEZWxldGUQGBISCg5lUmVzRmlsZURlbG'
    'V0ZRAZEhMKD2VSZXFTZXJ2aWNlTGlzdBAaEhMKD2VSZXNTZXJ2aWNlTGlzdBAbEhQKEGVSZXFT'
    'ZXJ2aWNlU3RhcnQQHBIUChBlUmVzU2VydmljZVN0YXJ0EB0SEwoPZVJlcVNlcnZpY2VTdG9wEB'
    '4SEwoPZVJlc1NlcnZpY2VTdG9wEB8SGAoUZVJlcUZpbGVSZWNlaXZlQmVnaW4QIBIYChRlUmVz'
    'RmlsZVJlY2VpdmVCZWdpbhAhEhoKFmVSZXFGaWxlUmVjZWl2ZUNvbmVudHMQIhIaChZlUmVzRm'
    'lsZVJlY2VpdmVDb25lbnRzECMSGQoVZU5vdGlmeUZpbGVSZWNlaXZlRW5kECQSDwoLZVJlcUNo'
    'YXRCb3QQJRIPCgtlUmVzQ2hhdEJvdBAmEgcKA01BWBAn');

@$core.Deprecated('Use reqLoginInfoDescriptor instead')
const ReqLoginInfo$json = {
  '1': 'ReqLoginInfo',
};

/// Descriptor for `ReqLoginInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List reqLoginInfoDescriptor = $convert.base64Decode(
    'CgxSZXFMb2dpbkluZm8=');

@$core.Deprecated('Use resLoginInfoDescriptor instead')
const ResLoginInfo$json = {
  '1': 'ResLoginInfo',
  '2': [
    {'1': 'result', '3': 1, '4': 1, '5': 14, '6': '.tutorial.ErrorCode', '10': 'result'},
    {'1': 'bandWidth', '3': 2, '4': 1, '5': 5, '10': 'bandWidth'},
  ],
};

/// Descriptor for `ResLoginInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List resLoginInfoDescriptor = $convert.base64Decode(
    'CgxSZXNMb2dpbkluZm8SKwoGcmVzdWx0GAEgASgOMhMudHV0b3JpYWwuRXJyb3JDb2RlUgZyZX'
    'N1bHQSHAoJYmFuZFdpZHRoGAIgASgFUgliYW5kV2lkdGg=');

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

