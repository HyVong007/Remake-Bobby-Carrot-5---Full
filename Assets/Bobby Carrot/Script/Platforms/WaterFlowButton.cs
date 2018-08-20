using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;

namespace BobbyCarrot.Platforms
{
	public class WaterFlowButton : Platform, IButtonProcessor
	{
		public bool isOn { get; private set; }

		[SerializeField] private Sprite ONSprite, OFFSprite;

		public static readonly List<IButtonProcessor> list = new List<IButtonProcessor>();


		static WaterFlowButton()
		{
			Board.onReset += () => { list.Clear(); };
		}


		public static new WaterFlowButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var button = Instantiate(R.asset.prefab.button.waterFlow, wPos, Quaternion.identity);
			button.isOn = (ID == 165);
			if (use) button.Use();
			return button;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			list.Add(this);
		}


		private void Start()
		{
			spriteRenderer.sprite = isOn ? ONSprite : OFFSprite;
		}


		public void ChangeState()
		{
			spriteRenderer.sprite = (isOn = !isOn) ? ONSprite : OFFSprite;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker) || isOn) return;

			foreach (var button in list) button.ChangeState();
			foreach (var waterFlow in WaterFlow.list) waterFlow.ChangeState();
		}
	}
}
