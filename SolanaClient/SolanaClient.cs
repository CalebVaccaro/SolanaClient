using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Wallet;

namespace Solana;

public class SolanaClient(string privateKey, string rpcUrl)
{
    private readonly IRpcClient _rpcClient = ClientFactory.GetClient(rpcUrl);
    private readonly Wallet _wallet = new Wallet(privateKey);
    
    public async Task<string> Buy(decimal amount, string wsolMint, string customTokenMint, int tokenDenomination)
    {
        Console.WriteLine($"Buying {amount} of {customTokenMint} using {wsolMint}...");
        
        // Fetch WSOL account
        var wsolAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(_wallet.Account, new PublicKey(wsolMint));
        
        // Fetch custom token account
        var customTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(_wallet.Account, new PublicKey(customTokenMint));

        // Perform token swap via Serum, Raydium, or other DEX
        var transactionBuilder = new TransactionBuilder()
            .SetRecentBlockHash((await _rpcClient.GetLatestBlockHashAsync()).Result.Value.Blockhash)
            .SetFeePayer(_wallet.Account)
            .AddInstruction(TokenProgram.Transfer(
                wsolAccount, 
                customTokenAccount, 
                (ulong)(amount * tokenDenomination), // Convert SOL to lamports
                _wallet.Account.PublicKey
            ));

        // Build and send the transaction
        var transaction = transactionBuilder.Build(new[] { _wallet.Account });
        var signature = await _rpcClient.SendTransactionAsync(transaction);

        Console.WriteLine($"Transaction signature: {signature.Result}");
        return signature.Result;
    }

    public async Task<string> Sell(decimal amount, string wsolMint, string customTokenMint, int tokenDenomination)
    {
        Console.WriteLine($"Selling {amount} of {customTokenMint} for {wsolMint}...");
        
        // Fetch WSOL account
        var wsolAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(_wallet.Account, new PublicKey(wsolMint));
        
        // Fetch custom token account
        var customTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(_wallet.Account, new PublicKey(customTokenMint));

        // Perform token swap via Serum, Raydium, or other DEX
        var transactionBuilder = new TransactionBuilder()
            .SetRecentBlockHash((await _rpcClient.GetLatestBlockHashAsync()).Result.Value.Blockhash)
            .SetFeePayer(_wallet.Account)
            .AddInstruction(TokenProgram.Transfer(
                customTokenAccount, 
                wsolAccount,
                (ulong)(amount * tokenDenomination), // Convert SOL to lamports
                _wallet.Account.PublicKey
            ));

        // Build and send the transaction
        var transaction = transactionBuilder.Build(new[] { _wallet.Account });
        var signature = await _rpcClient.SendTransactionAsync(transaction);

        Console.WriteLine($"Transaction signature: {signature.Result}");
        return signature.Result;
    }
}