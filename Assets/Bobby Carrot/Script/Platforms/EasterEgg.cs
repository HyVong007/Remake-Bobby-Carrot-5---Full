using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class EasterEgg : Platform
	{
		public bool hasEgg { get; private set; }

		public static int countDown { get; private set; }

		[SerializeField] private Sprite noEggSprite, hasEggSprite;


		public static new EasterEgg DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var egg = Instantiate(R.asset.prefab.easterEgg, wPos, Quaternion.identity);
			egg.hasEgg = (ID == 204);
			if (use) egg.Use();
			return egg;
		}



		private void Start()
		{
			spriteRenderer.sprite = hasEgg ? hasEggSprite : noEggSprite;
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			if (mover is Flyer || mover is FireBall) return true;
			return !hasEgg;
		}


		public static event System.Action onAddEgg;

		public override async Task OnExit(Mover mover)
		{
			if (hasEgg || !(mover is Walker)) return;
			hasEgg = true;
			spriteRenderer.sprite = hasEggSprite;
			--countDown;

			onAddEgg?.Invoke();
		}


		static EasterEgg()
		{
			Board.onReset += () => countDown = 0;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			if (!hasEgg) ++countDown;
		}
	}
}
