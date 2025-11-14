//
//  Generated code. Do not modify.
//  source: Proto/Any.proto
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
class PacketID extends $pb.ProtobufEnum {
  static const PacketID NONE = PacketID._(0, _omitEnumNames ? '' : 'NONE');
  static const PacketID PacketID_NotifyServerInfo = PacketID._(1, _omitEnumNames ? '' : 'PacketID_NotifyServerInfo');
  static const PacketID PacketID_NotifyClientInfo = PacketID._(2, _omitEnumNames ? '' : 'PacketID_NotifyClientInfo');

  static const $core.List<PacketID> values = <PacketID> [
    NONE,
    PacketID_NotifyServerInfo,
    PacketID_NotifyClientInfo,
  ];

  static final $core.Map<$core.int, PacketID> _byValue = $pb.ProtobufEnum.initByValue(values);
  static PacketID? valueOf($core.int value) => _byValue[value];

  const PacketID._($core.int v, $core.String n) : super(v, n);
}


const _omitEnumNames = $core.bool.fromEnvironment('protobuf.omit_enum_names');
