using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M17E_TrabalhoModelo_2022_2023.Data;
using M17E_TrabalhoModelo_2022_2023.Models;

namespace M17E_TrabalhoModelo_2022_2023.Controllers
{
    [Authorize]
    public class EstadiasController : Controller
    {
        private M17E_TrabalhoModelo_2022_2023Context db = new M17E_TrabalhoModelo_2022_2023Context();

        // GET: Estadias
        public ActionResult Index()
        {
            var estadias = db.Estadias.Include(e => e.cliente).Include(e => e.quarto);
            return View(estadias.ToList());
        }

        // GET: Estadias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = db.Estadias.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            return View(estadia);
        }

        // GET: Estadias/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome");
            ViewBag.QuartoID = new SelectList(db.Quartos.Where( q => q.Estado==true ), "Id", "Tipo_Quarto");
            return View();
        }

        // POST: Estadias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadiaId,data_entrada,data_saida,valor_pago,ClienteID,QuartoID")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                estadia.data_saida = estadia.data_entrada;
                estadia.valor_pago = 0;
                //alterar o estado do quarto
                var quarto = db.Quartos.Find(estadia.QuartoID);
                quarto.Estado = false;
                db.Entry(quarto).CurrentValues.SetValues(quarto);
                db.Estadias.Add(estadia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos, "Id", "Tipo_Quarto", estadia.QuartoID);
            return View(estadia);
        }

        // GET: Estadias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = db.Estadias.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos, "Id", "Tipo_Quarto", estadia.QuartoID);
            return View(estadia);
        }

        // POST: Estadias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadiaId,data_entrada,data_saida,valor_pago,ClienteID,QuartoID")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos, "Id", "Tipo_Quarto", estadia.QuartoID);
            return View(estadia);
        }

        //// GET: Estadias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Estadia estadia = db.Estadias.Find(id);
        //    if (estadia == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(estadia);
        //}

        //// POST: Estadias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Estadia estadia = db.Estadias.Find(id);
        //    db.Estadias.Remove(estadia);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Lista das estadias que ainda não acabaram
        public ActionResult ListaEstadiasEmCurso()
        {
            var estadias = db.Estadias.Where( e => e.valor_pago==0 && e.data_saida==e.data_entrada)
                .Include(e => e.cliente).Include(e => e.quarto);
            return View(estadias.ToList());
        }

        public ActionResult ProcessaSaida(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var estadia = db.Estadias.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            //calcular o valor a pagar
            //dados do quarto
            var quarto = db.Quartos.Find(estadia.QuartoID);
            //duração da estadia em dias
            TimeSpan dias = DateTime.Now.Date.Subtract(estadia.data_entrada);
            int diasPagar = (int)(dias.TotalDays <= 0 ? 1 : dias.TotalDays);
            estadia.valor_pago = diasPagar * quarto.Custo_dia;
            estadia.data_saida = DateTime.Now.Date;
            ViewBag.dias = diasPagar;
            estadia.cliente = db.Clientes.Find(estadia.ClienteID);
            return View(estadia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessaSaida(Estadia estadia)
        {
            //atualiza a estadia
            db.Entry(estadia).State= EntityState.Modified;

            //atualiza o quarto
            var quarto = db.Quartos.Find(estadia.QuartoID);
            quarto.Estado = true;
            db.Entry(quarto).CurrentValues.SetValues(quarto);
            db.SaveChanges();

            return RedirectToAction("ListaEstadiasEmCurso");
        }
    }
}
