﻿using Kaching.Data;
using Kaching.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaching.Repositories
{
    public class ExpenseRepository : IExpenseRepository, IDisposable
    {
        private readonly DataContext _context;

        public ExpenseRepository(DataContext context)
        {
            _context = context;
        }   

        public void DeleteExpense(Expense expense)
        {
             _context
                .Expense.Remove(expense);
        }

        public async Task<Expense> GetExpenseById(int expenseId)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Include(b => b.Buyer)
                .FirstOrDefaultAsync(m => m.ExpenseId == expenseId);
        }

        public async Task<List<Expense>> GetExpenses(int monthNumber)
        {
            return await _context.Expense
                .Include(e => e.Buyer)
                .Include(e => e.BaseExpense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .OrderByDescending(e => e.PaymentDate)
                .ToListAsync();
        }

        public decimal GetSumExpensesByMonth(int monthNumber)
        {
            return  _context.Expense
                .Include(e => e.Buyer)
                .Include(e => e.BaseExpense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Sum(i => i.Price);
        }


        public async Task<List<Expense>> GetPersonExpensesByMonth(int personId, int monthNumber)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .ToListAsync();
        }

        public decimal GetSumOfPersonExpensesByMonth(int personId, int monthNumber)
        {
            return _context.Expense
                .Include(v => v.BaseExpense)
                .Include(b => b.Buyer)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .Sum(e => e.Price);
        }

        public void InsertExpense(Expense expense)
        {
            _context.Add(expense);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            _context.Update(expense);
        }

        public bool GetExpenseExistence(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
