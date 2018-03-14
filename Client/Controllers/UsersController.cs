using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Client.BE;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        HttpClient client;

        // GET: Users
        public IActionResult Index()
        {
            if (client == null)
            {
                SetAccessToken();
            }

            List<User> userList = new List<User>();

            var response = client.GetStringAsync("http://localhost:5001/api/users/");
            userList = JsonConvert.DeserializeObject<List<User>>(response.Result);

            return View(userList);
        }

        //GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (client == null)
            {
                SetAccessToken();
            }

            User user = new User();

            if (id == null)
            {
                
                return NotFound();
            }
            
            var response = client.GetStringAsync("http://localhost:5001/api/users/" + id);
            user = JsonConvert.DeserializeObject<User>(response.Result);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //// GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,Username,Password,IsLocal,BirthDate,ProfileUri,IsDeleted")] User user)
        {
            if (client == null)
            {
                SetAccessToken();
            }

            if (ModelState.IsValid)
            {
                string userCredentials = user.Username + user.Password;
                user.Password = BCrypt.Net.BCrypt.HashPassword(userCredentials, SaltRevision.Revision2B);

                var myContent = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("http://localhost:5001/api/users/", byteContent).Result;

               //HttpContent content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
               //await client.PostAsync("http://localhost:5001/api/users/", content);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (client == null)
        //    {
        //        SetAccessToken();
        //    }

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
        //    if (client == null)
        //    {
        //        SetAccessToken();
        //    }

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
        //    if (client == null)
        //    {
        //        SetAccessToken();
        //    }

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
        //    if (client == null)
        //    {
        //        SetAccessToken();
        //    }

        //    var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.Id == id);
        //}

        public void SetAccessToken()
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").Result;

            client = new HttpClient();
            client.SetBearerToken(accessToken);
        }
    }
}
