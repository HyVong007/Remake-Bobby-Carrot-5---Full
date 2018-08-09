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
	}
}
