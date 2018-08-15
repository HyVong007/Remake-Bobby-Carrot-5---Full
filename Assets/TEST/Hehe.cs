using UnityEngine;


public class Hehe : MonoBehaviour
{
	private int count;
	public Kaka kakaPrefab;

	private Kaka kaka;


	private void Awake()
	{
		print("Hehe Awake");
	}


	private void OnEnable()
	{
		print("Hehe OnEnable");
	}


	private void Start()
	{
		print("Hehe Start");
	}


	private void Update()
	{
		if (count++ > 10) return;
		print("Hehe Update");
		if (!kaka) kaka = Instantiate(kakaPrefab);
	}
}
