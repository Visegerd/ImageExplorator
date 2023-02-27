using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/ImageListDatabase")]
public class ImageList : ScriptableObject
{
    public List<ImageInfo> imageList;
    public string folderPath;

    public void ReloadList()
    {
        imageList = new List<ImageInfo>();
        DirectoryInfo files = new DirectoryInfo(folderPath);
        FileInfo[] filesInfo = files.GetFiles("*.png");
        foreach(FileInfo file in filesInfo)
        {
            ImageInfo newImage = new ImageInfo();
            newImage.name = file.ToString();
            for(int i=newImage.name.Length-1;i>=0;i--)
            {
                if(newImage.name[i] == '\\')
                {
                    newImage.name = newImage.name.Substring(i + 1);
                    break;
                }
            }
            newImage.creationTime = File.GetCreationTime(file.ToString());
            Texture2D picture = new Texture2D(1,1);
            picture.LoadImage(File.ReadAllBytes(file.ToString()));
            Sprite sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
            newImage.image = sprite;
            imageList.Add(newImage);
        }
    }
}

public class ImageInfo
{
    public Sprite image;
    public string name;
    public System.DateTime creationTime;
}
