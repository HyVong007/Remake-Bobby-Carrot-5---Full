using UnityEngine;
using BobbyCarrot.Platforms;


[System.Serializable]
public class Vector3Int_Sprite_Dict : SerializableDictionary<Vector3Int, Sprite> { }

[System.Serializable]
public class PinWheelColor_Sprite_Dict : SerializableDictionary<PinWheel.Color, Sprite> { }

[System.Serializable]
public class PinWheelColor_Anim_Dict : SerializableDictionary<PinWheel.Color, RuntimeAnimatorController> { }

[System.Serializable]
public class MazeRotation_Sprite_Dict : SerializableDictionary<Maze.Rotation, Sprite> { }

[System.Serializable]
public class CarrotState_Sprite_Dict : SerializableDictionary<Carrot.State, Sprite> { }

[System.Serializable]
public class ItemName_Sprite_Dict : SerializableDictionary<Item.Name, Sprite> { }

[System.Serializable]
public class Vector3Int_Transform_Dict : SerializableDictionary<Vector3Int, Transform> { }

[System.Serializable]
public class MirrorRotation_Sprite_Dict : SerializableDictionary<Mirror.Rotation, Sprite> { }

[System.Serializable]
public class BeanTreeNodeName_Sprite_Dict : SerializableDictionary<BeanTreeNode.Name, Sprite> { }

[System.Serializable]
public class DragonNodeName_Sprite_Dict : SerializableDictionary<DragonNode.Name, Sprite> { }
