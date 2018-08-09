using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class WaterFlow : Platform, IButtonProcessor
	{
		public Vector2Int direction { get; private set; }

		public static readonly List<WaterFlow> list = new List<WaterFlow>();


		public void ChangeState(bool ON_OFF)
		{
			throw new System.NotImplementedException();
		}
	}
}
