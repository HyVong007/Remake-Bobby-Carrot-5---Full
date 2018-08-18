using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using BobbyCarrot.Platforms;
using System.Threading;


namespace BobbyCarrot.Movers
{
	public class GrassMower : Mover
	{
		public static GrassMower instance { get; private set; }

		[SerializeField] private Vector3Int_Transform_Dict smokeAnchors;
		[SerializeField] private GameObject smokePrefab;


		private System.Action runPlatform;

		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}

			currentSmokeAnchor = smokeAnchors[Vector3Int.up];
			runPlatform = async () =>
			  {
				  bool? result = await RunPlatform();
				  if (result == true) direction = Vector3Int.zero;
				  else if (result == false) GotoIdle(direction);
			  };
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
			if (!isLock && direction == Vector3Int.zero) direction = GetInputDirection();
			if (!isLock && direction != Vector3Int.zero)
			{
				// Process Input
				runPlatform();
			}
		}


		private async void RunPlatform(IPlatformProcessor currentPlatform, IPlatformProcessor nextPlatform)
		{
			await currentPlatform.OnExit(this);
			if (isLock || !gameObject.activeSelf) return;

			await Move();
			await nextPlatform.OnEnter(this);
			if (isLock || !gameObject.activeSelf) return;

			direction = Vector3Int.zero;
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
