using UnityEngine;
using Script.Terrains;
using Script.MovingAgents;
using Script.Tilemaps;


namespace Script
{
	public class GameBoard : MonoBehaviour
	{
		public Platform[][] topArray, bottomArray;
		public Mover[][] moverArray;

		public static GameBoard This;


		private void Awake()
		{
			This = this;
		}


		private void Start()
		{
			
		}


		private void Init(Level level)
		{
			var origin = level.bottom.origin;
			var max = level.bottom.size + origin;
			var cell = new Vector3Int();
			var index = new Vector3Int();
			for (cell.x=origin.x, index.x=0;		cell.x<max.x;	++cell.x, ++index.x)
				for (cell.y=origin.y, index.y=0;	cell.y<max.y;	++cell.y, ++index.y)
				{
					var pos = Coordinate.MapToWorld(cell);
					Platform obj = null;
					Sprite ON=null, OFF=null;

					// Quét tile ở dưới
					var tile = level.bottom.GetTile<LevelTile>(cell);
					switch (tile.name)
					{
/*********************************************************************************/
// Khởi tạo Normal Plaform
						case Name.OBSTACLE:
						case Name.SKY:
						case Name.ANIM_STAR:
						case Name.WATER:
						case Name.ANIM_WATER:
						case Name.WATER_FALL:
						case Name.GROUND:
						case Name.SLIPPERY_ICE:
						case Name.START:
						case Name.TARGET:
							obj = Instantiate(R.asset.prefabs.normalPlatform, pos, Quaternion.identity);
							obj.Init(tile.name);
							switch (tile.name)
							{
								case Name.ANIM_STAR:
									obj.animator.runtimeAnimatorController = R.asset.anims.star;
									obj.animator.enabled = true;
									break;
								case Name.ANIM_WATER:
									obj.animator.runtimeAnimatorController = R.asset.anims.water;
									obj.animator.enabled = true;
									break;
								case Name.TARGET:
									obj.animator.runtimeAnimatorController = R.asset.anims.target;
									break;
							}
							break;

/*********************************************************************************/
// Khởi tạo Switch
						case Name.SWITCH_RAIL_ROAD:
							ON = R.asset.switchRail.none.ON; OFF = R.asset.switchRail.none.OFF;
							goto CREATE_SWITCH;
						case Name.SWITCH_MAZE:
							ON = R.asset.switchMaze.none.ON; OFF = R.asset.switchMaze.none.OFF;
							goto CREATE_SWITCH;
						case Name.SWITCH_WATER_FLOW:
							ON = R.asset.switchWaterFlow.none.ON; OFF = R.asset.switchWaterFlow.none.OFF;
							goto CREATE_SWITCH;
						case Name.SWITCH_PINWHEEL:
							var s = R.asset.switchPinWheel;
							switch (tile.color)
							{
								case Color.YELLOW:
									ON = s.yellow.ON; OFF = s.yellow.OFF;
									break;
								case Color.RED:
									ON = s.red.ON; OFF = s.red.OFF;
									break;
								case Color.GREEN:
									ON = s.green.ON; OFF = s.green.OFF;
									break;
								case Color.VIOLET:
									ON = s.violet.ON; OFF = s.violet.OFF;
									break;
							}
							goto CREATE_SWITCH;
						case Name.SWITCH_BOX:
							var sb = R.asset.switchBox;
							if (tile.color==Color.YELLOW)
							{
								ON = sb.yellow.ON; OFF = sb.yellow.OFF;
							}
							else
							{
								ON = sb.pink.ON; OFF = sb.pink.OFF;
							}

							CREATE_SWITCH:

							obj = Instantiate(R.asset.prefabs._switch, pos, Quaternion.identity);
							((Switch)obj).Init(tile.name, tile.state == State.ON, tile.color, ON, OFF);
							break;

/*********************************************************************************/
// Khởi tạo các lớp đặc biệt

						case Name.WATER_FLOW:
							obj = Instantiate(R.asset.prefabs.waterFlow, pos, Quaternion.identity);
							((WaterFlow)obj).Init(tile.name, tile.state);
							break;

						case Name.TRAP:
							obj = Instantiate(R.asset.prefabs.trap, pos, Quaternion.identity);
							((Trap)obj).Init(tile.name, tile.state == State.ON);
							break;

						case Name.MIRROR:
							obj = Instantiate(R.asset.prefabs.mirror, pos, Quaternion.identity);
							((Mirror)obj).Init(tile.name, tile.state);
							break;

						case Name.RAIL_ROAD:
							obj = Instantiate(R.asset.prefabs.railRoad, pos, Quaternion.identity);
							((RailRoad)obj).Init(tile.name, tile.state);
							break;

						case Name.MAZE:
							obj = Instantiate(R.asset.prefabs.maze, pos, Quaternion.identity);
							((Maze)obj).Init(tile.name, tile.state);
							break;

						case Name.BOX:
							obj = Instantiate(R.asset.prefabs.box, pos, Quaternion.identity);
							((Box)obj).Init(tile.name, tile.state == State.ON, tile.color == Color.YELLOW);
							break;
					}
/*********************************************************************************/
// Quét tile ở trên

					tile = level.top.GetTile<LevelTile>(cell);
					switch (tile.name)
					{
						// Quét normal Tile
						case Name.SNOW:
						case Name.GRASS:
						case Name.LOCKER:
						case Name.WOOD:
						case Name.DRAGON_HEAD:
						case Name.DRAGON_BODY:
						case Name.DRAGON_TAIL:
						case Name.CLOUD:
						case Name.HOLE:
						case Name.ICY_BLOCK:
						case Name.ROCK:
						case Name.WIND:
						case Name.WIND_STOP:
						case Name.FENCE:
							obj = Instantiate(R.asset.prefabs.normalPlatform, pos, Quaternion.identity);
							obj.Init(tile.name);
							RuntimeAnimatorController anim = null;
							switch (tile.name)
							{
								case Name.WOOD:
									anim = R.asset.anims.wood;
									break;

								case Name.ICY_BLOCK:
									anim = R.asset.anims.icyBlock;
									break;

								case Name.WIND:
									anim = R.asset.anims.wind;
									break;
							}

							obj.animator.runtimeAnimatorController = anim;
							obj.animator.enabled = false;
							break;

						/*********************************************************************************/
						// Quét Item

						case Name.KEY:
						case Name.SNOW_SCRATCHER:
						case Name.BEAN:
						case Name.GAS:
						case Name.KITE:
						case Name.GOLDEN_CARROT:
						case Name.GOLDEN_COIN:
							obj = Instantiate(R.asset.prefabs.item, pos, Quaternion.identity);
							((Item)obj).Init(tile.name);
							if (tile.name == Name.GOLDEN_COIN)
								obj.animator.runtimeAnimatorController = R.asset.anims.goldenCoin;
							break;

						// Quét tile đặc biệt khác
						case Name.CARROT:
							obj = Instantiate(R.asset.prefabs.carrot, pos, Quaternion.identity);
							((Carrot)obj).Init(tile.name, tile.state);
							break;

						case Name.EASTER_EGG:
							obj = Instantiate(R.asset.prefabs.easterEgg, pos, Quaternion.identity);
							((EasterEgg)obj).Init(tile.name, tile.state==State.ON);
							break;

						case Name.PINWHEEL:
							obj = Instantiate(R.asset.prefabs.pinWheel, pos, Quaternion.identity);
							((PinWheel)obj).Init(tile.name, tile.state == State.ON, tile.color);
							break;

						case Name.BEAN_TREE:
							if (tile.state == State.ON) obj = Instantiate(R.asset.prefabs.beanTree, pos, Quaternion.identity);
							break;

						case Name.LOTUS_LEAF:
							moverArray[index.x][index.y] = Instantiate(R.asset.prefabs.lotusLeaf, pos, Quaternion.identity);
							break;
					}
				}
		}
	}
}
