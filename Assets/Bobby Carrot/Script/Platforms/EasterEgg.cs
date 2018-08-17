using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;


namespace BobbyCarrot.Platforms
{
	public class EasterEgg : Platform
	{
		public bool hasEgg { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private Sprite noEggSprite, hasEggSprite;


		private void Start()
		{
			spriteRenderer.sprite = hasEgg ? hasEggSprite : noEggSprite;
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			if (mover is Flyer || mover is FireBall) return true;
			return !hasEgg;
		}


		public override async Task OnExit(Mover mover)
		{
			if (hasEgg || !(mover is Walker)) return;
			hasEgg = true;
			spriteRenderer.sprite = hasEggSprite;
			--countDown;

			// Check countDown
		}


		static EasterEgg()
		{
			Board.onReset += () => countDown = 0;
		}


		public static new EasterEgg DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var egg = Instantiate(R.asset.prefab.easterEgg, wPos, Quaternion.identity);
			egg.hasEgg = (ID == 204);

			if (use) egg.Use();
			return egg;
		}


		public static new byte[] Serialize(object obj)
		{
			var egg = (EasterEgg)obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = egg.transform.position;
				w.Write(pos.x); w.Write(pos.y);
				w.Write(egg.hasEgg);

				return m.ToArray();
			}
		}


		public static new EasterEgg DeSerialize(byte[] data)
		{
			var egg = Instantiate(R.asset.prefab.easterEgg);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				egg.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
				egg.hasEgg = r.ReadBoolean();
			}

			return egg;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			if (!hasEgg) ++countDown;
		}
	}
}
