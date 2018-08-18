using UnityEngine;
using BobbyCarrot.Platforms;
using BobbyCarrot.Movers;


namespace BobbyCarrot
{
	public class Board : MonoBehaviour
	{
		public static Board instance { get; private set; }

		public static event System.Action onReset;

		public Transform platformAnchor => _platformAnchor;

		public Transform moverAnchor => _moverAnchor;

		private bool startGame;
		[SerializeField] private Walker walker;
		[SerializeField] private Transform _platformAnchor, _moverAnchor;


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
			bool hasBottom = level.bottomArray != null;
			var size = new Vector2Int(level.middleArray.Length, level.middleArray[0].Length);
			var index = new Vector3Int();

			for (index.x = 0; index.x < size.x; ++index.x)
				for (index.y = 0; index.y < size.y; ++index.y)
				{
					Platform bottomPlatform = null, middlePlatform = null, topPlatform = null;
					Mover mover = null;
					var wPos = index.ArrayToWorld();
					int bottomID = -1, middleID = -1, topID = -1, moverID = -1;

					// Find all IDs
					middleID = level.middleArray[index.x][index.y];
					topID = level.topArray[index.x][index.y];
					if (hasBottom)
					{
						bottomID = level.bottomArray[index.x][index.y];
						moverID = middleID;
					}
					else moverID = topID;

					// Deserialize Platform and Mover
					if (bottomID != -1) bottomPlatform = Platform.DeSerialize(bottomID, wPos);
					if (moverID != -1) mover = Mover.DeSerialize(moverID, wPos);
					if (middleID != -1) middlePlatform = Platform.DeSerialize(middleID, wPos);
					if (topID != -1) topPlatform = Platform.DeSerialize(topID, wPos);
				}

			startGame = false;
		}


		private void Update()
		{
			if (!startGame)
			{
				// The Game starts here !
				startGame = true;
				walker.transform.position = NormalGround.startPoint.transform.position;





				walker.gameObject.SetActive(true);
			}
		}
	}
}
