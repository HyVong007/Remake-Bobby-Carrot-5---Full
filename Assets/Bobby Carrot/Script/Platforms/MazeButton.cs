﻿using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class MazeButton : Platform
	{
		public bool isOn { get; private set; }

		public static readonly List<MazeButton> list = new List<MazeButton>();

		[SerializeField] private Sprite ONSprite, OFFSprite;
	}
}
