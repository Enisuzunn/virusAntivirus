using VirusAntivirusSimulator.Models;

namespace VirusAntivirusSimulator.Services;

/// <summary>
/// Antivirüs tarama servisi
/// İmza tabanlı tespit mantığını uygular
/// </summary>
public class AntivirusScanner
{
    private readonly string _virusSignature;

    /// <summary>
    /// AntivirusScanner constructor
    /// </summary>
    /// <param name="virusSignature">Aranacak virüs imzası</param>
    public AntivirusScanner(string virusSignature)
    {
        _virusSignature = virusSignature;
    }

    /// <summary>
    /// Belirtilen klasördeki tüm .txt dosyalarını tarar
    /// </summary>
    /// <param name="folderPath">Taranacak klasör yolu</param>
    /// <returns>Tarama sonuçları listesi</returns>
    public List<ScanResult> ScanFolder(string folderPath)
    {
        var results = new List<ScanResult>();
        string[] txtFiles = Directory.GetFiles(folderPath, "*.txt", SearchOption.TopDirectoryOnly);

        foreach (string filePath in txtFiles)
        {
            var result = ScanFile(filePath);
            results.Add(result);
        }

        return results;
    }

    /// <summary>
    /// Tek bir dosyayı tarar
    /// </summary>
    /// <param name="filePath">Dosya yolu</param>
    /// <returns>Tarama sonucu</returns>
    private ScanResult ScanFile(string filePath)
    {
        bool isThreat = false;

        try
        {
            string content = File.ReadAllText(filePath);
            isThreat = content.Contains(_virusSignature);
        }
        catch
        {
            // Dosya okunamazsa temiz olarak işaretle
            isThreat = false;
        }

        return new ScanResult
        {
            FileName = Path.GetFileName(filePath),
            FilePath = filePath,
            IsThreat = isThreat,
            ScanTime = DateTime.Now
        };
    }

    /// <summary>
    /// Tehdit dosyasını siler
    /// </summary>
    /// <param name="filePath">Silinecek dosya yolu</param>
    /// <returns>İşlem başarılı ise true</returns>
    public (bool success, string message) DeleteThreat(string filePath)
    {
        try
        {
            File.Delete(filePath);
            return (true, "Dosya başarıyla silindi");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}
