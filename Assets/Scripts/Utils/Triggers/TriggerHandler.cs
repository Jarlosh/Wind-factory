using System;
using UnityEngine;

namespace EasyGames.Utils
{
    public class TriggerHandler : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEnterEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Trigger {gameObject?.name}, {other?.gameObject?.name}");
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}