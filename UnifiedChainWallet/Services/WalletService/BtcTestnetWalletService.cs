using NBitcoin;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class BtcTestnetWalletService
    {
        // Bu değişken sadece Testnet üzerinde işlemi sağlar.

        private readonly Network _network = Network.TestNet;

        // TEST BTC LEGACY ADRES OLUŞTURMA (P2PKH)
        public WalletModel CreateTestBtcLegacyWallet()
        {
            //yeni private key oluşturma Nbitcoin kütüphanesi ile
            var key = new Key();
            //private keyi WIF formatına çeviriyoruz çünkü Bitcoin cüzdanları private keyi WIF formatında kullanır
            var wif = key.GetWif(_network).ToWif();
            // Public key’den Legacy adresi oluşturuyoruz (1... / m... ile başlar)
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC TEST WALLET Legacy (P2PKH)"
            };

        }


        // TEST NESTED SEGWIT ADRESİ (P2SH-P2WPKH)
        public WalletModel CreateTestBtcNestedSegWitWallet()
        {
            var key = new Key();

            var wif = key.GetWif(_network).ToWif();

            // P2SH (3... / 2...) tipinde adres üret
            // Bu tür adresler hem eski hem yeni sistemlerle uyumludur
            var address = key.PubKey.GetAddress(ScriptPubKeyType.SegwitP2SH, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC TEST WALLET Nested SegWit (P2SH-P2WPKH)"
            };
        }


        // TEST NATIVE SEGWIT ADRESİ (BECH32)
        public WalletModel CreateTestBtcBech32Wallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();

            // Bech32 tipi (tb1q...) adres üret
            // Bu tür adresler daha az fee (işlem ücreti) öder.
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Segwit, _network).ToString();
            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "BTC TEST WALLET Bech32 (Native SegWit)"
            };
        }

        // TEST HD CÜZDAN (Mnemonic Seed ile) - BIP44 / BIP49 / BIP84
        public WalletModel CreateTestBtcHdWallet(string bip = "84")
        {
            // 12 kelimelik rastgele bir mnemonic oluştur (örnek: "apple bird moon ...")
            var mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);

            // Bu kelimelerden master private key türet
            var master = mnemonic.DeriveExtKey();

            // BIP standardına göre hangi adres tipini istiyorsak yol belirlenir:
            // BIP44 = Legacy, BIP49 = P2SH, BIP84 = Bech32
            string path = bip switch
            {
                "44" => "44'/1'/0'/0/0", // Testnet için coin_type 1 (Mainnet’te 0 olur)
                "49" => "49'/1'/0'/0/0",
                _ => "84'/1'/0'/0/0"
            };

            // Yukarıdaki yoldan private key türet
            var key = master.Derive(new KeyPath(path)).PrivateKey;

            // Private key’i WIF’e çevir
            var wif = key.GetWif(_network).ToWif();

            // Hangi Script tipi kullanılacak?
            ScriptPubKeyType spk = bip switch
            {
                "44" => ScriptPubKeyType.Legacy,
                "49" => ScriptPubKeyType.SegwitP2SH,
                _ => ScriptPubKeyType.Segwit
            };

            // Adresi oluştur
            var address = key.PubKey.GetAddress(spk, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Mnemonic = mnemonic.ToString(),
                Network = $"BTC TEST WALLET HD (BIP{bip})"
            };
        }


        // ------------------------------------- Bitcoin Adres Tipleri Açıklamaları:-------------------------------------------
        // Legacy (P2PKH) : En eski adres tipi Her cüzdanla uyumlu ama yüksek işlem ücreti alır.

        // P2SH-P2WPKH (Nested SegWit) : Eski-yeni sistemlerle uyumlu ara çözüm.

        // Bech32 (Native SegWit) : En modern format ve düşük işlem ücreti alır.

        // HD (BIP44/49/84) : Birden fazla adres türetebilen seed sistemidir ve Tek yedek (12 kelime) ile sınırsız adres.

    }
}
