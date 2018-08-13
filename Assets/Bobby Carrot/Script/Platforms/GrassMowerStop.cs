using UnityEngine;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class GrassMowerStop : Platform
	{
		public GrassMower grassMower { get; private set; }

		[SerializeField] private GameObject mowerUI;


		public static new GrassMowerStop DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
