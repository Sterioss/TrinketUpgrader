using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace TrinketUpgrader
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static Menu Trinkets;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Trinkets = MainMenu.AddMenu("Trinket Upgrader", "TrinketUpgrader");
            Trinkets.AddGroupLabel("Upgrade trinket at level 9");
            Trinkets.Add("UpgradeOn", new CheckBox("Turn it on"));
            Trinkets.Add("TrinketType", new ComboBox("Trinket Choice", new[] { "Oracle Alteration (Red)", "Farsight Alteration (Blue)" }));
            Trinkets.AddLabel("As soon as you're level 9 it'll upgrade your trinket!");
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if(ObjectManager.Player.Level >= 9 && Trinkets["UpgradeOn"].Cast<CheckBox>().CurrentValue && ObjectManager.Player.IsInShopRange())
            {
                if (Trinkets["TrinketType"].Cast<ComboBox>().CurrentValue == 1 && !ObjectManager.Player.HasItem(ItemId.Farsight_Alteration))
                    Shop.BuyItem(ItemId.Farsight_Alteration);
                if (Trinkets["TrinketType"].Cast<ComboBox>().CurrentValue == 0 && !ObjectManager.Player.HasItem(ItemId.Oracle_Alteration))
                    Shop.BuyItem(ItemId.Oracle_Alteration);
            }
        }

    }
}