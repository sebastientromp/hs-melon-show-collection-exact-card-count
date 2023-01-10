using MelonLoader;
using UnityEngine;
using HarmonyLib;

namespace ShowCollectionExactCount
{
    public class ShowCollectionExactCountMod : MelonMod
    {
        public static MelonLogger.Instance SharedLogger;

        public override void OnInitializeMelon()
        {
            ShowCollectionExactCountMod.SharedLogger = LoggerInstance;
            var harmony = this.HarmonyInstance;
            harmony.PatchAll(typeof(CollectionCardCountPatcher));
        }
    }

    public static class CollectionCardCountPatcher
    {
        [HarmonyPatch(typeof(CollectionCardCount), "UpdateVisibility")]
        [HarmonyPostfix]
        public static void Postfix(
            CollectionCardCount __instance, 
            ref TAG_PREMIUM ___m_premium, 
            ref GameObject ___m_normalBorder,
            ref GameObject ___m_normalWideBorder,
            ref UberText ___m_normalCountText)
        {
            var count = __instance.GetCount(___m_premium);
            if (count >= 10)
            {
                ___m_normalCountText.Text = GameStrings.Format("GLUE_COLLECTION_CARD_COUNT", count);
            }
        }
    }
}
