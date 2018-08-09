using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class Box : Platform, IButtonProcessor
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;

		public static readonly List<Box> yellowBoxs = new List<Box>(), pinkBoxs = new List<Box>();


		public void ChangeState(bool ON_OFF)
		{
			throw new System.NotImplementedException();
		}
	}
}
