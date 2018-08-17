using UnityEditor;


[CustomPropertyDrawer(typeof(Vector3Int_Sprite_Dict))]
[CustomPropertyDrawer(typeof(PinWheelColor_Sprite_Dict))]
[CustomPropertyDrawer(typeof(PinWheelColor_Anim_Dict))]
[CustomPropertyDrawer(typeof(MazeRotation_Sprite_Dict))]
[CustomPropertyDrawer(typeof(CarrotState_Sprite_Dict))]
[CustomPropertyDrawer(typeof(ItemName_Sprite_Dict))]
[CustomPropertyDrawer(typeof(Vector3Int_Transform_Dict))]
[CustomPropertyDrawer(typeof(MirrorRotation_Sprite_Dict))]
public class MyPropertyDrawer : SerializableDictionaryPropertyDrawer { }
