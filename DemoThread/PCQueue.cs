using System.Collections.Concurrent;

namespace DemoThread;
public class PCQueue : IDisposable
{
    class WorkItem
    {
        public readonly TaskCompletionSource<object> TaskSource;
        public readonly Action Action;
        public readonly CancellationToken? CancelToken;
        public WorkItem(TaskCompletionSource<object> taskSource, Action action, CancellationToken? cancelToken = null)
        {
            TaskSource = taskSource;
            Action = action;
            this.CancelToken = cancelToken;
        }
    }
    BlockingCollection<WorkItem> _taskQ = new BlockingCollection<WorkItem>();
    public PCQueue(int workCount)
    {
        for(int i=0; i<workCount; i++)
        {
            Task.Factory.StartNew(Consume);
        }
    }

    private void Consume()
    {
        foreach( var workItem in _taskQ.GetConsumingEnumerable())
        {
            if (workItem.CancelToken.HasValue && workItem.CancelToken.Value.IsCancellationRequested)
            {
                workItem.TaskSource.SetCanceled();
            }
            else
            {
                try
                {
                    workItem.Action();
                    workItem.TaskSource.SetResult("world");

                } catch (OperationCanceledException ex)
                {
                    if (ex.CancellationToken == workItem.CancelToken)
                    {
                        workItem.TaskSource.SetCanceled();
                    } else
                    {
                        workItem.TaskSource.SetException(ex);
                    }
                } catch (Exception ex)
                {
                    workItem.TaskSource.SetException(ex);
                }
            }
        }
            
    }

    public void Dispose()
    {
        _taskQ.CompleteAdding();
    }

    public Task EnqueueTask(Action action, CancellationToken? cancelToken)
    {
        var tcs= new TaskCompletionSource<object>();
        _taskQ.Add(new WorkItem(tcs, action, cancelToken));
        return tcs.Task;
    }
}
