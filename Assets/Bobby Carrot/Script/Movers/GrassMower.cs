using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using BobbyCarrot.Platforms;
using System.Threading;


namespace BobbyCarrot.Movers
{
	public class GrassMower : Mover
	{
		public static readonly int DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY");

		public static GrassMower instance { get; private set; }

		[SerializeField] private Vector3Int_Transform_Dict smokeAnchors;
		[SerializeField] private GameObject smokePrefab;


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}

			currentSmokeAnchor = smokeAnchors[Vector3Int.up];
		}


		private void OnEnable()
		{
			cts.Cancel();
			cts = new CancellationTokenSource();
			SpawnSmoke(cts.Token);
		}


		private void Start()
		{
			transform.parent = Board.instance.moverAnchor;
		}


		private void Update()
		{
			if (receiveInput && direction == Vector3Int.zero) direction = CommonUtil.GetInputDirection();
			if (receiveInput && direction != Vector3Int.zero)
			{
				// Process Input
				var pos = transform.position.WorldToArray();
				var nextPos = pos + direction;

				bool canGo = false;
				if (0 <= nextPos.x && nextPos.x < Platform.array.Length && 0 <= nextPos.y && nextPos.y < Platform.array[0].Length)
				{
					var currentPlatform = Platform.array[pos.x][pos.y].Peek();
					var nextPlatform = Platform.array[nextPos.x][nextPos.y].Peek();
					if (currentPlatform.CanExit(this) && nextPlatform.CanEnter(this))
					{
						// Platform processes mover
						canGo = true;
						RunPlatform(currentPlatform, nextPlatform);
					}
				}

				if (!canGo)
				{
					// Mover cannot go
					GotoIdle(direction);
				}
			}
		}


		/// <summary>
		/// Test
		/// </summary>
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				direction = Vector3Int.zero;
				animator.SetInteger(DIR_X, 0);
				animator.SetInteger(DIR_Y, 0);
			}
		}


		public static byte[] Serialize(object _obj)
		{
			var obj = (GrassMower)_obj;
			BinaryWriter[] w = null;
			var s = Serialize(obj, w);
			s.MoveNext(); s.MoveNext(); s.MoveNext();

			return s.Current;
		}


		public static GrassMower DeSerialize(byte[] data)
		{
			BinaryReader[] r = null;
			var d = DeSerialize(data, R.asset.prefab.grassMower, r);
			d.MoveNext(); d.MoveNext();

			return d.Current;
		}


		private async void RunPlatform(IPlatformProcessor currentPlatform, IPlatformProcessor nextPlatform)
		{
			await currentPlatform.OnExit(this);
			if (!receiveInput || !gameObject.activeSelf) return;

			await Move();
			await nextPlatform.OnEnter(this);
			if (!receiveInput || !gameObject.activeSelf) return;

			direction = Vector3Int.zero;
		}


		public async Task Move()
		{
			receiveInput = false;
			var dir = new Vector3Int(animator.GetInteger(DIR_X), animator.GetInteger(DIR_Y), 0);
			if (dir != direction)
			{
				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);
				currentSmokeAnchor = smokeAnchors[direction];
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


		public void GotoIdle(Vector3Int direction)
		{
			var dir = new Vector3Int(animator.GetInteger(DIR_X), animator.GetInteger(DIR_Y), 0);
			if (dir != direction)
			{
				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);
				currentSmokeAnchor = smokeAnchors[direction];
			}

			this.direction = Vector3Int.zero;
		}


		private Transform currentSmokeAnchor;
		private CancellationTokenSource cts = new CancellationTokenSource();
		[SerializeField] private float spawnSmokeDelaySeconds = 0.5f, smokeLifeTime = 2f;

		private async void SpawnSmoke(CancellationToken token)
		{
			int delayMS = Mathf.CeilToInt(spawnSmokeDelaySeconds * 1000f);
			while (gameObject.activeSelf && !token.IsCancellationRequested)
			{
				Destroy(Instantiate(smokePrefab, currentSmokeAnchor.position, Quaternion.identity), smokeLifeTime);
				await Task.Delay(delayMS);
			}
		}
	}
}
