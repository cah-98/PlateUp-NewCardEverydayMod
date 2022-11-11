using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MorePlayers.Config;

namespace MorePlayers;

[BepInPlugin(Guid, Name, Version)]
[BepInProcess("PlateUp.exe")]
public class NewCardEveryday : BaseUnityPlugin
{
    private const string Guid = "NewCardEveryday";
    private const string Name = "NewCardEveryday";
    private const string Version = "1.0.0";
    
    internal static ManualLogSource Log;

    private void Awake()
    {
        Log = Logger;
        
        var harmony = new Harmony(Guid);
        harmony.PatchAll();

        Log.LogMessage("Loaded NewCardEveryday version: " + Version);
        
        ConfigHelper.SetUp(Config);
    }
}