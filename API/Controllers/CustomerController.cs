using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class CustomerController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Customer
        public IQueryable<CustomerTable> GetCustomerTables()
        {
            return db.CustomerTables;
        }

        // GET: api/Customer/5
        [ResponseType(typeof(CustomerTable))]
        public IHttpActionResult GetCustomerTable(int id)
        {
            CustomerTable customerTable = db.CustomerTables.Find(id);
            if (customerTable == null)
            {
                return NotFound();
            }

            return Ok(customerTable);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerTable(int id, CustomerTable customerTable)
        {
            if (id != customerTable.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customerTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer
        [ResponseType(typeof(CustomerTable))]
        public IHttpActionResult PostCustomerTable(CustomerTable customerTable)
        {

            db.CustomerTables.Add(customerTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerTable.CustomerID }, customerTable);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(CustomerTable))]
        public IHttpActionResult DeleteCustomerTable(int id)
        {
            CustomerTable customerTable = db.CustomerTables.Find(id);
            if (customerTable == null)
            {
                return NotFound();
            }

            db.CustomerTables.Remove(customerTable);
            db.SaveChanges();

            return Ok(customerTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerTableExists(int id)
        {
            return db.CustomerTables.Count(e => e.CustomerID == id) > 0;
        }
    }
}