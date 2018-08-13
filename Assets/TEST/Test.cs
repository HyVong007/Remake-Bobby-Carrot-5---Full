using UnityEngine;
using BobbyCarrot;


public class Test : MonoBehaviour
{
	public Level level;


	private void Awake()
	{
		DontDestroyOnLoad(Level.instance = Level.DeSerialize(Level.Serialize(level)));
		Destroy(level.gameObject);
	}
}
