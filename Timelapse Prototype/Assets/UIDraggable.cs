using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragged = false;

    private Vector2 offset;
    private void Update()
    {
        if(isDragged)
        {
            transform.position = Input.mousePosition + (Vector3)offset;
        }
    }

    public void OnPointerDown(PointerEventData e) 
    {
        isDragged = true;

        offset = (Vector2)transform.position - e.position;
    }

    public void OnPointerUp(PointerEventData e)
    {
        isDragged = false;
    }
}
