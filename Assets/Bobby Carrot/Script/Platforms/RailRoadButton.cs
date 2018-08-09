using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class RailRoadButton : Platform
	{
		public bool isOn { get; private set; }

		public static readonly List<RailRoadButton> list = new List<RailRoadButton>();

		[SerializeField] private Sprite ONSprite, OFFSprite;
	}
}
