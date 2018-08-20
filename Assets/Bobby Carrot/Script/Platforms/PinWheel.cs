using UnityEngine;
using System;
using System.Collections.Generic;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class PinWheel : Platform, IButtonProcessor
	{
		public Color color { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private PinWheelColor_GameObject_Dict winds;

		public static readonly IReadOnlyDictionary<Color, List<IButtonProcessor>> dict = new Dictionary<Color, List<IButtonProcessor>>()
		{
			[Color.GREEN] = new List<IButtonProcessor>(),
			[Color.RED] = new List<IButtonProcessor>(),
			[Color.VIOLET] = new List<IButtonProcessor>(),
			[Color.YELLOW] = new List<IButtonProcessor>()
		};

		public static readonly IReadOnlyDictionary<Color, Vector3Int> directions = new Dictionary<Color, Vector3Int>()
		{
			[Color.GREEN] = Vector3Int.left,
			[Color.RED] = Vector3Int.down,
			[Color.VIOLET] = Vector3Int.right,
			[Color.YELLOW] = Vector3Int.up
		};

		public enum Color
		{
			YELLOW, RED, GREEN, VIOLET
		}

		private static readonly int COLOR_PARAM = Animator.StringToHash("color"),
			BLOW_PARAM = Animator.StringToHash("blow");


		static PinWheel()
		{
			var colors = Enum.GetValues(typeof(Color));
			Board.onReset += () =>
			  {
				  foreach (Color color in colors) dict[color].Clear();
			  };
		}


		public static new PinWheel DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var pinWheel = Instantiate(R.asset.prefab.pinWheel, wPos, Quaternion.identity);
			switch (ID)
			{
				case 208: pinWheel.color = Color.YELLOW; break;
				case 209: pinWheel.color = Color.RED; break;
				case 210: pinWheel.color = Color.GREEN; break;
				case 211: pinWheel.color = Color.VIOLET; break;
			}

			if (use) pinWheel.Use();
			return pinWheel;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			dict[color].Add(this);
		}


		private void Start()
		{
			animator.SetInteger(COLOR_PARAM, (int)color);
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall;


		public void ChangeState()
		{
			if (isOn = !isOn)
			{
				// ON
				animator.SetBool(BLOW_PARAM, true);
				winds[color].SetActive(true);
				Blow();
			}
			else
			{
				// OFF
				animator.SetBool(BLOW_PARAM, false);
				winds[color].SetActive(false);
			}
		}


		private void Blow()
		{
			var direction = directions[color];
			var pos = transform.position.WorldToArray() + direction;

			while (0 <= pos.x && pos.x < array.Length && 0 <= pos.y && pos.y < array[0].Length)
			{
				var platform = array[pos.x][pos.y].Peek();
				pos += direction;
				if (!(platform is MobileCloud)) continue;

				var mobileCloud = (MobileCloud)platform;
				if (mobileCloud.color != color) continue;
				mobileCloud.direction = direction;
				mobileCloud.enabled = true;
			}
		}
	}
}
