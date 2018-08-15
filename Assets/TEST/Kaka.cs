using UnityEngine;


public class Kaka : MonoBehaviour
{
	private int count;


	private void Awake()
	{
		print("Kaka Awake");
	}


	private void OnEnable()
	{
		print("Kaka OnEnable");
	}


	private void Start()
	{
		print("Kaka Start");
	}


	private void Update()
	{
		if (count++ > 10) return;
		print("Kaka Update");
	}
}
