using UnityEngine;


namespace Script.MovingAgents
{
	public class FireBall : Mover
	{
		private Animator animator;


		private new void Awake()
		{
			base.Awake();
			animator = GetComponent<Animator>();
		}
	}
}
