using CrudComAngularJsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudComAngularJsWebApi.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class CelularController : ApiController
    {

        private readonly CelularDbContext _db = new CelularDbContext();

        [HttpGet]
        [Route("celulares")]
        public IQueryable<Celular> ObterCelulares()
        {
            return _db.Celulares;
        }


        [HttpGet]
        [Route("celular/{id:int}")]
        public HttpResponseMessage ObterPorId(int id)
        {
            if (id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Celular celular = _db.Celulares.Find(id);

            return Request.CreateResponse(HttpStatusCode.OK, celular);
        }

        //CRUD - READ - UPDATE

        [HttpPut]
        [Route("putcelular")]
        public HttpResponseMessage Alterar(Celular celular)
        {
            if (celular == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            _db.Entry(celular).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        //CRUD - CREATE - DELETE
        [HttpPost]
        [Route("postcelular")]
        public HttpResponseMessage Incluir(Celular celular)
        {
            if (celular == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            _db.Celulares.Add(celular);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);

        }


        //CRUD - DELETE - 
        [HttpDelete]
        [Route("deletecelular/{id:int}")]
        public HttpResponseMessage Excluir(int id)
        {
            if (id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Celular celular = _db.Celulares.Find(id);
            _db.Celulares.Remove(celular);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
