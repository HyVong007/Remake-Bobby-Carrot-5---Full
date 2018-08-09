using UnityEngine;


namespace BobbyCarrot.Movers
{
	public class Walker : Mover
	{
		public static readonly int IS_RELAXING = Animator.StringToHash("isRelaxing"),
			DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY"),
			DIE = Animator.StringToHash("die"),
			DISAPPEAR = Animator.StringToHash("disappear"),
			SCRATCH = Animator.StringToHash("scratch");

		[SerializeField] private float relaxDelaySeconds = 5f;
		private float relaxTime = -1f;
	}
}
