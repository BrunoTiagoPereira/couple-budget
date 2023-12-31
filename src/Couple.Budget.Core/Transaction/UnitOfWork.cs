﻿using Microsoft.EntityFrameworkCore;

namespace Couple.Budget.Core.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}