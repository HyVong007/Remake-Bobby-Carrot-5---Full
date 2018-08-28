using UnityEngine;
using BobbyCarrot.Platforms;
using System.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	public class LotusLeaf : Mover, IPlatformProcessor, IUsable
	{
		public bool hasWalker { get; private set; }


		public static new LotusLeaf DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = Instantiate(R.asset.prefab.lotusLeaf, wPos, Quaternion.identity);
			if (use) obj.Use();
			return obj;
		}


		private void Start()
		{
			enabled = false;
		}


		public bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall || (mover is Walker && direction == Vector3Int.zero);


		public bool CanExit(Mover mover) => CanEnter(mover);


		public async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			hasWalker = true;
			direction = mover.direction;
			mover.transform.parent = transform;
			R.isGlobalLock = true;
			enabled = true;
		}


		public async Task OnExit(Mover mover)
		{
			if (mover is Walker)
			{
				hasWalker = false;
				mover.transform.parent = Board.instance.moverAnchor;
			}
		}


		public void Use(Vector3Int? pos = null)
		{
			if (pos == null) pos = transform.position.WorldToArray();
			var p = pos.Value;
			Platform.array[p.x][p.y].Push(this);
			transform.parent = Board.instance.moverAnchor;
		}


		private Task runningPlatform;

		private void Update()
		{
			if (!isLock && runningPlatform?.IsCompleted != false)
				runningPlatform = RunPlatform();
		}


		private new async Task RunPlatform()
		{
			var pos = transform.position.WorldToArray();
			var stack = Platform.array[pos.x][pos.y];
			stack.Pop();
			bool? result = await base.RunPlatform();

			if (result == true)
			{
				movingDistance = 1;
			}
			else if (result == false)
			{
				movingDistance = 1;
				stack.Push(this);
				direction = Vector3Int.zero;
				R.isGlobalLock = false;
				enabled = false;
			}
		}


		protected override async Task Move()
		{
			var stop = transform.position.WorldToArray() + direction;
			Platform.array[stop.x][stop.y].Push(this);
			await base.Move();
		}
	}
}
