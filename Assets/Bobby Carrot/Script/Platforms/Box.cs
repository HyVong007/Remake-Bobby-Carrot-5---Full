using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.IO;


namespace BobbyCarrot.Platforms
{
	public class Box : Platform, IButtonProcessor
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;
		private Sprite ON, OFF;
		public static readonly List<IButtonProcessor> yellowList = new List<IButtonProcessor>(), pinkList = new List<IButtonProcessor>();


		private void Start()
		{
			if (isYellow)
			{
				ON = yellowON; OFF = yellowOFF;
			}
			else
			{
				ON = pinkON; OFF = pinkOFF;
			}

			spriteRenderer.sprite = isOn ? ON : OFF;
		}


		public void ChangeState() => spriteRenderer.sprite = (isOn = !isOn) ? ON : OFF;


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer) return true;
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			return !isOn;
		}


		static Box()
		{
			Board.onReset += () =>
			  {
				  yellowList.Clear(); pinkList.Clear();
			  };
		}


		public static new Box DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var box = Instantiate(R.asset.prefab.box, wPos, Quaternion.identity);
			switch (ID)
			{
				case 195:
					box.isYellow = true; box.isOn = true;
					break;

				case 196:
					box.isYellow = true; box.isOn = false;
					break;

				case 197:
					box.isYellow = false; box.isOn = true;
					break;

				case 198:
					box.isYellow = false; box.isOn = false;
					break;
			}

			if (use) box.Use();
			return box;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			if (isYellow) yellowList.Add(this); else pinkList.Add(this);
		}


		public static new byte[] Serialize(object obj)
		{
			var box = (Box)obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = box.transform.position;
				w.Write(pos.x); w.Write(pos.y);
				w.Write(box.isYellow);
				w.Write(box.isOn);

				return m.ToArray();
			}
		}


		public static new Box DeSerialize(byte[] data)
		{
			var box = Instantiate(R.asset.prefab.box);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				box.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
				box.isYellow = r.ReadBoolean();
				box.isOn = r.ReadBoolean();
			}

			return box;
		}
	}
}
