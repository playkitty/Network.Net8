//
//  Generated code. Do not modify.
//  source: Proto/dartany.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

///  `Any` contains an arbitrary serialized protocol buffer message along with a
///  URL that describes the type of the serialized message.
///
///  Protobuf library provides support to pack/unpack Any values in the form
///  of utility functions or additional generated methods of the Any type.
///
///  Example 1: Pack and unpack a message in C++.
///
///      Foo foo = ...;
///      Any any;
///      any.PackFrom(foo);
///      ...
///      if (any.UnpackTo(&foo)) {
///        ...
///      }
///
///  Example 2: Pack and unpack a message in Java.
///
///      Foo foo = ...;
///      Any any = Any.pack(foo);
///      ...
///      if (any.is(Foo.class)) {
///        foo = any.unpack(Foo.class);
///      }
///
///   Example 3: Pack and unpack a message in Python.
///
///      foo = Foo(...)
///      any = Any()
///      any.Pack(foo)
///      ...
///      if any.Is(Foo.DESCRIPTOR):
///        any.Unpack(foo)
///        ...
///
///   Example 4: Pack and unpack a message in Go
///
///       foo := &pb.Foo{...}
///       any, err := ptypes.MarshalAny(foo)
///       ...
///       foo := &pb.Foo{}
///       if err := ptypes.UnmarshalAny(any, foo); err != nil {
///         ...
///       }
///
///  The pack methods provided by protobuf library will by default use
///  'type.googleapis.com/full.type.name' as the type URL and the unpack
///  methods only use the fully qualified type name after the last '/'
///  in the type URL, for example "foo.bar.com/x/y.z" will yield type
///  name "y.z".
///
///
///  JSON
///  ====
///  The JSON representation of an `Any` value uses the regular
///  representation of the deserialized, embedded message, with an
///  additional field `@type` which contains the type URL. Example:
///
///      package google.profile;
///      message Person {
///        string first_name = 1;
///        string last_name = 2;
///      }
///
///      {
///        "@type": "type.googleapis.com/google.profile.Person",
///        "firstName": <string>,
///        "lastName": <string>
///      }
///
///  If the embedded message type is well-known and has a custom JSON
///  representation, that representation will be embedded adding a field
///  `value` which holds the custom JSON in addition to the `@type`
///  field. Example (for message [google.protobuf.Duration][]):
///
///      {
///        "@type": "type.googleapis.com/google.protobuf.Duration",
///        "value": "1.212s"
///      }
class ErrorCode extends $pb.ProtobufEnum {
  static const ErrorCode None = ErrorCode._(0, _omitEnumNames ? '' : 'None');
  static const ErrorCode Success = ErrorCode._(1, _omitEnumNames ? '' : 'Success');
  static const ErrorCode Failed = ErrorCode._(2, _omitEnumNames ? '' : 'Failed');

  static const $core.List<ErrorCode> values = <ErrorCode> [
    None,
    Success,
    Failed,
  ];

  static final $core.Map<$core.int, ErrorCode> _byValue = $pb.ProtobufEnum.initByValue(values);
  static ErrorCode? valueOf($core.int value) => _byValue[value];

  const ErrorCode._($core.int v, $core.String n) : super(v, n);
}

