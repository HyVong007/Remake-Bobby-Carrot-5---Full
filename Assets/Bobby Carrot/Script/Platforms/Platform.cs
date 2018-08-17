using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using BobbyCarrot.Util;
using System.IO;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public abstract class Platform : MonoBehaviour, IPlatformProcessor, IUsable
	{
		protected int ID;

		public static Stack<IPlatformProcessor>[][] array;

		public SpriteRenderer spriteRenderer => _spriteRenderer;

		public Animator animator => _animator;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Animator _animator;


		public virtual bool CanExit(Mover mover) => true;


		public virtual bool CanEnter(Mover mover) => true;


		public virtual async Task OnExit(Mover mover) { }


		public virtual async Task OnEnter(Mover mover) { }


		public virtual void Use(Vector3Int? pos = null)
		{
			if (pos == null) pos = transform.position.WorldToArray();
			var p = pos.Value;
			array[p.x][p.y].Push(this);
			transform.parent = Board.instance.platformAnchor;
		}


		static Platform()
		{
			Board.onReset += () =>
			  {
				  var size = new Vector2Int(Level.instance.middleArray.Length, Level.instance.middleArray[0].Length);
				  array = new Stack<IPlatformProcessor>[size.x][];
				  for (int x = 0; x < size.x; ++x)
				  {
					  array[x] = new Stack<IPlatformProcessor>[size.y];
					  for (int y = 0; y < size.y; ++y)
						  array[x][y] = new Stack<IPlatformProcessor>();
				  }
			  };
		}


		public static byte[] Serialize(object _obj)
		{
			var obj = (Platform)_obj;
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				w.Write(obj.ID);
				var pos = obj.transform.position;
				w.Write(pos.x); w.Write(pos.y); w.Write(pos.z);

				return m.ToArray();
			}
		}


		public static Platform DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			Platform platform = null;
			if ((0 <= ID && ID <= 76) || (78 <= ID && ID <= 86) || ID == 91 || (249 <= ID && ID <= 254))
				platform = NormalObstacle.DeSerialize(ID, wPos, use);

			else if ((94 <= ID && ID <= 147) || ID == 149 || ID == 150 || ID == 158)
				platform = NormalGround.DeSerialize(ID, wPos, use);

			else if (ID == 77)
				platform = Snow.DeSerialize(ID, wPos, use);

			else if (87 <= ID && ID <= 90)
				platform = WaterFlow.DeSerialize(ID, wPos, use);

			else if (ID == 148)
				platform = SlipperyIce.DeSerialize(ID, wPos, use);

			else if ((151 <= ID && ID <= 157) || ID == 159 || ID == 207 || ID == 221 || ID == 243 || ID == 246 || ID == 248)
				platform = Item.DeSerialize(ID, wPos, use);

			else if (ID == 160)
				platform = GrassMowerStop.DeSerialize(ID, wPos, use);

			else if (ID == 161 || ID == 162)
				platform = RailRoadButton.DeSerialize(ID, wPos, use);

			else if (ID == 163 || ID == 164)
				platform = MazeButton.DeSerialize(ID, wPos, use);

			else if (ID == 165 || ID == 166)
				platform = WaterFlowButton.DeSerialize(ID, wPos, use);

			else if (167 <= ID && ID <= 174)
				platform = PinWheelButton.DeSerialize(ID, wPos, use);

			else if (ID == 175 || ID == 176)
				platform = Trap.DeSerialize(ID, wPos, use);

			else if (177 <= ID && ID <= 180)
				platform = Mirror.DeSerialize(ID, wPos, use);

			else if (181 <= ID && ID <= 184)
				platform = RailRoad.DeSerialize(ID, wPos, use);

			else if (185 <= ID && ID <= 190)
				platform = Maze.DeSerialize(ID, wPos, use);

			else if (191 <= ID && ID <= 194)
				platform = BoxButton.DeSerialize(ID, wPos, use);

			else if (195 <= ID && ID <= 198)
				platform = Box.DeSerialize(ID, wPos, use);

			else if (ID == 199)
				platform = Grass.DeSerialize(ID, wPos, use);

			else if (200 <= ID && ID <= 202)
				platform = Carrot.DeSerialize(ID, wPos, use);

			else if (ID == 203 || ID == 204)
				platform = EasterEgg.DeSerialize(ID, wPos, use);

			else if (ID == 205)
				platform = Locker.DeSerialize(ID, wPos, use);

			else if (ID == 206 || ID == 222 || ID == 223 || ID == 238)
				platform = BeanTree.DeSerialize(ID, wPos, use);

			else if (208 <= ID && ID <= 211)
				platform = PinWheel.DeSerialize(ID, wPos, use);

			else if (ID == 212)
				platform = Wood.DeSerialize(ID, wPos, use);

			else if (215 <= ID && ID <= 217)
				platform = Dragon.DeSerialize(ID, wPos, use);

			else if (ID == 227)
				platform = IcyBlock.DeSerialize(ID, wPos, use);

			else if (ID == 237)
				platform = Rock.DeSerialize(ID, wPos, use);

			else if (240 <= ID && ID <= 242)
				platform = CloudHole.DeSerialize(ID, wPos, use);

			else if (ID == 244 || ID == 245)
				platform = Wind.DeSerialize(ID, wPos, use);

			return platform;
		}


		public static Platform DeSerialize(byte[] data)
		{
			int ID; Vector3 wPos;
			DeSerialize(data, out ID, out wPos);
			return DeSerialize(ID, wPos, false);
		}


		protected static void DeSerialize(byte[] data, out int ID, out Vector3 wPos)
		{
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				ID = r.ReadInt32();
				wPos = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
			}
		}


		protected static T New<T>(int ID, Vector3 wPos, T prefab) where T : Platform
		{
			T obj = Instantiate(prefab, wPos, Quaternion.identity);
			obj.ID = ID;
			obj.spriteRenderer.sprite = R.asset.myTile.platforms[ID].sprite;
			return obj;
		}


		protected static T DeSerialize<T>(int ID, Vector3 wPos, T prefab, bool use = true) where T : Platform
		{
			var obj = Instantiate(prefab, wPos, Quaternion.identity);
			obj.ID = ID;
			if (use) obj.Use();
			return obj;
		}
	}



	public interface IPlatformProcessor
	{
		bool CanExit(Mover mover);

		bool CanEnter(Mover mover);

		Task OnExit(Mover mover);

		Task OnEnter(Mover mover);
	}



	public interface IButtonProcessor
	{
		void ChangeState();
	}



	public interface IUsable
	{
		void Use(Vector3Int? pos = null);
	}
}
