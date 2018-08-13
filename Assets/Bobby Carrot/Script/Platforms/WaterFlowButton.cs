using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class WaterFlowButton : Platform
	{
		public bool isOn { get; private set; }

		[SerializeField] private Sprite ONSprite, OFFSprite;

		public static readonly List<WaterFlowButton> list = new List<WaterFlowButton>();


		public static new WaterFlowButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
