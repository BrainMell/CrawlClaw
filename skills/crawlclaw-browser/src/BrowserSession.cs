using Microsoft.Playwright;

public class BrowserSession : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IPage? _page;
    private bool _headed = false;

    public async Task<IPage> GetPageAsync(bool headed = false)
    {
        if (_page != null && _headed == headed)
            return _page;

        await DisposeAsync();

        _headed = headed;
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new()
        {
            Headless = !headed
        });
        _page = await _browser.NewPageAsync();
        return _page;
    }

    public async ValueTask DisposeAsync()
    {
        if (_page != null) { await _page.CloseAsync(); _page = null; }
        if (_browser != null) { await _browser.CloseAsync(); _browser = null; }
        _playwright?.Dispose();
        _playwright = null;
    }
}
