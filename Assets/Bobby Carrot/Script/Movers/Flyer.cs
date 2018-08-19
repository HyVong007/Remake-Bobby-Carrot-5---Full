using UnityEngine;
using System.Threading.Tasks;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Movers
{
	public class Flyer : Mover
	{
		public static Flyer instance { get; private set; }

		[SerializeField] private Vector3Int_Sprite_Dict dirSprites;


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		private void Start()
		{
			spriteRenderer.sprite = dirSprites[direction];
			transform.parent = Board.instance.moverAnchor;
		}


		private void OnEnable()
		{
			movingDistance = 1;
		}


		private Task runningPlatform;

		private void Update()
		{
			if (!isLock && runningPlatform?.IsCompleted != false)
				runningPlatform = RunPlatform();
		}


		private new async Task RunPlatform()
		{
			bool? result = await base.RunPlatform();
			if (result == true)
			{
				movingDistance = 1;
			}
			else if (result == false)
			{
				// The flyer has landed
				Landing();
			}
		}


		public async void Landing()
		{
			gameObject.SetActive(false);
			var walker = Walker.instance;
			walker.transform.position = transform.position;
			walker.direction = direction;
			walker.speed = speed;
			walker.gameObject.SetActive(true);
			var pos = transform.position.WorldToArray();

			// Nếu hàm OnEnter/ OnExit có khóa (lock) thì nó phải chạy Async
			// Và platform phải chịu trách nhiệm hoàn toàn cho Mover,
			// Nếu không khóa (Not lock) thì phải chạy sync.

			walker.isLock = false;
			await Platform.array[pos.x][pos.y].Peek().OnEnter(walker);
			if (walker.isLock || !walker.gameObject.activeSelf || walker.direction == Vector3Int.zero || walker.speed <= 0f) return;

			// Walker có thể/ có khả năng đi thêm 1 bước nữa (do quán tính) theo direction
			// Tự động chạy Walker.Update()
			walker.movingDistance = 1;
		}


		protected override Task Move()
		{
			spriteRenderer.sprite = dirSprites[direction];
			return base.Move();
		}
	}
}
