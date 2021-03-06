﻿using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Rock : Platform
	{
		public static new Rock DeSerialize(int ID, Vector3 wPos, bool use = true) =>
			Platform.DeSerialize(ID, wPos, R.asset.prefab.rock);


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall || (mover is GrassMower && mover.speed >= RailRoad.HIGH_SPEED);


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			gameObject.SetActive(false);
			var pos = transform.position.WorldToArray();
			array[pos.x][pos.y].Pop();

			// Shock the Camera

			Destroy(gameObject);
		}
	}
}
