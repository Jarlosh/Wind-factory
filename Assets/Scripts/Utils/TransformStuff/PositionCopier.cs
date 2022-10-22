using System;
using UnityEngine;

namespace EasyGames.Utils.TransformStuff
{
    public class PositionCopier : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private void Update()
        {
            transform.position = target.position;
        }
    }
}