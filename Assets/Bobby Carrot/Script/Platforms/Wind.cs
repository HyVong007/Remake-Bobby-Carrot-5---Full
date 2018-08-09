using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Wind : Platform
	{
		public bool isStop { get; private set; }
		[SerializeField] Sprite windSource, windStop;
	}
}
