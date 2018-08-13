using UnityEngine;


namespace BobbyCarrot.Movers
{
	public class LotusLeaf : Mover
	{
		public Walker walker { get; private set; }
		public static LotusLeaf[][] array;


		public override void Use(Vector3Int? pos = null)
		{
			throw new System.NotImplementedException();
		}


		public static new LotusLeaf DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
