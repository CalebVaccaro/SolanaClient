
# Solana Client Project

## Installation

### Install .NET SDK:
Download and install the .NET SDK from the [official .NET website](https://dotnet.microsoft.com/download).

### Clone the Repository:
```bash
git clone https://github.com/your-repo/solana-client.git
cd solana-client
```

### Restore Dependencies:
```bash
dotnet restore
```

## Usage

### Running the Program

#### Set Up Configuration:
- Open `SolanaClient/Program.cs`.
- Replace `your-private-key` with your actual private key.
- Replace `your-custom-token-mint` with your custom token mint address.

#### Run the Application:
```bash
dotnet run --project SolanaClient
```

## Logic Overview

### Program.cs

#### Initialization:
- Creates an instance of `SolanaClient` with the provided private key and RPC URL.

#### Buy Custom Token:
- Calls `client.Buy(1.5m, wsolMint, customTokenMint, tokenDenomination)` to buy 1.5 units of the custom token.
- Prints the transaction signature.

#### Sell Custom Token:
- Calls `client.Sell(1.0m, wsolMint, customTokenMint, tokenDenomination)` to sell 1.0 units of the custom token.
- Prints the transaction signature.

### SolanaClient.cs

#### Constructor:
- Initializes the RPC client and wallet using the provided private key and RPC URL.

#### Buy Method:
- Derives associated token accounts for WSOL and the custom token.
- Builds and sends a transaction to transfer the specified amount of WSOL to the custom token account.
- Returns the transaction signature.

#### Sell Method:
- Derives associated token accounts for WSOL and the custom token.
- Builds and sends a transaction to transfer the specified amount of the custom token to the WSOL account.
- Returns the transaction signature.
