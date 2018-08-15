using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class Item : Platform
	{
		public new Name name { get; private set; }

		[SerializeField] private ItemName_Sprite_Dict sprites;

		public static readonly Dictionary<Name, int> count = new Dictionary<Name, int>();

		public enum Name
		{
			ORANGE_MAP, BLUE_MAP, KEY, SPEAKER, MUSIC, SHOE,
			MAGNIFYING_GLASS, SNOW_SCRATCHER, BEAN, GAS, KITE,
			GOLDEN_CARROT, GOLDEN_COIN
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[name];
			// Check to add Anim
		}


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;
			++count[name];

			// Remove this out of Platform array list
			Destroy(gameObject);
		}


		static Item()
		{
			System.Action f = () =>
			{
				foreach (Name name in System.Enum.GetValues(typeof(Name)))
					count[name] = 0;
			};

			f();
			Board.onReset += f;
		}


		public static new Item DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
