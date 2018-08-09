using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class BoxButton : Platform
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;
	}
}
