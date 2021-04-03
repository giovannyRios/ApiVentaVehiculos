using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ventaVehiculosModels.Models.EntityFramework;
using AutoMapper;
using ventaVehiculosModels.Models.DTOs;
using ventaVehiculosModels.Models;

namespace ventaVehiculosAPI.Classes.Modules
{
    public class carActions
    {
        public List<carDTO> getAllCars()
        {
            using (var objConexion = new ventaVehiculosEntities())
            {
                try 
                {
                    return Mapper.Map<List<carDTO>>(objConexion.car);
                } 
                catch (Exception e) 
                { 
                    throw e; 
                }
            }
        }
    }
}