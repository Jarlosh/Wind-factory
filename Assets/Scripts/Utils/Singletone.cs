
public class Singletone<T> where T : new()
{
    private static T s_Instance;
    public static T Instance
    {
        get
        {
            EnsureInstanceCreated();
            return s_Instance;
        }
    }

    public static bool IsInstanceExists => s_Instance != null;

    public static T EnsureInstanceCreated()
    {
        if (s_Instance == null)
        {
            s_Instance = new T();
        }
        return s_Instance;
    }
}
