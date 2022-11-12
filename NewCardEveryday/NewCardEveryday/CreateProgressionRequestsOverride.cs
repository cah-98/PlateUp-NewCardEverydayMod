using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kitchen;
using UnityEngine;
using Unity.Entities;
using MorePlayers.Config;
using KitchenData;
using Unity.Collections;

namespace MorePlayers
{
    [HarmonyPatch(typeof(CreateProgressionRequests), "OnUpdate")]
    public class CreateProgressionRequestsOverridePatch_OnUpdate
    {
        [HarmonyPrefix]
        public static bool Prefix(CreateProgressionRequests __instance)
        {
            Traverse CE = Traverse.Create(__instance);
            Traverse CEAddRequest = CE.Method("AddRequest", UnlockGroup.Generic);

            bool flag = __instance.HasSingleton<SIsRestartedDay>();
            if (!flag)
            {
                int day = CE.Field("_SingletonEntityQuery_SDay_54").GetValue<EntityQuery>().GetSingleton<SDay>().Day;
                bool flag2 = !__instance.HasSingleton<CreateNewKitchen.SKitchenFirstFrame>() && day % ConfigHelper.GetCardDays() == 0 && day != 15 && day != 5;
                if (flag2)
                {
                    CEAddRequest.GetValue(UnlockGroup.Generic);
                    CEAddRequest.GetValue(UnlockGroup.Dish);
                }
                bool flag3 = day == 15;
                if (flag3)
                {
                    CEAddRequest.GetValue(UnlockGroup.FranchiseCard);
                    CEAddRequest.GetValue(UnlockGroup.FranchiseCard);
                }
                bool flag4 = day > 0 && day % 5 == 0;
                if (flag4)
                {
                    bool flag5 = day == 5;
                    if (flag5)
                    {
                        UnlockGroup unlockGroup = CE.Method("DetermineNextThemeUnlock").GetValue<UnlockGroup>();
                        bool flag6 = unlockGroup > UnlockGroup.Generic;
                        if (flag6)
                        {
                            CEAddRequest.GetValue(unlockGroup);
                            CEAddRequest.GetValue(unlockGroup);
                        }
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        CE.Method("AddShop", ShoppingTags.Decoration).GetValue(ShoppingTags.Decoration);
                    }
                    for (int j = 0; j < 6; j++)
                    {
                        CE.Method("AddDecorShop").GetValue();
                    }
                }
                else
                {
                    int num = 0;
                    using (NativeArray<CRemovesShopBlueprint> nativeArray = CE.Field("ShopRemover").GetValue<EntityQuery>().ToComponentDataArray<CRemovesShopBlueprint>(Allocator.Temp))
                    {
                        foreach (CRemovesShopBlueprint cremovesShopBlueprint in nativeArray)
                        {
                            num += cremovesShopBlueprint.Count;
                        }
                        ShoppingTags defaultShoppingTag = ShoppingTagsExtensions.DefaultShoppingTag;
                        int num2 = Math.Max(1, DifficultyHelpers.TotalShopCount(day) - num);
                        int num3 = Math.Max(0, Math.Min(DifficultyHelpers.StapleCount(day), num2));
                        int num4 = Math.Max(0, num2 - num3);
                        for (int k = 0; k < num3; k++)
                        {
                            CE.Method("AddShop", ShoppingTags.Decoration).GetValue(ShoppingTags.Basic);
                        }
                        for (int l = 0; l < num4; l++)
                        {
                            CE.Method("AddShop", ShoppingTags.Decoration).GetValue(defaultShoppingTag);
                        }
                    }
                }
            }
            return false;
        }
    }
}
