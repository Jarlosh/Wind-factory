using System;
using EasyGames.Events;
using UnityEngine;

namespace EasyGames.Utils
{
    public class CollisionEventSource : MonoBehaviour
    {       
        public GameEvent<Collision> OnCollisionEnterEvent { get; } = new GameEvent<Collision>();
        public GameEvent<Collider> OnTriggerEnterEvent { get; } = new GameEvent<Collider>();
        public GameEvent<Collider> OnTriggerExitEvent { get; } = new GameEvent<Collider>();
        
        private void OnCollisionEnter(Collision other)
        {
            OnCollisionEnterEvent?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }
    }
}