using UnityEngine;

namespace EasyGames.Utils
{
    public class SwitchableGameobject : MonoBehaviour
    {
        public bool ActiveSelf => gameObject.activeSelf;
        public bool ActiveInHierarchy => gameObject.activeInHierarchy;
        public void SetActive(bool state) => gameObject.SetActive(state);
        public void Toggle() => SetActive(!ActiveSelf);
    }
}