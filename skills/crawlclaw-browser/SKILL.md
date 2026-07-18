---
name: crawlclaw-browser
description: "Full browser automation via Playwright. Use when basic URL fetching or web search fails — JS-heavy sites, login walls, captchas, dynamic content, or when human takeover is needed mid-navigation."
metadata:
  {
    "openclaw":
      {
        "emoji": "🌐",
        "requires": { "bins": ["crawlclaw-browser"] },
        "install":
          [
            {
              "id": "dotnet",
              "kind": "manual",
              "label": "Build from source: cd skills/crawlclaw-browser/src && dotnet publish -c Release",
            },
          ],
      },
  }
---

# CrawlClaw Browser

Use when standard web fetch or search is insufficient. Launches a real Playwright browser — headless by default, headed when human interaction is needed.

## When to use
- Site requires JavaScript to render content
- Login, auth, or captcha wall blocking access
- Dynamic content (infinite scroll, SPAs)
- Need to fill forms or click through flows
- Human takeover needed (use browser_request_human)

## Tools

### browser_navigate
Navigate to a URL and return page content.

### browser_click
Click an element by CSS selector.

### browser_type
Type text into an input field.

### browser_screenshot
Full page screenshot, returns base64.

### browser_extract
Extract text from a CSS selector.

### browser_request_human
Switch to headed mode and pause for human takeover.

### browser_close
Close the current browser session.

## Rules
- Always call browser_close when done.
- Use browser_request_human for any auth/login wall.
- Prefer browser_extract over browser_screenshot when you just need text.
- One browser session at a time.