class ePacketID extends $pb.ProtobufEnum {
  static const ePacketID NONE = ePacketID._(0, _omitEnumNames ? '' : 'NONE');
  static const ePacketID eReqLoginInfo = ePacketID._(1, _omitEnumNames ? '' : 'eReqLoginInfo');
  static const ePacketID eResLoginInfo = ePacketID._(2, _omitEnumNames ? '' : 'eResLoginInfo');
  static const ePacketID eReqFolderInfo = ePacketID._(3, _omitEnumNames ? '' : 'eReqFolderInfo');
  static const ePacketID eResFolderInfoBegin = ePacketID._(4, _omitEnumNames ? '' : 'eResFolderInfoBegin');
  static const ePacketID eNotifyFolderInfo = ePacketID._(5, _omitEnumNames ? '' : 'eNotifyFolderInfo');
  static const ePacketID eNotifyFileInfo = ePacketID._(6, _omitEnumNames ? '' : 'eNotifyFileInfo');
  static const ePacketID eResFolderInfoEnd = ePacketID._(7, _omitEnumNames ? '' : 'eResFolderInfoEnd');
  static const ePacketID eReqDuplicateCheckBegin = ePacketID._(8, _omitEnumNames ? '' : 'eReqDuplicateCheckBegin');
  static const ePacketID eResDuplicateCheckBegin = ePacketID._(9, _omitEnumNames ? '' : 'eResDuplicateCheckBegin');
  static const ePacketID eReqDuplicateCheck = ePacketID._(10, _omitEnumNames ? '' : 'eReqDuplicateCheck');
  static const ePacketID eResDuplicateCheck = ePacketID._(11, _omitEnumNames ? '' : 'eResDuplicateCheck');
  static const ePacketID eReqDuplicateCheckEnd = ePacketID._(12, _omitEnumNames ? '' : 'eReqDuplicateCheckEnd');
  static const ePacketID eResDuplicateCheckEnd = ePacketID._(13, _omitEnumNames ? '' : 'eResDuplicateCheckEnd');
  static const ePacketID eReqFileSendBegin = ePacketID._(14, _omitEnumNames ? '' : 'eReqFileSendBegin');
  static const ePacketID eResFileSendBegin = ePacketID._(15, _omitEnumNames ? '' : 'eResFileSendBegin');
  static const ePacketID eReqFileOverwriteCheck = ePacketID._(16, _omitEnumNames ? '' : 'eReqFileOverwriteCheck');
  static const ePacketID eResFileOverwriteCheck = ePacketID._(17, _omitEnumNames ? '' : 'eResFileOverwriteCheck');
  static const ePacketID eReqFileContentsSend = ePacketID._(18, _omitEnumNames ? '' : 'eReqFileContentsSend');
  static const ePacketID eResFileContentsSend = ePacketID._(19, _omitEnumNames ? '' : 'eResFileContentsSend');
  static const ePacketID eReqFileSendEnd = ePacketID._(20, _omitEnumNames ? '' : 'eReqFileSendEnd');
  static const ePacketID eResFileSendEnd = ePacketID._(21, _omitEnumNames ? '' : 'eResFileSendEnd');
  static const ePacketID eReqFileNameChange = ePacketID._(22, _omitEnumNames ? '' : 'eReqFileNameChange');
  static const ePacketID eResFileNameChange = ePacketID._(23, _omitEnumNames ? '' : 'eResFileNameChange');
  static const ePacketID eReqFileDelete = ePacketID._(24, _omitEnumNames ? '' : 'eReqFileDelete');
  static const ePacketID eResFileDelete = ePacketID._(25, _omitEnumNames ? '' : 'eResFileDelete');
  static const ePacketID eReqServiceList = ePacketID._(26, _omitEnumNames ? '' : 'eReqServiceList');
  static const ePacketID eResServiceList = ePacketID._(27, _omitEnumNames ? '' : 'eResServiceList');
  static const ePacketID eReqServiceStart = ePacketID._(28, _omitEnumNames ? '' : 'eReqServiceStart');
  static const ePacketID eResServiceStart = ePacketID._(29, _omitEnumNames ? '' : 'eResServiceStart');
  static const ePacketID eReqServiceStop = ePacketID._(30, _omitEnumNames ? '' : 'eReqServiceStop');
  static const ePacketID eResServiceStop = ePacketID._(31, _omitEnumNames ? '' : 'eResServiceStop');
  static const ePacketID eReqFileReceiveBegin = ePacketID._(32, _omitEnumNames ? '' : 'eReqFileReceiveBegin');
  static const ePacketID eResFileReceiveBegin = ePacketID._(33, _omitEnumNames ? '' : 'eResFileReceiveBegin');
  static const ePacketID eReqFileReceiveConents = ePacketID._(34, _omitEnumNames ? '' : 'eReqFileReceiveConents');
  static const ePacketID eResFileReceiveConents = ePacketID._(35, _omitEnumNames ? '' : 'eResFileReceiveConents');
  static const ePacketID eNotifyFileReceiveEnd = ePacketID._(36, _omitEnumNames ? '' : 'eNotifyFileReceiveEnd');
  static const ePacketID eReqChatBot = ePacketID._(37, _omitEnumNames ? '' : 'eReqChatBot');
  static const ePacketID eResChatBot = ePacketID._(38, _omitEnumNames ? '' : 'eResChatBot');
  static const ePacketID MAX = ePacketID._(39, _omitEnumNames ? '' : 'MAX');

  static const $core.List<ePacketID> values = <ePacketID> [
    NONE,
    eReqLoginInfo,
    eResLoginInfo,
    eReqFolderInfo,
    eResFolderInfoBegin,
    eNotifyFolderInfo,
    eNotifyFileInfo,
    eResFolderInfoEnd,
    eReqDuplicateCheckBegin,
    eResDuplicateCheckBegin,
    eReqDuplicateCheck,
    eResDuplicateCheck,
    eReqDuplicateCheckEnd,
    eResDuplicateCheckEnd,
    eReqFileSendBegin,
    eResFileSendBegin,
    eReqFileOverwriteCheck,
    eResFileOverwriteCheck,
    eReqFileContentsSend,
    eResFileContentsSend,
    eReqFileSendEnd,
    eResFileSendEnd,
    eReqFileNameChange,
    eResFileNameChange,
    eReqFileDelete,
    eResFileDelete,
    eReqServiceList,
    eResServiceList,
    eReqServiceStart,
    eResServiceStart,
    eReqServiceStop,
    eResServiceStop,
    eReqFileReceiveBegin,
    eResFileReceiveBegin,
    eReqFileReceiveConents,
    eResFileReceiveConents,
    eNotifyFileReceiveEnd,
    eReqChatBot,
    eResChatBot,
    MAX,
  ];

  static final $core.Map<$core.int, ePacketID> _byValue = $pb.ProtobufEnum.initByValue(values);
  static ePacketID? valueOf($core.int value) => _byValue[value];

  const ePacketID._($core.int v, $core.String n) : super(v, n);
}


const _omitEnumNames = $core.bool.fromEnvironment('protobuf.omit_enum_names');
