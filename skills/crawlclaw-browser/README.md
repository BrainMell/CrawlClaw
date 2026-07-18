# crawlclaw-browser

A Playwright-powered browser automation skill for OpenClaw, written in C#/.NET over MCP.

Gives any agent (OpenClaw, Claude Code, whatever speaks MCP) the ability to actually drive a browser — navigate, click, type, extract, screenshot — and hand control back to a human when it hits something it can't solve alone (logins, captchas, anything needing eyes on screen).

## Why

Basic fetch/search tools break on JS-heavy sites, auth walls, and dynamic content. This skill is the fallback: when the cheap tools fail, spin up a real browser instead.

## Requirements

- .NET 10 SDK
- Playwright CLI + Chromium (installed once via `dotnet tool install --global Microsoft.Playwright.CLI && playwright install chromium`)
- Python 3 (only needed for the local `repl.py` test harness, not for the skill itself)

## Build

cd src
dotnet restore
dotnet build

## Test it standalone (no OpenClaw needed)

python3 repl.py            # headless
python3 repl.py --headed   # opens a real visible browser window

Then type tool calls at the prompt:

browser_navigate {"url": "https://example.com"}
browser_extract {"selector": "h1"}
browser_close {}

## Tools

| Tool | What it does |
|---|---|
| browser_navigate | Go to a URL, return page text |
| browser_click | Click an element by CSS selector |
| browser_type | Fill an input field |
| browser_extract | Pull text from a specific selector |
| browser_screenshot | Full-page screenshot, base64 |
| browser_request_human | Switch to headed mode, pause until you press Enter, then hand back to the agent |
| browser_close | Kill the session |

Rule of thumb: prefer browser_extract over browser_screenshot when you just need text — cheaper and less to parse.

## How it's wired

- Program.cs — boots the MCP server over stdio. Logs go to stderr so stdout stays pure JSON-RPC.
- BrowserSession.cs — owns the single Playwright browser instance. Headed/headless is controlled by the CRAWLCLAW_HEADED=1 env var or an explicit flag.
- BrowserTools.cs — the actual tools. Each method tagged [McpServerTool] is auto-discovered by the SDK; no manual registration.
- SKILL.md — the file OpenClaw reads to know this skill exists and when to use it.

## Adding a new tool

Drop a new static method into BrowserTools.cs following the existing pattern: tag it [McpServerTool] with a [Description], take a BrowserSession plus whatever args you need, do the Playwright logic, return a string.

Test it in the REPL before wiring anything into OpenClaw.

## Status

Working standalone. Not yet integrated/routed into OpenClaw's gateway — that's the next step once all tools are tested independently.

## License

MIT, same as the base OpenClaw project this is forked from.

