using UnityEngine;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class CloudHole : Platform
	{
		public PinWheel.Color color { get; private set; }

		public MobileCloud mobileCloud { get; private set; }

		[SerializeField] PinWheelColor_Sprite_Dict sprites;


		public static new CloudHole DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
