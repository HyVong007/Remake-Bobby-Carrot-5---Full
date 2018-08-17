using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class Maze : Platform, IButtonProcessor
	{
		public Rotation rotation { get; private set; }

		[SerializeField] private MazeRotation_Sprite_Dict sprites;

		public static readonly List<IButtonProcessor> list = new List<IButtonProcessor>();

		public enum Rotation
		{
			LEFT_DOWN, RIGHT_DOWN, RIGHT_UP, LEFT_UP, HORIZONTAL, VERTICAL
		}


		static Maze()
		{
			Board.onReset += () =>
			{
				list.Clear();
			};
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[rotation];
		}


		public static new Maze DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var maze = Instantiate(R.asset.prefab.maze, wPos, Quaternion.identity);
			switch (ID)
			{
				case 185: maze.rotation = Rotation.RIGHT_UP; break;
				case 186: maze.rotation = Rotation.LEFT_UP; break;
				case 187: maze.rotation = Rotation.LEFT_DOWN; break;
				case 188: maze.rotation = Rotation.RIGHT_DOWN; break;
				case 189: maze.rotation = Rotation.VERTICAL; break;
				case 190: maze.rotation = Rotation.HORIZONTAL; break;
			}

			if (use) maze.Use();
			return maze;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			list.Add(this);
		}


		public void ChangeState()
		{
			// Spin the rotation by clock-wise
			switch (rotation)
			{
				case Rotation.RIGHT_UP: rotation = Rotation.RIGHT_DOWN; break;
				case Rotation.RIGHT_DOWN: rotation = Rotation.LEFT_DOWN; break;
				case Rotation.LEFT_DOWN: rotation = Rotation.LEFT_UP; break;
				case Rotation.LEFT_UP: rotation = Rotation.RIGHT_UP; break;
				case Rotation.HORIZONTAL: rotation = Rotation.VERTICAL; break;
				case Rotation.VERTICAL: rotation = Rotation.HORIZONTAL; break;
			}

			spriteRenderer.sprite = sprites[rotation];
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;
			if (!(mover is Walker)) return false;

			var dir = mover.direction;
			switch (rotation)
			{
				case Rotation.RIGHT_UP:
					return dir == Vector3Int.left || dir == Vector3Int.down;

				case Rotation.LEFT_UP:
					return dir == Vector3Int.right || dir == Vector3Int.down;

				case Rotation.LEFT_DOWN:
					return dir == Vector3Int.right || dir == Vector3Int.up;

				case Rotation.RIGHT_DOWN:
					return dir == Vector3Int.left || dir == Vector3Int.up;

				case Rotation.HORIZONTAL:
					return dir == Vector3Int.left || dir == Vector3Int.right;

				case Rotation.VERTICAL:
					return dir == Vector3Int.up || dir == Vector3Int.down;
			}

			return false;
		}


		public override bool CanExit(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;

			var dir = mover.direction;
			switch (rotation)
			{
				case Rotation.RIGHT_UP:
					return dir == Vector3Int.right || dir == Vector3Int.up;

				case Rotation.LEFT_UP:
					return dir == Vector3Int.left || dir == Vector3Int.up;

				case Rotation.RIGHT_DOWN:
					return dir == Vector3Int.right || dir == Vector3Int.down;

				case Rotation.LEFT_DOWN:
					return dir == Vector3Int.left || dir == Vector3Int.down;

				case Rotation.HORIZONTAL:
					return dir == Vector3Int.right || dir == Vector3Int.left;

				case Rotation.VERTICAL:
					return dir == Vector3Int.up || dir == Vector3Int.down;
			}

			return false;
		}


		public override async Task OnExit(Mover mover)
		{
			if (!(mover is Walker)) return;
			ChangeState();
		}
	}
}
