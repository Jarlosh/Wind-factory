using System;
using System.Collections.Generic;
using EasyGames.Utils;
using UnityEngine;

namespace EasyGames.Sources.Utils
{
    public class GroupSwitcherTrigger : MonoBehaviour
    {
        [Tooltip("Объекты с какими слоями активируют триггер")] 
        [SerializeField] private LayerMask triggerObjectLayers;
        [Tooltip("Сперва список на выключение? Иначе наоборот")] 
        [SerializeField] private bool disableFirst;
        [SerializeField] private bool onlyOnce;
        [SerializeField] private List<GameObject> toEnable;
        [SerializeField] private List<GameObject> toDisable;
        
        private bool used;

        private void OnTriggerEnter(Collider other)
        {
            if (onlyOnce && used)
                return;

            if (triggerObjectLayers.Contains(other.gameObject.layer))
            {
                used = true;
                if (disableFirst)
                {
                    Disable();
                    Enable();
                }
                else
                {
                    Enable();
                    Disable();
                }
            }
        }

        private void Enable()
        {
            toEnable.ForEach(go => go.SetActive(true));
        }

        private void Disable()
        {
            toDisable.ForEach(go => go.SetActive(false));
        }
    }
}