using UnityEditor;


namespace Test
{
    [CustomPropertyDrawer(typeof(Vector2Int_Sprite_Dict))]
    public class MyPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}
