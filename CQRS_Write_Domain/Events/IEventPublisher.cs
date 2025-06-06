﻿namespace CQRS_Write_Domain.Events
{
    public interface IEventPublisher
    {
        void RegisterEventHandlers(IEventHandler eventHandler);
        void Publish<T>(T @event) where T : IEvent;
    }
}
