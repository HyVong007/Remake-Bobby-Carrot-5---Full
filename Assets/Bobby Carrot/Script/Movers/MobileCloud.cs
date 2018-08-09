using UnityEngine;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Movers
{
	public class MobileCloud : Mover
	{
		public PinWheel.Color color { get; private set; }
		public static MobileCloud[][] array;

		[SerializeField] PinWheelColor_Sprite_Dict sprites;
	}
}
