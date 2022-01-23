using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

[HideInInspector]
public class DuLNode
{
    public string strName { get; set; }
    public int index { get; set; }
    public DuLNode prev { get; set; }
    public DuLNode next { get; set; }

    public DuLNode(string i_strName, int i_index){
        strName = i_strName;
        index = i_index;
        prev = null;
        next = null;
    }
}

public class PhotoGallery : MonoBehaviour
{
    public RawImage DisplayImage;

    DuLNode headNode;
    DuLNode tailNode;
    DuLNode curNode;
    int cntPhoto;
    string pathPhotos;

    private void Start()
    {
        headNode = null;
        tailNode = null;
        cntPhoto = 0;

        pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(pathPhotos)) 
        {
            Debug.Log("Clearing Old Photos.");
            Directory.Delete(pathPhotos); 
        }
        Debug.Log("Creating a new gallery.");
        Directory.CreateDirectory(pathPhotos);
    }

    public void Capture()
    {
        string strDateAndTime = System.DateTime.Now.ToString();
        ScreenCapture.CaptureScreenshot(pathPhotos + cntPhoto + ".png");
        if (headNode == null)
        {
            headNode = new DuLNode(strDateAndTime, cntPhoto);
            tailNode = headNode;
            headNode.next = tailNode;
            tailNode.prev = headNode;
        }
        else
        {
            tailNode.next = new DuLNode(strDateAndTime, cntPhoto);
            tailNode.next.prev = tailNode;
            tailNode = tailNode.next;
            tailNode.next = headNode;
            headNode.prev = tailNode;
        }
        cntPhoto++;
    }

    public void Show(DuLNode node)
    {
        if (node == null)
        {

        }
        else
        {
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(pathPhotos + node.index + ".png");
            Texture2D textureLoad = new Texture2D(1, 1);
            textureLoad.LoadImage(bytes);
            if (textureLoad)
            {
                Debug.Log("Load Picture Success");
                DisplayImage.texture = textureLoad;
            }
            else
            {
                Debug.Log("Load Picture Failure");
            }
        }
    }

    public void EnterGallery()
    {
        curNode = headNode;
        Show(curNode);
    }

    public void ShowNextPhoto()
    {
        if (curNode == null)
            return;
        Show(curNode.next);
        curNode = curNode.next;
    }

    public void ShowPrevPhoto()
    {
        if (curNode == null)
            return;
        Show(curNode.prev);
        curNode = curNode.prev;
    }




}
