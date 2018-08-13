using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class PinWheelButton : Platform
	{
		public PinWheel.Color color { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private PinWheelColor_Sprite_Dict ONSprite, OFFSprite;


		public static new PinWheelButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
