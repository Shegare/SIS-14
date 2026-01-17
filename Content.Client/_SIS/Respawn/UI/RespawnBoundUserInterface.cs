using JetBrains.Annotations;
using Robust.Client.Console;
using Robust.Client.UserInterface;

namespace Content.Client._SIS.Respawn.UI;

[UsedImplicitly]
public sealed class RespawnBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    [Dependency] private readonly IClientConsoleHost _console = default!;

    private RespawnWindow? _window;

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<RespawnWindow>();
        _window.OpenCentered();

        _window.OnRequestPressed += ButtonPressed;
        _window.OnClose += Close;
    }

    private void ButtonPressed()
    {
        _console.ExecuteCommand("respawn");
        Close();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing)
            return;

        _window?.Close();
        _window = null;
    }
}
