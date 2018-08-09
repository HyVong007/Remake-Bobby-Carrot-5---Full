using UnityEngine;
using System.Collections.Generic;


namespace Script.Terrains
{
	public class Item : Platform
	{
		public static readonly Dictionary<Name, int> count = new Dictionary<Name, int>();


		/// <summary>
		/// Khởi tạo Item
		/// </summary>
		/// <param name="name">The name.</param>
		public override void Init(Name name)
		{
			base.Init(name);
			if (!count.ContainsKey(name)) count[name] = 0;
		}
	}
}
