using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class EasterEgg : Platform
	{
		public bool hasEgg { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private Sprite noEggSprite, hasEggSprite;


		private void Start()
		{
			spriteRenderer.sprite = hasEgg ? hasEggSprite : noEggSprite;
			if (!hasEgg) ++countDown;
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			if (mover is Flyer || mover is FireBall) return true;
			return !hasEgg;
		}


		public override void OnEnter(Mover mover)
		{
			if (hasEgg || !(mover is Walker)) return;
			hasEgg = true;
			spriteRenderer.sprite = hasEggSprite;
			--countDown;

			// Check countDown
		}


		static EasterEgg()
		{
			Board.onReset += () => countDown = 0;
		}
	}
}
