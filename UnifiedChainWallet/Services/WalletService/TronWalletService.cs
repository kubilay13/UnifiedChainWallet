using TronAksaSharp.Wallet;
using TronNet;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class TronWalletService
    {
        // Tron Cüzdan Oluşturma
        public WalletModel TronCreateWallet()
        {
            string Network = "TRON";
            var key = TronECKey.GenerateKey(TronNetwork.MainNet);
            var address = key.GetPublicAddress();
            var privateKey = key.GetPrivateKey();

            return new WalletModel
            {
                Address = address,
                PrivateKey = privateKey,
                Network = Network
            };
        }
    }
}
