using UnityEngine;
using System.IO;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class GrassMowerStop : Platform
	{
		public bool hasMower { get; private set; } = true;
		[SerializeField] private GameObject mowerUI;


		public static new GrassMowerStop DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = Instantiate(R.asset.prefab.grassMowerStop, wPos, Quaternion.identity);
			if (use) obj.Use();
			return obj;
		}


		public static new byte[] Serialize(object _obj)
		{
			var obj = (GrassMowerStop)_obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = obj.transform.position;
				w.Write(pos.x); w.Write(pos.y);
				w.Write(obj.hasMower);

				return m.ToArray();
			}
		}


		public static new GrassMowerStop DeSerialize(byte[] data)
		{
			var obj = Instantiate(R.asset.prefab.grassMowerStop);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				obj.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
				obj.hasMower = r.ReadBoolean();
			}

			return obj;
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall || mover is GrassMower) return true;
			if (mover is Walker)
				if (Item.count[Item.Name.GAS] > 0) return true;
				else Item.BlinkItem(Item.Name.GAS, mover.transform, 5f);
			return false;
		}


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Walker)
			{
				mover.gameObject.SetActive(false);
				mowerUI.SetActive(false);
				var grassMower = GrassMower.instance;
				if (!grassMower) grassMower = Instantiate(R.asset.prefab.grassMower, transform.position, Quaternion.identity);
				else grassMower.gameObject.SetActive(true);
				grassMower.GotoIdle(mover.direction);
			}
			else if (mover is GrassMower)
			{
				mover.gameObject.SetActive(false);
				mowerUI.SetActive(true);
				var walker = Walker.instance;
				var idle = Walker.dirToIdle[mover.direction];
				walker.gameObject.SetActive(true);
				walker.GotoIdle((Walker.RelaxState)idle);
			}
		}
	}
}
