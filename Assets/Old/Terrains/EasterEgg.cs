using UnityEngine;


namespace Script.Terrains
{
	public class EasterEgg : Platform
	{
		public bool hasEgg;

		[SerializeField]
		private Sprite spriteHasEgg, spriteNone;

		public static int count;


		/// <summary>
		/// Khởi tạo Easter Egg
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="hasEgg">if set to <c>true</c> [has egg].</param>
		public void Init(Name name, bool hasEgg)
		{
			Init(name);
			this.hasEgg = hasEgg;
			++count;
		}
	}
}