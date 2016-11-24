using System;
using System.Threading;

namespace Test
{
	public class TimerTestRun
	{
		//public TimerTestRun()
		//{
			
		//}

		public void Run()
		{
			TimeExampleState s = new TimeExampleState();

			// Create the delegate that invokes methods for the timer.
			TimerCallback timerDelegate = new TimerCallback(CheckStatus);

			// Create a timer that waits one second, then invokes every second.
			Timer timer = new Timer(timerDelegate, s, 1000, 1000);

			// Keep a handle to the timer, so it can be disposed.
			s.tmr = timer;

			// The main thread does nothing until the timer is disposed.
			while (s.tmr != null)
				Thread.Sleep(0);
			Console.WriteLine("Timer example done.");
		}
		// The following method is called by the timer's delegate.

		void CheckStatus(Object state)
		{
			TimeExampleState s = (TimeExampleState)state;
			s.counter++;
			Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);
			if (s.counter == 5)
			{
				// Shorten the period. Wait 10 seconds to restart the timer.
				(s.tmr).Change(10000, 100);
				Console.WriteLine("changed...");
			}
			if (s.counter == 10)
			{
				Console.WriteLine("disposing of timer...");
				s.tmr.Dispose();
				s.tmr = null;
			}
		}
	}
}
