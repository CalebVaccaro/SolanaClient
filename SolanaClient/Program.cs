using Solana;

var privateKey = "your-private-key"; // Replace with your private key
var rpcUrl = "https://api.mainnet-beta.solana.com"; // Solana RPC URL
var wsolMint = "So11111111111111111111111111111111111111112"; // Wrapped SOL mint address
var customTokenMint = "your-custom-token-mint"; // Replace with your custom token mint
var tokenDenomination = 1_000_000_000; // (int) Token denomination is the number of decimal places for the token

var client = new SolanaClient(privateKey, rpcUrl);

// Buy custom token
var buySignature = await client.Buy(1.5m, wsolMint, customTokenMint, tokenDenomination);
Console.WriteLine($"Buy transaction completed: {buySignature}");

// Sell custom token
var sellSignature = await client.Sell(1.0m, wsolMint, customTokenMint, tokenDenomination);
Console.WriteLine($"Sell transaction completed: {sellSignature}");