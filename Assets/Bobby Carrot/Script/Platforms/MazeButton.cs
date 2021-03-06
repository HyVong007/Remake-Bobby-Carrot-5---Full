﻿using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class MazeButton : Platform, IButtonProcessor
	{
		public bool isOn { get; private set; }

		public static readonly List<IButtonProcessor> list = new List<IButtonProcessor>();

		[SerializeField] private Sprite ONSprite, OFFSprite;


		static MazeButton()
		{
			Board.onReset += () =>
			  {
				  list.Clear();
			  };
		}


		private void Start()
		{
			spriteRenderer.sprite = isOn ? ONSprite : OFFSprite;
		}


		public static new MazeButton DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var button = Instantiate(R.asset.prefab.button.maze, wPos, Quaternion.identity);
			button.isOn = (ID == 163);
			if (use) button.Use();
			return button;
		}


		public override void Use(Vector3Int? pos = null)
		{
			base.Use(pos);
			list.Add(this);
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (!(mover is Walker) || isOn) return;

			foreach (var button in list) button.ChangeState();
			foreach (var maze in Maze.list) maze.ChangeState();
		}


		public void ChangeState()
		{
			spriteRenderer.sprite = (isOn = !isOn) ? ONSprite : OFFSprite;
		}
	}
}
