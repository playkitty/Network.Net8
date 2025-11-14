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

import 'dartany.pbenum.dart';

export 'dartany.pbenum.dart';

class ReqLoginInfo extends $pb.GeneratedMessage {
  factory ReqLoginInfo() => create();
  ReqLoginInfo._() : super();
  factory ReqLoginInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ReqLoginInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ReqLoginInfo', package: const $pb.PackageName(_omitMessageNames ? '' : 'tutorial'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ReqLoginInfo clone() => ReqLoginInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ReqLoginInfo copyWith(void Function(ReqLoginInfo) updates) => super.copyWith((message) => updates(message as ReqLoginInfo)) as ReqLoginInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ReqLoginInfo create() => ReqLoginInfo._();
  ReqLoginInfo createEmptyInstance() => create();
  static $pb.PbList<ReqLoginInfo> createRepeated() => $pb.PbList<ReqLoginInfo>();
  @$core.pragma('dart2js:noInline')
  static ReqLoginInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ReqLoginInfo>(create);
  static ReqLoginInfo? _defaultInstance;
}

class ResLoginInfo extends $pb.GeneratedMessage {
  factory ResLoginInfo({
    ErrorCode? result,
    $core.int? bandWidth,
  }) {
    final $result = create();
    if (result != null) {
      $result.result = result;
    }
    if (bandWidth != null) {
      $result.bandWidth = bandWidth;
    }
    return $result;
  }
  ResLoginInfo._() : super();
  factory ResLoginInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ResLoginInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ResLoginInfo', package: const $pb.PackageName(_omitMessageNames ? '' : 'tutorial'), createEmptyInstance: create)
    ..e<ErrorCode>(1, _omitFieldNames ? '' : 'result', $pb.PbFieldType.OE, defaultOrMaker: ErrorCode.None, valueOf: ErrorCode.valueOf, enumValues: ErrorCode.values)
    ..a<$core.int>(2, _omitFieldNames ? '' : 'bandWidth', $pb.PbFieldType.O3, protoName: 'bandWidth')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ResLoginInfo clone() => ResLoginInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ResLoginInfo copyWith(void Function(ResLoginInfo) updates) => super.copyWith((message) => updates(message as ResLoginInfo)) as ResLoginInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ResLoginInfo create() => ResLoginInfo._();
  ResLoginInfo createEmptyInstance() => create();
  static $pb.PbList<ResLoginInfo> createRepeated() => $pb.PbList<ResLoginInfo>();
  @$core.pragma('dart2js:noInline')
  static ResLoginInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ResLoginInfo>(create);
  static ResLoginInfo? _defaultInstance;

  @$pb.TagNumber(1)
  ErrorCode get result => $_getN(0);
  @$pb.TagNumber(1)
  set result(ErrorCode v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasResult() => $_has(0);
  @$pb.TagNumber(1)
  void clearResult() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get bandWidth => $_getIZ(1);
  @$pb.TagNumber(2)
  set bandWidth($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasBandWidth() => $_has(1);
  @$pb.TagNumber(2)
  void clearBandWidth() => clearField(2);
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
