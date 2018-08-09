using UnityEngine;
using UnityEngine.Tilemaps;


namespace Script.Tilemaps
{
	[CreateAssetMenu]
	public class LevelTile : Tile
	{
		public new Name name;
		public new Color color;
		public State state;


		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			base.GetTileData(position, tilemap, ref tileData);
			Debug.Log(name + ", " + color + ", " + state + ", " + position);
		}
	}
}
