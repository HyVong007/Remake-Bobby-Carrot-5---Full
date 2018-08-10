using UnityEngine;
using BobbyCarrot.Platforms;


[System.Serializable]
public class Vector2Int_Sprite_Dict : SerializableDictionary<Vector2Int, Sprite> { }

[System.Serializable]
public class PinWheelColor_Sprite_Dict : SerializableDictionary<PinWheel.Color, Sprite> { }

[System.Serializable]
public class PinWheelColor_Anim_Dict : SerializableDictionary<PinWheel.Color, RuntimeAnimatorController> { }

[System.Serializable]
public class MazeShape_Sprite_Dict : SerializableDictionary<Maze.Shape, Sprite> { }

[System.Serializable]
public class CarrotState_Sprite_Dict : SerializableDictionary<Carrot.State, Sprite> { }

[System.Serializable]
public class ItemName_Sprite_Dict : SerializableDictionary<Item.Name, Sprite> { }
