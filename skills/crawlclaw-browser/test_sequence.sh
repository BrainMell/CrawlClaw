#!/bin/bash
(
  echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0"}}}'
  sleep 1
  echo '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"browser_navigate","arguments":{"url":"https://google.com"}}}'
  sleep 5
  echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"browser_type","arguments":{"selector":"textarea","text":"openclaw github"}}}'
  sleep 2
  echo '{"jsonrpc":"2.0","id":4,"method":"tools/call","params":{"name":"browser_screenshot","arguments":{}}}'
  sleep 5
  echo '{"jsonrpc":"2.0","id":5,"method":"tools/call","params":{"name":"browser_close","arguments":{}}}'
  sleep 1
) | dotnet run --project src 2>/dev/null
