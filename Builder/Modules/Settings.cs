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
                return JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
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
