using UnityEngine;


namespace BobbyCarrot.Movers
{
	public class FireBall : Mover
	{
		public static FireBall instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}
	}
}
