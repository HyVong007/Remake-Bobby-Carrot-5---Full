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

		/*for (int x = 0; x < myTexture.width; x += SliceWidth)
		{
			for (int y = myTexture.height; y > 0; y -= SliceHeight)
			{
				SpriteMetaData smd = new SpriteMetaData();
				smd.pivot = new Vector2(0.5f, 0.5f);
				smd.alignment = 9;
				//smd.name = (myTexture.height - j) / SliceHeight + ", " + i / SliceWidth;
				smd.name = (ID++).ToString();
				smd.rect = new Rect(x, y - SliceHeight, SliceWidth, SliceHeight);

				newData.Add(smd);
			}
		}*/

		for (int y = myTexture.height; y > 0; y -= SliceHeight)
			for (int x = 0; x < myTexture.width; x += SliceWidth)
			{
				SpriteMetaData smd = new SpriteMetaData();
				smd.pivot = new Vector2(0.5f, 0.5f);
				smd.alignment = 9;
				smd.name = (ID++).ToString();
				smd.rect = new Rect(x, y - SliceHeight, SliceWidth, SliceHeight);

				newData.Add(smd);
			}



		ti.spritesheet = newData.ToArray();
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		AssetDatabase.SaveAssets();
		print("Slice xong roi !");
	}
}
