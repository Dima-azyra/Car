using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Event_object : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool push;
    public void OnPointerDown(PointerEventData eventData)
    {
        push = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        push = false;
    }
}
