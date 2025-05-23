﻿namespace CQRS_Write_Domain.Commands
{
    public interface ICommandSender
    {
        void RegisterCommandHandlers(ICommandHandler commandHandler);
        void Send<T>(T command) where T : ICommand;
    }
}
