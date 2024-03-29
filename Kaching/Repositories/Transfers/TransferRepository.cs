﻿using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories
{
    public class TransferRepository : ITransferRepository, IDisposable
    {
        private readonly DataContext _context;

        public TransferRepository(DataContext context)
        {
            _context = context;
        }

        public void DeleteTransfer(Transfer transfer)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public Task<Transfer> GetTransferById(int transferId)
        {
            throw new NotImplementedException();
        }

        public bool GetTransferExistence(int transferId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transfer>> GetTransfers(int groupId)
        {
            return await _context.Transfer
                .Include(e => e.Sender)
                .Include(c => c.Currency)
                .Include(f=> f.Receiver)
                .Include(e => e.Expense)
                .Where(x => x.GroupId == groupId)
                .OrderByDescending(e=>e.PaymentMonth)
                .ToListAsync(); 
        }

        public async Task<List<Transfer>> GetTransfersByMonthYear(int month, int year, int groupId)
        {
            return await _context.Transfer
                .Include(e => e.Sender)
                .Include(c => c.Currency)
                .Include(f => f.Receiver)
                .Include(e => e.Expense)
                .Where(x => x.GroupId == groupId)
                .Where(x => x.PaymentMonth.Month == month)
                .Where(x => x.PaymentMonth.Year == year)
                .ToListAsync();
        }

        public Task<List<Transfer>> GetPersonTransfers(int personId, int monthNumber)
        {
            throw new NotImplementedException();
        }

        public void InsertTransfer(Transfer transfer)
        {
            _context.Add(transfer);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public decimal GetSumOfPersonSentTransfers(int monthNumber, int personId)
        {
            return _context.Transfer
                .Include(e => e.Sender)
                .Where(p => p.Sender.PersonId == personId)
                .Where(p => p.PaymentMonth.Month == monthNumber)
                .Sum(i => i.Amount);
        }
        public decimal GetSumOfPersonReceivedTransfers(int monthNumber, int personId)
        {
            return _context.Transfer
                .Include(e => e.Receiver)
                .Where(p => p.Receiver.PersonId == personId)
                .Where(p => p.PaymentMonth.Month == monthNumber)
                .Sum(i => i.Amount);
        }
    }
}
