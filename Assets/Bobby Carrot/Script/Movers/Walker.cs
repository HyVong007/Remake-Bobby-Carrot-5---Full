using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using BobbyCarrot.Util;


namespace BobbyCarrot.Movers
{
	public class Walker : Mover
	{
		public static readonly int RELAX_STATE = Animator.StringToHash("relax_state"),
			DIE = Animator.StringToHash("die"),
			DISAPPEAR = Animator.StringToHash("disappear"),
			SCRATCH = Animator.StringToHash("scratch");

		public static Walker instance { get; private set; }

		[SerializeField] private float relaxDelaySeconds = 5f;
		private float relaxTime = -1f;

		public enum RelaxState : int
		{
			RELAX, LEFT_IDLE, RIGHT_IDLE, UP_IDLE, DOWN_IDLE
		}

		public static readonly IReadOnlyDictionary<Vector3Int, int> dirToIdle = new Dictionary<Vector3Int, int>()
		{
			[Vector3Int.left] = (int)RelaxState.LEFT_IDLE,
			[Vector3Int.right] = (int)RelaxState.RIGHT_IDLE,
			[Vector3Int.up] = (int)RelaxState.UP_IDLE,
			[Vector3Int.down] = (int)RelaxState.DOWN_IDLE
		};

		public static readonly IReadOnlyDictionary<RelaxState, Vector3Int> idleToDir = new Dictionary<RelaxState, Vector3Int>()
		{
			[RelaxState.LEFT_IDLE] = Vector3Int.left,
			[RelaxState.RIGHT_IDLE] = Vector3Int.right,
			[RelaxState.UP_IDLE] = Vector3Int.up,
			[RelaxState.DOWN_IDLE] = Vector3Int.down
		};


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
			if (!isLock && direction == Vector3Int.zero) direction = GetInputDirection();
			if (direction == Vector3Int.zero)
			{
				// Check to count down relax
				if (animator.GetInteger(RELAX_STATE) != (int)RelaxState.RELAX) CountDownRelax();
			}
			else if (!isLock)
			{
				// Process Input
				relaxTime = -1f;
				if (animator.GetInteger(RELAX_STATE) == (int)RelaxState.RELAX)
					animator.SetInteger(RELAX_STATE, dirToIdle[direction]);

				RunPlatform();
			}
		}


		private new async void RunPlatform()
		{
			bool? result = await base.RunPlatform();

			if (result == true)
			{
				// Normal finish of running from OnExit -> Move -> OnEnter
				animator.SetInteger(DIR_X, 0);
				animator.SetInteger(DIR_Y, 0);
				var idle = dirToIdle[direction];
				direction = Vector3Int.zero;
				animator.SetInteger(RELAX_STATE, idle);
			}
			else if (result == false)
			{
				// Mover cannot go
				var g = GotoIdle((RelaxState)dirToIdle[direction]);
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


		public async Task GotoIdle(RelaxState idleState = RelaxState.DOWN_IDLE)
		{
			if (idleState == RelaxState.RELAX) throw new System.Exception("Idle state cannot be Relax !");
			relaxTime = -1f;
			var dir = new Vector3Int(animator.GetInteger(DIR_X), animator.GetInteger(DIR_Y), 0);

			if (animator.GetInteger(RELAX_STATE) == (int)RelaxState.RELAX)
				animator.SetInteger(RELAX_STATE, (int)idleState);
			else if (dir != Vector3Int.zero)
			{
				animator.SetInteger(DIR_X, 0);
				animator.SetInteger(DIR_Y, 0);
				animator.SetInteger(RELAX_STATE, dirToIdle[dir]);
			}
			else
			{
				var currentIdle = (RelaxState)animator.GetInteger(RELAX_STATE);
				if (idleState != currentIdle)
				{
					var d = idleToDir[idleState];
					animator.SetInteger(DIR_X, d.x);
					animator.SetInteger(DIR_Y, d.y);

					isLock = true;
					await Task.Delay(1);
					isLock = false;

					animator.SetInteger(DIR_X, 0);
					animator.SetInteger(DIR_Y, 0);
					animator.SetInteger(RELAX_STATE, (int)(currentIdle = idleState));
				}
			}

			direction = Vector3Int.zero;
		}


		public async Task ScratchSnow(Vector3Int? direction = null)
		{
			Vector3Int dir = (direction != null) ? direction.Value : this.direction;
			animator.SetBool(SCRATCH, true);
			animator.SetInteger(DIR_X, dir.x);
			animator.SetInteger(DIR_Y, dir.y);
			await Task.Delay(500);
			animator.SetBool(SCRATCH, false);
		}


		public void Die()
		{
			isLock = true;
			animator.SetTrigger(DIE);
			Destroy(gameObject, 3f);
		}
	}
}
