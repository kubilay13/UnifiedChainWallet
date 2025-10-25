using NBitcoin;
using NBitcoin.Altcoins;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class LtcTestnetWalletService
    {
        // Testnet ağı — Gerçek coin içermez, deneme amaçlıdır.
        private readonly Network _network = Litecoin.Instance.Testnet;

        // TEST LEGACY ADRES (P2PKH) — m veya n ile başlar
        public WalletModel CreateTestLtcLegacyWallet()
        {
            var key = new Key();  // Rastgele anahtar
            var wif = key.GetWif(_network).ToWif();  // WIF formatına çevir
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,                 // Örn: "mZp5..." gibi
                PrivateKey = wif,               // Private key (WIF)
                Network = "LTC TEST WALLET Legacy (P2PKH)"
            };
        }

        // TEST NESTED SEGWIT (P2SH-P2WPKH) — Q ile başlar
        public WalletModel CreateTestLtcNestedSegWitWallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // SegWit uyumlu adres (P2SH tipi)
            var address = key.PubKey.GetAddress(ScriptPubKeyType.SegwitP2SH, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "LTC TEST WALLET Nested SegWit (P2SH-P2WPKH)"
            };
        }

        // TEST NATIVE SEGWIT (BECH32) — tltc1q ile başlar

        public WalletModel CreateTestLtcBech32Wallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // Bech32 adres (tltc1q...) üret
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Segwit, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "LTC TEST WALLET Bech32 (P2WPKH)"
            };
        }

        // TEST HD WALLET (MNEMONIC SEED) — BIP44/49/84
        public WalletModel CreateTestLtcHdWallet(string bip = "84")
        {
            // Rastgele 12 kelimelik mnemonic oluştur
            var mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            var master = mnemonic.DeriveExtKey();

            // Litecoin Testnet coin_type = 1 (mainnet’te 2 idi)
            string path = bip switch
            {
                "44" => "44'/1'/0'/0/0",
                "49" => "49'/1'/0'/0/0",
                _ => "84'/1'/0'/0/0"
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
                Network= $"LTC TEST WALLET HD (BIP{bip})"
            };
        }


        // ------------------------------------- LİTECOİN ADRES Tipleri Açıklamaları:-------------------------------------------
        // Legacy (P2PKH) — L ile başlar, en eski adres tipi, uyumluluk için kullanılır.
        // Nested SegWit (P2SH-P2WPKH) — M ile başlar, SegWit özelliklerini destekler, eski sistemlerle uyumludur.
        // Native SegWit (Bech32) — ltc1q ile başlar, en yeni adres tipi, daha düşük işlem ücretleri sunar.
        // HD Wallet (BIP44/49/84) — Birden fazla adres türetmek için kullanılır, tek seed (12 kelime) ile sınırsız adres oluşturulabilir.
    }
}
