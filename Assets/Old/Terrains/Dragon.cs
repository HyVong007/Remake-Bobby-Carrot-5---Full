using System.Collections.Generic;
using UnityEngine;
using Script.MovingAgents;


namespace Script.Terrains
{
	public class Dragon : MonoBehaviour
	{
		public Vector3Int headPos;

		[SerializeField]
		private FireBall fireBall;

		[SerializeField]
		private GameObject headAnim;

		public static readonly List<Dragon> all = new List<Dragon>();
	}
}
