using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class GA : Role
    {
        public PlayerControl target;
        public bool TargetVotedOut;

        public GA(PlayerControl player) : base(player)
        {
            Name = "Guardian Angel";
            ImpostorText = () =>
                target == null ? "You don't have a target for some reason... weird..." : $"Vote {target.name} out";
            TaskText = () =>
                target == null
                    ? "You don't have a target for some reason... weird..."
                    : $"Vote {target.name} out\nFake Tasks:";
            Color = new Color(0.12f, 0.60f, 0.00f, 0.45f);
            RoleType = RoleEnum.GA;
            Faction = Faction.Neutral;
            Scale = 1.4f;
        }

        protected override void IntroPrefix(IntroCutscene._CoBegin_d__14 __instance)
        {
            var gateam = new List<PlayerControl>();
            gateam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = gateam;
        }

        internal override bool EABBNOODFGL(ShipStatus __instance)
        {
            if (Player.Data.IsDead) return true;
            if (!TargetVotedOut || !target.Data.IsDead) return true;
            Utils.EndGame();
            return false;
        }

        public void Wins()
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return;
            TargetVotedOut = true;
        }

        public void Loses()
        {
            Player.Data.IsImpostor = true;
        }
    }
}