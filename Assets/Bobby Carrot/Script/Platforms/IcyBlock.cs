using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class IcyBlock : Platform
	{
		public static new IcyBlock DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var block = Instantiate(R.asset.prefab.icyBlock, wPos, Quaternion.identity);
			if (use) block.Use();
			return block;
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall;


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer) return;

			animator.enabled = true;
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject, 3f);
		}
	}
}
