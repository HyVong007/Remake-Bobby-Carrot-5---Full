using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Wind : Platform
	{
		public bool isStop { get; private set; }
		[SerializeField] Sprite windSource, windStop;


		public static new Wind DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
