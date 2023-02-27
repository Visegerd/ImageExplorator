using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageListController : MonoBehaviour
{
    public ImageList imageDatabase;
    public GameObject container;
    public GameObject imageElementPrefab;

    private void Awake()
    {
        imageDatabase.ReloadList();
    }

    private void Start()
    {
        PopulateImageList();
    }

    public void RefreshImageFolder()
    {
        imageDatabase.ReloadList();
        PopulateImageList();
    }

    public void PopulateImageList()
    {
        foreach(Transform t in container.GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
        int imagesCount = imageDatabase.imageList.Count;
        for(int i=0;i<imagesCount;i++)
        {
            GameObject imageElement = Instantiate(imageElementPrefab, container.transform);
            ImageElement script = imageElement.GetComponent<ImageElement>();
            script.PNGImage.sprite = imageDatabase.imageList[i].image;
            script.imageName.text = "File Name " + imageDatabase.imageList[i].name;
            //Truncating miliseconds from dateTime
            script.timeFileExists.text = "File exists for " + (System.DateTime.Now.AddTicks(-(System.DateTime.Now.Ticks % System.TimeSpan.TicksPerSecond)) 
                - (imageDatabase.imageList[i].creationTime.AddTicks(-(imageDatabase.imageList[i].creationTime.Ticks % System.TimeSpan.TicksPerSecond)))).ToString();
        }
    }
}
