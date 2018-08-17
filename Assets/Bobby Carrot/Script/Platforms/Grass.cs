using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;


namespace BobbyCarrot.Platforms
{
	public class Grass : Platform
	{
		[SerializeField] private GameObject anim;


		public override bool CanEnter(Mover mover) =>
			mover is GrassMower || mover is Flyer || mover is FireBall;


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			Destroy(Instantiate(anim, transform.position, Quaternion.identity), 3f);
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			Destroy(gameObject);
		}


		public static new Grass DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var grass = Instantiate(R.asset.prefab.grass, wPos, Quaternion.identity);
			if (use) grass.Use();
			return grass;
		}


		public static new byte[] Serialize(object obj)
		{
			var grass = (Grass)obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = grass.transform.position;
				w.Write(pos.x); w.Write(pos.y);

				return m.ToArray();
			}
		}


		public static new Grass DeSerialize(byte[] data)
		{
			var grass = Instantiate(R.asset.prefab.grass);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				grass.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
			}

			return grass;
		}
	}
}
