using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class PinWheel : Platform, ISwitchHandler
	{
		public bool isOn;
		public Color color;

		[SerializeField]
		private GameObject horizontalWind, verticalWind;

		public static readonly Dictionary<Color, List<PinWheel>> all = new Dictionary<Color, List<PinWheel>>();


		/// <summary>
		/// Khởi tạo Pin Wheel
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="isOn">if set to <c>true</c> [is on].</param>
		/// <param name="color">The color.</param>
		public void Init(Name name, bool isOn, Color color)
		{
			Init(name);
			this.isOn = isOn; this.color = color;
			if (!all.ContainsKey(color)) all[color] = new List<PinWheel>();
			all[color].Add(this);
		}


		public void ChangeState(bool isOn)
		{
			throw new System.NotImplementedException();
		}
	}
}
