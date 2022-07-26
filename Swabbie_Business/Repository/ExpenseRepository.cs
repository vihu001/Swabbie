﻿using AutoMapper;
using Swabbie_DataAccess.Transactions;
using Swabbie_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Swabbie_Models.DTO.Transactions;
using Swabbie_Business.Repository.IRepository;

namespace Swabbie_Business.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ExpenseRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ExpenseDTO> Create(ExpenseDTO objDTO)
        {
            var obj = _mapper.Map<ExpenseDTO, Expense>(objDTO);
            var addedObj = _db.Expenses.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Expense, ExpenseDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Expenses.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Expenses.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ExpenseDTO> Get(int id)
        {
            var obj = await _db.Expenses.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Expense, ExpenseDTO>(obj);
            }
            return new ExpenseDTO();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Expense>, IEnumerable<ExpenseDTO>>(_db.Expenses);
        }

        public async Task<ExpenseDTO> Update(ExpenseDTO objDTO)
        {
            var objFromDb = await _db.Expenses.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = objDTO.Description;
                objFromDb.TransactionDate = objDTO.TransactionDate;
                objFromDb.CreatedDate = DateTime.UtcNow;
                objFromDb.Amount = objDTO.Amount;
                objFromDb.Category = objDTO.Category;
                objFromDb.TransactionType = objDTO.TransactionType;
                _db.Expenses.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Expense, ExpenseDTO>(objFromDb);
            }
            return objDTO;

        }
    }
}
