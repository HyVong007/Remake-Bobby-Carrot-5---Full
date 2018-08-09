using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class Maze : Platform, ISwitchHandler
	{
		public State currentState;

		private State[] states;
		private Sprite[] sprites;
		private int currentIndex;

		public static readonly List<Maze> all = new List<Maze>();


		/// <summary>
		/// Khởi tạo Maze
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="currentState">State of the current.</param>
		public void Init(Name name, State currentState)
		{
			Init(name);
			this.currentState = currentState;
			all.Add(this);
		}


		public void ChangeState(bool isOn)
		{
			throw new System.NotImplementedException();
		}
	}
}
