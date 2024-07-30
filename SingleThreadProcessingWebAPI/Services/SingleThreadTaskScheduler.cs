using System.Collections.Concurrent;

namespace SingleThreadProcessingWebAPI.Services;

public class SingleThreadTaskScheduler : TaskScheduler
{
    private readonly Thread _thread;
    private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();

    public SingleThreadTaskScheduler()
    {
        _thread = new Thread(Run) { IsBackground = true };
        _thread.Start();
    }

    private void Run()
    {
        foreach (var task in _tasks.GetConsumingEnumerable())
        {
            TryExecuteTask(task);
        }
    }

    protected override IEnumerable<Task> GetScheduledTasks() => _tasks;

    protected override void QueueTask(Task task) => _tasks.Add(task);

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        if (Thread.CurrentThread == _thread)
        {
            return TryExecuteTask(task);
        }

        return false;
    }
}