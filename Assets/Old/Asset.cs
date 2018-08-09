using UnityEngine;
using Script.Terrains;
using Script.MovingAgents;


namespace Script
{
	[CreateAssetMenu]
	public class Asset : ScriptableObject
	{
		[System.Serializable]
		public struct PinWheelData
		{
			public Sprite left, right, up, down;
			public RuntimeAnimatorController leftAnim, rightAnim, upAnim, downAnim;
		}
		public PinWheelData pinWheel;

		[System.Serializable]
		public struct MazeData
		{
			public Sprite leftUp, leftDown, rightUp, rightDown, vertical, horizontal;
		}
		public MazeData maze;

		[System.Serializable]
		public struct PrefabData
		{
			public Platform normalPlatform, waterFlow, item, mowerPark,
							  _switch, trap, mirror, railRoad, maze, box,
							  carrot, easterEgg, pinWheel, beanTree;

			public Dragon dragon;
			public ButterFly butterFly;
			public Mover lotusLeaf, bobby, fireBall;
		}
		public PrefabData prefabs;

		[System.Serializable]
		public struct AnimData
		{
			public RuntimeAnimatorController water, star, waterFall, target,
										 wood, icyBlock, wind, goldenCoin;
		}
		public AnimData anims;
		
		[System.Serializable]
		public struct SwitchData
		{
			[System.Serializable]
			public struct LogicalData
			{
				public Sprite ON, OFF;
			}

			public LogicalData none, yellow, red, green, violet, pink;
		}
		public SwitchData switchRail, switchMaze, switchWaterFlow, switchBox, switchPinWheel;
	}
}
