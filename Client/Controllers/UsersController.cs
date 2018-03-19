using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Client.BE;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using Client.Models;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        HttpClient client;

        // GET: Users
        public IActionResult Index()
        {
            setAccessToken();

            List<User> userList = new List<User>();

            var response = client.GetStringAsync("http://localhost:5001/api/users/");
            userList = JsonConvert.DeserializeObject<List<User>>(response.Result);

            return View(userList);
        }

        //GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            setAccessToken();

            if (id == null)
            {
                return NotFound();
            }

            User user = getUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            setAccessToken();

            UserViewModel uvm = new UserViewModel();
            List<Role> roleList = new List<Role>();

            var response = client.GetStringAsync("http://localhost:5001/api/Roles/");
            roleList = JsonConvert.DeserializeObject<List<Role>>(response.Result);

            if (roleList != null)
            {
                uvm.Roles = roleList;
                uvm.User = null;
            }
            return View(uvm);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,Username,Password,ProfileUri,IsDeleted")] User user, [Bind("Id, Name")] List<Role> roles)
        {
            setAccessToken();
            List<Role> selectedRoleList = new List<Role>();
            UserViewModel uvm = new UserViewModel();
            if (ModelState.IsValid)
            {
                user = encryptUserCredentials(user);

                foreach (var role in roles)
                {
                    if (role.isChecked == true)
                    {
                        selectedRoleList.Add(role);
                    }
                }
                uvm.User = user;
                uvm.Roles = selectedRoleList;

                var myContent = JsonConvert.SerializeObject(uvm);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("http://localhost:5001/api/users/", byteContent).Result;

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            UserViewModel uvm = new UserViewModel();
            List<Role> roleList = new List<Role>();

            setAccessToken();

            if (id == null)
            {
                return NotFound();
            }

            User user = getUserById(id);
            var response = client.GetStringAsync("http://localhost:5001/api/Roles/");
            roleList = JsonConvert.DeserializeObject<List<Role>>(response.Result);

            if (user == null)
            {
                return NotFound();
            }

            List<Role> newRoleList = new List<Role>();
            foreach (var role in roleList)
            {
                foreach (var userRole in user.Roles)
                {
                    if (userRole.Id == role.Id)
                    {
                        role.isChecked = true;
                    }
                }
                newRoleList.Add(role);
            }
            uvm.User = user;
            uvm.Roles = newRoleList;

            return View(uvm);
        }

        // PUT: Users/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("Id,Firstname,Lastname,Email,Username,Password,ProfileUri,IsDeleted")] User user, [Bind("Id, Name")] List<Role> roles)
        {
            setAccessToken();
            List<Role> selectedRoleList = new List<Role>();
            UserViewModel uvm = new UserViewModel();

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user = encryptUserCredentials(user);

                    foreach (var role in roles)
                    {
                        if (role.isChecked == true)
                        {
                            selectedRoleList.Add(role);
                        }
                    }
                    uvm.User = user;
                    uvm.Roles = selectedRoleList;

                    var myContent = JsonConvert.SerializeObject(uvm);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = client.PutAsync("http://localhost:5001/api/users/", byteContent).Result;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        //GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setAccessToken();

            if (id == null)
            {
                return NotFound();
            }

            User user = getUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5 : only soft deletes a user in the database (using UPDATE)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setAccessToken();
            UserViewModel uvm = new UserViewModel();


            User user = getUserById(id);
            user.IsDeleted = true;

            uvm.User = user;
            uvm.Roles = null;

            var myContent = JsonConvert.SerializeObject(uvm);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("http://localhost:5001/api/users/", byteContent).Result;

            return RedirectToAction(nameof(Index));
        }

        private void setAccessToken()
        {
            if (client == null)
            {
                var accessToken = HttpContext.GetTokenAsync("access_token").Result;

                client = new HttpClient();
                client.SetBearerToken(accessToken);
            }
        }

        private User getUserById(int? id)
        {
            User user = new User();

            var response = client.GetStringAsync("http://localhost:5001/api/users/" + id);
            user = JsonConvert.DeserializeObject<User>(response.Result);
            return user;
        }

        private User encryptUserCredentials(User user)
        {
            string userCredentials = user.Username + user.Password;
            user.Password = BCrypt.Net.BCrypt.HashPassword(userCredentials, SaltRevision.Revision2B);
            return user;
        }
    }
}
