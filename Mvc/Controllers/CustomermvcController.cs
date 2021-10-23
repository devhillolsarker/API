using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class CustomermvcController : Controller
    {
        // GET: CustomMvc
        public ActionResult Index()
        {
            IEnumerable<MvcCustomerModel> customerList;
            HttpResponseMessage response = Global.WebApiClient.GetAsync("Customer").Result;
            customerList = response.Content.ReadAsAsync<IEnumerable<MvcCustomerModel>>().Result;
            return View(customerList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new MvcCustomerModel());
            }
            else
            {
                HttpResponseMessage response = Global.WebApiClient.GetAsync("Customer/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcCustomerModel>().Result);

            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(MvcCustomerModel customer)
        {
            if(customer.CustomerID == 0)
            {
                HttpResponseMessage response = Global.WebApiClient.PostAsJsonAsync("Customer", customer).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else{
                HttpResponseMessage response = Global.WebApiClient.PutAsJsonAsync("Customer/"+customer.CustomerID,customer).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = Global.WebApiClient.DeleteAsync("Customer/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}