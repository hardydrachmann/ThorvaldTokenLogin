﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Client.BE;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public IActionResult Index()
        {
            string path = "user";
            List<User> userList = new List<User>();
            userList = JsonConvert.DeserializeObject<List<User>>(CallAPi(path).Result);

            return View(userList);
        }

        //GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            User user = new User();

            if (id == null)
            {
                
                return NotFound();
            }

            string path = "User/" + id;

            user = JsonConvert.DeserializeObject<User>(CallAPi(path).Result);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,Username,Password,IsLocal,BirthDate,ProfileUri,IsDeleted")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Email,Username,Password,IsLocal,BirthDate,ProfileUri,IsDeleted")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.User
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.Id == id);
        //}

        private async Task<string> CallAPi(string path)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://localhost:5001/api/"+ path);
                return response;
            }
        }
    }
}
