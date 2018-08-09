using UnityEngine;
using Script.MovingAgents;


namespace Script.Terrains
{
	public class Platform : MonoBehaviour, IPlatformHandler
	{
		public new Name name;
		public SpriteRenderer spriteRenderer;
		public Animator animator;


		/// <summary>
		/// Khởi tạo platform
		/// </summary>
		/// <param name="name">The name.</param>
		public virtual void Init(Name name)
		{
			this.name = name;
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
		}


		public virtual bool CanEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}

		public virtual bool CanExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public virtual void OnEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public virtual void OnExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}
	}
}
