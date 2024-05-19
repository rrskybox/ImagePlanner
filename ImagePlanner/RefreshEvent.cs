using System;

namespace ImagePlanner
{
    public class RefreshEvent
    {
        /// Class for creating event handler for passing Log value to 
        /// a subscriber form (HumasonForm) and saving it to a file
        /// 
        /// 
        /// The subscribing form class (which wants to display the log entry
        /// 1) instantiates a public field for this class object
        ///         public static Logger logstatus = new Logger();
        /// 2) Creates the log file and subscribes a handler to the event subscriber list when initializing the form class
        ///             logstatus.QuickPickEventHandler += StatusLogUpdate_Handler;
        /// 3) installs a method for handling the event
        ///         private void StatusLogUpdate_Handler(object sender, Logger.QuickPickEventArgs e)
        ///         {
        ///            StatusStripLine.Text = e.TargetEntry;
        ///            Show();
        ///            return;
        ///         }
        ///Then an event publisher gets the Logger object from the controlling form
        ///            Logger lg = HumasonForm.logstatus;
        ///And generates an event whenever it needs to
        ///            lg.targetName("Acquiring guide star");
        ///            

        //Event declaration
        public event EventHandler<RefreshEventArgs> RefreshEventHandler;

        //Method for initiating target event
        public void RefreshUpdate(DateTime newDate)
        {
            OnRefreshEventHandler(new RefreshEventArgs(newDate));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnRefreshEventHandler(RefreshEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            RefreshEventHandler?.Invoke(this, e);
        }

        //Class to hold logging event arguments, i.e. log entry string
        public class RefreshEventArgs : System.EventArgs
        {
            private DateTime privateNewDate;

            public RefreshEventArgs(DateTime newDate)
            {
                this.privateNewDate = newDate;
            }

            public DateTime NewDate
            { get { return privateNewDate; } }
        }

        public void RefreshtUpdate(DateTime newDate)
        {
            //Raises a refresh event for anyone who is listening

            RefreshUpdate(newDate);
            System.Windows.Forms.Application.DoEvents();
            return;
        }

    }

}
