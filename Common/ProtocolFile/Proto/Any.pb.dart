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

export 'Any.pbenum.dart';

class NotifyServerInfo extends $pb.GeneratedMessage {
  factory NotifyServerInfo({
    $core.List<$core.int>? serverKey,
  }) {
    final $result = create();
    if (serverKey != null) {
      $result.serverKey = serverKey;
    }
    return $result;
  }
  NotifyServerInfo._() : super();
  factory NotifyServerInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory NotifyServerInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'NotifyServerInfo', package: const $pb.PackageName(_omitMessageNames ? '' : 'tutorial'), createEmptyInstance: create)
    ..a<$core.List<$core.int>>(1, _omitFieldNames ? '' : 'serverKey', $pb.PbFieldType.OY, protoName: 'serverKey')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  NotifyServerInfo clone() => NotifyServerInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  NotifyServerInfo copyWith(void Function(NotifyServerInfo) updates) => super.copyWith((message) => updates(message as NotifyServerInfo)) as NotifyServerInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static NotifyServerInfo create() => NotifyServerInfo._();
  NotifyServerInfo createEmptyInstance() => create();
  static $pb.PbList<NotifyServerInfo> createRepeated() => $pb.PbList<NotifyServerInfo>();
  @$core.pragma('dart2js:noInline')
  static NotifyServerInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<NotifyServerInfo>(create);
  static NotifyServerInfo? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.int> get serverKey => $_getN(0);
  @$pb.TagNumber(1)
  set serverKey($core.List<$core.int> v) { $_setBytes(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasServerKey() => $_has(0);
  @$pb.TagNumber(1)
  void clearServerKey() => clearField(1);
}

class NotifyClientInfo extends $pb.GeneratedMessage {
  factory NotifyClientInfo({
    $core.List<$core.int>? clientKey,
  }) {
    final $result = create();
    if (clientKey != null) {
      $result.clientKey = clientKey;
    }
    return $result;
  }
  NotifyClientInfo._() : super();
  factory NotifyClientInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory NotifyClientInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'NotifyClientInfo', package: const $pb.PackageName(_omitMessageNames ? '' : 'tutorial'), createEmptyInstance: create)
    ..a<$core.List<$core.int>>(1, _omitFieldNames ? '' : 'clientKey', $pb.PbFieldType.OY, protoName: 'clientKey')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  NotifyClientInfo clone() => NotifyClientInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  NotifyClientInfo copyWith(void Function(NotifyClientInfo) updates) => super.copyWith((message) => updates(message as NotifyClientInfo)) as NotifyClientInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static NotifyClientInfo create() => NotifyClientInfo._();
  NotifyClientInfo createEmptyInstance() => create();
  static $pb.PbList<NotifyClientInfo> createRepeated() => $pb.PbList<NotifyClientInfo>();
  @$core.pragma('dart2js:noInline')
  static NotifyClientInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<NotifyClientInfo>(create);
  static NotifyClientInfo? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.int> get clientKey => $_getN(0);
  @$pb.TagNumber(1)
  set clientKey($core.List<$core.int> v) { $_setBytes(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasClientKey() => $_has(0);
  @$pb.TagNumber(1)
  void clearClientKey() => clearField(1);
}

class Any extends $pb.GeneratedMessage {
  factory Any({
    $core.String? typeUrl,
    $core.List<$core.int>? value,
  }) {
    final $result = create();
    if (typeUrl != null) {
      $result.typeUrl = typeUrl;
    }
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  Any._() : super();
  factory Any.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Any.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Any', package: const $pb.PackageName(_omitMessageNames ? '' : 'tutorial'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'typeUrl')
    ..a<$core.List<$core.int>>(2, _omitFieldNames ? '' : 'value', $pb.PbFieldType.OY)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Any clone() => Any()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Any copyWith(void Function(Any) updates) => super.copyWith((message) => updates(message as Any)) as Any;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Any create() => Any._();
  Any createEmptyInstance() => create();
  static $pb.PbList<Any> createRepeated() => $pb.PbList<Any>();
  @$core.pragma('dart2js:noInline')
  static Any getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Any>(create);
  static Any? _defaultInstance;

  ///  A URL/resource name that uniquely identifies the type of the serialized
  ///  protocol buffer message. This string must contain at least
  ///  one "/" character. The last segment of the URL's path must represent
  ///  the fully qualified name of the type (as in
  ///  `path/google.protobuf.Duration`). The name should be in a canonical form
  ///  (e.g., leading "." is not accepted).
  ///
  ///  In practice, teams usually precompile into the binary all types that they
  ///  expect it to use in the context of Any. However, for URLs which use the
  ///  scheme `http`, `https`, or no scheme, one can optionally set up a type
  ///  server that maps type URLs to message definitions as follows:
  ///
  ///  * If no scheme is provided, `https` is assumed.
  ///  * An HTTP GET on the URL must yield a [google.protobuf.Type][]
  ///    value in binary format, or produce an error.
  ///  * Applications are allowed to cache lookup results based on the
  ///    URL, or have them precompiled into a binary to avoid any
  ///    lookup. Therefore, binary compatibility needs to be preserved
  ///    on changes to types. (Use versioned type names to manage
  ///    breaking changes.)
  ///
  ///  Note: this functionality is not currently available in the official
  ///  protobuf release, and it is not used for type URLs beginning with
  ///  type.googleapis.com.
  ///
  ///  Schemes other than `http`, `https` (or the empty scheme) might be
  ///  used with implementation specific semantics.
  @$pb.TagNumber(1)
  $core.String get typeUrl => $_getSZ(0);
  @$pb.TagNumber(1)
  set typeUrl($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTypeUrl() => $_has(0);
  @$pb.TagNumber(1)
  void clearTypeUrl() => clearField(1);

  /// Must be a valid serialized protocol buffer of the above specified type.
  @$pb.TagNumber(2)
  $core.List<$core.int> get value => $_getN(1);
  @$pb.TagNumber(2)
  set value($core.List<$core.int> v) { $_setBytes(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasValue() => $_has(1);
  @$pb.TagNumber(2)
  void clearValue() => clearField(2);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');
