using BobbyCarrot.Movers;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;


namespace BobbyCarrot.Platforms
{
	public class BoxButton : Platform
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;
		private Sprite ON, OFF;


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


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;
			spriteRenderer.sprite = (isOn = !isOn) ? ON : OFF;
			var list = isYellow ? Box.yellowList : Box.pinkList;
			foreach (var box in list) box.ChangeState();
		}


		public static new BoxButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var button = Instantiate(R.asset.prefab.button.box, wPos, Quaternion.identity);
			switch (ID)
			{
				case 191:
					button.isYellow = true; button.isOn = false;
					break;

				case 192:
					button.isYellow = true; button.isOn = true;
					break;

				case 193:
					button.isYellow = false; button.isOn = false;
					break;

				case 194:
					button.isYellow = false; button.isOn = true;
					break;
			}

			if (use) button.Use();
			return button;
		}


		public static new byte[] Serialize(object obj)
		{
			var box = (BoxButton)obj;
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


		public static new BoxButton DeSerialize(byte[] data)
		{
			var box = Instantiate(R.asset.prefab.button.box);
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
