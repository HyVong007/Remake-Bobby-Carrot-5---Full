using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class Box : Platform, ISwitchHandler
	{
		public bool isOn, isYellow;

		[SerializeField]
		private Sprite yellowON, yellowOFF, pinkON, pinkOFF;

		public static readonly List<Box> all = new List<Box>();


		public void Init(Name name, bool isOn, bool isYellow)
		{
			Init(name);
			this.isOn = isOn; this.isYellow = isYellow;
			all.Add(this);
		}


		public void ChangeState(bool isOn)
		{
			throw new System.NotImplementedException();
		}
	}
}
