using GR_MVC_17.Partial;
using GR_MVC_17.Servicios;
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
                if (registro.IdPerfil == (int) Enums.Enum.Perfil.Duatlón || registro.IdPerfil == (int)Enums.Enum.Perfil.Carreras)
                {
                    // Añadimos en primer lugar el registro del Perfil (4) Duatlón o (5) Carrera

                    List<RegistroRutas> listaRegs = new List<RegistroRutas>();
                    listaRegs.Add(registro);

                    //Insertar registro en Perfil Running (1)
                    RegistroRutas primerRegistroCorrer = new RegistroRutas()
                    {
                        Fecha = registro.Fecha,
                        IdUsuario = registro.IdUsuario,
                        IdPerfil = (int)Enums.Enum.Perfil.Running, // Running
                        IdHerramienta = 2, // La Sportiva Bushido III
                        IdInconveniente = registro.IdInconveniente,

                        Km = registro.Km,
                        IdRuta = registro.IdRuta,
                    };

                    listaRegs.Add(primerRegistroCorrer);

                    // Si es Duatlón o Carrera Duatlón
                    if (registro.EsDuatlon.Value == true)
                    {
                        //Insertar registro en Perfil Ciclismo (3)
                        RegistroRutas segundoRegistroBici = new RegistroRutas()
                        {
                        Fecha = registro.Fecha,
                        IdUsuario = registro.IdUsuario,
                        IdPerfil = (int)Enums.Enum.Perfil.Ciclismo, // Ciclismo
                        IdHerramienta = 6, // Coluer Limbo 292 2021
                        IdInconveniente = registro.IdInconveniente,

                        Km = registro.Km_Bici,
                        IdRuta = registro.IdRutaBici,
                        };

                        listaRegs.Add(segundoRegistroBici);


                        //Insertar registro en Perfil Running (1)
                        RegistroRutas tercerRegistroCorrer = new RegistroRutas()
                        {
                            Fecha = registro.Fecha,
                            IdUsuario = registro.IdUsuario,
                            IdPerfil = (int)Enums.Enum.Perfil.Running, // Running
                            IdHerramienta = 2, // La Sportiva Bushido III
                            IdInconveniente = registro.IdInconveniente,

                            Km = registro.Km_Alternativa,
                            IdRuta = registro.IdRuta_Alternativa,

                        };

                        listaRegs.Add(tercerRegistroCorrer);
                    }

                    foreach (var reg in listaRegs)
                    {
                        db.RegistroRutas.Add(reg);
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.RegistroRutas.Add(registro);
                    db.SaveChanges();
                }
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
                //Alternativa
                r.IdRuta_Alt = registro.IdRuta_Alternativa;
                r.Km_Correr_Alt = registro.Km_Alternativa;
                r.TiempoRutaCorrer_Alt = registro.TiempoRutaCorrer_Alternativa;
                if (registro.Observaciones != null)
                {
                    r.Observaciones = registro.Observaciones.Replace("\n", "<br />"); ;
                }
               
                
                r.RutasUsuario = registro.RutasUsuario;
                r.TipoInconveniente = registro.TipoInconveniente;
                r.Usuario = registro.Usuario;

                r.nombreRutaCorrer = rutasRepo.dameNombreRuta(registro.IdRuta);
                r.nombreRutaBici = rutasRepo.dameNombreRuta(registro.IdRutaBici);
                r.nombreRutaCorrer_Alt = rutasRepo.dameNombreRuta(registro.IdRuta_Alternativa);

                listaPartial.Add(r);
            }

            return listaPartial;
        }

        public RegistroRutas dameRegistroPorId(int idRegistro)
        {
            return db.RegistroRutas.Where(x => x.Id == idRegistro).FirstOrDefault();
        }
    }
}