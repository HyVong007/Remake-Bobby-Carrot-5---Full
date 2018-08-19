using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class SlipperyIce : Platform
	{
		public static new SlipperyIce DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var ice = Instantiate(R.asset.prefab.slipperyIce, wPos, Quaternion.identity);
			if (use) ice.Use();
			return ice;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;

			mover.movingDistance = 1;
			await Task.Delay(1);
		}
	}
}
