using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Carrot : Platform
	{
		public State state { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private CarrotState_Sprite_Dict sprites;

		public enum State
		{
			LEAF, HOLE, CARROT
		}
	}
}
