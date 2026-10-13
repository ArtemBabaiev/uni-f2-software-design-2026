namespace BaseDotNetApp.Lecture11;

public class LiveCollection<T>
{
    private T[] _items;

    public LiveCollection()
    {
        _items = new T[10];
    }

    public T Get(int index)
    {
        return _items[index];
    }

    
    public T this[int i]
    {
        get => _items[i];
        set => _items[i] = value;
    }
    
    public T this[int i, int j]
    {
        get => _items[i];
        set => _items[i] = value;
    }

    public void Set(int index, T value)
    {
        _items[index] = value;
    }
    
}

public class Live
{
    public Live()
    {}

    public static void Exec()
    {
        LiveCollection<int> c = new LiveCollection<int>();
        c.Set(0, 1);
        c.Set(1, 2);
        //c.Set(3, 4.0);
        object o = c.Get(0);
        
        int value = (int) o;
    }
}

public class Box<T> where T : new()
{
    private T _item;
    
    public Box(T item)
    {
        _item = new T();
    }
    
    public T Get()
    {
        return _item;
    }

    public void Put(T item)
    {
        _item = item;
    }
}