using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Mirror : Platform
	{
		public Vector2Int direction { get; private set; }

		[SerializeField] private Vector2Int_Sprite_Dict sprites;
	}
}
