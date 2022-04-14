﻿using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        IRepository<Resume> ResumeRepository { get; }
        IRepository<Quote> QuoteRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
