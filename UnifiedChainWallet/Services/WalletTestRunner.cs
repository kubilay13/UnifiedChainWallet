using UnifiedChainWallet.Models;
using UnifiedChainWallet.Services.WalletService;

namespace UnifiedChainWallet.Services
{
    public class WalletTestRunner
    {
        public void RunAllTests()
        {
            Console.WriteLine("🚀 Başlatılıyor: Tüm Ağ Cüzdan Testleri\n");
            // BTC Mainnet
            var btcMain = new BtcMainWalletService();
            PrintWallet(btcMain.CreateBtcLegacyWallet());
            PrintWallet(btcMain.CreateBtcNestedSegWitWallet());
            PrintWallet(btcMain.CreateBtcBech32Wallet());
            PrintWallet(btcMain.CreateBtcHdWallet("84"));

            // BTC Testnet
            var btcTest = new BtcTestnetWalletService();
            PrintWallet(btcTest.CreateTestBtcLegacyWallet());
            PrintWallet(btcTest.CreateTestBtcNestedSegWitWallet());
            PrintWallet(btcTest.CreateTestBtcBech32Wallet());
            PrintWallet(btcTest.CreateTestBtcHdWallet("84"));

            // LTC Mainnet
            var ltcMain = new LtcMainWalletService();
            PrintWallet(ltcMain.CreateLtcLegacyWallet());
            PrintWallet(ltcMain.CreateLtcNestedSegWitWallet());
            PrintWallet(ltcMain.CreateLtcBech32Wallet());
            PrintWallet(ltcMain.CreateLtcHdWallet("84"));

            // LTC Testnet
            var ltcTest = new LtcTestnetWalletService();
            PrintWallet(ltcTest.CreateTestLtcLegacyWallet());
            PrintWallet(ltcTest.CreateTestLtcNestedSegWitWallet());
            PrintWallet(ltcTest.CreateTestLtcBech32Wallet());
            PrintWallet(ltcTest.CreateTestLtcHdWallet("84"));

            // Doge Mainnet
            var dogeMain = new DogeMainWalletService();
            PrintWallet(dogeMain.CreateDogeLegacyWallet());
            PrintWallet(dogeMain.CreateDogeHdWallet("84"));

            // Doge Testnet
            var dogeTest = new DogeTestnetWalletService();
            PrintWallet(dogeTest.CreateTestDogeLegacyWallet());
            PrintWallet(dogeTest.CreateTestDogeHdWallet("84"));


            // EVM Ağları örnek
            var evm = new EvmWalletService();
            PrintWallet(evm.EvmCreateWallet("Ethereum"));
            PrintWallet(evm.EvmCreateWallet("Binance Smart Chain"));
            PrintWallet(evm.EvmCreateWallet("Pepe"));
            PrintWallet(evm.EvmCreateWallet("Arbitrium"));
            PrintWallet(evm.EvmCreateWallet("Bttc"));
            PrintWallet(evm.EvmCreateWallet("Polygon"));


            // TRON
            var tron = new TronWalletService();
            PrintWallet(tron.TronCreateWallet());

            // SOL
            var sol = new SolWalletService();
            PrintWallet(sol.SolCreateWallet());

            Console.WriteLine("\n✅ Tüm cüzdan testleri tamamlandı!");
        }

        private void PrintWallet(WalletModel wallet)
        {
            Console.WriteLine($"Ağ: {wallet.Network}");
            Console.WriteLine($"Adres: {wallet.Address}");
            Console.WriteLine($"Private Key: {wallet.PrivateKey}");
            if (!string.IsNullOrEmpty(wallet.Mnemonic))
                Console.WriteLine($"Mnemonic: {wallet.Mnemonic}");
            Console.WriteLine("--------------------------------------\n");
        }
    }
}

