using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class RailRoadButton : Platform
	{
		public bool isOn { get; private set; }

		public static readonly List<RailRoadButton> list = new List<RailRoadButton>();

		[SerializeField] private Sprite ONSprite, OFFSprite;


		public static new RailRoadButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
