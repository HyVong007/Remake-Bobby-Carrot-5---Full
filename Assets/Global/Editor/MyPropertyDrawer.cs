using UnityEditor;


[CustomPropertyDrawer(typeof(Vector2Int_Sprite_Dict))]
[CustomPropertyDrawer(typeof(PinWheelColor_Sprite_Dict))]
[CustomPropertyDrawer(typeof(PinWheelColor_Anim_Dict))]
[CustomPropertyDrawer(typeof(MazeShape_Sprite_Dict))]
[CustomPropertyDrawer(typeof(CarrotState_Sprite_Dict))]
[CustomPropertyDrawer(typeof(ItemName_Sprite_Dict))]
public class MyPropertyDrawer : SerializableDictionaryPropertyDrawer { }
