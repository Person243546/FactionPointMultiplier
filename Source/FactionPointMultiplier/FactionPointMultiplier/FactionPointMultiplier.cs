using System.Collections.Generic;
using Verse;
using UnityEngine;
using RimWorld;
using static FactionPointMultiplier.Startup;

namespace FactionPointMultiplier
{
    class FactionPointMultiplierSettings : ModSettings
    {

        public Dictionary<string, float> factionandmultiplier = new Dictionary<string, float>();
        public List<string> factionlist1 = new List<string>();
        public List<float> multiplierlist2 = new List<float>();
        private static Vector2 scrollPosition = new Vector2(0f, 0f);
        private static float totalContentHeight = 1000f;
        private const float ScrollBarWidthMargin = 18f;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref factionandmultiplier, "factionandmultiplier", LookMode.Value, LookMode.Value, ref factionlist1, ref multiplierlist2);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Rect outerRect = inRect.ContractedBy(10f);
            Widgets.DrawHighlight(outerRect);
            bool scrollBarVisible = totalContentHeight > outerRect.height;
            var scrollViewTotal = new Rect(0f, 0f, outerRect.width - (scrollBarVisible ? ScrollBarWidthMargin : 0), totalContentHeight);
            Widgets.BeginScrollView(outerRect, ref scrollPosition, scrollViewTotal);
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(new Rect(0f, 0f, scrollViewTotal.width, 9999f));
            ls.GapLine();
            ls.Label("Faction Point Multipliers:");
            for (var i = 0; i < allFactions.Length; i++)
            {
                var name = allFactions[i].defName;
                var value = FactionPointMultiplierMod.settings.factionandmultiplier[name];
                string buf = value.ToString();
                ls.TextFieldNumericLabeled<float>(name, ref value, ref buf, 0.01f, 100f);
                //ls.Label(i.ToString());
                FactionPointMultiplierMod.settings.factionandmultiplier[name] = value;
            }
            //ls.Label("Height: " + inRect.height + " Width " + inRect.width + " Number " + totalContentHeight);
            //Log.Message("Height: " + inRect.height + " Width " + inRect.width + " Number " + totalContentHeight);
            ls.GapLine();
            totalContentHeight = ls.CurHeight + 5f;
            ls.End();
            Widgets.EndScrollView();
        }
    }

    class FactionPointMultiplierMod : Mod
    {
        public static FactionPointMultiplierSettings settings;
        public FactionPointMultiplierMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<FactionPointMultiplierSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "FactionPointMultiplier".Translate();
        }
    }
}