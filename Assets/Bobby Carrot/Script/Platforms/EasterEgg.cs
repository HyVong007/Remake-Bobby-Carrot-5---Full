using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class EasterEgg : Platform
	{
		public bool hasEgg { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private Sprite noEggSprite, hasEggSprite;
	}
}
