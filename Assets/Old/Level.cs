using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Script.Tilemaps;


namespace Script
{
	public class Level : MonoBehaviour
	{
		public Tilemap top, bottom;


		private void Awake()
		{
			//CheckValid();
		}


		private void CheckValid()
		{
			var topNames = new List<Name>()
			{
				Name.KEY,           Name.SNOW_SCRATCHER,Name.CARROT,
				Name.EASTER_EGG,    Name.LOCKER,        Name.BEAN,
				Name.PINWHEEL,      Name.WOOD,          Name.DRAGON_HEAD,
				Name.DRAGON_BODY,   Name.DRAGON_TAIL,   Name.LOTUS_LEAF,
				Name.ROCK,          Name.BEAN_TREE,     Name.CLOUD,
				Name.ICY_BLOCK,     Name.HOLE,          Name.WIND,
				Name.WIND_STOP,     Name.GOLDEN_CARROT, Name.GOLDEN_COIN,
				Name.FENCE,         Name.KITE,          Name.GAS,
				Name.GRASS
			};

			bottom.CompressBounds();
			top.size = bottom.size;
			top.origin = bottom.origin;
			var origin = bottom.origin;
			var max = bottom.size + origin;
			var cell = new Vector3Int();
			for (cell.x = origin.x; cell.x < max.x; ++cell.x)
				for (cell.y = origin.y; cell.y < max.y; ++cell.y)
				{
					// Kiểm tra hợp lệ tile ở dưới
					var tile = bottom.GetTile<LevelTile>(cell);
					if (topNames.Contains(tile.name))
						throw new System.Exception("Tile trên đặt ở dưới. " + cell);

					// Kiểm tra hợp lệ tile ở trên
					tile = top.GetTile<LevelTile>(cell);
					if (!topNames.Contains(tile.name))
						throw new System.Exception("Tile dưới đặt ở trên. " + cell);
				}
		}
	}
}
