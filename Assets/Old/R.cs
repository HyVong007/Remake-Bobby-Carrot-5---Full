using UnityEngine;


namespace Script
{
	public class R : MonoBehaviour
	{
		public static Asset asset;

		[SerializeField]
		private Asset _asset;


		private void Awake()
		{
			asset = _asset;
		}
	}
}
