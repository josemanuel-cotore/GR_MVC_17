using GR_MVC_17.DAL;
using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;

namespace GR_MVC_17.Controllers
{
    public class HomeController : Controller
    {
        public UsuarioRepositiorio repoUsuario = new UsuarioRepositiorio();
        public ZapatillaRepositorio repoHerramienta = new ZapatillaRepositorio();
        public RutasRepositorio repoRutas = new RutasRepositorio();
        public RegistroRepositorio repoRegistro = new RegistroRepositorio();
        public PerfilRepositiorio repoPerfil = new PerfilRepositiorio();
        public InconvenienteRepositorio repoInconveniente = new InconvenienteRepositorio();

        public ActionResult Index()
        {
            return View();
        }

        /////
        /// <summary>
        /// PANTALLA DE LOGIN
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario = repoUsuario.ExisteUsuario(usuario);
                if (usuario != null)
                {
                   return RedirectToAction("Registro", "Home", new { idUsuario = usuario.Id, pagina = 1 });       
                }
                else
                {
                    ViewBag.Error = "El usuario no existe";
                    return View("Index");
                }

            }
            return PartialView();
        }


        /////
        /// <summary>
        /// PANTALLA DE REGISTRO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>

        public ActionResult Registro(int idUsuario, int pagina = 1)
        {
            var listaRegistro = repoRegistro.dameRegistros(idUsuario);
            ViewBag.Usuario = idUsuario;
            int totalRegistros = listaRegistro.Count();

            listaRegistro = listaRegistro.Skip((pagina - 1) * 7).Take(7).ToList();


            int cantidadPaginas = (int)Math.Ceiling((double)totalRegistros / 7);


            BasePaginacion bPaginacion = new BasePaginacion()
            {
                PaginaActual = pagina,
                RegistrosPorPagina = 7,
                TotalRegistros = totalRegistros,
                CantidadDePaginas = cantidadPaginas

            };

            ViewBag.BasePaginacionV = bPaginacion;

            return View(listaRegistro);
        }


        [HttpPost]
        public ActionResult _PasoParaCrearRegistro(int idUsuario)
        {

            var listaHerramientas = repoHerramienta.dameZapatillasUsuario(idUsuario);
            ViewBag.Herramientas = listaHerramientas.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            var listaPerfiles = repoPerfil.DameListaPerfilesUsuario(new Usuario { Id = idUsuario });
            ViewBag.Perfiles = listaPerfiles.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            var listaRutas = repoRutas.DameRutasOrdenNuevoSinPerfil(idUsuario);
            ViewBag.Rutas = listaRutas.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.nombre }).ToList<SelectListItem>();

            var listaInconvenientes = repoInconveniente.dameInconvenientes();
            ViewBag.Inconvenientes = listaInconvenientes.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            SeguimientoRegistro_DTO seguimiento = new SeguimientoRegistro_DTO();
            seguimiento.idUsuario_S = idUsuario;

            return PartialView("_CrearRegistro", seguimiento);
        }


        [HttpPost]
        public JsonResult _CrearRegistro(DateTime fecha, double km, int idHerramienta, int idPerfil, int ruta, int inconve, int idUsuario)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            try
            {
                if (km < 1)
                {
                    throw new ApplicationException("Error: Debes informar el campo KM");
                }

                RegistroRutas registro = new RegistroRutas
                {
                    Fecha = fecha,
                    Km = km,
                    IdRuta = ruta,
                    IdUsuario = idUsuario,
                    IdHerramienta = idHerramienta,
                    IdPerfil = idPerfil,
                    IdInconveniente = inconve
                };

                // Insertar registro ruta
                if (repoRegistro.AñadirRegistroRuta(registro))
                {
                    respuesta.ok = true;
                    respuesta.mensaje = "Registro Correcto";
                }

            }
            catch (Exception ex)
            {
                respuesta.ok = false;
                respuesta.mensaje = ex.Message;
            }

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult _ListadoPerfiles(string id)
        {
            return PartialView();
        }



        /////
        /// <summary>
        /// PANTALLA DE RUTAS
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>


        [HttpPost]
        public PartialViewResult _RutasDisponibles()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _PasoParaVerRutasDisponibles(int idUsuario, int idPerfil)
        {
            var listadoRutasUsuario = repoRutas.DameRutasUsuarioPerfil(idUsuario, idPerfil);
            //ViewBag.ListadoRutas = listadoRutasUsuario;

            return PartialView("_RutasDisponibles", listadoRutasUsuario);
        }



        /////
        /// <summary>
        /// PANTALLA DE ZAPATILLAS
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ActionResult Zapatillas(int idUsuario)
        {
            ViewBag.IdUsuario = idUsuario;

            var listaHerramientas = repoHerramienta.dameZapatillasUsuario(idUsuario);

            var listaHerramientasDTO = new List<Herramienta_DTO>();   

            foreach (var item in listaHerramientas)
            {
                var herramientaDTO = new Herramienta_DTO
                {
                    Nombre = item.Nombre,
                    Imagen = item.Imagen,
                    Tipo = item.HerramientasUsuario.FirstOrDefault().Perfil.Nombre,
                    Km = repoRegistro.dameCalculoPorHerramienta(item.HerramientasUsuario.FirstOrDefault().idUsuario, item.Id)
                };

                listaHerramientasDTO.Add(herramientaDTO);
            }




            return View(listaHerramientasDTO);
        }
       


        // Hago un POST _pasoParaCrear para poder pasarle un parametro y utilizarlo luego en la partial _CrearRuta
        // si es un GET no puedo pasarle ese parametro
        [HttpPost]
        public ActionResult _PasoParaCrearRuta(int idPerfil, int idUsuario)
        {
            ViewBag.IdUsuario = idUsuario;
            ViewBag.IdPerfil = idPerfil;
            return PartialView("_CrearRuta");
        }

        [HttpPost]
        public JsonResult _CrearRuta(string nombreRuta, int idPerfil, int idUsuario)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            try
            {
                if (nombreRuta == "")
                {
                    throw new Exception("Nombre de la ruta sin informar");
                }
                RutasUsuario ruta = new RutasUsuario
                {
                    Nombre = nombreRuta,
                    Orden = repoRutas.DameOrdenMaximo(idPerfil, idUsuario) + 1,
                    idUsuario = idUsuario,
                    idPerfil = idPerfil
                };

                // Insertar ruta


                if (repoRutas.AñadirNuevaRuta(ruta))
                {
                    respuesta.ok = true;
                    respuesta.mensaje = "Ruta creada correctamente";
                }
                else
                {
                    throw new Exception("Error al añadir nueva ruta");
                }
            }
            catch (Exception ex)
            {
                respuesta.ok = false;
                respuesta.mensaje = ex.Message;
            }

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult _PasoParaCrearHerramienta(int idUsuario, int idPerfil)
        {

            ViewBag.IdUsuario = idUsuario;
            ViewBag.IdPerfil = idPerfil;

            return PartialView("_CrearHerramienta");
        }

        [HttpPost]
        public JsonResult _CrearHerramienta(string herramientaNueva, int idUsuario, int idPerfil)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();
            try
            {
                if (repoHerramienta.InsertarZapatilla(herramientaNueva))
                {
                    Herramienta insertada = new Herramienta();
                    insertada = repoHerramienta.DameZapatillaPorNombre(herramientaNueva);

                    if (repoUsuario.InsertaUsuarioHerramientaPerfil(insertada.Id, idUsuario, idPerfil))
                    {
                        respuesta.ok = true;
                        respuesta.mensaje = "Herramienta creada correctamente";
                    }
                    else
                    {
                        throw new Exception("Hubo algún error insertando Usuario Herramienta Perfil");
                    }
                }
                else
                {
                    throw new Exception("Hubo algún error insertando Herramienta Nueva");
                }

            }
            catch (Exception ex)
            {
                respuesta.ok = false;
                respuesta.mensaje = ex.Message;
            }

            return Json(respuesta);
        }

       

        public ActionResult _PasoParaCrearRegistroNuevo(int idUsuario, int idPerfil)
        {
            return View();
        }


        [HttpPost]
        public ActionResult procesoParaEliminarOcultar(int? id, string tabla, int? idUsuario)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            if (id != null && idUsuario != null)
            {
                ViewBag.Identificador = id;
                ViewBag.Tabla = tabla;
                // PASAMOS TMB EL USUARIO
                ViewBag.IdUsuario = idUsuario;

                respuesta.ok = true;
                respuesta.mensaje = "Todo correcto para eliminar";

                if (tabla != "")
                {
                    switch (tabla)
                    {
                        case "RegistroRutas":
                            return PartialView("_ConfirmarEliminar");

                        case "PerfilUsuario":
                            return PartialView("_ConfirmarOcultarPerfil");

                    }
                }

                return PartialView("_ConfirmarEliminar");
            }
            else
            {

                respuesta.ok = false;
                respuesta.mensaje = "No se ha encontrado el identificador para eliminar";
            }

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult EliminarOcultarRegistro(int? id, string tabla, int idUsuario)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            try
            {
                if (id != null)
                {
                    switch (tabla)
                    {
                        case "RegistroRutas":
                            if (repoRegistro.eliminarRegistroRutas(id) == false)
                            {
                                throw new Exception("Error al eliminar registro de la tabla: " + tabla);
                            }

                            respuesta.ok = true;
                            respuesta.mensaje = "Registro eliminado correctamente";
                            break;

                        //case "PerfilUsuario":
                        //    if (repoPerfil.ocultarMostrarPerfilUsuario(id, idUsuario, false) == false)
                        //    {
                        //        throw new Exception("Error al ocultar perfil del usuario: " + idUsuario);
                        //    }
                        //    respuesta.ok = true;
                        //    respuesta.mensaje = "Registro ocultado correctamente";
                        //    //return RedirectToAction("Perfiles_Usuario", new Usuario { Id = idUsuario });
                        //    break;
                    }
                }
                else
                {
                    throw new Exception("No existe el id " + id);
                }
            }
            catch (Exception ex)
            {
                respuesta.ok = false;
                respuesta.mensaje = ex.Message;
            }

            return Json(respuesta);
        }


    }

}