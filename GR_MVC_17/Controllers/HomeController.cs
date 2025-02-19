using GR_MVC_17.DAL;
using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GR_MVC_17.Controllers
{
    public class HomeController : Controller
    {
        public UsuarioRepositiorio repoUsuario = new UsuarioRepositiorio();
        public HerramientaRepositorio repoHerramienta = new HerramientaRepositorio();
        public RutasRepositorio repoRutas = new RutasRepositorio();
        public RegistroRepositorio repoRegistro = new RegistroRepositorio();
        public PerfilRepositorio repoPerfil = new PerfilRepositorio();
        public InconvenienteRepositorio repoInconveniente = new InconvenienteRepositorio();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario = repoUsuario.ExisteUsuario(usuario);
                if (usuario != null)
                {
                    if (usuario.IdRol == (int)Enums.Enum.Rol.Administrador)
                    {
                        return RedirectToAction("Acceso_Admin", "Home", usuario);
                    }
                    else
                    {
                        var prueba2 = repoRutas.DameRutasOrdenNuevo(1, 1);


                        //return Content("ok");
                        return RedirectToAction("Perfiles_Usuario", "Home", usuario);
                    }

                }
                else
                {
                    ViewBag.Error = "El usuario no existe";
                    return View("Index");

                    //return Content("El usuario no existe");
                }

            }
            return PartialView();
        }

        public ActionResult Acceso_Admin(Usuario usuario)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = repoUsuario.DameTodosUsuarios();
            ViewBag.Usuarios = listaUsuarios;

            // Le daremos a un boton al lado del usuario y mostrará su lista a la derecha
            // Crear una clase partial DTO concreto que tenga los diferentes perfiles de cada usuario

            // Por ejemplo: Usuario 1 --> Ciclismo Senderismo TrailRunning
            //              Usuario 2 --> TrailRunning Carreras
            //              Usuario 3 --> ....


            return View(listaUsuarios);
        }

        [HttpGet]
        public ActionResult Perfiles_Usuario(Usuario usuario)
        {
            ViewBag.IdUsuario = usuario.Id;

            var listaPerfiles = repoPerfil.DameListaPerfilesUsuario(usuario);

            if (listaPerfiles.Count == repoPerfil.DameMaxPerfiles())
            {
                // Hacemos que el botón de crear nuevo perfil no aparezca
                ViewBag.PerfilCompleto = true;
            }
            else
            {
                ViewBag.PerfilCompleto = false;
            }
            ViewBag.NumPerfiles = listaPerfiles.Count;

            if (listaPerfiles.Count == 0)
            {
                var listaPerfilesGeneral = repoPerfil.PerfilesPosiblesUsuario(listaPerfiles);
                ViewBag.PerfilesGeneral = listaPerfilesGeneral;
            }

            return View(listaPerfiles);
        }

        [HttpPost]
        public ActionResult Actualizar_Perfiles_Usuario(int idUsuario)
        {
            ViewBag.IdUsuario = idUsuario;

            var listaPerfiles = repoPerfil.ActualizarPerfilesOcultos(idUsuario);

            return RedirectToAction("Perfiles_Usuario", new Usuario { Id = idUsuario });
        }

        public ActionResult Herramientas_Usuario(int idPerfil, int idUsuario)
        {
            ViewBag.IdUsuario = idUsuario;
            ViewBag.IdPerfil = idPerfil;

            if (idPerfil == (int)Enums.Enum.Perfil.Duatlón || idPerfil == (int)Enums.Enum.Perfil.Carreras)
            {
                return RedirectToAction("Entrenos_Duatlon_Carreras", new { idUsuario = idUsuario, idPerfil = idPerfil });
            }

            var listaHerramientas = repoHerramienta.dameHerramientasUsuarioPerfil(idPerfil, idUsuario);

            ViewBag.NumHerramientas = listaHerramientas.Count;

            var listadoRutasUsuario = repoRutas.DameRutasUsuarioPerfil(idUsuario, idPerfil);
            ViewBag.ListadoRutas = listadoRutasUsuario;

            ViewBag.NumRutas = listadoRutasUsuario.Count;
            ViewBag.TemaPerfil = repoPerfil.dameTemaPerfil(idPerfil);

            return View(listaHerramientas);
        }

        public ActionResult Registro_Herramienta(int idHerramienta, int idUsuario, int idPerfil, int reg)
        {
            var listaRegistro = repoRegistro.dameRegistroHerramienta(idHerramienta, idPerfil);

            ViewBag.Usuario = idUsuario;
            ViewBag.Herramienta = idHerramienta;
            ViewBag.Perfil = idPerfil;
            ViewBag.NombreHerramienta = repoHerramienta.DameNombreHerramientaPorId(idHerramienta).Nombre;
            ViewBag.CalculoKm = repoRegistro.dameCalculoPorHerramienta(idUsuario, idHerramienta);

            if (reg > 0)
            {
                listaRegistro = listaRegistro.Take(reg).ToList();
            }


            return View(listaRegistro);
        }

        [HttpPost]
        public ActionResult _PasoParaCrearRegistro(int idUsuario, int idHerramienta, int idPerfil)
        {

            //var listaRutas = repoRutas.DameRutasUsuarioPerfil(idUsuario, idPerfil);
            //ViewBag.Rutas = listaRutas.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            var listaRutas = repoRutas.DameRutasOrdenNuevo(idUsuario, idPerfil);
            ViewBag.Rutas = listaRutas.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.nombre }).ToList<SelectListItem>();

            var listaInconvenientes = repoInconveniente.dameInconvenientes();
            ViewBag.Inconvenientes = listaInconvenientes.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            SeguimientoRegistro_DTO seguimiento = new SeguimientoRegistro_DTO();
            seguimiento.idUsuario_S = idUsuario;
            seguimiento.idHerramienta_S = idHerramienta;
            seguimiento.idPerfil_S = idPerfil;

            return PartialView("_CrearRegistro", seguimiento);
        }


        [HttpPost]
        public JsonResult _CrearRegistro(DateTime fecha, double km, int ruta, int inconve, int idUsuario, int idHerramienta, int idPerfil)
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

        // Hago un POST _pasoParaCrear para poder pasarle un parametro y utilizarlo luego en la partial _CrearRuta
        // si es un GET no puedo pasarle ese parametro
        [HttpPost]
        public ActionResult _PasoParaCrearRuta(int idPerfil, int idUsuario, int idTipoRuta)
        {
            ViewBag.IdUsuario = idUsuario;
            ViewBag.IdPerfil = idPerfil;
            ViewBag.IdTipoRuta = idTipoRuta;
            return PartialView("_CrearRuta");
        }

        [HttpPost]
        public JsonResult _CrearRuta(string nombreRuta, int idPerfil, int idUsuario, int idTipoRuta)
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
        public ActionResult _PasoParaCrearPerfil(int idUsuario)
        {
            var perfilesUsuario = repoPerfil.DameListaPerfilesIdUsuario(idUsuario);
            var listaPerfilesGeneral = repoPerfil.PerfilesPosiblesUsuario(perfilesUsuario);
            ViewBag.IdUsuario = idUsuario;

            if (listaPerfilesGeneral.Count > 0)
            {
                ViewBag.DesplegablePerfilesGeneral = listaPerfilesGeneral.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();
            }

            return PartialView("_CrearPerfil");

        }

        [HttpPost]
        public JsonResult _CrearPerfil(int idPerfil, int idUsuario)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();
            try
            {
                Perfil existe = repoPerfil.DamePerfilPorId(idPerfil);

                if (existe != null)
                {
                    ////Insertar en tb el nuevo perfil del usuario
                    ///

                    Usuario existeUsuario = new Usuario();
                    existeUsuario = repoUsuario.DameUsuarioPorId(idUsuario);

                    if (existeUsuario == null)
                    {
                        throw new Exception("El usuario no existe");
                    }

                    PerfilUsuario nuevoPerfil = new PerfilUsuario()
                    {
                        IdPerfil = existe.Id,
                        IdUsuario = idUsuario,
                        MostrarPerfil = true,
                        FechaInserta = DateTime.Now
                    };
                    repoPerfil.InsertarPerfil(nuevoPerfil);

                }
                else
                {
                    throw new Exception("El perfil no existe");
                }

                respuesta.ok = true;
                respuesta.mensaje = "Perfil creado";

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
                if (repoHerramienta.InsertarHerramienta(herramientaNueva))
                {
                    Herramienta insertada = new Herramienta();
                    insertada = repoHerramienta.DameHerramientaPorNombre(herramientaNueva);

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

                        case "PerfilUsuario":
                            if (repoPerfil.ocultarMostrarPerfilUsuario(id, idUsuario, false) == false)
                            {
                                throw new Exception("Error al ocultar perfil del usuario: " + idUsuario);
                            }
                            respuesta.ok = true;
                            respuesta.mensaje = "Registro ocultado correctamente";
                            //return RedirectToAction("Perfiles_Usuario", new Usuario { Id = idUsuario });
                            break;
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

        [HttpPost]
        public ActionResult _PasoParaCrearRegistroDuatlon_Carrera(int idUsuario, int idPerfil)
        {
            var listaRutasCorrer = repoRutas.DameRutasUsuarioPerfil(idUsuario, (int)Enums.Enum.Perfil.Running);
            ViewBag.RutasCorrer = listaRutasCorrer.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            var listaRutasBici = repoRutas.DameRutasUsuarioPerfil(idUsuario, (int)Enums.Enum.Perfil.Ciclismo);
            ViewBag.RutasBici = listaRutasBici.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();


            var listaInconvenientes = repoInconveniente.dameInconvenientes();
            ViewBag.Inconvenientes = listaInconvenientes.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            SeguimientoRegistro_DTO seguimiento = new SeguimientoRegistro_DTO();
            seguimiento.idUsuario_S = idUsuario;
            seguimiento.idPerfil_S = idPerfil;

            return PartialView("_CrearRegistroDuatlon_Carrera", seguimiento);
        }

        [HttpPost]
        public JsonResult _CrearRegistroDuatlon_Carrera(DateTime fecha, double kmCorrer, int rutaCorrer, double kmBici, int rutaBici, double kmCorrerAlt, int rutaCorrerAlt, string tiempoCorrer, string tiempoBici, string tiempoCorrerAlt, int inconve, bool esDuatlon, string observa, int idUsuario, int idPerfil)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            try
            {

                RegistroRutas registro = new RegistroRutas
                {
                    Fecha = fecha,
                    Km = kmCorrer,
                    IdUsuario = idUsuario,
                    IdPerfil = idPerfil,
                    IdRuta = rutaCorrer,
                    IdHerramienta = null,
                    IdInconveniente = inconve,
                    IdRutaBici = rutaBici,
                    Km_Bici = kmBici,
                    TiempoRutaCorrer = tiempoCorrer,
                    TiempoRutaBici = tiempoBici,
                    Km_Alternativa = kmCorrerAlt,
                    IdRuta_Alternativa = rutaCorrerAlt,
                    TiempoRutaCorrer_Alternativa = tiempoCorrerAlt,
                    Observaciones = observa,
                    EsDuatlon = esDuatlon
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

        public ActionResult Entrenos_Duatlon_Carreras(int idUsuario, int idPerfil)
        {
            ViewBag.IdPerfil = idPerfil;
            ViewBag.IdUsuario = idUsuario;
            ViewBag.TemaPerfil = repoPerfil.dameTemaPerfil(idPerfil);
            var LRegistroCarreras = repoRegistro.dameRegistroPorPerfil(idPerfil);

            return View(LRegistroCarreras);
        }


        [HttpPost]
        public ActionResult _PasoParaModificarRegistroDuatlon_Carrera(int idUsuario, int idPerfil, int idRegistro)
        {
            var listaRutasCorrer = repoRutas.DameRutasUsuarioPerfil(idUsuario, (int)Enums.Enum.Perfil.Running);
            ViewBag.RutasCorrer = listaRutasCorrer.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            var listaRutasBici = repoRutas.DameRutasUsuarioPerfil(idUsuario, (int)Enums.Enum.Perfil.Ciclismo);
            ViewBag.RutasBici = listaRutasBici.Select(p => new SelectListItem() { Value = p.id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();


            var listaInconvenientes = repoInconveniente.dameInconvenientes();
            ViewBag.Inconvenientes = listaInconvenientes.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nombre }).ToList<SelectListItem>();

            SeguimientoRegistro_DTO seguimiento = new SeguimientoRegistro_DTO();
            seguimiento.idUsuario_S = idUsuario;
            seguimiento.idPerfil_S = idPerfil;

            RegistroRutas registro = new RegistroRutas();
            registro = repoRegistro.dameRegistroPorId(idRegistro);

            return PartialView("_ModificarRegistroDuatlon_Carrera", registro);
        }

        [HttpPost]
        public JsonResult _ModificarRegistroDuatlon_Carrera(DateTime fecha, double kmCorrer, int rutaCorrer, double kmBici, int rutaBici, double kmCorrerAlt, int rutaCorrerAlt, string tiempoCorrer, string tiempoBici, string tiempoCorrerAlt, int inconve, bool esDuatlon, string observa, int idUsuario, int idPerfil)
        {
            Respuesta_DTO respuesta = new Respuesta_DTO();

            try
            {

                RegistroRutas registro = new RegistroRutas
                {
                    Fecha = fecha,
                    Km = kmCorrer,
                    IdUsuario = idUsuario,
                    IdPerfil = idPerfil,
                    IdRuta = rutaCorrer,
                    IdHerramienta = null,
                    IdInconveniente = inconve,
                    IdRutaBici = rutaBici,
                    Km_Bici = kmBici,
                    TiempoRutaCorrer = tiempoCorrer,
                    TiempoRutaBici = tiempoBici,
                    Km_Alternativa = kmCorrerAlt,
                    IdRuta_Alternativa = rutaCorrerAlt,
                    TiempoRutaCorrer_Alternativa = tiempoCorrerAlt,
                    Observaciones = observa,
                    EsDuatlon = esDuatlon
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

    }

}