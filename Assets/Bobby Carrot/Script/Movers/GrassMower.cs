using UnityEngine;


namespace BobbyCarrot.Movers
{
	public class GrassMower : Mover
	{
		public static readonly int DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY");

		public bool receiveInput = true;

		public static GrassMower instance { get; private set; }


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
			if (receiveInput && direction == Vector2Int.zero) direction = CommonUtil.GetInputDirection();
			if (direction != Vector2Int.zero)
			{
				// platform.CanExit() & platform.CanEnter() ? If True:
				// platform.OnExit()

				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);

				// move pos and check array
				// platform.OnEnter()
			}
		}


		/// <summary>
		/// Test
		/// </summary>
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				direction = Vector2Int.zero;
				animator.SetInteger(DIR_X, 0);
				animator.SetInteger(DIR_Y, 0);
			}
		}


		public override void Use(Vector3Int? pos = null)
		{
			throw new System.NotImplementedException();
		}
	}
}
