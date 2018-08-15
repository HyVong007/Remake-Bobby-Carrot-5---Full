using UnityEngine;
using BobbyCarrot.Platforms;
using System.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	public class MobileCloud : Mover, IPlatformProcessor, IUsable
	{
		public PinWheel.Color color { get; private set; }

		[SerializeField] PinWheelColor_Sprite_Dict sprites;


		public static new MobileCloud DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = Instantiate(R.asset.prefab.mobileCloud, wPos, Quaternion.identity);
			switch (ID)
			{
				case 224:
					obj.color = PinWheel.Color.RED;
					break;

				case 225:
					obj.color = PinWheel.Color.VIOLET;
					break;

				case 226:
					obj.color = PinWheel.Color.GREEN;
					break;
			}

			if (use) obj.Use();
			return obj;
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[color];
		}


		public bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall ||
				(
					(mover is Walker || mover is GrassMower) && (direction == Vector3Int.zero)
				);


		public bool CanExit(Mover mover) => CanEnter(mover);


		public async Task OnExit(Mover mover) { }


		public async Task OnEnter(Mover mover) { }


		public void Use(Vector3Int? pos = null)
		{
			if (pos == null) pos = transform.position.WorldToArray();
			var p = pos.Value;
			Platform.array[p.x][p.y].Push(this);
			transform.parent = Board.instance.moverAnchor;
		}
	}
}
