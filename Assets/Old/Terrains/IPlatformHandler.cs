using Script.MovingAgents;


namespace Script.Terrains
{
	/// <summary>
	/// Giao diện hành động của platform
	/// </summary>
	public interface IPlatformHandler
	{
		bool CanEnter(Mover mover);
		bool CanExit(Mover mover);
		void OnEnter(Mover mover);
		void OnExit(Mover mover);
	}


	public interface ISwitchHandler
	{
		void ChangeState(bool isOn);
	}
}
