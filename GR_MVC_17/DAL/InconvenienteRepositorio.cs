using GR_MVC_17.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class InconvenienteRepositorio : IInconvenienteRepositorio
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public List<TipoInconveniente> dameInconvenientes()
        {
            return db.TipoInconveniente.OrderBy(x => x.Orden).ToList();
        }
    }
}