﻿namespace CQRS_Write_Domain.Commands
{
    public interface ICommand
    {
        string Type { get; }
    }
}
