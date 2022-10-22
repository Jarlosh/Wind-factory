using System;
using EasyGames.Events;

namespace EasyGames.Utils
{
    public static class GameEventExtensions
    {
        public static IDisposable SubscribeRx(this GameEvent gameEvent, Action action)
        {
            gameEvent.Subscribe(action);
            return new Disposable(() => gameEvent.Unsubscribe(action));
        }    
        
        public static IDisposable SubscribeRx<T>(this GameEvent<T> gameEvent, Action action)
        {
            Action<T> decorated = (i) => action(); 
            gameEvent.Subscribe(decorated);
            return new Disposable(() => gameEvent.Unsubscribe(decorated));
        }    
        
        public static IDisposable SubscribeRx<T>(this GameEvent<T> gameEvent, Action<T> action)
        {
            gameEvent.Subscribe(action);
            return new Disposable(() => gameEvent.Unsubscribe(action));
        }    
        
        public static IDisposable SubscribeRx<T1, T2>(this GameEvent<T1, T2> gameEvent, Action<T1, T2> action)
        {
            gameEvent.Subscribe(action);
            return new Disposable(() => gameEvent.Unsubscribe(action));
        }    
    }
}