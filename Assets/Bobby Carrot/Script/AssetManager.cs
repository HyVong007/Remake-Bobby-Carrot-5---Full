using UnityEngine;
using UnityEngine.Tilemaps;
using BobbyCarrot.Platforms;
using BobbyCarrot.Movers;


namespace BobbyCarrot
{
	[CreateAssetMenu]
	public class AssetManager : ScriptableObject
	{
		[System.Serializable]
		public struct Anim
		{
			public RuntimeAnimatorController star, water, waterFall, end;
		}
		public Anim anim;

		[System.Serializable]
		public struct Prefab
		{
			// Platform

			public NormalObstacle normalObstacle;
			public NormalGround normalGround;
			public Snow snow;
			public WaterFlow waterFlow;
			public SlipperyIce slipperyIce;
			public Item item;
			public GrassMowerStop grassMowerStop;

			[System.Serializable]
			public struct Button
			{
				public RailRoadButton railRoad;
				public MazeButton maze;
				public WaterFlowButton waterFlow;
				public PinWheelButton pinWheel;
				public BoxButton box;
			}
			public Button button;

			public Trap trap;
			public Mirror mirror;
			public RailRoad railRoad;
			public Maze maze;
			public Box box;
			public Grass grass;
			public Carrot carrot;
			public EasterEgg easterEgg;
			public Locker locker;
			public PinWheel pinWheel;
			public Wood wood;
			public BeanTree beanTree;
			public MobileCloud mobileCloud;
			public IcyBlock icyBlock;
			public LotusLeaf lotusLeaf;
			public Rock rock;
			public CloudHole cloudHole;
			public Wind wind;

			// Level

			[System.Serializable]
			public struct Levels
			{
				public Level testLevel, empty;
			}
			public Levels levels;
		}
		public Prefab prefab;

		[System.Serializable]
		public struct MyTile
		{
			public Tile[] platforms;
		}
		public MyTile myTile;
	}
}
