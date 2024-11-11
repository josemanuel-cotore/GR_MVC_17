using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IPerfilRepositorio
    {
        List<Perfil> PerfilesPosiblesUsuario(List<Perfil> listaExisten);
        List<Perfil> DameListaPerfilesUsuario(Usuario usuario);
        int DameListaPerfilesOcultos(Usuario usuario);
        List<Perfil> DameListaPerfilesIdUsuario(int idUsuario);
        void InsertarPerfil(PerfilUsuario perfilUsuario);
        PerfilUsuario ComprobarUsuarioTienePerfil(int idPerfil, int idUsuario);
        int DameMaxPerfiles();
        string dameTemaPerfil(int idPerfil);
        bool ocultarMostrarPerfilUsuario(int? idPerfil, int? idUsuario, bool accion);
        List<Perfil> ActualizarPerfilesOcultos(int idUsuario);
        List<Perfil> DameListaPerfilesParaMostrar(Usuario usuario);
    }
}
