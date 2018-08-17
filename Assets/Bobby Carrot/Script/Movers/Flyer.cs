using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Movers
{
	public class Flyer : Mover
	{
		public IReadOnlyDictionary<Vector3Int, Sprite> dirSprites => _dirSprites;

		[SerializeField] private Vector3Int_Sprite_Dict _dirSprites;

		public static Flyer instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		private void Start()
		{
			transform.parent = Board.instance.moverAnchor;
		}
	}
}
