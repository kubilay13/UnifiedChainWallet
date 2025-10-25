using NBitcoin;
using NBitcoin.Altcoins;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class DogeMainWalletService
    {
        // Dogecoin ana ağı (gerçek ağ)
        private readonly Network _network = Dogecoin.Instance.Mainnet;

        // MAİN LEGACY ADRES (P2PKH) — D ile başlar
        public WalletModel CreateDogeLegacyWallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,                 // Örn: D8L9K...
                PrivateKey = wif,
                Network = "DOGECOİN MAİNNET Legacy (P2PKH)"
            };
        }

        // MAİN HD WALLET (MNEMONIC SEED) — BIP44/49/84
        public WalletModel CreateDogeHdWallet(string bip = "84")
        {
            var mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            var master = mnemonic.DeriveExtKey();

            // Dogecoin coin_type = 3 (BIP44)
            string path = "44'/3'/0'/0/0";

            var key = master.Derive(new KeyPath(path)).PrivateKey;
            var wif = key.GetWif(_network).ToWif();

            // Legacy adres tipini kullan
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Mnemonic = mnemonic.ToString(),
                Network = "DOGECOİN MAINNET HD Wallet (BIP44 Legacy)"
            };
        }
    }
}
