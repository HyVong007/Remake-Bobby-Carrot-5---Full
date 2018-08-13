using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Platforms
{
	public class PinWheel : Platform, IButtonProcessor
	{
		public Color color { get; private set; }

		public Vector2Int direction { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private PinWheelColor_Sprite_Dict sprites;
		[SerializeField] private PinWheelColor_Anim_Dict anims;
		[SerializeField] private GameObject horizontalWind, verticalWind;

		public static readonly IReadOnlyDictionary<Color, List<PinWheel>> dict = new Dictionary<Color, List<PinWheel>>()
		{
			[Color.GREEN] = new List<PinWheel>(),
			[Color.RED] = new List<PinWheel>(),
			[Color.VIOLET] = new List<PinWheel>(),
			[Color.YELLOW] = new List<PinWheel>()
		};


		public enum Color
		{
			YELLOW, RED, GREEN, VIOLET
		}


		public void ChangeState()
		{
			throw new System.NotImplementedException();
		}


		public static new PinWheel DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
