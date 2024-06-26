﻿using System;

namespace ImagePlanner
{
    public class TargetChangeEvent
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
        public event EventHandler<TargetChangeEventArgs> TargetChangeEventHandler;

        //Method for initiating target event
        public void TargetChangeUpdate(string targetName)
        {
            OnTargetChangeEventHandler(new TargetChangeEventArgs(targetName));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnTargetChangeEventHandler(TargetChangeEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            TargetChangeEventHandler?.Invoke(this, e);
            //         QuickPickEventHandler thisEvent = ThisEvent;  //assign the event to a local variable
            //         If(thisEvent != null)
            //         {
            //             thisEvent(this, args);
            //         }
        }

        //Class to hold logging event arguments, i.e. log entry string
        public class TargetChangeEventArgs : System.EventArgs
        {
            private string privateTargetName;

            public TargetChangeEventArgs(string targetName)
            {
                this.privateTargetName = targetName;
            }

            public string TargetName
            { get { return privateTargetName; } }
        }

        public void TargetUpdate(string target)
        {
            //Raises a log event for anyone who is listening

            TargetChangeUpdate(target);
            System.Windows.Forms.Application.DoEvents();
            return;
        }

    }

}
