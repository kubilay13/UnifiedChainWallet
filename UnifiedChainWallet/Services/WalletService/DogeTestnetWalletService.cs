using NBitcoin;
using NBitcoin.Altcoins;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class DogeTestnetWalletService
    {
        // Testnet ağı — sahte coinlerle test içindir
        private readonly Network _network = Dogecoin.Instance.Testnet;

        // TEST LEGACY ADRES (P2PKH) — n veya m ile başlar
        public WalletModel CreateTestDogeLegacyWallet()
        {
            var key = new Key();
            var wif = key.GetWif(_network).ToWif();
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Network = "DOGECOİN TESTNET Legacy (P2PKH)"
            };
        }

        // TEST HD WALLET (MNEMONIC SEED)
        public WalletModel CreateTestDogeHdWallet(string bip = "84")
        {
            var mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            var master = mnemonic.DeriveExtKey();

            // Dogecoin testnet coin_type = 1 (BIP44)
            string path = "44'/1'/0'/0/0";

            var key = master.Derive(new KeyPath(path)).PrivateKey;
            var wif = key.GetWif(_network).ToWif();

            // Legacy adres tipi kullan
            var address = key.PubKey.GetAddress(ScriptPubKeyType.Legacy, _network).ToString();

            return new WalletModel
            {
                Address = address,
                PrivateKey = wif,
                Mnemonic = mnemonic.ToString(),
                Network = "DOGECOİN TESTNET HD Wallet (BIP44 Legacy)"
            };
        }
    }
}
