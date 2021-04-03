using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using ventaVehiculosModels.Models;
using ventaVehiculosModels.Models.DTOs;
using ventaVehiculosAPI.Classes.Modules;
using ventaVehiculosAPI.Classes.JWT;
using System.Collections;

namespace ventaVehiculosAPI.Controllers
{
    public class carController : ApiController
    {
        [Authorize]
        [HttpGet, Route("~/api/getAllCars")]
        [SwaggerOperation("getAllCars")]
        public HttpResponseMessage getAllCars()
        {
            try
            {
                carActions car = new carActions();
                List<carDTO> cars = null;
                var response = Request.CreateResponse(HttpStatusCode.NotImplemented);
                cars = car.getAllCars();
                if (cars.Count > 0)
                {
                    response = Request.CreateResponse<IEnumerable>(HttpStatusCode.OK, cars);
                }
                else 
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound,"No hay vehiculos en el sistema");
                }

                return response;
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

    }
}
