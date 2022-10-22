using System;
using UnityEngine.UI;

namespace EasyGames.Utils
{
    public static class ButtonRxExtensions
    {
        public static IDisposable SubscribeClickRx(this Button button, Action action)
        {
            button.onClick.AddListener(Wrapper);
            return new Disposable(() =>
            {
                if (button != null)
                {
                    button.onClick.RemoveListener(Wrapper);
                }
            });

            void Wrapper()
            {
                action?.Invoke();
            }
        }
    }
}