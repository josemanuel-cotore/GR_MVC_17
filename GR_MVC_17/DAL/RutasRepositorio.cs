using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Configuration;
using GR_MVC_17.Servicios;

namespace GR_MVC_17.DAL
{
    public class RutasRepositorio: IRutasRepositorio
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<RutasUsuario> DameRutasUsuarioPerfil(int idUsuario, int idPerfil)
        {
            var listadoRutas = (from j in db.RutasUsuario
                                where j.Orden > 0 &&
                                j.idUsuario == idUsuario &&
                                j.idPerfil == idPerfil
                                orderby j.Orden
                                select j).ToList();
            return listadoRutas;
        }

        public List<RutaOrdenUso_DTO> DameRutasOrdenNuevo(int idUsuario, int idPerfil)
        {
            var listaRutas = new List<RutaOrdenUso_DTO>();

            var sql = "SELECT count(*) as contador, idruta as id, b.Nombre as nombre " +
                "FROM [GestionRutasDB].[dbo].[RegistroRutas] a " +
                "inner join RutasUsuario b on a.IdRuta = b.id and a.IdPerfil = b.idPerfil and a.IdUsuario = b.idUsuario " +
                "where a.IdUsuario = " + idUsuario + " and a.IdPerfil = " + idPerfil + "" +
                "group by IdRuta, b.Nombre " +
                "order by count(*) desc";

            var cn = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(cn))
            {
                var cmd = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        var nuevo = new RutaOrdenUso_DTO();
                        nuevo.contador = int.Parse(r.ItemArray[0].ToString());
                        nuevo.id = int.Parse(r.ItemArray[1].ToString());
                        nuevo.nombre = r.ItemArray[2].ToString();

                        listaRutas.Add(nuevo);
                    }
                }
            }



            return listaRutas;
        }

        public string dameNombreRuta(int? idRuta)
        {
            return db.RutasUsuario.Where(x => x.id == idRuta).Select(x => x.Nombre).FirstOrDefault();
        }

        public Boolean AñadirNuevaRuta(RutasUsuario ruta)
        {
            try
            {
                RutasUsuario existe = new RutasUsuario();

                existe = db.RutasUsuario
                    .Where(x => x.idPerfil == ruta.idPerfil &&
                    x.idUsuario == ruta.idUsuario &&
                    x.Nombre == ruta.Nombre).FirstOrDefault();

                if (existe == null)
                {
                    db.RutasUsuario.Add(ruta);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("La ruta ya existe");
                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return true;
        }

        public RutasUsuario DameRutaPorNombrePerfilUsuario(string nombreRuta, int idPerfil, int idUsuario)
        {
            return db.RutasUsuario.Where(x => x.idPerfil == idPerfil && x.idUsuario == idUsuario && x.Nombre == nombreRuta).FirstOrDefault();
        }

        public int DameOrdenMaximo(int idPerfil, int idUsuario)
        {
            RutasUsuario existe = new RutasUsuario();
            existe = db.RutasUsuario.Where(w => w.idUsuario == idUsuario).Where(q => q.idPerfil == idPerfil).FirstOrDefault();

            if (existe != null)
            {
                return db.RutasUsuario.Where(w => w.idUsuario == idUsuario).Where(q => q.idPerfil == idPerfil).Max(x => x.Orden);
            }
            else
            {
                return 0;
            }

        }

    }
}