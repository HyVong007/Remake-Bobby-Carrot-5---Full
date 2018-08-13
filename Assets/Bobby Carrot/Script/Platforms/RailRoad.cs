using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class RailRoad : Platform, IButtonProcessor
	{
		public Vector2Int direction { get; private set; }

		public static readonly List<RailRoad> list = new List<RailRoad>();


		public void ChangeState()
		{
			throw new System.NotImplementedException();
		}


		public static new RailRoad DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
