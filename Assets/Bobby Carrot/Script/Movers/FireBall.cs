using System.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	public class FireBall : Mover
	{
		public static FireBall instance { get; private set; }


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
				// Normal finish of running OnExit -> Move -> OnEnter
				movingDistance = 1;
			}
			else if (result == false)
			{
				// Cannot Go
				gameObject.SetActive(false);
			}
		}
	}
}
