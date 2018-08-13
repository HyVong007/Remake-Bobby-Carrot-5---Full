using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class Box : Platform, IButtonProcessor
	{
		public bool isYellow { get; private set; }

		public bool isOn { get; private set; }

		[SerializeField] private Sprite yellowON, yellowOFF, pinkON, pinkOFF;
		private Sprite ON, OFF;
		public static readonly List<IButtonProcessor> yellowList = new List<IButtonProcessor>(), pinkList = new List<IButtonProcessor>();


		private void Start()
		{
			if (isYellow)
			{
				ON = yellowON; OFF = yellowOFF; yellowList.Add(this);
			}
			else
			{
				ON = pinkON; OFF = pinkOFF; pinkList.Add(this);
			}

			spriteRenderer.sprite = isOn ? ON : OFF;
		}


		public void ChangeState() => spriteRenderer.sprite = (isOn = !isOn) ? ON : OFF;


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer) return true;
			if (mover is LotusLeaf || mover is MobileCloud) return false;
			return !isOn;
		}


		static Box()
		{
			Board.onReset += () =>
			  {
				  yellowList.Clear(); pinkList.Clear();
			  };
		}


		public static new Box DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
