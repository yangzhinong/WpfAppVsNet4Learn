namespace DemoThread.Dis;

public class Test2 : IDisposable
{

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        
       
    }

    

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Call Dispose() on other objects owned by this instance.
            // You can reference other finalizable objects here.
        }
        // Release unmanaged resources owned by (just) this object.'
        
    }

    ~Test2() => Dispose(false);
}
public class Foo
{
    private int _suspendCount;

    public IDisposable SuspendEvents()
    {
        _suspendCount++;
        return Disposable.Create(()=> _suspendCount--);
    }

    public void FireSomeEvnet()
    {
        if (_suspendCount == 0)
        {
            //todo
        }
    }

    
}

public class Disposable : IDisposable
{
    private Action _onDispose;

    public static Disposable Create(Action onDispose) => new(onDispose);

    public Disposable(Action onDispose)
    {
        _onDispose = onDispose;
    }
    public void Dispose()
    {
        _onDispose?.Invoke();
        _onDispose = null;
    }
}






