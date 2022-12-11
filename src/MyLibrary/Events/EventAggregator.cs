//MIT License

//Copyright (c) 2021-2022

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace MyLibrary.Events
{
    public class EventAggregator : IDisposable, IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, List<object>> _subscriptions = new ConcurrentDictionary<Type, List<object>>();

        private static readonly EventAggregator _instance = new EventAggregator();
        public static EventAggregator GetInstance() => _instance;

        public void Publish<T>(T message) where T : IAppEvent
        {
            List<object> subscribers;
            if (this._subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                foreach (var subscriber in subscribers.ToArray())
                {
                    ((Action<T>)subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : IAppEvent
        {
            var subscribers = this._subscriptions.GetOrAdd(typeof(T), t => new List<object>());
            lock (subscribers)
            {
                subscribers.Add(action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IAppEvent
        {
            List<object> subscribers;
            if (this._subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                lock (subscribers)
                {
                    subscribers.Remove(action);
                }
            }
        }

        public void Dispose()
        {
            this._subscriptions.Clear();
        }
    }//class
}