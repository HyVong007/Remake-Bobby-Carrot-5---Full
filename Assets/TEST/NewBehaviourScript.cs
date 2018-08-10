using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class NameSpritesAutomatically : MonoBehaviour
{

	[MenuItem("Sprites/Rename Sprites")]
	static void SetSpriteNames()
	{
		Texture2D myTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Bobby Carrot/Texture/Map/terrain.png");

		string path = AssetDatabase.GetAssetPath(myTexture);
		TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
		ti.isReadable = true;
		ti.spriteImportMode = SpriteImportMode.Multiple;

		List<SpriteMetaData> newData = new List<SpriteMetaData>();

		int SliceWidth = 32;
		int SliceHeight = 32;

		int ID = 0;

		for (int i = 0; i < myTexture.width; i += SliceWidth)
		{
			for (int j = myTexture.height; j > 0; j -= SliceHeight)
			{
				SpriteMetaData smd = new SpriteMetaData();
				smd.pivot = new Vector2(0.5f, 0.5f);
				smd.alignment = 9;
				//smd.name = (myTexture.height - j) / SliceHeight + ", " + i / SliceWidth;
				smd.name = (ID++).ToString();
				smd.rect = new Rect(i, j - SliceHeight, SliceWidth, SliceHeight);

				newData.Add(smd);
			}
		}

		ti.spritesheet = newData.ToArray();
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		AssetDatabase.SaveAssets();
		print("Slice xong roi !");
	}
}
