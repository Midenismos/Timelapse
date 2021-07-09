using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable] public class PointerDataGameObjectEvent : UnityEvent<PointerEventData, GameObject> { }

public class UIPointerEvents : MonoBehaviour, IPointerClickHandler
{
    public PointerDataGameObjectEvent OnClick;
    public PointerDataGameObjectEvent OnDoubleClick;
    public UnityEvent OnRightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(eventData.clickCount == 1)
            {
                OnClick?.Invoke(eventData, gameObject);
            }

            if(eventData.clickCount == 2)
            {
                OnDoubleClick?.Invoke(eventData, gameObject);
            }
        } else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke();
        }
    }
}
