using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameFrameworks
{
    public static partial class Simulation
    {
        /// <summary>
        /// An event is something that happens at a point in time in a simulation.
        /// The Precondition method is used to check if the event should be executed,
        /// as conditions may have changed in the simulation since the event was 
        /// originally scheduled.
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        [Serializable]
        public abstract class Event : System.IComparable<Event>
        {
            internal float tick;
            public bool isDebugShow = false;

            public int CompareTo(Event other)
            {
                return tick.CompareTo(other.tick);
            }

            public async Task RunAsync() => await ExecuteEventAsync();

            protected abstract Task ExecuteAsync();
            public abstract void Reset();

            public virtual bool Precondition() => true;

            internal virtual async Task ExecuteEventAsync()
            {
                if (Precondition())
                {
                    await ExecuteAsync();

                    var eventType = typeof(Event);
                    if (!eventExecutionCount.ContainsKey(eventType)) {
                        eventExecutionCount[eventType] = 0;
                    }
                    eventExecutionCount[eventType]++;

                    if(isDebugShow) { DBug.Log($"Executing Event: {eventType.Name} at tick {tick}"); }

                    await OnCompleteAsync();
                }
            }

            /// <summary>
            /// Called after an event's execution completes. Can be overridden for custom behavior.
            /// </summary>
            protected virtual Task OnCompleteAsync()
            {
                return Task.CompletedTask;
            }

            /// <summary>
            /// This method is generally used to set references to null when required.
            /// It is automatically called by the Simulation when an event has completed.
            /// </summary>
            internal virtual void Cleanup()
            {
                Reset();
                isDebugShow = false;
            }
        }

        /// <summary>
        /// Event<T> adds the ability to hook into the OnExecute callback
        /// whenever the event is executed. Use this class to allow functionality
        /// to be plugged into your application with minimal or zero configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Serializable]
        public abstract class Event<T> : Event where T : Event<T>
        {
            //Repeat Not Reset
            public static System.Action<T> OnExecute;

            internal override async Task ExecuteEventAsync()
            {
                if (Precondition())
                {
                    await ExecuteAsync();
                    
                    OnExecute?.Invoke((T)this);

                    var eventType = typeof(T);
                    if (!eventExecutionCount.ContainsKey(eventType)) {
                        eventExecutionCount[eventType] = 0;
                    }
                    eventExecutionCount[eventType]++;

                    if(isDebugShow) { DBug.Log($"Executing Event: {typeof(T).Name} at tick {tick}"); }

                    await OnCompleteAsync();
                }
            }

            internal override void Cleanup()
            {
                base.Cleanup();
            }
        }
    }
}