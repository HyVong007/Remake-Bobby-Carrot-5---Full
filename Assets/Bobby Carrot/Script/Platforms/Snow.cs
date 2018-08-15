using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Snow : Platform
	{
		public static new Snow DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = New(ID, wPos, R.asset.prefab.snow);
			if (use) obj.Use();
			return obj;
		}


		public static new Snow DeSerialize(byte[] data)
		{
			int ID; Vector3 wPos;
			DeSerialize(data, out ID, out wPos);
			return DeSerialize(ID, wPos, false);
		}

		// Platform.Serialize

		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;
			return mover is Walker && Item.count[Item.Name.SNOW_SCRATCHER] > 0;
		}


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker) || (Item.count[Item.Name.SNOW_SCRATCHER] == 0)) return;

			// Anim and walker move anim

			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject, 2f);
		}
	}
}
