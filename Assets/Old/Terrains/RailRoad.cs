using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class RailRoad : Platform, ISwitchHandler
	{
		public State currentState;

		[SerializeField]
		private RuntimeAnimatorController leftAnim, rightAnim, upAnim, downAnim;

		public static readonly List<RailRoad> all = new List<RailRoad>();


		/// <summary>
		/// Khởi tạo Rail Road
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
