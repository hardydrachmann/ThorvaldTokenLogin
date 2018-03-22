using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.BE;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {

        HttpClient client;

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            setAccessToken();
            List<Role> roles = new List<Role>();

            var response = await client.GetStringAsync("http://localhost:5001/api/Roles");

            roles = JsonConvert.DeserializeObject<List<Role>>(response);
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setAccessToken();
            Role role = new Role();

            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);

            role = JsonConvert.DeserializeObject<Role>(response);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            setAccessToken();
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Role role)
        {
            setAccessToken();
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(role);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await client.PostAsync("http://localhost:5001/api/roles/", byteContent);

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setAccessToken();
            if (id == null)
            {
                return NotFound();
            }
            
            var response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);
            
            var role = JsonConvert.DeserializeObject<Role>(response);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Role role)
        {
            setAccessToken();
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(role);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await client.PutAsync("http://localhost:5001/api/roles/", byteContent);

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setAccessToken();
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);
            
            var role = JsonConvert.DeserializeObject<Role>(response);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setAccessToken();

            await client.DeleteAsync("http://localhost:5001/api/roles/" + id);
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
    }
}