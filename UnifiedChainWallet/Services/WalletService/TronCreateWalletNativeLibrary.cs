using Nethereum.Hex.HexConvertors.Extensions;
using TronAksaSharp.Wallet;
using UnifiedChainWallet.Models;

namespace UnifiedChainWallet.Services.WalletService
{
    public class TronCreateWalletNativeLibrary
    {
        //Yerli Kütüphane kullanarak Tron cüzdan oluşturma örneği
        public WalletModel TronCreateWalletNative()
        {
            string Network = "TRON YERLİ KÜTÜPHANE TARAFINDAN ÜRETİLEN CÜZDAN";
            var createWallet = TronClient.CreateTronWallet();
            return new WalletModel
            {
                Address = createWallet.Address,
                PrivateKey = createWallet.PrivateKey.ToHex(),
                Network = Network
            };
        }
    }
}
