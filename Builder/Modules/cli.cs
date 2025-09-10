/*
 * Stealerium Builder - Enhanced CLI Module
 * 
 * 🔥 Enhanced by @chocolaid on GitHub
 * 
 * Features Added:
 * - Smart placeholder system with settings memory
 * - Markup escaping to prevent Spectre.Console crashes
 * - Wallet validation integration with attitude
 * - Enhanced user experience with auto-fill capabilities
 * 
 * "CLI that doesn't take shit from users" 💀
 */

using Spectre.Console;

namespace Builder.Modules;

internal sealed class Cli
{
    public static string GetBoolValue(string text, string? defaultValue = null)
    {
        var placeholder = defaultValue != null ? $" [{defaultValue}]" : "";
        Console.Write($"(?) {text} (y/n){placeholder}: ");
        Console.ForegroundColor = ConsoleColor.White;
        var result = Console.ReadLine();
        
        // If empty input and we have a default, use the default
        if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(defaultValue))
        {
            return defaultValue;
        }
        
        return result != null && result.ToUpper() == "Y" ? "1" : "0";
    }

    public static string? GetStringValue(string text, string? placeholder = null)
    {
        var placeholderText = !string.IsNullOrEmpty(placeholder) ? $" [{EscapeMarkup(placeholder)}]" : "";
        AnsiConsole.Write(new Markup($"[bold yellow](?)[/] [red]{EscapeMarkup(text)}[/]{placeholderText}\n>>> "));
        var result = Console.ReadLine();
        
        // If empty input and we have a placeholder, use the placeholder
        if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(placeholder))
        {
            return placeholder;
        }
        
        return result;
    }

    private static string EscapeMarkup(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        
        // Escape special characters that Spectre.Console interprets as markup
        return text
            .Replace("[", "[[")
            .Replace("]", "]]")
            .Replace("(", "[(")
            .Replace(")", ")]");
    }

    public static string GetEncryptedString(string text, string? placeholder = null)
    {
        var result = GetStringValue(text, placeholder);
        return !string.IsNullOrEmpty(result) ? Crypt.EncryptConfig(result) : "";
    }

    public static string GetValidatedWallet(string text, string walletType, string? placeholder = null)
    {
        string? result;
        do
        {
            result = GetStringValue(text, placeholder);
            if (string.IsNullOrEmpty(result))
            {
                ShowError($"You fucking moron! You need to enter a {walletType} address!");
                continue;
            }
            
            if (!WalletValidator.ValidateWallet(result, walletType))
            {
                result = null; // Force retry
            }
        } while (string.IsNullOrEmpty(result));
        
        return Crypt.EncryptConfig(result);
    }

    public static void ShowError(string text)
    {
        AnsiConsole.Write(new Markup("[red] (!) " + text + "\n Press any key to exit...[/]"));
        Console.ReadKey();
        Environment.Exit(1);
    }

    public static void ShowInfo(string text)
    {
        AnsiConsole.Write(new Markup("[yellow] (i) " + text + "[/]"));
    }

    public static void ShowSuccess(string text)
    {
        AnsiConsole.Write(new Markup("[green] (+) " + text + "[/]"));
    }
}