using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EasyGames.Utils
{
    public class UnitaskHelper
    {
        public static UniTask DoAfter(float delaySeconds, Action callback, CancellationToken cts=default)
        {
            return UniTask.Delay(Mathf.FloorToInt(delaySeconds * 1000), cancellationToken: cts)
                .ContinueWith(callback);
        }
    }
}