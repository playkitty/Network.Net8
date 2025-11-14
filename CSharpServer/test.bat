

.\Protocol\protoc.exe --proto_path=.\Protocol --cpp_out=.\Protocol\ProtocolFile .\Protocol\Proto\any.proto
@$(ProjectDir)protoc.exe" --proto_path=.\ --cpp_out=.\ProtocolFile .\Proto\%(Filename)%(Extension)

pause
