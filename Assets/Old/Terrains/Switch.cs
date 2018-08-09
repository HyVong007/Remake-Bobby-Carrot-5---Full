using UnityEngine;
using System.Collections.Generic;
using Script.MovingAgents;

namespace Script.Terrains
{
	public class Switch : Platform
	{
		public bool isOn;
		public Color color;

		[SerializeField]
		private Sprite spriteON, spriteOFF;

		public static readonly Dictionary<Name, List<Switch>> all = new Dictionary<Name, List<Switch>>();


		/// <summary>
		/// Khởi tạo Switch
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="isOn">if set to <c>true</c> [is on].</param>
		/// <param name="spriteON">The sprite on.</param>
		/// <param name="spriteOFF">The sprite off.</param>
		public void Init(Name name, bool isOn, Color color, Sprite spriteON, Sprite spriteOFF)
		{
			Init(name);
			this.isOn = isOn; this.color = color;
			this.spriteON = spriteON; this.spriteOFF = spriteOFF;
			if (!all.ContainsKey(name)) all[name] = new List<Switch>();
			all[name].Add(this);
		}
	}
}
