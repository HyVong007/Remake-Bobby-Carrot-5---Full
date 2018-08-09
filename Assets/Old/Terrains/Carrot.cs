using UnityEngine;


namespace Script.Terrains
{
	public class Carrot : Platform
	{
		public State state;

		[SerializeField]
		private Sprite spriteNone, spriteVisible, spriteHidden;

		public static int count;


		/// <summary>
		/// Khởi tạo Carrot
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="state">The state.</param>
		public void Init(Name name, State state)
		{
			Init(name);
			this.state = state;
			++count;
		}
	}
}