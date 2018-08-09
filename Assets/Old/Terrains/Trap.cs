using UnityEngine;


namespace Script.Terrains
{
	public class Trap : Platform
	{
		public bool isOn;

		[SerializeField]
		private Sprite spriteON, spriteOFF;


		/// <summary>
		/// Khởi tạo Trap
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="isOn">if set to <c>true</c> [is on].</param>
		public void Init(Name name, bool isOn)
		{
			Init(name);
			this.isOn = isOn;
		}
	}
}
