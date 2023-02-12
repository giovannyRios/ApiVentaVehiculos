using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using ventaVehiculosModels.Models.Log;

namespace ventaVehiculosAPI.Classes.JWT
{
    public class validacionToken
    {
        /// <summary>
        ///     Valida Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        ///     true or false
        /// </returns>
        public bool Validation(string token)
        {
            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET-KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SecurityTokenValidationException se)
            {
                cunsumirLog.crearRegistroLog("ventaVehiculosAPI" + DateTime.Now.ToShortDateString(), "Ha ocurrido un error en el metodo validation " + se.ToString());
                return false;
            }
            catch (Exception e)
            {
                cunsumirLog.crearRegistroLog("ventaVehiculosAPI" + DateTime.Now.ToShortDateString(), "Ha ocurrido un error en el metodo validation " + e.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Valida el tiempo de vida del token
        /// </summary>
        /// <param name="notBefore"></param>
        /// <param name="expires"></param>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <returns>
        ///     true or false
        /// </returns>
        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}