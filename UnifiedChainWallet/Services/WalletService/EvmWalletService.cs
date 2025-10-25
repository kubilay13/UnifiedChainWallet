using Nethereum.Signer;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class EvmWalletService
    {
        // Genel EVM cüzdan oluşturma
        public WalletModel EvmCreateWallet(string network)
        {
            // Key oluştur
            var key = EthECKey.GenerateKey();
            var privateKey = key.GetPrivateKey();
            var address = key.GetPublicAddress();

            return new WalletModel
            {
                Address = address,
                PrivateKey = privateKey,
                Network = network
            };
        }

        // -----------------------------------------Kullanım örneği:------------------------------------------

        //var evmservice = new CreateEvmWallet();

        //var ethWallet = evmservice.EvmCreateWallet("Ethereum");
        //var polygonWallet = evmservice.EvmCreateWallet("Polygon");
        //var bscWallet = evmservice.EvmCreateWallet("BSC");
        //var avalancheWallet = evmservice.EvmCreateWallet("Avalanche");
        //var arbitrumWallet = evmservice.EvmCreateWallet("Arbitrum");
        //var optimismWallet = evmservice.EvmCreateWallet("Optimism");
    }
}
