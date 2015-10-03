using System;
using System.Threading;
using Topshelf;

namespace TopShelfLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(x =>
            {
                x.SetServiceName("SampleServiceWithTopShelf");
                x.Service<UhhMyService>();
                x.RunAsLocalSystem();

                x.SetDescription("Sample service with TopShelf");
                x.SetDisplayName("Sample Service With TopShelf");
                x.SetServiceName("SampleServiceWithTopShelf");
                x.EnablePauseAndContinue();
                x.EnableShutdown();
            });
            host.Run();
        }
    }

    public class UhhMyService : ServiceControl, IWindowsService
    {
        private readonly Timer _timer;

        public UhhMyService()
        {
            _timer = new Timer(state =>
            {
                Console.WriteLine($"{DateTime.Now:G}");
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Start");
            Console.ResetColor();
        }

        public void Stop()
        {
            _timer.Dispose();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Stop");
            Console.ResetColor();
        }

        public bool Start(HostControl hostControl)
        {
            Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Stop();
            return true;
        }
    }

    public interface IWindowsService
    {
        void Start();
        void Stop();
    }
}
