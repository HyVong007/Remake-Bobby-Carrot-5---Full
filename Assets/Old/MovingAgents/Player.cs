using UnityEngine;


namespace Script.MovingAgents
{
	public class Player : Mover
	{
		/// <summary>
		/// State of the Player
		/// </summary>
		public enum State
		{
			WALKING,
			MOWERING,
			FLYING
		}


		public State state;
		public Sprite[] walkingSprites, flyingSprites;
		public Animator animator;
	}
}