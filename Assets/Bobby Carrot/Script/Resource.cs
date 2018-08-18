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

			DontDestroyOnLoad(this);
		}
	}



	public static class R
	{
		public static AssetManager asset => Resource.instance?.assetManager;

		public static Camera camera => Resource.instance?.camera;

		public const string BOTTOM_LAYER = "Bottom", MIDDLE_LAYER = "Middle", TOP_LAYER = "Top",
			MOVER_LAYER = "Mover", UI_LAYER = "UI";


		public static bool isGlobalLock { get; set; }
	}
}
