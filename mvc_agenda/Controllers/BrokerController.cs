using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using agenda.Models;

namespace agenda.Controllers
{
    public class BrokerController : Controller
    {
        private agendaEntities1 db = new agendaEntities1();

        public ActionResult Gobroker(int id = 0) // Methode pour aller à la view AddBroker.
        {
            broker broker = new broker();
            return View("AddBroker"); //nom de la view.
        }

        [HttpPost]
        public ActionResult GoBroker (broker broker)
        {
            using (agendaEntities1 db = new agendaEntities1())
            {
                db.brokers.Add(broker);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.MessageSuccess = "successful";
            return View("AddBroker", new broker());
        }
        public ActionResult ListBrokers (int id = 0)
        {
            agendaEntities1 db = new agendaEntities1();
            return View("ListBrokers", db.brokers.ToList());
        }
        //return View(db.brokers.Where(x=> x.idBroker == id).FirstOrDefault());
        public ActionResult ProfilBroker(int? id)
        {
            var profilBrokerToFind = db.brokers.Find(id);
            if (profilBrokerToFind == null || id == null)
            {
                return HttpNotFound();
            }
            return View("profilBroker", "profilBrokerToFind");
        }

        public ActionResult EditProfilBroker(int id = 0)
        {
            return View("EditBroker", db.brokers.Find(id));
        }
        [HttpPost]
        public ActionResult EditProfilBroker(broker EditToBroker)
        {
            try
            {
                db.Entry(EditToBroker).State = EntityState.Modified; // si pas d'eerreur les changement s'effectue
                db.SaveChanges();
                return RedirectToAction("ProfilBroker/" + EditToBroker.idBroker); //redirige une fois le changement sauvegardé vers ...
            }
            catch
            {
                return View();
            }
        }
    }
}