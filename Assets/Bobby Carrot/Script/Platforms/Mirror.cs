using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Mirror : Platform
	{
		public Rotation rotation { get; private set; }

		public enum Rotation
		{
			RIGHT_DOWN, LEFT_DOWN, RIGHT_UP, LEFT_UP
		}

		[SerializeField] private MirrorRotation_Sprite_Dict sprites;


		private void Start()
		{
			spriteRenderer.sprite = sprites[rotation];
		}


		public static new Mirror DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var mirror = Instantiate(R.asset.prefab.mirror, wPos, Quaternion.identity);
			switch (ID)
			{
				case 177: mirror.rotation = Rotation.RIGHT_DOWN; break;
				case 178: mirror.rotation = Rotation.LEFT_DOWN; break;
				case 179: mirror.rotation = Rotation.RIGHT_UP; break;
				case 180: mirror.rotation = Rotation.LEFT_UP; break;
			}

			if (use) mirror.Use();
			return mirror;
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is Walker || mover is Flyer) return true;
			if (!(mover is FireBall)) return false;

			switch (rotation)
			{
				case Rotation.RIGHT_DOWN:
					return mover.direction == Vector3Int.up || mover.direction == Vector3Int.left;

				case Rotation.LEFT_DOWN:
					return mover.direction == Vector3Int.up || mover.direction == Vector3Int.right;

				case Rotation.RIGHT_UP:
					return mover.direction == Vector3Int.left || mover.direction == Vector3Int.down;

				case Rotation.LEFT_UP:
					return mover.direction == Vector3Int.right || mover.direction == Vector3Int.down;
			}

			return false;
		}


		public override async Task OnExit(Mover mover)
		{
			if (mover is Walker)
			{
				// Spin the rotation by clock-wise
				switch (rotation)
				{
					case Rotation.RIGHT_DOWN: rotation = Rotation.LEFT_DOWN; break;
					case Rotation.LEFT_DOWN: rotation = Rotation.LEFT_UP; break;
					case Rotation.LEFT_UP: rotation = Rotation.RIGHT_UP; break;
					case Rotation.RIGHT_UP: rotation = Rotation.RIGHT_DOWN; break;
				}

				spriteRenderer.sprite = sprites[rotation];
			}
			else if (mover is FireBall)
			{
				// Reflect the fireball's direction
				switch (rotation)
				{
					case Rotation.RIGHT_DOWN:
						mover.direction = (mover.direction == Vector3Int.up) ? Vector3Int.right : Vector3Int.down;
						break;

					case Rotation.LEFT_DOWN:
						mover.direction = (mover.direction == Vector3Int.up) ? Vector3Int.left : Vector3Int.down;
						break;

					case Rotation.LEFT_UP:
						mover.direction = (mover.direction == Vector3Int.right) ? Vector3Int.up : Vector3Int.left;
						break;

					case Rotation.RIGHT_UP:
						mover.direction = (mover.direction == Vector3Int.left) ? Vector3Int.up : Vector3Int.right;
						break;
				}
			}
		}
	}
}
