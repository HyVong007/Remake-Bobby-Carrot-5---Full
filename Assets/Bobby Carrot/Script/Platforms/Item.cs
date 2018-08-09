using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class Item : Platform
	{
		public new Name name { get; private set; }

		public static readonly Dictionary<Name, int> count = new Dictionary<Name, int>();

		public enum Name
		{
			ORANGE_MAP, BLUE_MAP, KEY, SPEAKER, MUSIC, SHOE,
			MAGNIFYING_GLASS, SNOW_SCRATCHER, BEAN, GAS, KITE,
			GOLDEN_CARROT, GOLDEN_COIN
		}
	}
}
