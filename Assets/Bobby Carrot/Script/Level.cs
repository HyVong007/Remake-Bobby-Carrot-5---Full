using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;


namespace BobbyCarrot
{
	public class Level : MonoBehaviour
	{
		public Tilemap topMap, middleMap, bottomMap;
		public short[][] topArray, middleArray, bottomArray;

		public static Level instance;


		private void Awake()
		{

		}


		public static byte[] Serialize(object obj)
		{
			var level = (Level)obj;
			level.topMap.CompressBounds();
			level.middleMap.CompressBounds();
			level.bottomMap.CompressBounds();
			var map = level.bottomMap.gameObject.activeSelf ? level.bottomMap : level.middleMap;
			level.topMap.origin = level.middleMap.origin = level.bottomMap.origin = map.origin;

			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var size = map.size;
				var origin = map.origin;
				var index = new Vector3Int();
				int x, y;

				bool hasBottom = level.bottomMap.gameObject.activeSelf;
				w.Write(hasBottom);
				w.Write(size.x);
				w.Write(size.y);
				for (x = 0, index.x = origin.x; x < size.x; ++x, ++index.x)
					for (y = 0, index.y = origin.y; y < size.y; ++y, ++index.y)
					{
						var tile = level.topMap.GetTile(index);
						w.Write(tile ? Convert.ToInt16(tile.name) : (short)-1);
						tile = level.middleMap.GetTile(index);
						w.Write(tile ? Convert.ToInt16(tile.name) : (short)-1);
						if (hasBottom)
						{
							tile = level.bottomMap.GetTile(index);
							w.Write(tile ? Convert.ToInt16(tile.name) : (short)-1);
						}
					}

				return m.ToArray();
			};
		}


		public static Level DeSerialize(byte[] data)
		{
			var level = Instantiate(R.asset.prefab.levels.empty);

			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				bool hasBottom = r.ReadBoolean();
				//level.bottomMap.gameObject.SetActive(hasBottom);
				var size = new Vector2Int(r.ReadInt32(), r.ReadInt32());
				level.topArray = new short[size.x][];
				level.middleArray = new short[size.x][];
				level.bottomArray = hasBottom ? new short[size.x][] : null;

				for (int x = 0; x < size.x; ++x)
				{
					level.topArray[x] = new short[size.y];
					level.middleArray[x] = new short[size.y];
					if (hasBottom) level.bottomArray[x] = new short[size.y];

					for (int y = 0; y < size.y; ++y)
					{
						level.topArray[x][y] = r.ReadInt16();
						level.middleArray[x][y] = r.ReadInt16();
						if (hasBottom) level.bottomArray[x][y] = r.ReadInt16();
					}
				}
			}

			return level;
		}


		public bool CheckValid() { throw new NotImplementedException(); }


		public override string ToString()
		{
			string s = "Top Array:\n";
			for (int x = 0; x < topArray.Length; ++x)
				for (int y = 0; y < topArray[0].Length; ++y)
					s += $"Top[{x}][{y}]= {topArray[x][y]}, ";

			s += "\nMiddle Array:\n";
			for (int x = 0; x < middleArray.Length; ++x)
				for (int y = 0; y < middleArray[0].Length; ++y)
					s += $"Middle[{x}][{y}]= {middleArray[x][y]}, ";

			s += "\nBottom Array:\n";
			if (bottomArray != null)
			{
				for (int x = 0; x < bottomArray.Length; ++x)
					for (int y = 0; y < bottomArray[0].Length; ++y)
						s += $"Bottom[{x}][{y}]= {bottomArray[x][y]}, ";
			}

			return s;
		}
	}
}
