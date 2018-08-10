using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class Wood : Platform
	{
		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override void OnExit(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			// Remove this out of Platform.array list
			animator.enabled = true;
			Destroy(gameObject, 2f);
		}
	}
}
