
call .\protoc.exe --proto_path=.\ --csharp_out=..\..\Common\ProtocolFile .\Proto\Any.proto

call .\protoc.exe --proto_path=.\ --dart_out=..\..\Common\ProtocolFile .\Proto\dartany.proto

pause
