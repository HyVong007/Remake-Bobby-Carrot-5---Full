using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class BoxButton : Platform
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;
		private Sprite ON, OFF;


		private void Start()
		{
			if (isYellow)
			{
				ON = yellowON; OFF = yellowOFF;
			}
			else
			{
				ON = pinkON; OFF = pinkOFF;
			}

			spriteRenderer.sprite = isOn ? ON : OFF;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override void OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;
			spriteRenderer.sprite = (isOn = !isOn) ? ON : OFF;
			var list = isYellow ? Box.yellowList : Box.pinkList;
			foreach (var box in list) box.ChangeState();
		}


		public static new BoxButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
