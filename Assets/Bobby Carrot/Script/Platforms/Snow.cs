using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Snow : Platform
	{
		public static new Snow DeSerialize(int ID, Vector3 wPos, bool use = true) =>
			Platform.DeSerialize(ID, wPos, R.asset.prefab.snow);


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;
			if (mover is Walker)
				if (Item.count[Item.Name.SNOW_SCRATCHER] > 0) return true;
				else Item.BlinkItem(Item.Name.SNOW_SCRATCHER, mover.transform);

			return false;
		}


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker) || (Item.count[Item.Name.SNOW_SCRATCHER] == 0)) return;

			var walker = (Walker)mover;
			walker.isLock = true;
			await walker.ScratchSnow();

			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject);
			walker.GotoIdle((Walker.RelaxState)Walker.dirToIdle[walker.direction]);
			walker.isLock = false;
		}
	}
}
