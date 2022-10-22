using System;
using EasyGames.Events;
using EasyGames.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyGames.UISystem
{
    public class ClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public RxValue<bool> IsPressed;
        public GameEvent OnPointerDownEvent;

        public ClickHandler()
        {
            OnPointerDownEvent = new GameEvent();
            IsPressed = new RxValue<bool>();
        }

        public Vector2 LastPointerPosition { get; private set; }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            LastPointerPosition = eventData.position;
            OnPointerDownEvent?.Invoke();
            IsPressed.Value = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LastPointerPosition = eventData.position;
            IsPressed.Value = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            LastPointerPosition = eventData.position;
        }
    }
}