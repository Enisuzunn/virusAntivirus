# 🦠 Virüs & Antivirüs Simülasyonu

> **⚠️ EĞİTİM AMAÇLIDIR** - Bu uygulama gerçek bir virüs veya antivirüs yazılımı değildir.

## 📋 Proje Hakkında

Bu proje, **imza tabanlı antivirüs mantığını** öğretmek amacıyla geliştirilmiş bir Windows Forms uygulamasıdır. Antivirüs yazılımlarının temel çalışma prensiplerini görsel olarak gösterir.

### Özellikler

- 🦠 **Virüs Simülasyonu**: Zararsız bir metin dosyası oluşturarak "virüs bulaşmasını" simüle eder
- 🛡️ **Antivirüs Tarayıcı**: İmza tabanlı tespit yöntemiyle dosyaları tarar
- 🗑️ **Tehdit Temizleme**: Tespit edilen tehditleri silme imkanı
- 📋 **İşlem Günlüğü**: Tüm işlemleri zaman damgalı olarak kaydeder

## 🏗️ Proje Yapısı

```
virusAntivirus/
├── virusAntivirus.sln              # Solution dosyası
└── virusAntivirus/
    ├── Program.cs                  # Uygulama giriş noktası
    ├── MainForm.cs                 # Ana form (UI ve event handler'lar)
    ├── virusAntivirus.csproj       # Proje dosyası
    │
    ├── Models/                     # Veri modelleri
    │   └── ScanResult.cs           # Tarama sonucu modeli
    │
    └── Services/                   # İş mantığı servisleri
        ├── VirusSimulator.cs       # Virüs simülasyon servisi
        └── AntivirusScanner.cs     # Antivirüs tarama servisi
```

## 🔧 Gereksinimler

- **.NET 9.0** veya üzeri
- **Windows** işletim sistemi (Windows Forms kullanıldığı için)

## 🚀 Çalıştırma

```bash
# Projeyi derle
dotnet build

# Uygulamayı çalıştır
dotnet run --project virusAntivirus
```

## 📖 Nasıl Kullanılır?

### 1. Virüs Simülasyonu
1. Sol panelden "Gözat" butonuyla hedef klasörü seçin
2. "🦠 Bulaştır" butonuna tıklayın
3. `fake_virus.txt` adında zararsız bir dosya oluşturulacak

### 2. Antivirüs Taraması
1. Sağ panelden "Gözat" butonuyla taranacak klasörü seçin
2. "🔍 Tara" butonuna tıklayın
3. Sonuçlar listede gösterilecek:
   - 🚨 **Kırmızı**: Tehdit tespit edildi
   - ✅ **Yeşil**: Dosya temiz

### 3. Tehdit Temizleme
1. Listeden tehditli dosyayı seçin
2. "🗑️ Seçili Tehdidi Sil" butonuna tıklayın
3. Onay verdikten sonra dosya silinecek

## 🧠 İmza Tabanlı Tespit Mantığı

```
┌─────────────────────────────────────────────────────────┐
│                     TARAMA SÜRECİ                       │
├─────────────────────────────────────────────────────────┤
│                                                         │
│   Dosya İçeriği          Virüs İmzası                  │
│   ┌───────────┐          ┌───────────┐                 │
│   │  ...      │          │ SIMULATED │                 │
│   │  SIMULATED│  ═══►    │ _VIRUS_   │  ═══► EŞLEŞTİ! │
│   │  _VIRUS_  │          │ SIGNATURE │       🚨        │
│   │  SIGNATURE│          └───────────┘                 │
│   │  ...      │                                        │
│   └───────────┘                                        │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

Bu yöntem, antivirüs yazılımlarının temel çalışma prensibidir:
1. Bilinen zararlı yazılımların "imzaları" (benzersiz kod parçaları) veritabanında tutulur
2. Tarama sırasında dosya içeriği bu imzalarla karşılaştırılır
3. Eşleşme bulunursa tehdit tespit edilmiş olur

## 📁 Mimari Açıklama

### Models
- **ScanResult**: Tarama sonuçlarını tutan veri transfer nesnesi (DTO)

### Services
- **VirusSimulator**: Virüs dosyası oluşturma mantığını içerir
- **AntivirusScanner**: İmza tabanlı tarama ve tehdit silme mantığını içerir

### Forms
- **MainForm**: Kullanıcı arayüzü ve kullanıcı etkileşimlerini yönetir

## ⚠️ Uyarı

Bu uygulama **tamamen eğitim amaçlıdır**:
- Gerçek bir virüs içermez
- Sisteminize zarar vermez
- Sadece belirttiğiniz klasörde `.txt` dosyası oluşturur
- Antivirüs yazılımlarının temel mantığını gösterir