using System.Windows.Forms;
using System.Drawing;
using VirusAntivirusSimulator.Services;
using VirusAntivirusSimulator.Models;

namespace VirusAntivirusSimulator;

/// <summary>
/// Virüs ve Antivirüs Simülasyonu Ana Formu
/// Bu uygulama eğitim amaçlıdır ve gerçek bir virüs/antivirüs değildir.
/// İmza tabanlı antivirüs mantığını göstermeyi amaçlar.
/// </summary>
public partial class MainForm : Form
{
    // Servisler
    private readonly VirusSimulator _virusSimulator;
    private readonly AntivirusScanner _antivirusScanner;

    // UI Kontrolleri - Virüs Paneli
    private GroupBox grpVirus = null!;
    private Label lblVirusFolder = null!;
    private TextBox txtVirusFolder = null!;
    private Button btnSelectVirusFolder = null!;
    private Button btnInfect = null!;

    // UI Kontrolleri - Antivirüs Paneli
    private GroupBox grpAntivirus = null!;
    private Label lblAntivirusFolder = null!;
    private TextBox txtAntivirusFolder = null!;
    private Button btnSelectAntivirusFolder = null!;
    private Button btnScan = null!;
    private Button btnDelete = null!;
    private ListView lvResults = null!;

    // UI Kontrolleri - Log Paneli
    private GroupBox grpLog = null!;
    private TextBox txtLog = null!;

    public MainForm()
    {
        _virusSimulator = new VirusSimulator();
        _antivirusScanner = new AntivirusScanner(_virusSimulator.VirusSignature);
        InitializeComponent();
    }

    /// <summary>
    /// Form kontrollerini oluşturur ve yapılandırır
    /// </summary>
    private void InitializeComponent()
    {
        // Form ayarları
        this.Text = "Virüs & Antivirüs Simülasyonu (Eğitim Amaçlı)";
        this.Size = new Size(900, 650);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MinimumSize = new Size(800, 550);

        // ========================================
        // SOL PANEL - VİRÜS SİMÜLASYONU
        // ========================================
        grpVirus = new GroupBox
        {
            Text = "🦠 Virüs Simülasyonu",
            Location = new Point(10, 10),
            Size = new Size(420, 180),
            Anchor = AnchorStyles.Top | AnchorStyles.Left
        };

        lblVirusFolder = new Label
        {
            Text = "Hedef Klasör:",
            Location = new Point(15, 30),
            AutoSize = true
        };

        txtVirusFolder = new TextBox
        {
            Location = new Point(15, 50),
            Size = new Size(300, 25),
            ReadOnly = true,
            BackColor = SystemColors.Window
        };

        btnSelectVirusFolder = new Button
        {
            Text = "Gözat...",
            Location = new Point(320, 48),
            Size = new Size(80, 27)
        };
        btnSelectVirusFolder.Click += BtnSelectVirusFolder_Click;

        btnInfect = new Button
        {
            Text = "🦠 Bulaştır",
            Location = new Point(15, 90),
            Size = new Size(385, 40),
            BackColor = Color.FromArgb(255, 200, 200),
            Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold),
            Enabled = false
        };
        btnInfect.Click += BtnInfect_Click;

        // Açıklama etiketi
        var lblVirusInfo = new Label
        {
            Text = "⚠️ Bu simülasyon zararsızdır. Sadece bir metin dosyası oluşturur.",
            Location = new Point(15, 140),
            Size = new Size(385, 30),
            ForeColor = Color.Gray,
            Font = new Font(this.Font.FontFamily, 8)
        };

        grpVirus.Controls.AddRange(new Control[] { 
            lblVirusFolder, txtVirusFolder, btnSelectVirusFolder, 
            btnInfect, lblVirusInfo 
        });

        // ========================================
        // SAĞ PANEL - ANTİVİRÜS
        // ========================================
        grpAntivirus = new GroupBox
        {
            Text = "🛡️ Antivirüs Tarayıcı",
            Location = new Point(450, 10),
            Size = new Size(420, 350),
            Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left
        };

        lblAntivirusFolder = new Label
        {
            Text = "Taranacak Klasör:",
            Location = new Point(15, 30),
            AutoSize = true
        };

        txtAntivirusFolder = new TextBox
        {
            Location = new Point(15, 50),
            Size = new Size(300, 25),
            ReadOnly = true,
            BackColor = SystemColors.Window
        };

