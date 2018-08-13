using UnityEngine;


namespace BobbyCarrot.Movers
{
	public class Walker : Mover
	{
		public static readonly int RELAX_STATE = Animator.StringToHash("relax_state"),
			DIE = Animator.StringToHash("die"),
			DISAPPEAR = Animator.StringToHash("disappear"),
			DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY"),
			SCRATCH = Animator.StringToHash("scratch");

		public bool receiveInput = true;

		public static Walker instance { get; private set; }

		[SerializeField] private float relaxDelaySeconds = 5f;
		private float relaxTime = -1f;

		public enum RelaxState : int
		{
			RELAX, LEFT_IDLE, RIGHT_IDLE, UP_IDLE, DOWN_IDLE
		}


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		private void Update()
		{
			if (receiveInput && direction == Vector2Int.zero) direction = CommonUtil.GetInputDirection();
			if (direction == Vector2Int.zero)
			{
				// Check to stop any movement
				// Check to count down relax
				if (animator.GetInteger(RELAX_STATE) != (int)RelaxState.RELAX) CountDownRelax();
			}
			else if (receiveInput)
			{
				// Process Input
				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);
				animator.SetInteger(RELAX_STATE, (int)RelaxState.DOWN_IDLE);
				relaxTime = -1f;
			}
		}


		private void CountDownRelax()
		{
			if (relaxTime < 0f) relaxTime = Time.time + relaxDelaySeconds;
			else if (Time.time >= relaxTime)
			{
				relaxTime = -1f;
				animator.SetInteger(RELAX_STATE, (int)RelaxState.RELAX);
			}
		}


		/// <summary>
		/// Test
		/// </summary>
		private void LateUpdate()
		{
			var dir = CommonUtil.GetInputDirection();
			if (Input.GetKeyDown(KeyCode.Space) || (dir != Vector2Int.zero && dir != direction))
			{
				direction = Vector2Int.zero;
				relaxTime = -1f;
				animator.SetInteger(RELAX_STATE, (int)RelaxState.DOWN_IDLE);

				if (Input.GetKeyDown(KeyCode.Space))
				{
					animator.SetInteger(DIR_X, 0);
					animator.SetInteger(DIR_Y, 0);
				}
			}
		}


		public override void Use(Vector3Int? pos = null)
		{
			throw new System.NotImplementedException();
		}
	}
}
