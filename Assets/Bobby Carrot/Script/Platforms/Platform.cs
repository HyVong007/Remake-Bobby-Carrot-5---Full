using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public abstract class Platform : MonoBehaviour, IPlatformProcessor
	{
		public static List<IPlatformProcessor>[][] array;

		public SpriteRenderer spriteRenderer => _spriteRenderer;

		public Animator animator => _animator;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Animator _animator;


		public virtual bool CanExit(Mover mover)
		{
			return true;
		}


		public virtual bool CanEnter(Mover mover)
		{
			return true;
		}


		public virtual void OnExit(Mover mover) { }


		public virtual void OnEnter(Mover mover) { }
	}



	public interface IPlatformProcessor
	{
		bool CanExit(Mover mover);

		bool CanEnter(Mover mover);

		void OnExit(Mover mover);

		void OnEnter(Mover mover);
	}



	public interface IButtonProcessor
	{
		void ChangeState(bool ON_OFF);
	}
}
