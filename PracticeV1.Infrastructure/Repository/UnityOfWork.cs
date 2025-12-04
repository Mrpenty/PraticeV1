using Microsoft.EntityFrameworkCore.Storage;
using PracticeV1.Application.Repositories;
using PracticeV1.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Repository
{
    public class UnityOfWork :IUnitOfWork
    {
        private readonly PRDBContext _context;
        private  IDbContextTransaction _transaction;
        public UnityOfWork(PRDBContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                return _transaction;
            }

            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }


        public Task CommitAsync()
        {
            if(_transaction != null)
            {
                _transaction.Commit();
            }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await  _context.SaveChangesAsync();
        }
    }
}
