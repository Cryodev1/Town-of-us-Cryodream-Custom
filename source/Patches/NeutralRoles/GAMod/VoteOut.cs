using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.GAMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    internal class MeetingExiledEnd
    {
        private static void Postfix(ExileController __instance)
        {
            var exiled = __instance.exiled;
            if (exiled == null) return;
            var player = exiled.Object;

            foreach (var role in Role.GetRoles(RoleEnum.GA))
                if (player.PlayerId == ((GA) role).target.PlayerId)
                    ((GA) role).Wins();
        }
    }
}