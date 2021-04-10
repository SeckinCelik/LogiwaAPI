using Logiwa.Models.Dto;
using Logiwa.Repository;
using Logiwa.Repository.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LogiwaAPI.Controllers
{
    [RoutePrefix("logiwa/product")]
    public class ProductController : ApiController
    {
        private IRepository _repository;

        public ProductController()
        {
            _repository = new ProductRepository();
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                var product = _repository.Add(productDto);

                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Product NOT Added");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                bool isSaved = _repository.Update(productDto);

                if (isSaved)
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Saved Successfully");
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Product NOT Saved");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("filter")]
        public HttpResponseMessage FilterProduct([FromBody] FilterDto filterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                return Request.CreateResponse(HttpStatusCode.OK, _repository.Filter(filterDto));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _repository.GetAll());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("getById")]
        public HttpResponseMessage GetAll([FromUri] string productId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _repository.Get(productId));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
