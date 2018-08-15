using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot
{
	public static class CommonUtil
	{
		public static Vector3 origin { get; private set; }

		public static readonly Vector3 ZERO_DOT_FIVE = new Vector3(0.5f, 0.5f);


		public static void Init()
		{
			origin = new Vector3(-Level.instance.middleArray.Length, -Level.instance.middleArray[0].Length, 0f) * 0.5f;
		}


		public static Vector3 ArrayToWorld(this Vector3Int array, float z = 0f)
		{
			var result = array + origin + ZERO_DOT_FIVE;
			result.z = z;
			return result;
		}


		public static Vector3Int WorldToArray(this Vector3 world)
		{
			var result = Vector3Int.FloorToInt(world - ZERO_DOT_FIVE - origin);
			result.z = 0;
			return result;
		}


		public static Vector3Int ScreenToArray(this Vector3 screen)
		{
			var result = Vector3Int.FloorToInt(R.camera.ScreenToWorldPoint(screen) - origin);
			result.z = 0;
			return result;
		}


		public static Vector3 ScreenToWorld(this Vector3 screen, float z = 0f)
		{
			var result = Vector3Int.FloorToInt(R.camera.ScreenToWorldPoint(screen) - origin) + origin + ZERO_DOT_FIVE;
			result.z = z;
			return result;
		}


		public static bool Contains<T>(this IReadOnlyList<T> list, T item) => ((List<T>)list).Contains(item);


		public static Vector3Int GetInputDirection()
		{
			var dir = Input.GetKey(KeyCode.LeftArrow) ? Vector3Int.left :
				Input.GetKey(KeyCode.RightArrow) ? Vector3Int.right :
				Input.GetKey(KeyCode.UpArrow) ? Vector3Int.up :
				Input.GetKey(KeyCode.DownArrow) ? Vector3Int.down :
				Vector3Int.zero;

			return dir;
		}
	}
}
