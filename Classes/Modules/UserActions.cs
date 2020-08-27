using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ventaVehiculosModels.Models.EntityFramework;
using System.Data.Entity;
using AutoMapper;
using ventaVehiculosModels.Models.DTOs;
using ventaVehiculosModels.Models;


namespace ventaVehiculosAPI.Classes.Modules
{
    public class UserActions
    {
        /// <summary>
        /// Metodo que realiza la validacion de usuario y contraseña en el sistema
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<loginUserDTO> getUserName(dateUsersModels obj)
        {
            using (ventaVehiculosEntities db = new ventaVehiculosEntities())
            {
                try
                {
                    return Mapper.Map<List<loginUserDTO>>(db.loginUser.Where(p => p.UserStr == obj.user)
                                                                       .Where(p => p.pass == obj.pass));
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }
    }
}