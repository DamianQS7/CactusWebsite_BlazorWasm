using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CactusWebsite.Components;

public partial class NavBar
{
    [Inject] IJSRuntime Javascript { get; set; }
    private IJSObjectReference? module;
    private DotNetObjectReference<NavBar> objRef;
    private double scrollY;
    private string blurClass = string.Empty;
    private bool isMenuVisible = false;
    private string menuStyle => isMenuVisible ? "right: 0" : "right: -100%";

    // Methods
    private void ToggleMenu() => isMenuVisible = !isMenuVisible;
    private void CloseMenu() => isMenuVisible = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await Javascript.InvokeAsync<IJSObjectReference>
                            ("import", "./Components/NavBar.razor.js");

            objRef = DotNetObjectReference.Create(this);
            await module.InvokeVoidAsync("addWindowEventListener", objRef);
        }

    }

    [JSInvokable]
    public async Task BlurHeader()
    {
        if (module is not null)
        {
            scrollY = await Javascript.InvokeAsync<double>("jsFunctions.getScrollYPosition");
            blurClass = scrollY >= 50 ? "blur-header" : "";
            StateHasChanged();
        }
    }

    private void Dispose()
    {
        objRef?.Dispose();
    }
}
