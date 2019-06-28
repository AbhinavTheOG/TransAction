﻿namespace TransAction.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IEventRepository Event { get; }
        IUserRepository User { get; }
        ITeamRepository Team { get; }
        ITopicRepRository Topic { get; }
        IMessageRepository Message { get; }
        bool Save();
    }
}
