using Solnet.Wallet;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class SolWalletService
    {
        // Solana Cüzdan Oluşturma
        public WalletModel SolCreateWallet()
        {
            string Network = "SOLANA";
            var account = new Account();
            var address = account.PublicKey;
            var privateKeyBytes = account.PrivateKey;

            // Base58 encoder örneği:
            var encoder = new Solnet.Wallet.Utilities.Base58Encoder();
            var privateKeyBase58 = encoder.EncodeData(privateKeyBytes);

            return new WalletModel
            {
                Address = address,
                PrivateKey = privateKeyBytes,
                Network = Network
            };
        }
    }
}
