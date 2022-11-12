using System.Diagnostics;
using System.Linq;
using BepInEx.Configuration;
using Kitchen;
using KitchenData;
using Unity.Entities;
using UnityEngine;

namespace MorePlayers.Config;

public static class ConfigHelper
{
    private static ConfigEntry<int> _CardDays;

    public static void SetUp(ConfigFile config)
    {
        _CardDays = config.Bind("Cards", "Card days", 3, "Sets the days where cards popup.(eg. 3 means every 3 days)");
    }

    public static int GetCardDays()
    {
        return _CardDays.Value;
    }
}