using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using session2.Models;
using System;
using System.Globalization;

namespace session2.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public BankAccountsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: AccountsController
        public ActionResult Index()
        {
            var accounts = _context.Accounts!.ToList();
            return View(accounts);
        }

        // GET: AccountsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            Account account = _context.Accounts.Find(id);
            var entity = new AccountEditModel
            {
                Id = account.Id,
                Balance = account.Balance, // careful with null/parse
            };
            return View("Edit", entity);
        }

        // POST: AccountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*Guid id, IFormCollection collection*/ AccountEditModel model)
        {
            
             if (!ModelState.IsValid) 
                return View(model);

            Account dbEntity = _context.Accounts.Find(model.Id);
            dbEntity.Balance = model.Balance;
            _context.Accounts.Update(dbEntity);
            _context.SaveChanges();
            return RedirectToAction("Index");
             /*
            try
            {
                Account dbEntity = _context.Accounts.Find(id);
                var balanceString = collection["balance"].FirstOrDefault();
                if (!string.IsNullOrEmpty(balanceString) &&
                    decimal.TryParse(balanceString, NumberStyles.Number, CultureInfo.InvariantCulture, out var balance))
                {
                    dbEntity.Balance = balance;
                }
                else
                {
                    // handle parse error or missing value
                }
                

                _context.Accounts.Update(dbEntity);
                _context.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return View();
            }*/
        }

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
