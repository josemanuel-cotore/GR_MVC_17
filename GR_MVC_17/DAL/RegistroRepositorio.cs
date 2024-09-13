using GR_MVC_17.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class RegistroRepositorio
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
                return db.RegistroRutas.Where(x => x.IdUsuario == idUsuario && x.IdHerramienta == idHerramienta).Sum(x => x.Km);
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

        //REGISTROS DUATLóN
        public List<RegistroRutas_Partial> dameRegistroPorPerfil(int idPerfil)
        {
            RutasRepositorio repoRutas = new RutasRepositorio();
           var L = db.RegistroRutas.Where(x => x.IdPerfil == idPerfil).OrderBy(x=>x.Fecha).ToList();

            List<RegistroRutas_Partial> listaPartial = new List<RegistroRutas_Partial>();
            listaPartial = convierteListaRegistrosEnPartial(L);

            listaPartial = listaPartial.OrderBy(x => x.Fecha).ToList();

            return listaPartial;
        }

        public List<RegistroRutas_Partial> convierteListaRegistrosEnPartial(List<RegistroRutas> lista)
        {
            RutasRepositorio rutasRepo = new RutasRepositorio();
            List<RegistroRutas_Partial> listaPartial = new List<RegistroRutas_Partial>();

            foreach (var registro in lista)
            {
                RegistroRutas_Partial r = new RegistroRutas_Partial();

                r.Id = registro.Id;
                r.Fecha = registro.Fecha;
                r.Km_Correr = registro.Km;
                r.Km_Bici = registro.Km_Bici;
                r.IdUsuario = registro.IdUsuario;
                r.IdPerfil = registro.IdPerfil;
                r.IdRuta = registro.IdRuta;
                r.IdHerramienta = registro.IdHerramienta;
                r.IdInconveniente = registro.IdInconveniente;
                r.IdRutaBici = registro.IdRutaBici;
                r.TiempoRutaCorrer = registro.TiempoRutaCorrer;
                r.TiempoRutaBici = registro.TiempoRutaBici;
                if (registro.Observaciones != null)
                {
                    r.Observaciones = registro.Observaciones.Replace("\n", "<br />"); ;
                }
               
                
                r.RutasUsuario = registro.RutasUsuario;
                r.TipoInconveniente = registro.TipoInconveniente;
                r.Usuario = registro.Usuario;

                r.nombreRutaCorrer = rutasRepo.dameNombreRuta(registro.IdRuta);
                r.nombreRutaBici = rutasRepo.dameNombreRuta(registro.IdRutaBici);

                listaPartial.Add(r);
            }

            return listaPartial;
        }
    }
}