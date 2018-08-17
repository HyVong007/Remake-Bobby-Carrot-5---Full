using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Trap : Platform
	{
		public bool isOn { get; private set; }

		[SerializeField] private Sprite ONSprite, OFFSprite;


		private void Start()
		{
			spriteRenderer.sprite = isOn ? ONSprite : OFFSprite;
		}


		public static new Trap DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var trap = Instantiate(R.asset.prefab.trap, wPos, Quaternion.identity);
			trap.isOn = (ID == 175);
			if (use) trap.Use();
			return trap;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker) || !isOn) return;

			// Walker is dead => End Level.
			var walker = (Walker)mover;
			walker.Die();
		}


		public override async Task OnExit(Mover mover)
		{
			if (!(mover is Walker)) return;

			isOn = true;
			spriteRenderer.sprite = ONSprite;
		}
	}
}
