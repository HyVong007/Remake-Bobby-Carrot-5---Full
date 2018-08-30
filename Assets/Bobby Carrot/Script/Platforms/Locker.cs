using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Locker : Platform
	{
		public static new Locker DeSerialize(int ID, Vector3 wPos, bool use = true) =>
			Platform.DeSerialize(ID, wPos, R.asset.prefab.locker);


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;
			if (mover is Walker)
				if (Item.count[Item.Name.KEY] > 0) return true;
				else Item.BlinkItem(Item.Name.KEY, mover.transform);

			return false;
		}


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;
			--Item.count[Item.Name.KEY];
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject);
		}
	}
}
