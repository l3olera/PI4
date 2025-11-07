using System;

namespace Player47.Core
{
    public interface IEventSource
    {
        event Action Triggered;
    }
}