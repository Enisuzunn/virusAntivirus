namespace VirusAntivirusSimulator;

/// <summary>
/// Uygulama giriş noktası
/// </summary>
internal static class Program
{
    /// <summary>
    /// Uygulamanın ana giriş noktası
    /// </summary>
    [STAThread]
    static void Main()
    {
        // WinForms uygulama yapılandırması
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
