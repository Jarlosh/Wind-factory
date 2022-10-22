using UnityEngine;

namespace EasyGames.Utils
{
    public static class GameObjectUtils
    {
        private const string NameProperty = "name";
        
        public static T CopyComponentTo<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            var cloneComponent = destination.GetComponent(type) as T;
            if (cloneComponent == null)
            {
                cloneComponent = destination.AddComponent(type) as T;
            }
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic)
                {
                    continue;
                }
                field.SetValue(cloneComponent, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == NameProperty)
                {
                    continue;
                }
                prop.SetValue(cloneComponent, prop.GetValue(original, null), null);
            }
            return cloneComponent;
        }
    }
}