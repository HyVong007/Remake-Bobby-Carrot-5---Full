using UnityEngine;
using System.IO;
using System.Collections.Generic;
using BobbyCarrot.Platforms;
using System.Threading.Tasks;


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

		[System.NonSerialized]
		public bool receiveInput = false;

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
			if (receiveInput && direction == Vector3Int.zero) direction = CommonUtil.GetInputDirection();
			if (direction == Vector3Int.zero)
			{
				// Check to count down relax
				if (animator.GetInteger(RELAX_STATE) != (int)RelaxState.RELAX) CountDownRelax();
			}
			else if (receiveInput)
			{
				// Process Input
				relaxTime = -1f;
				if (animator.GetInteger(RELAX_STATE) == (int)RelaxState.RELAX)
					animator.SetInteger(RELAX_STATE, dirToIdle[direction]);

				var pos = transform.position.WorldToArray();
				var nextPos = pos + direction;

				bool canGo = false;
				if (0 <= nextPos.x && nextPos.x < Platform.array.Length && 0 <= nextPos.y && nextPos.y < Platform.array[0].Length)
				{
					var currentPlatform = Platform.array[pos.x][pos.y].Peek();
					var nextPlatform = Platform.array[nextPos.x][nextPos.y].Peek();
					if (currentPlatform.CanExit(this) && nextPlatform.CanEnter(this))
					{
						// Platform processes the mover
						canGo = true;
						RunPlatform(currentPlatform, nextPlatform);
					}
				}

				if (!canGo)
				{
					// Mover cannot go
					GotoIdle((RelaxState)dirToIdle[direction]);
				}
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


		public static byte[] Serialize(object _obj)
		{
			var obj = (Walker)_obj;
			BinaryWriter[] w = null;
			var s = Serialize(obj, w);
			s.MoveNext();
			w[0].Write(obj.relaxDelaySeconds);
			s.MoveNext(); s.MoveNext();
			return s.Current;
		}


		public static Walker DeSerialize(byte[] data)
		{
			BinaryReader[] r = null;
			var d = DeSerialize(data, R.asset.prefab.walker, r);
			d.MoveNext();
			var obj = d.Current;
			obj.relaxDelaySeconds = r[0].ReadSingle();
			d.MoveNext();

			return obj;
		}


		private async void RunPlatform(IPlatformProcessor currentPlatform, IPlatformProcessor nextPlatform)
		{
			await currentPlatform.OnExit(this);
			if (!receiveInput || !gameObject.activeSelf) return;

			await Move();
			await nextPlatform.OnEnter(this);
			if (!receiveInput || !gameObject.activeSelf) return;

			animator.SetInteger(DIR_X, 0);
			animator.SetInteger(DIR_Y, 0);
			var idle = dirToIdle[direction];
			direction = Vector3Int.zero;
			animator.SetInteger(RELAX_STATE, idle);
		}


		public async Task Move()
		{
			receiveInput = false;
			var dir = new Vector3Int(animator.GetInteger(DIR_X), animator.GetInteger(DIR_Y), 0);
			if (dir != direction)
			{
				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);
			}

			var stop = transform.position + direction;
			while (transform.position != stop)
			{
				transform.position = Vector3.MoveTowards(transform.position, stop, speed);
				await Task.Delay(1);
			}
			transform.position = stop;
			receiveInput = true;
		}


		public async void GotoIdle(RelaxState idleState = RelaxState.DOWN_IDLE)
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

					receiveInput = false;
					await Task.Delay(1);
					receiveInput = true;

					animator.SetInteger(DIR_X, 0);
					animator.SetInteger(DIR_Y, 0);
					animator.SetInteger(RELAX_STATE, (int)(currentIdle = idleState));
				}
			}

			direction = Vector3Int.zero;
		}
	}
}
