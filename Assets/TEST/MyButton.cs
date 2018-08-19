using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MyButton : Button
{
	public Vector3Int DIR;
	public TestTool tool;


	public override void OnPointerDown(PointerEventData eventData)
	{
		tool.direction = DIR;
	}


	public override void OnPointerUp(PointerEventData eventData)
	{
		tool.direction = Vector3Int.zero;
	}
}
