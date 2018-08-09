using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Trap : Platform
	{
		public bool isOn { get; private set; }

		[SerializeField] private Sprite ONSprite, OFFSprite;
	}
}
