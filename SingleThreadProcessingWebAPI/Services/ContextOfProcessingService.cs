namespace SingleThreadProcessingWebAPI.Services;

public static class ContextOfProcessingService
{
    private static readonly TaskScheduler _scheduler = new SingleThreadTaskScheduler();
    private static readonly TaskFactory _taskFactory = new TaskFactory(_scheduler);

    public static async Task ExecuteFunctionAsync(Func<Task> funcion)
    {
        await _taskFactory.StartNew(async () =>
        {
            // Imprime el ID del hilo actual
            Console.WriteLine($"Ejecutando en el hilo ID: {Thread.CurrentThread.ManagedThreadId}");

            // Ejecuta la función
            await funcion();
        }).Unwrap();
    }

    public static void SimulateConnection()
    {
        Thread.Sleep( 1000 );
        Console.WriteLine($"Método estático SimulateConnection ejecutado en el hilo ID: {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void SimulateDispensation()
    {
        Thread.Sleep( 1000 );
        Console.WriteLine($"Método estático SimulateDispensation ejecutado en el hilo ID: {Thread.CurrentThread.ManagedThreadId}");
    }
}
