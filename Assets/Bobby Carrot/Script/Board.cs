using UnityEngine;
using BobbyCarrot.Platforms;
using BobbyCarrot.Movers;


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

			CommonUtil.Init();
			//onReset();
		}


		private void Start()
		{
			var level = Level.instance;
			bool hasBottom = level.bottomArray != null;
			var size = new Vector2Int(level.middleArray.Length, level.middleArray[0].Length);
			var index = new Vector3Int();

			for (index.x = 0; index.x < size.x; ++index.x)
				for (index.y = 0; index.y < size.y; ++index.y)
				{
					// Deserialize Platforms
					var wPos = index.ArrayToWorld();
					Platform bottomPlatform = null, middlePlatform = null, topPlatform = null;

					int ID = level.topArray[index.x][index.y];
					if (ID != -1) topPlatform = Platform.DeSerialize(ID, wPos);

					ID = level.middleArray[index.x][index.y];
					if (ID != -1) middlePlatform = Platform.DeSerialize(ID, wPos);

					if (hasBottom)
					{
						ID = level.bottomArray[index.x][index.y];
						if (ID != -1) bottomPlatform = Platform.DeSerialize(ID, wPos);
					}

					// Deserialze Movers
					Mover mover = null;
					if (hasBottom) ID = level.middleArray[index.x][index.y];
					else ID = level.topArray[index.x][index.y];
					if (ID != -1) mover = Mover.DeSerialize(ID, wPos);
				}
		}
	}
}
