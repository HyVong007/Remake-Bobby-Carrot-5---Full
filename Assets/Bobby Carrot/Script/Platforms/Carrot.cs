using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Carrot : Platform
	{
		public State state { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private CarrotState_Sprite_Dict sprites;

		[SerializeField] private GameObject mowingAnim;

		public enum State
		{
			LEAF, HOLE, CARROT
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[state];
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			switch (state)
			{
				case State.HOLE: return true;
				case State.LEAF: return !(mover is Walker);
				case State.CARROT: return !(mover is GrassMower);

				default: return false;
			}
		}


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;
			switch (state)
			{
				case State.HOLE: return;
				case State.LEAF:
					state = State.CARROT;
					spriteRenderer.sprite = sprites[State.CARROT];
					return;

				case State.CARROT:
					state = State.HOLE;
					spriteRenderer.sprite = sprites[State.HOLE];
					--countDown;

					// Check countDown and change NormalGround (End)
					return;
			}
		}


		public override async Task OnExit(Mover mover)
		{
			if (!(mover is GrassMower) || state != State.CARROT) return;
			Destroy(Instantiate(mowingAnim, transform.position, Quaternion.identity), 3f);
		}


		static Carrot()
		{
			Board.onReset += () => countDown = 0;
		}


		public static new Carrot DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var carrot = Instantiate(R.asset.prefab.carrot, wPos, Quaternion.identity);
			switch (ID)
			{
				case 200: carrot.state = State.LEAF; break;
				case 201: carrot.state = State.HOLE; break;
				case 202: carrot.state = State.CARROT; break;
			}

			if (use) carrot.Use();
			return carrot;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			if (state != State.HOLE) ++countDown;
		}
	}
}
