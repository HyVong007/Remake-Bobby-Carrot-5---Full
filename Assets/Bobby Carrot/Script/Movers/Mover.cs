using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public abstract class Mover : MonoBehaviour
	{
		public Vector2Int direction;

		public float speed;

		public SpriteRenderer spriteRenderer => _spriteRenderer;

		public Animator animator => _animator;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Animator _animator;


		public abstract void Use(Vector3Int? pos = null);


		public static Mover DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			Mover mover = null;
			if (ID == 236)
				mover = LotusLeaf.DeSerialize(ID, wPos, use);

			else if (224 <= ID && ID <= 226)
				mover = MobileCloud.DeSerialize(ID, wPos, use);

			return mover;
		}
	}
}
