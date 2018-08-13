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
			onReset();
		}


		private void Start()
		{
			var level = Level.instance;
			var size = new Vector2Int(level.middleArray.Length, level.middleArray[0].Length);
			var index = new Vector3Int();
			bool hasBottom = level.bottomArray != null;
			for (index.x = 0; index.x < size.x; ++index.x)
				for (index.y = 0; index.y < size.y; ++index.y)
				{
					// Deserialize Platforms
					var wPos = index.ArrayToWorld();
					var platform = Platform.DeSerialize(level.topArray[index.x][index.y], wPos);
					platform = Platform.DeSerialize(level.middleArray[index.x][index.y], wPos);
					if (hasBottom) platform = Platform.DeSerialize(level.bottomArray[index.x][index.y], wPos);

					// Deserialze Movers
					var mover = hasBottom ? Mover.DeSerialize(level.middleArray[index.x][index.y], wPos) :
						Mover.DeSerialize(level.topArray[index.x][index.y], wPos);
				}
		}
	}
}
