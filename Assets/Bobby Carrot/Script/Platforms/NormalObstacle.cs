using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class NormalObstacle : Platform
	{
		public new Name name { get; private set; }

		public enum Name
		{
			NORMAL, SKY, ANIM_STAR, WATER, ANIM_WATER, WATER_FALL,
			DRAGON_HEAD, DRAGON_BODY, DRAGON_TAIL
		}
	}
}
