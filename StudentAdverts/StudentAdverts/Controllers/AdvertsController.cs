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
using StudentAdverts.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;

namespace StudentAdverts.Controllers
{
    public class AdvertsController : ApiController
    {
        private AdvertsDatabase db = new AdvertsDatabase();


        public IQueryable<Advert> GetAdverts()
        {
            return db.Advert;
        }

        [ResponseType(typeof(Advert))]
        public IHttpActionResult GetAdvert(string title)
        {
            try
            {
                var advert = db.Advert.Where(a => a.title.ToUpper().Contains(title.ToUpper()));
                if (advert == null)
                {
                    return NotFound();
                }

                return Ok(advert);
            }
            catch (Exception ex)
            {
                return responseMaker(ex);
            }
        }

        [ResponseType(typeof(Advert))]
        public IHttpActionResult GetAdvertById(int id)
        {
            try
            {
                Advert advert = db.Advert.Find(id);
                if (advert == null)
                {
                    return NotFound();
                }

                return Ok(advert);
            }
            catch (Exception ex)
            {
                return responseMaker(ex);
            }

        }

        [Authorize]
        [ResponseType(typeof(Advert))]
        public IHttpActionResult GetUsersAdverts()
        {
            try
            {
                string userName = RequestContext.Principal.Identity.GetUserName();
                var advert = db.Advert.Where(a => a.author == userName);
                if (advert == null)
                {
                    return NotFound();
                }

                return Ok(advert);
            }
            catch (Exception ex)
            {
                return responseMaker(ex);
            }
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdvert(int id, Advert advert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != advert.id)
            {
                return BadRequest();
            }

            string userName = RequestContext.Principal.Identity.GetUserName();

            if (advert.author != userName)
            {
                return Unauthorized();
            }

            db.Entry(advert).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertExists(id))
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

        [Authorize]
        [ResponseType(typeof(Advert))]
        public IHttpActionResult PostAdvert(Advert advert)
        {          
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                advert.author = RequestContext.Principal.Identity.GetUserName();
                db.Advert.Add(advert);
                db.SaveChanges();


                return CreatedAtRoute("DefaultApi", new { id = advert.id }, advert);
            }
            catch (Exception ex)
            {
                return responseMaker(ex);
            }
        }

        [Authorize]
        [ResponseType(typeof(Advert))]
        public IHttpActionResult DeleteAdvert(int id)
        {
            try
            {
                Advert advert = db.Advert.Find(id);
                if (advert == null)
                {
                    return NotFound();
                }
                string userName = RequestContext.Principal.Identity.GetUserName();
                if (advert.author != userName)
                {
                    return Unauthorized();
                }

                db.Advert.Remove(advert);
                db.SaveChanges();

                return Ok(advert);
            }
            catch (Exception ex)
            {
                return responseMaker(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdvertExists(int id)
        {
            return db.Advert.Count(e => e.id == id) > 0;
        }

        public IHttpActionResult responseMaker(Exception ex)
        {
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            var message = string.Format(ex.HResult + " Message: " + ex.Message);
            responseMsg.Content = new StringContent(message);
            IHttpActionResult response = this.ResponseMessage(responseMsg);
            return response;
        }
    }
}