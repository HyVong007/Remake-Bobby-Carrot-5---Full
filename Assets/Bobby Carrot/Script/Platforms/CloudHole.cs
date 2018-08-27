using UnityEngine;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class CloudHole : Platform
	{
		public PinWheel.Color color { get; private set; }

		[SerializeField] PinWheelColor_Sprite_Dict sprites;


		public static new CloudHole DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var hole = Instantiate(R.asset.prefab.cloudHole, wPos, Quaternion.identity);
			switch (ID)
			{
				case 240:
					hole.color = PinWheel.Color.RED;
					break;

				case 241:
					hole.color = PinWheel.Color.VIOLET;
					break;

				case 242:
					hole.color = PinWheel.Color.GREEN;
					break;
			}

			if (use) hole.Use();
			return hole;
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[color];
		}


		public override bool CanEnter(Mover mover) => 
			mover is Flyer || mover is FireBall || mover is MobileCloud;


		public override bool CanExit(Mover mover) =>
			mover is Flyer || mover is FireBall || ((MobileCloud)mover).color != color;
	}
}
