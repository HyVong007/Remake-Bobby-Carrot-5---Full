using UnityEngine;


namespace BobbyCarrot
{
	public class Resource : MonoBehaviour
	{
		public static Resource instance { get; private set; }
		public AssetManager assetManager;
		public new Camera camera;


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}
	}



	public static class R
	{
		public static AssetManager asset => Resource.instance?.assetManager;

		public static Camera camera => Resource.instance?.camera;
	}
}
