using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class NewDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Vector2 originalPos;
    private Vector2 trueOriginalPos;
    public const string DRAGGABLE_TAG = "UIDraggable";

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.position = basePos;
        Transform trashCan = GetDraggableTransformUnderMouse();
        if (trashCan != null && trashCan.tag == "TrashCan")
        {
            GetComponent<ResearchNotes>().researchType = -1;
            transform.position = trueOriginalPos;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = originalPos;
        }
    }

    // Use this for initialization
    void Start () {

        trueOriginalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> hitObjects = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, hitObjects);
        if (hitObjects.Count < 0) return null;

        return hitObjects[0].gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();
        if (clickedObject != null && (clickedObject.tag == DRAGGABLE_TAG || clickedObject.tag == "TrashCan"))
        {
            return clickedObject.transform;
        }
        return null;
    }
}
