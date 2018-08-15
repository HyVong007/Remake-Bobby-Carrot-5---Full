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

		public static readonly IReadOnlyDictionary<Vector3Int, int> idleDirections = new Dictionary<Vector3Int, int>()
		{
			[Vector3Int.left] = (int)RelaxState.LEFT_IDLE,
			[Vector3Int.right] = (int)RelaxState.RIGHT_IDLE,
			[Vector3Int.up] = (int)RelaxState.UP_IDLE,
			[Vector3Int.down] = (int)RelaxState.DOWN_IDLE
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
				// Check to stop any movement
				// Check to count down relax
				if (animator.GetInteger(RELAX_STATE) != (int)RelaxState.RELAX) CountDownRelax();
			}
			else if (receiveInput)
			{
				// Process Input
				relaxTime = -1f;
				if (animator.GetInteger(RELAX_STATE) == (int)RelaxState.RELAX)
					animator.SetInteger(RELAX_STATE, idleDirections[direction]);

				var pos = transform.position.WorldToArray();
				var platform = Platform.array[pos.x][pos.y].Peek();

				if (!platform.CanExit(this) || !platform.CanEnter(this))
				{
					// Mover cannot go
				}
				else
				{
					// Platform processes the mover
					DoPlatformJobs(platform);
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


		/// <summary>
		/// Test
		/// </summary>
		private void TamLateUpdate()
		{
			var dir = CommonUtil.GetInputDirection();
			if (Input.GetKeyDown(KeyCode.Space) || (dir != Vector3Int.zero && dir != direction))
			{
				direction = Vector3Int.zero;
				relaxTime = -1f;
				animator.SetInteger(RELAX_STATE, (int)RelaxState.DOWN_IDLE);

				if (Input.GetKeyDown(KeyCode.Space))
				{
					animator.SetInteger(DIR_X, 0);
					animator.SetInteger(DIR_Y, 0);
				}
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


		private async void DoPlatformJobs(IPlatformProcessor platform)
		{
			await platform.OnExit(this);
			if (!receiveInput) return;

			await Move();
			await platform.OnEnter(this);
			if (!receiveInput) return;

			direction = Vector3Int.zero;
			animator.SetInteger(DIR_X, 0);
			animator.SetInteger(DIR_Y, 0);
		}


		public async Task Move()
		{
			var index = transform.position.WorldToArray() + direction;
			print("index= " + index);
			if (index.x < 0 || index.x >= Platform.array.Length || index.y < 0 || index.y >= Platform.array[0].Length) return;

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
	}
}
