using UnityEngine;


namespace Test
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public class Walker : MonoBehaviour
	{
		public Vector2Int direction;
		public Animator animator;
		public SpriteRenderer spriteRenderer;
		public float relaxDelay, speed;
		public Vector2Int_Sprite_Dict idleSprites;

		[SerializeField] private float relaxTime = -1f;
		public static readonly int IS_RELAXING = Animator.StringToHash("isRelaxing"),
			DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY"),
			DIE = Animator.StringToHash("die"),
			DISAPPEAR = Animator.StringToHash("disappear"),
			SCRATCH = Animator.StringToHash("scratch");

		
		private void Update()
		{
			if (direction == Vector2Int.zero)
			{
				direction = GetControlDirection();
				if (direction == Vector2Int.zero)
				{
					// Đếm ngược time để relax
					var lastDir = new Vector2Int(animator.GetInteger(DIR_X), animator.GetInteger(DIR_Y));
					bool isRelaxing = animator.GetBool(IS_RELAXING);
					if (!isRelaxing && lastDir != Vector2Int.zero)
					{
						animator.SetInteger(DIR_X, 0);
						animator.SetInteger(DIR_Y, 0);
						spriteRenderer.sprite = idleSprites[lastDir];
					}

					if (!isRelaxing) CountDownRelaxing();
				}
				else
				{
					// Điều khiển U, D, L, R
					if (animator.GetBool(IS_RELAXING)) animator.SetBool(IS_RELAXING, false);
					relaxTime = -1f;

					// CanExit && CanEnter ? => Yes:
					// OnExit

					if (direction.x != animator.GetInteger(DIR_X) || direction.y != animator.GetInteger(DIR_Y))
					{
						spriteRenderer.sprite = idleSprites[direction];
						animator.SetInteger(DIR_X, direction.x);
						animator.SetInteger(DIR_Y, direction.y);
					}

					// Move Transform.position and check array
					// OnEnter
				}
			}
		}


		private void LateUpdate()
		{
			var d = GetControlDirection();
			if ((d != Vector2Int.zero && d != direction) || Input.GetKeyDown(KeyCode.Space))
			{
				direction = Vector2Int.zero;
				//animator.SetInteger(DIR_X, 0);
				//animator.SetInteger(DIR_Y, 0);
				animator.SetBool(IS_RELAXING, false);
				relaxTime = -1f;
			}
		}


		private Vector2Int GetControlDirection()
		{
			Vector2Int dir = Input.GetKey(KeyCode.LeftArrow) ? Vector2Int.left :
				Input.GetKey(KeyCode.RightArrow) ? Vector2Int.right :
				Input.GetKey(KeyCode.UpArrow) ? Vector2Int.up :
				Input.GetKey(KeyCode.DownArrow) ? Vector2Int.down :
				Vector2Int.zero;

			return dir;
		}


		private void CountDownRelaxing()
		{
			print("count down: " + (relaxTime - Time.time));
			if (relaxTime < 0f) relaxTime = Time.time + relaxDelay;
			else if (Time.time >= relaxTime)
			{
				// Chuyển sang relax
				relaxTime = -1f;
				animator.SetBool(IS_RELAXING, true);
			}
		}


		private void HamEvent()
		{
			print("Idle clip chay !");
		}
	}
}

