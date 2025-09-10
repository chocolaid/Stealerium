/*
 * Stealerium Builder - Settings Persistence Module
 * 
 * 🔥 Enhanced by @chocolaid on GitHub
 * 
 * Features Added:
 * - Smart settings persistence with JSON storage
 * - Auto-loads last inputted values as placeholders
 * - Seamless CLI experience with memory
 * 
 * "Remembering your last moves like a true professional" 💀
 */

using System.Text.Json;
using System.IO;

namespace Builder.Modules;

internal sealed class Settings
{
    private static readonly string SettingsPath = "builder_settings.json";
    
    public static SettingsData LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                var settings = JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
                
                // Decrypt clipper addresses for display as placeholders
                settings.ClipperBTC = Crypt.DecryptConfig(settings.ClipperBTC);
                settings.ClipperETH = Crypt.DecryptConfig(settings.ClipperETH);
                settings.ClipperLTC = Crypt.DecryptConfig(settings.ClipperLTC);
                settings.ClipperUSDT = Crypt.DecryptConfig(settings.ClipperUSDT);
                
                // Convert boolean values from "1"/"0" to "y"/"n" for display
                settings.Debug = settings.Debug == "1" ? "y" : settings.Debug == "0" ? "n" : settings.Debug;
                settings.AntiAnalysis = settings.AntiAnalysis == "1" ? "y" : settings.AntiAnalysis == "0" ? "n" : settings.AntiAnalysis;
                settings.Startup = settings.Startup == "1" ? "y" : settings.Startup == "0" ? "n" : settings.Startup;
                settings.StartDelay = settings.StartDelay == "1" ? "y" : settings.StartDelay == "0" ? "n" : settings.StartDelay;
                settings.WebcamScreenshot = settings.WebcamScreenshot == "1" ? "y" : settings.WebcamScreenshot == "0" ? "n" : settings.WebcamScreenshot;
                settings.Keylogger = settings.Keylogger == "1" ? "y" : settings.Keylogger == "0" ? "n" : settings.Keylogger;
                settings.Clipper = settings.Clipper == "1" ? "y" : settings.Clipper == "0" ? "n" : settings.Clipper;
                settings.Grabber = settings.Grabber == "1" ? "y" : settings.Grabber == "0" ? "n" : settings.Grabber;
                
                return settings;
            }
        }
        catch
        {
            // If loading fails, return default settings
        }
        return new SettingsData();
    }
    
    public static void SaveSettings(SettingsData settings)
    {
        try
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // If saving fails, silently continue
        }
    }
}

internal sealed class SettingsData
{
    public string Webhook { get; set; } = "";
    public string Debug { get; set; } = "";
    public string AntiAnalysis { get; set; } = "";
    public string Startup { get; set; } = "";
    public string StartDelay { get; set; } = "";
    public string WebcamScreenshot { get; set; } = "";
    public string Keylogger { get; set; } = "";
    public string Clipper { get; set; } = "";
    public string Grabber { get; set; } = "";
    public string ClipperBTC { get; set; } = "";
    public string ClipperETH { get; set; } = "";
    public string ClipperLTC { get; set; } = "";
    public string ClipperUSDT { get; set; } = "";
}
