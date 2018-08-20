using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class WaterFlow : Platform, IButtonProcessor
	{
		public Vector3Int direction { get; private set; }

		public static readonly List<WaterFlow> list = new List<WaterFlow>();


		static WaterFlow()
		{
			Board.onReset += () => { list.Clear(); };
			Board.onStart += () =>
			  {
				  foreach (var waterFlow in list) CheckLotusLeaf(waterFlow);
			  };
		}


		public static new WaterFlow DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var waterFlow = Instantiate(R.asset.prefab.waterFlow, wPos, Quaternion.identity);
			switch (ID)
			{
				case 87:
					waterFlow.direction = Vector3Int.down;
					break;

				case 88:
					waterFlow.direction = Vector3Int.up;
					break;

				case 89:
					waterFlow.direction = Vector3Int.right;
					break;

				case 90:
					waterFlow.direction = Vector3Int.left;
					break;
			}

			if (use) waterFlow.Use();
			return waterFlow;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			list.Add(this);
		}


		private void Start()
		{
			animator.SetInteger(Mover.DIR_X, direction.x);
			animator.SetInteger(Mover.DIR_Y, direction.y);
		}


		public void ChangeState()
		{
			direction *= -1;
			animator.SetInteger(Mover.DIR_X, direction.x);
			animator.SetInteger(Mover.DIR_Y, direction.y);
			CheckLotusLeaf(this);
		}


		private static void CheckLotusLeaf(WaterFlow waterFlow)
		{
			var pos = waterFlow.transform.position.WorldToArray();
			var platform = array[pos.x][pos.y].Peek();
			if (platform is LotusLeaf)
			{
				var lotusLeaf = (LotusLeaf)platform;
				lotusLeaf.direction = waterFlow.direction;
				lotusLeaf.enabled = true;
			}
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall ||
				(
					mover is LotusLeaf && mover.direction != direction * -1
				);


		public override bool CanExit(Mover mover) => CanEnter(mover);


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			var lotusLeaf = (LotusLeaf)mover;
			lotusLeaf.direction = direction;
			lotusLeaf.enabled = true;
		}
	}
}
