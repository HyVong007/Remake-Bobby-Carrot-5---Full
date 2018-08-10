using UnityEngine;
using UnityEngine.Tilemaps;


namespace BobbyCarrot
{
	public class Level : MonoBehaviour
	{
		public Tilemap topMap, middleMap, bottomMap;
		public short[][] topArray, middleArray, bottomArray;

		public static Level instance { get; private set; }


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
