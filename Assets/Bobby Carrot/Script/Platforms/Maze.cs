using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class Maze : Platform, IButtonProcessor
	{
		public Shape shape { get; private set; }

		[SerializeField] private MazeShape_Sprite_Dict sprites;

		public static readonly List<Maze> list = new List<Maze>();

		public enum Shape
		{
			LEFT_DOWN, RIGHT_DOWN, RIGHT_UP, LEFT_UP, HORIZONTAL, VERTICAL
		}


		public void ChangeState()
		{
			throw new System.NotImplementedException();
		}


		public static new GrassMowerStop DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
