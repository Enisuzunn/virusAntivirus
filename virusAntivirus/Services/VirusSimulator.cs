namespace VirusAntivirusSimulator.Services;

/// <summary>
/// Virüs simülasyonu servisi
/// Zararsız bir dosya oluşturarak virüs bulaşmasını simüle eder
/// </summary>
public class VirusSimulator
{
    /// <summary>
    /// Simüle edilen virüs imzası - antivirüs taramasında aranacak
    /// </summary>
    public const string VIRUS_SIGNATURE = "SIMULATED_VIRUS_SIGNATURE";

    /// <summary>
    /// Oluşturulacak virüs dosyasının adı
    /// </summary>
    public const string VIRUS_FILENAME = "fake_virus.txt";

    /// <summary>
    /// Virüs imzasını döndürür
    /// </summary>
    public string VirusSignature => VIRUS_SIGNATURE;

    /// <summary>
    /// Virüs dosya adını döndürür
    /// </summary>
    public string VirusFileName => VIRUS_FILENAME;

    /// <summary>
    /// Belirtilen klasöre simüle edilmiş virüs dosyası oluşturur
    /// </summary>
    /// <param name="targetFolder">Hedef klasör yolu</param>
    /// <returns>İşlem sonucu ve mesajı</returns>
    public (bool success, string message, string? filePath) CreateFakeVirus(string targetFolder)
    {
        string targetPath = Path.Combine(targetFolder, VIRUS_FILENAME);

        if (File.Exists(targetPath))
        {
            return (false, $"'{VIRUS_FILENAME}' dosyası bu klasörde zaten mevcut!", null);
        }

        try
        {
            string content = $"""
                ═══════════════════════════════════════════════════════════
                Bu dosya bir EĞİTİM SİMÜLASYONUDUR.
                Gerçek bir virüs DEĞİLDİR ve zararsızdır.
                ═══════════════════════════════════════════════════════════
                
                Virüs İmzası: {VIRUS_SIGNATURE}
                
                Oluşturulma Tarihi: {DateTime.Now}
                
                Bu imza antivirüs tarayıcısı tarafından tespit edilecektir.
                ═══════════════════════════════════════════════════════════
                """;

            File.WriteAllText(targetPath, content);
            return (true, "Dosya başarıyla oluşturuldu", targetPath);
        }
        catch (Exception ex)
        {
            return (false, ex.Message, null);
        }
    }
}
