using UnityEngine;


namespace Script.MovingAgents
{
	public abstract class Mover : MonoBehaviour
	{
		protected Vector3Int velocity;
		protected float speed;
		protected SpriteRenderer spriteRenderer;


		protected void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}
}
