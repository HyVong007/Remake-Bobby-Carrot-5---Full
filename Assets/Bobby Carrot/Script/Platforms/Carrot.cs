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

		public enum State
		{
			LEAF, HOLE, CARROT
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[state];
			if (state != State.HOLE) ++countDown;
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
					// Anim
					return;

				case State.CARROT:
					state = State.HOLE;
					spriteRenderer.sprite = sprites[State.HOLE];
					--countDown;

					// Check countDown and change NormalGround (End)
					return;
			}
		}


		static Carrot()
		{
			Board.onReset += () => countDown = 0;
		}


		public static new Carrot DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
