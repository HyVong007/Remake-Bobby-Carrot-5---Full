using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class WaterFlow : Platform, ISwitchHandler
	{
		public State currentState;

		[SerializeField]
		private RuntimeAnimatorController leftAnim, rightAnim, upAnim, downAnim;

		public static readonly List<WaterFlow> all = new List<WaterFlow>();


		/// <summary>
		/// Khởi tạo water flow
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