        btnSelectAntivirusFolder = new Button
        {
            Text = "Gözat...",
            Location = new Point(320, 48),
            Size = new Size(80, 27)
        };
        btnSelectAntivirusFolder.Click += BtnSelectAntivirusFolder_Click;

        btnScan = new Button
        {
            Text = "🔍 Tara",
            Location = new Point(15, 85),
            Size = new Size(180, 35),
            BackColor = Color.FromArgb(200, 230, 255),
            Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold),
            Enabled = false
        };
        btnScan.Click += BtnScan_Click;

        btnDelete = new Button
        {
            Text = "🗑️ Seçili Tehdidi Sil",
            Location = new Point(205, 85),
            Size = new Size(195, 35),
            BackColor = Color.FromArgb(255, 220, 200),
            Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold),
            Enabled = false
        };
        btnDelete.Click += BtnDelete_Click;

        // Tarama sonuçları için ListView
        lvResults = new ListView
        {
            Location = new Point(15, 130),
            Size = new Size(385, 200),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            MultiSelect = false
        };
        lvResults.Columns.Add("Dosya Adı", 120);
        lvResults.Columns.Add("Dosya Yolu", 150);
        lvResults.Columns.Add("Durum", 100);
        lvResults.SelectedIndexChanged += LvResults_SelectedIndexChanged;

        grpAntivirus.Controls.AddRange(new Control[] { 
            lblAntivirusFolder, txtAntivirusFolder, btnSelectAntivirusFolder, 
            btnScan, btnDelete, lvResults 
        });

        // ========================================
        // ALT PANEL - LOG
        // ========================================
        grpLog = new GroupBox
        {
            Text = "📋 İşlem Günlüğü",
            Location = new Point(10, 370),
            Size = new Size(860, 230),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        txtLog = new TextBox
        {
            Location = new Point(15, 25),
            Size = new Size(830, 190),
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical,
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.LightGreen,
            Font = new Font("Consolas", 9)
        };

        grpLog.Controls.Add(txtLog);

        // Tüm kontrolleri forma ekle
        this.Controls.AddRange(new Control[] { grpVirus, grpAntivirus, grpLog });

        // Başlangıç log mesajı
        Log("Uygulama başlatıldı. Bu bir eğitim amaçlı simülasyondur.");
        Log("İmza tabanlı antivirüs mantığını göstermektedir.");
        Log("═══════════════════════════════════════════════════════════");
    }

    /// <summary>
    /// Log alanına zaman damgalı mesaj yazar
    /// </summary>
    private void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        txtLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
    }

    /// <summary>
    /// Virüs simülasyonu için klasör seçimi
    /// </summary>
    private void BtnSelectVirusFolder_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Virüs simülasyonu için hedef klasör seçin",
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtVirusFolder.Text = dialog.SelectedPath;
            btnInfect.Enabled = true;
            Log($"Virüs hedef klasörü seçildi: {dialog.SelectedPath}");
        }
    }

    /// <summary>
    /// Antivirüs taraması için klasör seçimi
    /// </summary>
    private void BtnSelectAntivirusFolder_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Taranacak klasörü seçin",
            ShowNewFolderButton = false
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtAntivirusFolder.Text = dialog.SelectedPath;
            btnScan.Enabled = true;
            Log($"Tarama klasörü seçildi: {dialog.SelectedPath}");
        }
    }

    /// <summary>
    /// Virüs simülasyonu - Zararsız bir dosya oluşturur
    /// </summary>
    private void BtnInfect_Click(object? sender, EventArgs e)
    {
        var (success, message, filePath) = _virusSimulator.CreateFakeVirus(txtVirusFolder.Text);

        if (!success)
        {
            Log($"⚠️ {message}");
            MessageBox.Show(
                message,
                "Bilgi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            return;
        }

        Log($"✅ Simüle edilmiş virüs dosyası oluşturuldu: {filePath}");
        Log($"   İmza: {_virusSimulator.VirusSignature}");

        MessageBox.Show(
            $"'{_virusSimulator.VirusFileName}' dosyası başarıyla oluşturuldu!\n\n" +
            "Şimdi antivirüs panelinden bu klasörü tarayabilirsiniz.",
            "Başarılı",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    /// <summary>
    /// Antivirüs taraması - İmza tabanlı tespit
    /// Tüm .txt dosyalarını kontrol eder ve virüs imzası arar
    /// </summary>
    private void BtnScan_Click(object? sender, EventArgs e)
    {
        // Önceki sonuçları temizle
        lvResults.Items.Clear();
        btnDelete.Enabled = false;

        string scanPath = txtAntivirusFolder.Text;
        Log($"🔍 Tarama başlatılıyor: {scanPath}");

        try
        {
            // Servis ile tarama yap
            var results = _antivirusScanner.ScanFolder(scanPath);

            if (results.Count == 0)
            {
                Log("   Hiçbir .txt dosyası bulunamadı.");
                MessageBox.Show(
                    "Bu klasörde taranacak .txt dosyası bulunamadı.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            int threatCount = 0;
            int cleanCount = 0;

            foreach (var result in results)
            {
                // Sonucu ListView'a ekle
                var item = new ListViewItem(result.FileName);
                item.SubItems.Add(result.FilePath);

                if (result.IsThreat)
                {
                    item.SubItems.Add("🚨 Tehdit Bulundu!");
                    item.BackColor = Color.FromArgb(255, 200, 200);
                    item.ForeColor = Color.DarkRed;
                    threatCount++;
                    Log($"   🚨 TEHDİT: {result.FileName}");
                }
                else
                {
                    item.SubItems.Add("✅ Temiz");
                    item.BackColor = Color.FromArgb(200, 255, 200);
                    item.ForeColor = Color.DarkGreen;
                    cleanCount++;
                    Log($"   ✅ Temiz: {result.FileName}");
                }

                // Tag'a tehdit durumunu kaydet (silme işlemi için)
                item.Tag = result.IsThreat;
                lvResults.Items.Add(item);
            }

            Log($"═══════════════════════════════════════════════════════════");
            Log($"📊 Tarama tamamlandı: {results.Count} dosya tarandı");
            Log($"   🚨 Tehdit: {threatCount} | ✅ Temiz: {cleanCount}");

            string resultMessage = threatCount > 0
                ? $"Tarama tamamlandı!\n\n" +
                  $"Taranan: {results.Count} dosya\n" +
                  $"Tehdit: {threatCount}\n" +
                  $"Temiz: {cleanCount}\n\n" +
                  "Tehditleri silmek için listeden seçip 'Sil' butonuna tıklayın."
                : $"Tarama tamamlandı!\n\n" +
                  $"Taranan: {results.Count} dosya\n" +
                  "Hiçbir tehdit bulunamadı! ✅";

            MessageBox.Show(
                resultMessage,
                "Tarama Sonucu",
                MessageBoxButtons.OK,
                threatCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            Log($"❌ Tarama hatası: {ex.Message}");
            MessageBox.Show(
                $"Tarama sırasında hata: {ex.Message}",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    /// <summary>
    /// ListView'da seçim değiştiğinde tetiklenir
    /// Tehdit seçiliyse silme butonunu aktif eder
    /// </summary>
    private void LvResults_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lvResults.SelectedItems.Count > 0)
        {
            var selectedItem = lvResults.SelectedItems[0];
            bool isThreat = selectedItem.Tag is bool threat && threat;
            btnDelete.Enabled = isThreat;
        }
        else
        {
            btnDelete.Enabled = false;
        }
    }

    /// <summary>
    /// Seçili tehdit dosyasını siler
    /// </summary>
    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (lvResults.SelectedItems.Count == 0) return;

        var selectedItem = lvResults.SelectedItems[0];
        string filePath = selectedItem.SubItems[1].Text;
        string fileName = selectedItem.SubItems[0].Text;

        // Silme onayı al
        var result = MessageBox.Show(
            $"'{fileName}' dosyasını kalıcı olarak silmek istediğinizden emin misiniz?\n\n" +
            $"Yol: {filePath}",
            "Silme Onayı",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (result == DialogResult.Yes)
        {
            try
            {
                File.Delete(filePath);
                Log($"🗑️ Tehdit silindi: {filePath}");

                // Listeden kaldır
                lvResults.Items.Remove(selectedItem);
                btnDelete.Enabled = false;

                MessageBox.Show(
                    $"'{fileName}' başarıyla silindi!",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                Log($"❌ Silme hatası: {ex.Message}");
                MessageBox.Show(
                    $"Dosya silinirken hata: {ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
