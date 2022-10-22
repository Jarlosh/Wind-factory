﻿
using System;

namespace EasyGames.Events
{
    public class GameEvent
    {
        private event Action m_Event;

        public void Subscribe(Action action)
        {
            m_Event += action;
        }

        public void Unsubscribe(Action action)
        {
            m_Event -= action;
        }

        public void Invoke()
        {
            m_Event?.Invoke();
        }
    }

    public class GameEvent<T>
    {
        private event Action<T> m_Event;

        public void Subscribe(Action<T> action)
        {
            m_Event += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            m_Event -= action;
        }

        public void Invoke(T param)
        {
            m_Event?.Invoke(param);
        }
    }

    public class GameEvent<T1, T2>
    {
        private event Action<T1, T2> m_Event;

        public void Subscribe(Action<T1, T2> action)
        {
            m_Event += action;
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            m_Event -= action;
        }

        public void Invoke(T1 param1, T2 param2)
        {
            m_Event?.Invoke(param1, param2);
        }
    }
}
