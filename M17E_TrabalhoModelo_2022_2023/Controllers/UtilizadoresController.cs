using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using M17E_TrabalhoModelo_2022_2023.Data;
using M17E_TrabalhoModelo_2022_2023.Models;

namespace M17E_TrabalhoModelo_2022_2023.Controllers
{
    
    public class UtilizadoresController : Controller
    {
        private M17E_TrabalhoModelo_2022_2023Context db = new M17E_TrabalhoModelo_2022_2023Context();

        [Authorize(Roles = "Administrador")]
        // GET: Utilizadores
        public ActionResult Index()
        {
            return View(db.Utilizadors.ToList());
        }
        [Authorize(Roles = "Administrador")]
        // GET: Utilizadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }
      

        // GET: Utilizadores/Create
        public ActionResult Create()
        {
            var utilizador = new Utilizador();
            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };
            return View(utilizador);
        }
        
        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Password,Perfil,Estado")] Utilizador utilizador)
        {
            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };
            if (ModelState.IsValid)
            {
                //verificar se o nome de utilizador já existe
                var temp = db.Utilizadors.Where(u => u.Nome == utilizador.Nome).ToList();
                if (temp != null && temp.Count > 0)
                {
                    ModelState.AddModelError("Nome", "Já existe um utilizador com esse nome");
                    return View(utilizador);
                }
                //validar a password
                if (utilizador.Password.Trim().Length < 5)
                {
                    ModelState.AddModelError("Password", "A palavra passe deve ter pelo menos 5 letras");
                    return View(utilizador);
                }
                //hash password
                HMACSHA512 hMACSHA512 = new HMACSHA512(new byte[] { 2 });
                var password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Password));
                utilizador.Password = Convert.ToBase64String(password);
                db.Utilizadors.Add(utilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilizador);
        }

        // GET: Utilizadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            //se não é admin só pode editar a sua própria conta
            if (User.IsInRole("Administrador"))
            {

                utilizador.Perfis = new[]
                {
                    new SelectListItem{Value="0", Text="Administrador"},
                    new SelectListItem{Value="1", Text="Funcionário"}
                };
            }
            else
            {
                var temp = db.Utilizadors.Where(u => u.Nome == User.Identity.Name).FirstOrDefault();
                utilizador = temp;
                utilizador.Perfis = new[]
                {
                    new SelectListItem{Value="1", Text="Funcionário"}
                };
            }
            return View(utilizador);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Password,Perfil,Estado")] Utilizador utilizador)
        {
            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };
            if (ModelState.IsValid)
            {
                //validar a password
                if (utilizador.Password.Trim().Length < 5)
                {
                    ModelState.AddModelError("Password", "A palavra passe deve ter pelo menos 5 letras");
                    return View(utilizador);
                }
                //hash password
                HMACSHA512 hMACSHA512 = new HMACSHA512(new byte[] { 2 });
                var password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Password));
                utilizador.Password = Convert.ToBase64String(password);

                db.Entry(utilizador).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Administrador"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "Estadias");
            }
            if (User.IsInRole("Administrador") == false)
            {
                utilizador.Perfis = new[]
                {
                    new SelectListItem{Value="1", Text="Funcionário"}
                };
            }
            return View(utilizador);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Utilizadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizadors.Find(id);
            db.Utilizadors.Remove(utilizador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
