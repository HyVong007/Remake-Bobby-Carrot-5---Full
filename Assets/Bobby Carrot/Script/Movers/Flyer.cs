using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Movers
{
	public class Flyer : Mover
	{
		public IReadOnlyDictionary<Vector2Int, Sprite> dirSprites => _dirSprites;

		[SerializeField] private Vector2Int_Sprite_Dict _dirSprites;
	}
}
