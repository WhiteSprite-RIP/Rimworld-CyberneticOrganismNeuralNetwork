using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;

namespace CONN
{
	[HarmonyPatch(typeof(Pawn), "GetGizmos")]
	public static class Pawn_GetGizmos
	{
		[HarmonyPostfix]
		private static void Postfix(Pawn __instance, ref IEnumerable<Gizmo> __result)
		{
			if (!__instance.InMentalState)
				return;
			
			var hediffs = new List<HediffGizmoBerserk>();
			__instance.health.hediffSet.GetHediffs(ref hediffs);
			if (hediffs.Count > 0)
			{
				var extraGizmos = hediffs.SelectMany(b => b.GetGizmos()).ToArray();
				if (extraGizmos.Length > 0)
					__result = __result.Concat(extraGizmos);
			}
		}
	}
}
