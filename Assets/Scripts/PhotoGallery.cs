using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

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

    DuLNode _headNode;
    DuLNode _tailNode;
    DuLNode _curNode;
    int _cntPhoto;
    string _pathPhotos;

    private void Start()
    {
        _headNode = null;
        _tailNode = null;
        _cntPhoto = 0;

        _pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(_pathPhotos)) 
        {
            Debug.Log("Clearing Old Photos.");
            Directory.Delete(_pathPhotos); 
        }
        Debug.Log("Creating a new gallery.");
        Directory.CreateDirectory(_pathPhotos);
    }

    public void Capture()
    {
        //string strDateAndTime = System.DateTime.Now.ToString();
        ScreenCapture.CaptureScreenshot(_pathPhotos + _cntPhoto + ".png");
        if (_headNode == null)
        {
            _headNode = new DuLNode(_cntPhoto.ToString(), _cntPhoto);
            _tailNode = _headNode;
            _headNode.next = _tailNode;
            _tailNode.prev = _headNode;
        }
        else
        {
            _tailNode.next = new DuLNode(_cntPhoto.ToString(), _cntPhoto);
            _tailNode.next.prev = _tailNode;
            _tailNode = _tailNode.next;
            _tailNode.next = _headNode;
            _headNode.prev = _tailNode;
        }
        _cntPhoto++;
    }

    public void Show(DuLNode node)
    {
        if (node != null)
        {
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(_pathPhotos + node.strName + ".png");
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
        _curNode = _headNode;
        Show(_curNode);
    }

    public void ShowNextPhoto()
    {
        if (_curNode == null)
            return;
        Show(_curNode.next);
        _curNode = _curNode.next;
    }

    public void ShowPrevPhoto()
    {
        if (_curNode == null)
            return;
        Show(_curNode.prev);
        _curNode = _curNode.prev;
    }




}
