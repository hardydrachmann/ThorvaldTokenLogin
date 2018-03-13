using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.BE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var response = "";
            List<Role> roles = new List<Role>();
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("http://localhost:5001/api/Roles");
            }
            roles = JsonConvert.DeserializeObject<List<Role>>(response);
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var response = "";
            Role role = new Role();

            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);
            }

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
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(role);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await client.PostAsync("http://localhost:5001/api/roles/", byteContent);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var response = "";
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);
            }

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
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(role);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await client.PutAsync("http://localhost:5001/api/roles/", byteContent);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var response = "";
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("http://localhost:5001/api/Roles/" + id);
            }

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
            using (var client = new HttpClient())
            {
                await client.DeleteAsync("http://localhost:5001/api/roles/" + id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}