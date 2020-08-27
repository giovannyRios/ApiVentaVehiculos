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

namespace ventaVehiculosAPI.Controllers
{
    public class LoginController : ApiController
    { 
        [AllowAnonymous]
        [HttpPost,Route("~/api/login")]
        [SwaggerOperation("login")]
        public HttpResponseMessage login(dateUsersModels objUser)
        {
            try {
                UserActions userActions = new UserActions();
                List<loginUserDTO> users = null;
                var response = Request.CreateResponse(HttpStatusCode.NotImplemented);
                dateUsersModels objDTO_User = new dateUsersModels { user = objUser.user, pass = objUser.pass };
                users = userActions.getUserName(objDTO_User);

                if (users.Count > 0)
                {
                    var token = TokenGenerator.tokenGeneratorJWT(objUser.user);
                    validacionToken objValToken = new validacionToken();
                    if (objValToken.Validation(token))
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK,users);
                        response.Headers.Add("token", token);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized,"Token de seguridad invalido");
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no existe en el sistema");
                }

            return response;

            } catch (Exception e) {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
            
        }
    }
}
