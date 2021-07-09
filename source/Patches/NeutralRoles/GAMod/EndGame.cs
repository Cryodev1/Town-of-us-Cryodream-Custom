using HarmonyLib;
using Hazel;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.GAMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.RpcEndGame))]
    public class EndGame
    {
        public static bool Prefix(ShipStatus __instance, [HarmonyArgument(0)] GameOverReason reason)
        {
            if (reason != GameOverReason.HumansByVote && reason != GameOverReason.HumansByTask) return true;

            foreach (var role in Role.AllRoles)
                if (role.RoleType == RoleEnum.GA)
                    ((GA) role).Loses();

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.GALose,
                SendOption.Reliable, -1);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            return true;
        }
    }
}