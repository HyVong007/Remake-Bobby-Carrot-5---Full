using UnityEngine;
using System.Collections.Generic;


namespace Script.MovingAgents
{
	public class ButterFly : MonoBehaviour
	{
		public float speed;

		private SpriteRenderer spriteRenderer;
		private Animator animator;

		public static readonly List<ButterFly> all = new List<ButterFly>();


		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
			all.Add(this);
		}
	}
}
