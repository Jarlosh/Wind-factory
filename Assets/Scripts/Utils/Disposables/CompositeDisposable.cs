using System;
using System.Collections.Generic;

namespace EasyGames.Utils
{
    public class CompositeDisposable : IDisposable
    {
        private List<IDisposable> disposables = new List<IDisposable>();
        
        public void Add(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void AddRange(IEnumerable<IDisposable> enumerable)
        {
            disposables.AddRange(enumerable);
        }

        public void AddRange(params IDisposable[] enumerable)
        {
            disposables.AddRange(enumerable);
        }

        public void Remove(IDisposable disposable)
        {
            disposables.Remove(disposable);
        }

        public void Dispose()
        {
            disposables.ForEach(d => d.Dispose());
            disposables.Clear();
        }
    }
}