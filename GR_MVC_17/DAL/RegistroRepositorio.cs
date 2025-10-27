using GR_MVC_17.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GR_MVC_17.DAL
{
    public class RegistroRepositorio : IRegistroRepositorio
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<RegistroRutas> dameRegistroHerramienta(int idHerramienta, int idPerfil)
        {
            List<RegistroRutas> listaRegistros = (from j in db.RegistroRutas where 
                                                  j.IdHerramienta == idHerramienta && j.IdPerfil == idPerfil
                                                  orderby j.Fecha descending
                                                  select j).ToList();
            return listaRegistros;
        }

        public List<RegistroRutas> dameRegistros(int idUsuario)
        {
            List<RegistroRutas> listaRegistros = (from j in db.RegistroRutas
                                                  where
                                                  j.IdUsuario == idUsuario
                                                  orderby j.Fecha descending
                                                  select j).ToList();
            return listaRegistros;
        }


        public Boolean AñadirRegistroRuta(RegistroRutas registro)
        {
            try
            {
                    db.RegistroRutas.Add(registro);
                    db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        public double dameCalculoPorHerramienta(int idUsuario, int idHerramienta)
        {
            if (db.RegistroRutas.Where(x => x.IdUsuario == idUsuario && x.IdHerramienta == idHerramienta).Select(y => y.Km).FirstOrDefault() > 0)
            {
                return db.RegistroRutas.Where(x => x.IdUsuario == idUsuario && x.IdHerramienta == idHerramienta).Sum(x => x.Km).Value;
            }
            else
            {
                return 0;
            }
            
        }

        public bool eliminarRegistroRutas(int? id)
        {
            if (id == null)
            {
                return false;
            }

            RegistroRutas existe = new RegistroRutas();
            existe = db.RegistroRutas.Where(x => x.Id == id).FirstOrDefault();

            if (existe!= null)
            {
                db.RegistroRutas.Remove(existe);
                db.SaveChanges();


                return true;
            }

            return false;

        }

        public RegistroRutas dameRegistroPorId(int idRegistro)
        {
            return db.RegistroRutas.Where(x => x.Id == idRegistro).FirstOrDefault();
        }
    }
}