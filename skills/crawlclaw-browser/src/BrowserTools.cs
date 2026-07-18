using ModelContextProtocol.Server;
using Microsoft.Playwright;
using System.ComponentModel;

[McpServerToolType]
public static class BrowserTools
{
    [McpServerTool, Description("Navigate to a URL and return page text.")]
    public static async Task<string> browser_navigate(
        BrowserSession session,
        [Description("URL to navigate to")] string url)
    {
        var page = await session.GetPageAsync();
        await page.GotoAsync(url, new() { WaitUntil = WaitUntilState.NetworkIdle });
        return await page.InnerTextAsync("body");
    }

    [McpServerTool, Description("Click an element by CSS selector.")]
    public static async Task<string> browser_click(
        BrowserSession session,
        [Description("CSS selector of element to click")] string selector)
    {
        var page = await session.GetPageAsync();
        await page.ClickAsync(selector);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return $"Clicked {selector}";
    }

    [McpServerTool, Description("Type text into an input field.")]
    public static async Task<string> browser_type(
        BrowserSession session,
        [Description("CSS selector of input")] string selector,
        [Description("Text to type")] string text)
    {
        var page = await session.GetPageAsync();
        await page.FillAsync(selector, text);
        return $"Typed into {selector}";
    }

    [McpServerTool, Description("Take a full page screenshot, returns base64.")]
    public static async Task<string> browser_screenshot(BrowserSession session)
    {
        var page = await session.GetPageAsync();
        var bytes = await page.ScreenshotAsync(new() { FullPage = true });
        return Convert.ToBase64String(bytes);
    }

    [McpServerTool, Description("Extract text from a CSS selector.")]
    public static async Task<string> browser_extract(
        BrowserSession session,
        [Description("CSS selector to extract from")] string selector)
    {
        var page = await session.GetPageAsync();
        return await page.InnerTextAsync(selector);
    }

    [McpServerTool, Description("Switch to headed mode and pause for human takeover.")]
    public static async Task<string> browser_request_human(
        BrowserSession session,
        [Description("Reason why human intervention is needed")] string reason)
    {
        var page = await session.GetPageAsync(headed: true);
        Console.Error.WriteLine($"\n[CrawlClaw] Human takeover: {reason}");
        Console.Error.WriteLine("[CrawlClaw] Press ENTER when done...");
        await Task.Run(() => Console.ReadLine());
        return "Human takeover complete. Browser back in agent control.";
    }

    [McpServerTool, Description("Close the current browser session.")]
    public static async Task<string> browser_close(BrowserSession session)
    {
        await session.DisposeAsync();
        return "Browser session closed.";
    }
}
