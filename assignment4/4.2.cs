using System;
using System.Threading;

public class AlarmClock
{    public event EventHandler Tick;
    public event EventHandler Alarm;

    public DateTime CurrentTime { get; private set; }
    public DateTime AlarmTime { get; set; }

    public AlarmClock(DateTime alarmTime)
    {
        CurrentTime = DateTime.Now;
        AlarmTime = alarmTime;
    }

   
    public void Start()
    {
        Console.WriteLine("闹钟已启动！");
        while (true)
        {
            CurrentTime = DateTime.Now;
            Console.WriteLine($"当前时间: {CurrentTime:HH:mm:ss}");
            OnTick();

            if (CurrentTime >= AlarmTime)
            {
                OnAlarm();
                break; 
            }

            Thread.Sleep(1000);
        }
    }


    protected virtual void OnTick()
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnAlarm()
    {
        Alarm?.Invoke(this, EventArgs.Empty);
    }
}
public class Program
{
    public static void Main()
    {
        DateTime alarmTime = DateTime.Now.AddSeconds(10);


        AlarmClock alarmClock = new AlarmClock(alarmTime);

        alarmClock.Tick += (sender, e) =>
        {
            Console.WriteLine("滴答...");
        };

        alarmClock.Alarm += (sender, e) =>
        {
            Console.WriteLine("叮铃铃！该起床了！");
        };

        alarmClock.Start();
    }
}