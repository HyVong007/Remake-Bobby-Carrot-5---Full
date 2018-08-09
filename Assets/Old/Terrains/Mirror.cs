using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class Mirror : Platform
	{
		public State currentState;

		[SerializeField]
		private State[] states;

		[SerializeField]
		private Sprite[] sprites;

		private int currentIndex;


		/// <summary>
		/// Khởi tạo Mirror
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="currentState">State of the current.</param>
		public void Init(Name name, State currentState)
		{
			Init(name);
			this.currentState = currentState;
		}
	}
}
