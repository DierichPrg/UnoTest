using System;
using System.Timers;

namespace UnoClockXamarin.ViewModel
{
    public class ClockViewModel
    {
        // a timer that notify a event, in this case i used it to raise a event every 1 second
        private readonly System.Timers.Timer timer;

        public ClockViewModel()
        {
            // timer constructor that raise a event every second
            this.timer = new System.Timers.Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);

            // add a delegate to Elapsed eventHandler
            this.timer.Elapsed += TimerOnElapsed;

            // start the timer
            this.timer.Start();
        }

        // a custom eventHandler that view sign to refresh clock
        public event EventHandler<DateTime> DateTimeChanged;

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // send event
            DateTimeChanged?.Invoke(sender, DateTime.Now);
        }

        // how this viewModel dont have any property that view show, I dont implemented INotifyPropertyChanged interface
    }
}

