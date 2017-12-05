using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElementDragger : MonoBehaviour {

    public const string DRAGGABLE_TAG = "UIDraggable";
    private bool dragging = false;
    private Vector2 trueOriginalPos;
    private Vector2 originalPos;
    private Vector2 mousePos;
    private Transform objectToDrag;
    private Image objectToDragImage;
    List<RaycastResult> hitObjects = new List<RaycastResult>();

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();
            if(objectToDrag != null)
            {
                dragging = true;
                objectToDrag.SetAsLastSibling();
                originalPos = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(objectToDrag != null)
            {
                Transform trashCan = GetDraggableTransformUnderMouse();
                if(trashCan != null && trashCan.tag == "TrashCan")
                {
                    objectToDrag.gameObject.GetComponent<ResearchNotes>().researchType = -1;
                    objectToDrag.position = trueOriginalPos;
                    objectToDrag.gameObject.SetActive(false);
                }
                else
                {
                    objectToDrag.position = originalPos;
                }
                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }
            dragging = false;
        }
	}

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjects);
        if (hitObjects.Count < 0) return null;

        return hitObjects[0].gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();
        if(clickedObject != null && (clickedObject.tag == DRAGGABLE_TAG || clickedObject.tag == "TrashCan"))
        {
            return clickedObject.transform;
        }
        return null;
    }
}
