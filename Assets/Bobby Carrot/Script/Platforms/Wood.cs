using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;


namespace BobbyCarrot.Platforms
{
	public class Wood : Platform
	{
		public override bool CanEnter(Mover mover) =>
			mover is Walker || mover is Flyer || mover is FireBall;


		public override async Task OnExit(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			animator.enabled = true;
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject, 3f);
		}


		public static new Wood DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var wood = Instantiate(R.asset.prefab.wood, wPos, Quaternion.identity);
			if (use) wood.Use();
			return wood;
		}


		public static new byte[] Serialize(object obj)
		{
			var wood = (Wood)obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = wood.transform.position;
				w.Write(pos.x); w.Write(pos.y);

				return m.ToArray();
			}
		}


		public static new Wood DeSerialize(byte[] data)
		{
			var wood = Instantiate(R.asset.prefab.wood);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				wood.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
			}

			return wood;
		}
	}
}
