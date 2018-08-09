using UnityEngine;


namespace Script.Terrains
{
	public class BeanTree : Platform
	{
		public bool isGrowed;
		public int height;

		[SerializeField]
		private Sprite garden, little, root, body, head;


		/// <summary>
		/// Khởi tạo Bean Tree
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="height">The height.</param>
		/// <param name="isGrowed">if set to <c>true</c> [is growed].</param>
		public void Init(Name name, int height, bool isGrowed)
		{
			Init(name);
			this.height = height; this.isGrowed = isGrowed;
		}
	}
}