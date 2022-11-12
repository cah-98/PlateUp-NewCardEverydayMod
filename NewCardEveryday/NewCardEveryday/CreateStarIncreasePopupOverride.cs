/*using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kitchen;
using UnityEngine;
using Unity.Entities;
using MorePlayers.Config;

namespace MorePlayers
{
    [HarmonyPatch(typeof(CreateStarIncreasePopup), "OnUpdate")]
    public class CreateStarIncreasePopupOverridePatch_OnUpdate
    {
        [HarmonyPrefix]
        public static bool Prefix(CreateStarIncreasePopup __instance)
        {
            Traverse CE = Traverse.Create(__instance);
            bool flag = __instance.HasSingleton<SIsRestartedDay>();
            if (!flag)
            {
                int day = CE.Field("_SingletonEntityQuery_SDay_51").GetValue<EntityQuery>().GetSingleton<SDay>().Day;
                bool flag2 = day % ConfigHelper.GetCardDays() != 0 || day >= 15;
                if (!flag2)
                {
                    Entity entity = __instance.EntityManager.CreateEntity(new ComponentType[]
                    {
                        typeof(CPopup),
                        typeof(CHideView),
                        typeof(CStarIncreasePopup),
                        typeof(CRequiresView),
                        typeof(CPosition),
                        typeof(CCaptureInput)
                    });
                    __instance.EntityManager.SetComponentData<CPopup>(entity, new CPopup
                    {
                        Priority = Kitchen.PopupPriority.StarIncrease
                    });
                    __instance.EntityManager.SetComponentData<CStarIncreasePopup>(entity, new CStarIncreasePopup
                    {
                        StarCount = Mathf.FloorToInt((float)(day / ConfigHelper.GetCardDays()))
                    });
                    __instance.EntityManager.SetComponentData<CCaptureInput>(entity, new CCaptureInput
                    {
                        AllUsers = true
                    });
                    __instance.EntityManager.SetComponentData<CRequiresView>(entity, new CRequiresView
                    {
                        Type = ViewType.StarUnlockPopup,
                        ViewMode = ViewMode.Screen
                    });
                    __instance.EntityManager.SetComponentData<CPosition>(entity, new CPosition(new Vector3(0.5f, 0.5f, 0f)));
                }
            }
            return false;
        }
    }
}
*/