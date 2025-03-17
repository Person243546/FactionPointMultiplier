using HarmonyLib;
using RimWorld;
using Verse;

namespace FactionPointMultiplier
{
    [HarmonyPatch(typeof(IncidentWorker_Raid))]
    [HarmonyPatch("AdjustedRaidPoints")]
    class FactionPointMultiplierPatch
    {
        static void Prefix(ref float points, ref Faction faction)
        {
            if (FactionPointMultiplierMod.settings.factionandmultiplier.ContainsKey(faction.def.defName))
            {
                points *= FactionPointMultiplierMod.settings.factionandmultiplier[faction.def.defName];
            }
        }
    }
}