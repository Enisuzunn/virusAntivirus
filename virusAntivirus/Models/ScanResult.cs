namespace VirusAntivirusSimulator.Models;

/// <summary>
/// Tarama sonucu veri modeli
/// </summary>
public class ScanResult
{
    /// <summary>
    /// Taranan dosyanın adı
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Taranan dosyanın tam yolu
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Dosyada tehdit bulunup bulunmadığı
    /// </summary>
    public bool IsThreat { get; set; }

    /// <summary>
    /// Tarama zamanı
    /// </summary>
    public DateTime ScanTime { get; set; } = DateTime.Now;
}
