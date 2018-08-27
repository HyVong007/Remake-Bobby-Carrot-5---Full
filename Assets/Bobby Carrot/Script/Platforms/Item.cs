using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace BobbyCarrot.Platforms
{
	public class Item : Platform
	{
		public new Name name { get; private set; }

		[SerializeField] private ItemName_Sprite_Dict sprites;
		[SerializeField] private NormalGround emptyGround;

		public static readonly Dictionary<Name, int> count = new Dictionary<Name, int>();

		public enum Name
		{
			ORANGE_MAP, BLUE_MAP, KEY, SPEAKER, MUSIC, SHOE,
			MAGNIFYING_GLASS, SNOW_SCRATCHER, BEAN, GAS, KITE,
			GOLDEN_CARROT, GOLDEN_COIN
		}


		private void Start()
		{
			if (name == Name.GOLDEN_COIN) animator.enabled = true;
			else spriteRenderer.sprite = sprites[name];
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is MobileCloud) && !(mover is LotusLeaf);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;

			++count[name];
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();
			switch (name)
			{
				case Name.BLUE_MAP:
				case Name.MAGNIFYING_GLASS:
				case Name.MUSIC:
				case Name.ORANGE_MAP:
				case Name.SHOE:
				case Name.SPEAKER:
					emptyGround.gameObject.SetActive(true);
					emptyGround.Use();
					break;
			}

			Destroy(gameObject);
			cts.Cancel();
			blinker?.SetActive(false);
		}


		static Item()
		{
			Board.onReset += () =>
			{
				foreach (Name name in System.Enum.GetValues(typeof(Name)))
					count[name] = 0;

				blinker = null;
			};
		}


		public static new Item DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var item = Instantiate(R.asset.prefab.item, wPos, Quaternion.identity);
			switch (ID)
			{
				case 151: item.name = Name.ORANGE_MAP; break;
				case 152: item.name = Name.BLUE_MAP; break;
				case 153: item.name = Name.KEY; break;
				case 154: item.name = Name.SPEAKER; break;
				case 155: item.name = Name.MUSIC; break;
				case 156: item.name = Name.SHOE; break;
				case 157: item.name = Name.MAGNIFYING_GLASS; break;
				case 159: item.name = Name.SNOW_SCRATCHER; break;
				case 207: item.name = Name.BEAN; break;
				case 221: item.name = Name.GAS; break;
				case 243: item.name = Name.KITE; break;
				case 246: item.name = Name.GOLDEN_CARROT; break;
				case 248: item.name = Name.GOLDEN_COIN; break;

				default: throw new System.Exception("Not found ID !");
			}

			if (use) item.Use();
			return item;
		}


		public static new byte[] Serialize(object obj)
		{
			var item = (Item)obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = item.transform.position;
				w.Write(pos.x); w.Write(pos.y);
				w.Write((byte)item.name);

				return m.ToArray();
			}
		}


		public static new Item DeSerialize(byte[] data)
		{
			var item = Instantiate(R.asset.prefab.item);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				item.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), 0f);
				item.name = (Name)r.ReadByte();
			}

			return item;
		}


		// Blinking Item UI

		private static GameObject blinker;
		private static Animator blinkerAnimator;
		private static readonly int NAME_PARAM = Animator.StringToHash("name");
		private static CancellationTokenSource cts = new CancellationTokenSource();

		public static void BlinkItem(Name name, Transform moverAnchor, float delaySeconds = 3f)
		{
			if (!blinker)
			{
				blinker = Instantiate(R.asset.prefab.ui.blinker);
				blinkerAnimator = blinker.transform.GetChild(0).GetComponent<Animator>();
			}

			blinker.transform.position = moverAnchor.position;
			blinker.transform.parent = moverAnchor;
			blinker.SetActive(true);
			blinkerAnimator.SetInteger(NAME_PARAM, (int)name);
			cts.Cancel();
			cts = new CancellationTokenSource();
			WaitToHideBlinker(delaySeconds, cts.Token);
		}


		private static async void WaitToHideBlinker(float delaySeconds, CancellationToken token)
		{
			await Task.Delay(Mathf.CeilToInt(delaySeconds * 1000));
			if (token.IsCancellationRequested) return;
			blinker?.gameObject.SetActive(false);
		}
	}
}
