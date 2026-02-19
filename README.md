

<img width="1024" height="1024" alt="Unified Chain Wallet" src="https://github.com/user-attachments/assets/3475330e-0d30-48e8-8af7-136fcdd809b0" />


[![NuGet Version](https://img.shields.io/nuget/v/UnifiedChainWallet.svg?style=flat&color=blue)](https://www.nuget.org/packages/UnifiedChainWallet)  
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)  
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blueviolet.svg)](https://dotnet.microsoft.com/)  


# 🪙 UnifiedChainWallet
```bash
NUGET: https://www.nuget.org/packages/UnifiedChainWallet
```
**UnifiedChainWallet**, birden fazla blockchain ağı (Bitcoin, Litecoin, Dogecoin, Ethereum, Tron, Solana vb.) için **tek çatı altında cüzdan oluşturma** imkanı sunan bir C# kütüphanesidir.  
NuGet üzerinden kolayca yüklenebilir ve geliştiricilere hızlı cüzdan üretimi sağlar.

---

## 🚀 Özellikler

## Desteklenen Ağlar ve Cüzdan Tipleri

| Ağ | Tipler | Mainnet / Testnet |
|----|--------|-----------------|
| Bitcoin (BTC) | Legacy, Nested SegWit, Bech32, HD Wallet (BIP84) | Mainnet & Testnet |
| Litecoin (LTC) | Legacy, Nested SegWit, Bech32, HD Wallet (BIP84) | Mainnet & Testnet |
| Dogecoin (DOGE) | Legacy, HD Wallet | Mainnet & Testnet |
| Ethereum ve EVM Tabanlı Ağlar | Standart HD Wallet | Ethereum, BSC, Polygon, Arbitrium, Pepe, Bttc |
| TRON | Standart + Yerli Kütüphane | Mainnet & Testnet |
| Solana (SOL) | Standart HD Wallet | Mainnet |

---

NuGet:https://www.nuget.org/packages/UnifiedChainWallet

```bash
using UnifiedChainWallet.Services.WalletService;

// Bitcoin Mainnet örneği
var btcMain = new BtcMainWalletService();

var legacy = btcMain.CreateBtcLegacyWallet();
var nested = btcMain.CreateBtcNestedSegWitWallet();
var bech32 = btcMain.CreateBtcBech32Wallet();
var hd = btcMain.CreateBtcHdWallet("84");

Console.WriteLine($"Legacy: {legacy.Address}");
Console.WriteLine($"Nested SegWit: {nested.Address}");
Console.WriteLine($"Bech32: {bech32.Address}");
Console.WriteLine($"HD Wallet: {hd.Address}, Mnemonic: {hd.Mnemonic}");

// EVM örneği (Ethereum, BSC, Polygon vb.)
var evm = new EvmWalletService();
var ethWallet = evm.EvmCreateWallet("Ethereum");

// Tron örneği
var tron = new TronWalletService();
var tronWallet = tron.TronCreateWallet();

// tron yerli kütüphane örneği
var tronNative = new TronCreateWalletNativeLibrary();
PrintWallet(tronNative.TronCreateWalletNative());

// Solana örneği
var sol = new SolWalletService();
var solWallet = sol.SolCreateWallet();
```


## 🧩 Kullanılan Kütüphaneler

```bash

NBitcoin

Nethereum

Solnet.Wallet

TronAksaSharp

TronNet

```


## 🧪 Test Aracı

Aşağıdaki örnek kod tüm desteklenen ağlar için test amaçlı cüzdan oluşturur:

```bash
using UnifiedChainWallet.Services;

class Program
{
    static void Main()
    {
        var runner = new WalletTestRunner();
        runner.RunAllTests();
        Console.ReadLine();
    }
}
```

## 🧾 Örnek Konsol Çıktısı
```bash

🚀 Başlatılıyor: Tüm Ağ Cüzdan Testleri

Ağ: BTC MAIN WALLET Native SegWit (Bech32)
Adres: bc1q3d4...
Private Key: L2X9Q...

Ağ: ETHEREUM
Adres: 0xABCD...
Private Key: 0x74a3...
--------------------------------------

✅ Tüm cüzdan testleri tamamlandı!
```

## 📄 LICENSE !!!!
```bash
MIT License

Copyright (c) 2026 Kubilay13

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
