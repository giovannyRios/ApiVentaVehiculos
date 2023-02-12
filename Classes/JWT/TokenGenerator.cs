using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using ventaVehiculosModels.Models.Log;

namespace ventaVehiculosAPI.Classes.JWT
{
    internal static class TokenGenerator
    {
        public static string tokenGeneratorJWT(string userName)
        {
            try
            {
                var key = ConfigurationManager.AppSettings["JWT_SECRET-KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var IssuerKey = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var timeLifeToken = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

                var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(key));
                var credencials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) });

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

                var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                    audience: audienceToken,
                    issuer: IssuerKey,
                    subject: claimsIdentity,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(timeLifeToken)),
                    signingCredentials: credencials);

                var strToken = tokenHandler.WriteToken(jwtSecurityToken);

                return strToken;

            }
            catch (Exception e)
            {
                cunsumirLog.crearRegistroLog("ventaVehiculosAPI" + DateTime.Now.ToShortDateString(), "Ha ocurrido un error en el metodo tokenGeneratorJWT " + e.ToString());
                return "";
            }
        }
    }
}