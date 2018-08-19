using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class RailRoad : Platform, IButtonProcessor
	{
		public Vector3Int direction { get; private set; }

		public static readonly List<IButtonProcessor> list = new List<IButtonProcessor>();

		public const float HIGH_SPEED = 0.15f;


		static RailRoad()
		{
			Board.onReset += () => { list.Clear(); };
		}


		public static new RailRoad DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var railRoad = Instantiate(R.asset.prefab.railRoad, wPos, Quaternion.identity);
			switch (ID)
			{
				case 181:
					railRoad.direction = Vector3Int.up;
					break;

				case 182:
					railRoad.direction = Vector3Int.down;
					break;

				case 183:
					railRoad.direction = Vector3Int.left;
					break;

				case 184:
					railRoad.direction = Vector3Int.right;
					break;
			}

			if (use) railRoad.Use();
			return railRoad;
		}


		private void Start()
		{
			animator.SetInteger(Mover.DIR_X, direction.x);
			animator.SetInteger(Mover.DIR_Y, direction.y);
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			list.Add(this);
		}


		public void ChangeState()
		{
			direction *= -1;
			animator.SetInteger(Mover.DIR_X, direction.x);
			animator.SetInteger(Mover.DIR_Y, direction.y);
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud) &&
			(
				mover.direction == direction || mover.direction == direction * -1
			);


		public override bool CanExit(Mover mover) =>
			mover is Flyer || mover is FireBall ||
			(
				(mover is Walker || mover is GrassMower) && mover.direction == direction
			);


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			mover.movingDistance = 3;
			mover.speed = HIGH_SPEED;
			mover.direction = direction;
			await Task.Delay(1);
		}
	}
}
