using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class Grass : Platform
	{
		public override bool CanEnter(Mover mover) =>
			mover is GrassMower || mover is Flyer || mover is FireBall;


		public override void OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			// Remove this out of Platform array list
			// Anim
			Destroy(gameObject, 2f);
		}
	}
}
