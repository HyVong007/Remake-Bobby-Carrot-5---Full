using UnityEngine;
using System;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class PinWheelButton : Platform, IButtonProcessor
	{
		public PinWheel.Color color { get; private set; }

		public bool isOn { get; private set; }

		protected static readonly IReadOnlyDictionary<PinWheel.Color, List<PinWheelButton>> dict = new Dictionary<PinWheel.Color, List<PinWheelButton>>()
		{
			[PinWheel.Color.GREEN] = new List<PinWheelButton>(),
			[PinWheel.Color.RED] = new List<PinWheelButton>(),
			[PinWheel.Color.VIOLET] = new List<PinWheelButton>(),
			[PinWheel.Color.YELLOW] = new List<PinWheelButton>()
		};

		[SerializeField] private PinWheelColor_Sprite_Dict ONSprites, OFFSprites;
		private Sprite ON, OFF;


		static PinWheelButton()
		{
			var colors = Enum.GetValues(typeof(PinWheel.Color));
			Board.onReset += () =>
			  {
				  foreach (PinWheel.Color color in colors) dict[color].Clear();
			  };

			Board.onStart += () =>
			  {
				  foreach (PinWheel.Color color in colors)
				  {
					  var buttonList = dict[color];
					  if (buttonList.Count == 0 || !buttonList[0].isOn) continue;

					  var wheelList = PinWheel.dict[color];
					  foreach (var pinWheel in wheelList) pinWheel.ChangeState();
				  }
			  };
		}


		public static new PinWheelButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var button = Instantiate(R.asset.prefab.button.pinWheel, wPos, Quaternion.identity);
			switch (ID)
			{
				case 167:
					button.color = PinWheel.Color.YELLOW;
					button.isOn = true;
					break;

				case 168:
					button.color = PinWheel.Color.YELLOW;
					button.isOn = false;
					break;

				case 169:
					button.color = PinWheel.Color.RED;
					button.isOn = true;
					break;

				case 170:
					button.color = PinWheel.Color.RED;
					button.isOn = false;
					break;

				case 171:
					button.color = PinWheel.Color.GREEN;
					button.isOn = true;
					break;

				case 172:
					button.color = PinWheel.Color.GREEN;
					button.isOn = false;
					break;

				case 173:
					button.color = PinWheel.Color.VIOLET;
					button.isOn = true;
					break;

				case 174:
					button.color = PinWheel.Color.VIOLET;
					button.isOn = false;
					break;
			}

			if (use) button.Use();
			return button;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			dict[color].Add(this);
		}


		private void Start()
		{
			ON = ONSprites[color]; OFF = OFFSprites[color];
			spriteRenderer.sprite = isOn ? ON : OFF;
		}


		public void ChangeState()
		{
			spriteRenderer.sprite = (isOn = !isOn) ? ON : OFF;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker)) return;

			var buttonList = dict[color];
			foreach (var button in buttonList) button.ChangeState();
			var wheelList = PinWheel.dict[color];
			foreach (var pinWheel in wheelList) pinWheel.ChangeState();
		}
	}
}
