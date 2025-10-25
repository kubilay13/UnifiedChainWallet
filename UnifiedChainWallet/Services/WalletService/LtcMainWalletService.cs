using NBitcoin;
using NBitcoin.Altcoins;
using UnifiedChainWallet.Models;  // Litecoin ağına erişmek için

namespace UnifiedChainWallet.Services.WalletService
{
    public class LtcMainWalletService
    {
        // Litecoin ana ağı (Mainnet)
        private readonly Network _network = Litecoin.Instance.Mainnet;

        // MAİN LEGACY ADRES (P2PKH) — L ile başlar
        public WalletModel CreateLtcLegacyWallet()
        {
            var key = new Key();  // Rastgele anahtar üret
            var wif = key.GetWif(_network).ToWif(); // Private key'i WIF formatına çevir
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,                 
                PrivateKey = wif, 
                Network = "LTC MAIN WALLET Legacy (P2PKH)"
            };
        }

        // MAİN NESTED SEGWIT (P2SH-P2WPKH) — M ile başlar
        public WalletModel CreateLtcNestedSegWitWallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // P2SH adres üret (M ile başlar)
            var address = key.PubKey.GetAddress(ScriptPubKeyType.SegwitP2SH, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "LTC MAIN WALLET Nested SegWit (P2SH-P2WPKH)"
            };
        }

        // MAİN NATIVE SEGWIT (BECH32) — ltc1q ile başlar
        public WalletModel CreateLtcBech32Wallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // ltc1q... formatında Bech32 adres üret
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Segwit, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "LTC MAIN WALLET Native SegWit (Bech32)"
            };
        }

        // MAİN HD WALLET (MNEMONIC SEED) — BIP44/49/84
        public WalletModel CreateLtcHdWallet(string bip = "84")
        {
            // 12 kelimelik seed oluştur
            var mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            var master = mnemonic.DeriveExtKey();

            // Litecoin için coin_type: 2 (BIP44 standardına göre)
            string path = bip switch
            {
                "44" => "44'/2'/0'/0/0",
                "49" => "49'/2'/0'/0/0",
                _ => "84'/2'/0'/0/0"
            };

            var key = master.Derive(new KeyPath(path)).PrivateKey;
            var wif = key.GetWif(_network).ToWif();

            ScriptPubKeyType spk = bip switch
            {
                "44" => ScriptPubKeyType.Legacy,
                "49" => ScriptPubKeyType.SegwitP2SH,
                _ => ScriptPubKeyType.Segwit
            };

            var address = key.PubKey.GetAddress(spk, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Mnemonic = mnemonic.ToString(),
                Network = $"LTC MAIN WALLET HD (BIP{bip})"
            };
        }


        // ------------------------------------- LİTECOİN ADRES Tipleri Açıklamaları:-------------------------------------------
        // Legacy (P2PKH) — L ile başlar, en eski adres tipi, uyumluluk için kullanılır.
        // Nested SegWit (P2SH-P2WPKH) — M ile başlar, SegWit özelliklerini destekler, eski sistemlerle uyumludur.
        // Native SegWit (Bech32) — ltc1q ile başlar, en yeni adres tipi, daha düşük işlem ücretleri sunar.
        // HD Wallet (BIP44/49/84) — Birden fazla adres türetmek için kullanılır, tek seed (12 kelime) ile sınırsız adres oluşturulabilir.
    }
}
