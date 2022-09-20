//MIT License

using System;

namespace MyLibrary.Events
{
    public interface IEventAggregator
    {
        void Publish<T>(T message) where T : IAppEvent;
        void Subscribe<T>(Action<T> action) where T : IAppEvent;
        void Unsubscribe<T>(Action<T> action) where T : IAppEvent;
    }
}