using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using agenda.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace agenda.Controllers
{
    public class CustomerController : Controller
    {
        private agendaEntities1 db = new agendaEntities1();
        // GET: Customer
        public ActionResult GoCustomer(int id = 0) // Methode qui permet l'affichage de la vue
        {
            customer customer = new customer();
            return View("AddCustomer"); //Affiche la vue
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoCustomer(customer customer) // création de la méthode
        {
            db.customers.Add(customer);
            db.SaveChanges(); //Enregistre les données  
            ModelState.Clear();
            ViewBag.MessageSuccess = "Succés"; // Envoi un message de succés 
            return View("AddCustomer", new customer()); // Afiche la vue
        }

        public ActionResult ListCustomers()
        {
            agendaEntities1 db = new agendaEntities1();
            string query =
                "SELECT [idCustomer], [lastname], [firstname], [mail], [phoneNumber], [budget]"
                + "FROM [dbo].[customers]"
                + "ORDER BY [lastname];";
            IEnumerable<agenda.Models.customer> data = db.Database.SqlQuery<agenda.Models.customer>(query);// Enumére une à une les données 
            return View("ListCustomers", data.ToList()); // Affiche la vue en list
            // A la place de ienumerable et tolist ==> ViewData.Model = db.customers.SqlQuery(query);
        }
        public ActionResult ProfilCustomer(int id = 0)
        {
            return View("ProfilCustomer", db.customers.Find(id)); // find renvoi la valeur du premier élément trouvé.
        }

        public ActionResult EditProfilCustomer(int id)
        {
            return View("EditCustomer", db.customers.Find(id));
        }
        [HttpPost]
        public ActionResult EditProfilCustomer(customer EditToCustomer)
        {
            try // relève une exception
            {
                db.Entry(EditToCustomer).State = EntityState.Modified;
                db.SaveChanges(); // enregistre 
                return RedirectToAction("ProfilCustomer/" + EditToCustomer.idCustomer); // renvoi au profil du client plus l'edit de son id
            }
            catch // fonctionne avec try relève une exception
            {
                return View(); //
            }
        }
        public ActionResult DeleteCustomer(int? id) //Création de la méthode DeleteCustomer
        {
            var customerToFind = db.customers.Find(id); // appelle la variable customerToFind et renvoi à l'id 
            if (customerToFind == null || id == null)
            {
                return HttpNotFound(); // retourne sur une page d'erreur
            }

            return View("DeleteCustomer", customerToFind); //Affiche la vue DeleteCustomer de notre db qui à l'id du parametre
        }
        [HttpPost] //Methode Post 
        public ActionResult DeleteCustomer(customer DeleteToCustomer) // objet DeleteToCustomer
        {
            try // Si les données sont valides 
            {
                customer CustomerToDelete = db.customers.Find(DeleteToCustomer.idCustomer);
                db.customers.Remove(CustomerToDelete); // Supprime
                db.SaveChanges(); // Enregistre
                return RedirectToAction("ListCustomers"); // Renvoi à la liste des clients
            }
            catch
            {
                return View("DeleteCustomer"); //Retourne la vue DeleteCustomer
            }
        }
    }
}

//customer customerToDisplay = db.customers.Find(DeleteToCustomer);
//if (customerToDisplay == null)
//{
//    return View("Erreur");
//}
//return View("deleteProfilCustomer", customerToDisplay);