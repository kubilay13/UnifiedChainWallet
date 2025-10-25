using NBitcoin;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class BtcMainWalletService
    {
        // Burada ana Bitcoin ağı (gerçek ağ) seçiyoruz.
        private readonly NBitcoin.Network _network = NBitcoin.Network.Main;

        // MAİN LEGACY ADRES(P2PKH) — Eski tip, 1 ile başlar
        public WalletModel CreateBtcLegacyWallet()
        {
            // Rastgele bir private key oluştur
            var key = new Key();

            // Private keyi WIF formatına çeviriyoruz çünkü Bitcoin cüzdanları private keyi WIF formatında kullanır
            var wif = key.GetWif(_network).ToWif();

            // 1 ile başlayan klasik Bitcoin adresi üret
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();
            
            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC MAIN WALLET Legacy (P2PKH)"
            };
        }

        // MAİN NESTED SEGWIT (P2SH-P2WPKH) — 3 ile başlar
        public WalletModel CreateBtcNestedSegWitWallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // 3 ile başlayan P2SH tipi adres oluştur
            var address = key.PubKey.GetAddress(ScriptPubKeyType.SegwitP2SH, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC MAIN WALLET Nested SegWit (P2SH-P2WPKH)"
            };
        }

        // MAİN NATIVE SEGWIT (BECH32) — bc1q ile başlar
        public WalletModel CreateBtcBech32Wallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();
            // bc1q ile başlayan Bech32 tipi adres oluştur
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Segwit, _network).ToString();
            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC MAIN WALLET Native SegWit (Bech32)"
            };
        }

        // MAİN HD WALLET (MNEMONIC SEED) — BIP44/49/84
        public WalletModel CreateBtcHdWallet(string bip = "84")
        {
            // 12 kelimelik rastgele bir mnemonic oluştur
            var mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);

            // Seed’den HD root key oluştur
            var hdRoot = new ExtKey();

            // BIP44 = Legacy, BIP49 = Nested SegWit, BIP84 = Native SegWit
            // Mainnet için coin_type 0 (Testnet'te 1’di)
            string path = bip switch
            {
                "44" => "m/44'/0'/0'/0/0",   // BIP44 Legacy
                "49" => "m/49'/0'/0'/0/0",   // BIP49 Nested SegWit
                "84" => "m/84'/0'/0'/0/0",   // BIP84 Native SegWit
                _ => throw new ArgumentException("Unsupported BIP type")
            };

            // Belirtilen path’e göre child key türet
            var childKey = hdRoot.Derive(new KeyPath(path));


            var wif = childKey.PrivateKey.GetWif(_network).ToWif();

            // Adres tipine göre adres oluştur
            ScriptPubKeyType addressType = bip switch
            {
                "44" => ScriptPubKeyType.Legacy,
                "49" => ScriptPubKeyType.SegwitP2SH,
                "84" => ScriptPubKeyType.Segwit,
                _ => throw new ArgumentException("Unsupported BIP type")
            };

            var address = childKey.PrivateKey.PubKey.GetAddress(addressType, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Mnemonic = mnemo.ToString(),
                Network = $"BTC MAIN WALLET HD (BIP{bip})"
            };
        }

        // ------------------------------------- Bitcoin Adres Tipleri Açıklamaları:-------------------------------------------
        // Legacy (P2PKH) : En eski adres tipi Her cüzdanla uyumlu ama yüksek işlem ücreti alır.

        // P2SH-P2WPKH (Nested SegWit) : Eski-yeni sistemlerle uyumlu ara çözüm.

        // Bech32 (Native SegWit) : En modern format ve düşük işlem ücreti alır.

        // HD (BIP44/49/84) : Birden fazla adres türetebilen seed sistemidir ve Tek yedek (12 kelime) ile sınırsız adres.
    }
}
