using UnifiedChainWallet.Services;

class Program
{
    static void Main()
    {
        var runner = new WalletTestRunner();
        runner.RunAllTests();
        Console.ReadLine();
    }
}
