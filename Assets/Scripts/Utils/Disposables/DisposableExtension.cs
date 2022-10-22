using System;
using System.Collections.Generic;

namespace EasyGames.Utils
{
    public static class DisposableExtension
    {
        public static void AddTo(this IDisposable disposable, CompositeDisposable composite)
        {
            composite.Add(disposable);
        }
        
        public static void AddTo(this IEnumerable<IDisposable> disposable, CompositeDisposable composite)
        {
            composite.AddRange(disposable);
        }
    }
}