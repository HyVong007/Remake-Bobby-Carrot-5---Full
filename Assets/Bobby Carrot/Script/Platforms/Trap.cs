using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Trap : Platform
	{
		public bool isOn { get; private set; }

		[SerializeField] private Sprite ONSprite, OFFSprite;


		public static new Trap DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
