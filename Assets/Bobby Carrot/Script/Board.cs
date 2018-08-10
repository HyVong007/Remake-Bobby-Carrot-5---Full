using UnityEngine;


namespace BobbyCarrot
{
	public class Board : MonoBehaviour
	{
		public static Board instance { get; private set; }

		public static event System.Action onReset;


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
