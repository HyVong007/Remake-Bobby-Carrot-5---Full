using UnityEngine;
using BobbyCarrot.Platforms;
using System.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public abstract class Mover : MonoBehaviour
	{
		[System.NonSerialized]
		public Vector3Int direction;

		[System.NonSerialized]
		public bool isLock;

		public float speed;

		public SpriteRenderer spriteRenderer => _spriteRenderer;

		public Animator animator => _animator;

		protected static readonly int DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY");

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Animator _animator;


		public static Mover DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			Mover mover = null;
			if (ID == 236)
				mover = LotusLeaf.DeSerialize(ID, wPos, use);

			else if (224 <= ID && ID <= 226)
				mover = MobileCloud.DeSerialize(ID, wPos, use);

			return mover;
		}


		protected async Task<bool?> RunPlatform()
		{
			var pos = transform.position.WorldToArray();
			var nextPos = pos + direction;
			var array = Platform.array;

			if (0 <= nextPos.x && nextPos.x < array.Length && 0 <= nextPos.y && nextPos.y < array[0].Length)
			{
				var currentPlatform = array[pos.x][pos.y].Peek();
				var nextPlatform = array[nextPos.x][nextPos.y].Peek();
				if (currentPlatform.CanExit(this) && nextPlatform.CanEnter(this))
				{
					await currentPlatform.OnExit(this);
					if (isLock || !gameObject.activeSelf || direction == Vector3Int.zero || speed <= 0f) return null;

					await Move();
					await nextPlatform.OnEnter(this);
					if (isLock || !gameObject.activeSelf || direction == Vector3Int.zero || speed <= 0f) return null;

					return true;
				}
			}

			return false;
		}


		protected virtual async Task Move()
		{
			isLock = true;
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
			isLock = false;
		}


		protected static Vector3Int GetInputDirection()
		{
			if (R.isGlobalLock) return Vector3Int.zero;

			var dir = Input.GetKey(KeyCode.LeftArrow) ? Vector3Int.left :
				Input.GetKey(KeyCode.RightArrow) ? Vector3Int.right :
				Input.GetKey(KeyCode.UpArrow) ? Vector3Int.up :
				Input.GetKey(KeyCode.DownArrow) ? Vector3Int.down :
				Vector3Int.zero;

			return dir;
		}
	}
}
