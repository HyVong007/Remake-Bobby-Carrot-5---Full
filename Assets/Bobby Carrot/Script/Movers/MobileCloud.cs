using UnityEngine;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Movers
{
	public class MobileCloud : Mover
	{
		public PinWheel.Color color { get; private set; }
		public static MobileCloud[][] array;

		[SerializeField] PinWheelColor_Sprite_Dict sprites;


		public override void Use(Vector3Int? pos = null)
		{
			throw new System.NotImplementedException();
		}


		public static new MobileCloud DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
