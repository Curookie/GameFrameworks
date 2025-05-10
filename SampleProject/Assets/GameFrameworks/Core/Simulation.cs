using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrameworks
{
    /// <summary>
    /// The Simulation class implements the discrete event simulator pattern.
    /// Events are pooled, with a default capacity of 4 instances.
    /// </summary>
    public static partial class Simulation
    {

        static HeapQueue<Event> eventQueue = new HeapQueue<Event>();
        static Dictionary<System.Type, Stack<Event>> eventPools = new Dictionary<System.Type, Stack<Event>>();
        static Dictionary<Type, int> eventExecutionCount = new Dictionary<Type, int>();

        /// <summary>
        /// Create a new event of type T and return it, but do not schedule it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public T New<T>() where T : Event, new()
        {
            Stack<Event> pool;
            if (!eventPools.TryGetValue(typeof(T), out pool))
            {
                pool = new Stack<Event>(4);
                pool.Push(new T());
                eventPools[typeof(T)] = pool;
            }
            if (pool.Count > 0)
                return (T)pool.Pop();
            else
                return new T();
        }

        /// <summary>
        /// Clear all pending events and reset the tick to 0.
        /// </summary>
        public static void Clear()
        {
            eventQueue.Clear();
            eventExecutionCount.Clear();
        }

        /// <summary>
        /// Schedule an event for a future tick, and return it.
        /// </summary>
        /// <returns>The event.</returns>
        /// <param name="tick">Tick.</param>
        /// <typeparam name="T">The event type parameter.</typeparam>
        static public T Schedule<T>(float tick = 0) where T : Event, new()
        {
            var ev = New<T>();
            ev.tick = Time.time + tick;
            eventQueue.Push(ev);
            return ev;
        }


        /// <summary>
        /// Runs an event of type T immediately without scheduling.
        /// The event is taken from the pool, optionally initialized via a delegate,
        /// executed asynchronously, and then returned to the pool upon completion.
        /// </summary>
        /// <typeparam name="T">The event type to execute.</typeparam>
        /// <param name="initializer">
        /// Optional initializer delegate used to configure the event instance before execution.
        /// </param>
        /// <returns>The event instance that was executed.</returns>
        public static T RunNow<T>(Action<T> initializer = null) where T : Event, new() {
            var ev = New<T>();
            initializer?.Invoke(ev);
            ev.ExecuteEventAsync().ContinueWith(_ => {
                ev.Cleanup();
                try {
                    eventPools[ev.GetType()].Push(ev);
                } catch (KeyNotFoundException) {
                    //This really should never happen inside a production build.
                    Debug.LogError($"No Pool for: {ev.GetType()}");
                }
            });
            return ev;
        }

        /// <summary>
        /// Reschedule an existing event for a future tick, and return it.
        /// </summary>
        /// <returns>The event.</returns>
        /// <param name="tick">Tick.</param>
        /// <typeparam name="T">The event type parameter.</typeparam>
        static public T Reschedule<T>(T ev, float tick) where T : Event, new()
        {
            ev.tick = Time.time + tick;
            eventQueue.Push(ev);
            return ev;
        }

        /// <summary>
        /// Checks if an event of type T is currently scheduled in the event queue.
        /// </summary>
        /// <typeparam name="T">The event type to check for.</typeparam>
        /// <returns>True if the event is scheduled, otherwise false.</returns>
        public static bool IsEventScheduled<T>() where T : Event
        {
            return eventQueue.Any(ev => ev is T);
        }

        /// <summary>
        /// Return the simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static public T GetModel<T>() where T : class, new()
        {
            return InstanceRegister<T>.instance;
        }

        /// <summary>
        /// Set a simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static public void SetModel<T>(T instance) where T : class, new()
        {
            InstanceRegister<T>.instance = instance;
        }

        /// <summary>
        /// Destroy the simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static public void DestroyModel<T>() where T : class, new()
        {
            InstanceRegister<T>.instance = null;
        }

        /// <summary>
        /// Tick the simulation. Returns the count of remaining events.
        /// If remaining events is zero, the simulation is finished unless events are
        /// injected from an external system via a Schedule() call.
        /// </summary>
        /// <returns></returns>
        static public int Tick()
        {
            var time = Time.time;
            var executedEventCount = 0;

            while (eventQueue.Count > 0 && eventQueue.Peek().tick <= time)
            {
                var ev = eventQueue.Pop();
                var tick = ev.tick;
                ev.ExecuteEventAsync().ContinueWith(_ => {
                    if (ev.tick > tick)
                    {
                        //event was rescheduled, so do not return it to the pool.
                    }
                    else
                    {
                        // Debug.Log($"<color=green>{ev.tick} {ev.GetType().Name}</color>");
                        ev.Cleanup();
                        try
                        {
                            eventPools[ev.GetType()].Push(ev);
                        }
                        catch (KeyNotFoundException)
                        {
                            //This really should never happen inside a production build.
                            Debug.LogError($"No Pool for: {ev.GetType()}");
                        }
                    }
                });
                executedEventCount++;
            }
            return eventQueue.Count;
        }

        public static int GetEventExecutionCount<T>() where T : Event
        {
            Type eventType = typeof(T);
            return eventExecutionCount.TryGetValue(eventType, out int count) ? count : 0;
        }
    }
}