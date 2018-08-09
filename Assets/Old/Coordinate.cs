using UnityEngine;


/* conversion tool :World space (W) , Array space (A), Map space (M)
* A->M, M->W, A->W
* W->M, M->A, W->A
* Deep copy values
*/

namespace Script
{
	public sealed class Coordinate
	{
		public static readonly Vector3 v0dot5 = new Vector3(0.5f, 0.5f, 0);
		public static Vector3 origin;   // left-bottom object.transform.position

		/// <summary>
		/// Convert Array pos to World pos. Deep copy value, can modify world Z
		/// </summary>
		/// <param name="aPos">a position.</param>
		/// <param name="worldZ">The world Z.</param>
		/// <returns></returns>
		public static Vector3 ArrayToWorld(Vector3Int aPos, float worldZ = 0)
		{
			var w = aPos + origin + v0dot5;
			w.z = worldZ;
			return w;
		}


		/// <summary>
		/// Convert World pos to Array pos. Deep copy value.
		/// </summary>
		/// <param name="wPos">The world position.</param>
		/// <returns></returns>
		public static Vector3Int WorldToArray(Vector3 wPos)
		{
			var a = Vector3Int.FloorToInt(wPos - origin - v0dot5);
			a.z = 0;
			return a;
		}


		public static Vector3 MapToWorld(Vector3Int mPos, float worldZ = 0)
		{
			Vector3 w = mPos + v0dot5;
			w.z = worldZ;
			return w;
		}


		public static Vector3Int ParseWorldToArray(Vector3 w)
		{
			var a = Vector3Int.FloorToInt(w - origin);
			a.z = 0;
			return a;
		}
	}


	/// <summary>
	///  Name of all platforms
	/// </summary>
	public enum Name
	{
		NONE,
		OBSTACLE,
		SKY,
		ANIM_STAR,
		SNOW,
		WATER,
		ANIM_WATER,
		WATER_FALL,
		GROUND,
		SLIPPERY_ICE,
		START,
		TARGET,
		KEY,
		SNOW_SCRATCHER,
		GRASS,
		LOCKER,
		BEAN,
		WOOD,
		GAS,
		CLOUD,
		ICY_BLOCK,
		ROCK,
		HOLE,
		KITE,
		WIND,
		WIND_STOP,
		GOLDEN_CARROT,
		GOLDEN_COIN,
		SWITCH_RAIL_ROAD,
		SWITCH_MAZE,
		SWITCH_WATER_FLOW,
		SWITCH_PINWHEEL,
		SWITCH_BOX,
		DRAGON_HEAD,
		DRAGON_BODY,
		DRAGON_TAIL,
		WATER_FLOW,
		RAIL_ROAD,
		MAZE,
		BOX,
		PINWHEEL,
		MOWER_PARK,
		TRAP,
		MIRROR,
		CARROT,
		EASTER_EGG,
		BEAN_TREE,
		LOTUS_LEAF,
		FENCE
	}


	/// <summary>
	/// Color of all platforms
	/// </summary>
	public enum Color
	{
		NONE,
		RED,
		YELLOW,
		GREEN,
		VIOLET,
		PINK
	}


	/// <summary>
	/// State of all platforms
	/// </summary>
	public enum State
	{
		NONE,
		LEFT,
		RIGHT,
		UP,
		DOWN,
		LEFT_UP,
		LEFT_DOWN,
		RIGHT_UP,
		RIGHT_DOWN,
		VERTICAL,
		HORIZONTAL,
		HIDDEN,
		VISIBLE,
		ON,
		OFF
	}
}
