using Robust.Client;

namespace Content.SIS.Client;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ContentStart.Start(args);
    }
}
