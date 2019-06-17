using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace agenda.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: appointment
        public ActionResult GoAppointment()
        {
            return View("AddApointment");
        }

namespace agenda.Controllers
    {
        public class AppointmentsController : Controller
        {
            public ActionResult GoAppointment()
            {

                agendaEntities1 entities = new agendaEntities1();
                var getlastnameBroker = entities.brokers.ToList<broker>();
                SelectList list = new SelectList(getlastnameBroker, "idBroker", "lastname");
                ViewBag.brokerListName = list;

                agendaEntities1 entitie = new agendaEntities1();
                var getlastnameCustomer = entities.customers.ToList<customer>();
                SelectList listCustomer = new SelectList(getlastnameCustomer, "idCustomer", "lastname");
                ViewBag.customerListName = listCustomer;

                return View();
            }


            [HttpPost]
            public ContentResult GoAppointment(appointment appointments) // Permet de lier à la bdd
            {
            }
}