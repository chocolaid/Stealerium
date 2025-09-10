/*
 * Stealerium Builder - Wallet Validation Module
 * 
 * 🔥 Enhanced by @chocolaid on GitHub
 * 
 * Features Added:
 * - Aggressive wallet address validation for BTC, ETH, LTC, USDT
 * - Regex-based validation with attitude-filled error messages
 * - Forces users to enter valid addresses or face the consequences
 * - Supports all major cryptocurrency address formats
 * 
 * "Validating wallets like a fucking professional" 💀
 */

using System.Text.RegularExpressions;

namespace Builder.Modules;

internal sealed class WalletValidator
{
    // Bitcoin address validation (Legacy, SegWit, Bech32)
    private static readonly Regex BitcoinRegex = new(@"^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,62}$", RegexOptions.Compiled);
    
    // Ethereum address validation (0x + 40 hex chars)
    private static readonly Regex EthereumRegex = new(@"^0x[a-fA-F0-9]{40}$", RegexOptions.Compiled);
    
    // Litecoin address validation (L, M, 3, or ltc1 prefix)
    private static readonly Regex LitecoinRegex = new(@"^[LM3][a-km-zA-HJ-NP-Z1-9]{26,33}$|^ltc1[a-z0-9]{39,59}$", RegexOptions.Compiled);
    
    // USDT address validation (same as Ethereum since it's ERC-20)
    private static readonly Regex UsdtRegex = new(@"^0x[a-fA-F0-9]{40}$", RegexOptions.Compiled);

    public static bool ValidateWallet(string address, string walletType)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            ShowError($"What the fuck? You didn't even enter a {walletType} address! Are you retarded?");
            return false;
        }

        address = address.Trim();
        
        return walletType.ToLower() switch
        {
            "bitcoin" or "btc" => ValidateBitcoin(address),
            "ethereum" or "eth" => ValidateEthereum(address),
            "litecoin" or "ltc" => ValidateLitecoin(address),
            "usdt" or "tether" => ValidateUsdt(address),
            _ => false
        };
    }

    private static bool ValidateBitcoin(string address)
    {
        if (!BitcoinRegex.IsMatch(address))
        {
            ShowError($"That's not a fucking Bitcoin address! Bitcoin addresses start with '1', '3', or 'bc1'. You entered: {address}");
            return false;
        }
        return true;
    }

    private static bool ValidateEthereum(string address)
    {
        if (!EthereumRegex.IsMatch(address))
        {
            ShowError($"That's not a fucking Ethereum address! Ethereum addresses start with '0x' and have 40 hex characters. You entered: {address}");
            return false;
        }
        return true;
    }

    private static bool ValidateLitecoin(string address)
    {
        if (!LitecoinRegex.IsMatch(address))
        {
            ShowError($"That's not a fucking Litecoin address! Litecoin addresses start with 'L', 'M', '3', or 'ltc1'. You entered: {address}");
            return false;
        }
        return true;
    }

    private static bool ValidateUsdt(string address)
    {
        if (!UsdtRegex.IsMatch(address))
        {
            ShowError($"That's not a fucking USDT address! USDT uses Ethereum format (0x + 40 hex chars). You entered: {address}");
            return false;
        }
        return true;
    }

    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n❌ {message}\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
