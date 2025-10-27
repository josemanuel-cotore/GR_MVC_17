using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public class DesnivelService : IDesnivelService
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public List<DesnivelFecha_DTO> dameDesnivelAño()
        {
            var resumenPorAño = db.RegistroRutas
                                .GroupBy(r => r.Fecha.Year)
                                .Select(g => new
                                {
                                    Año = g.Key,
                                    KmsAño = g.Sum(r => r.Km),
                                    DesnivelAño = g.Sum(r => r.Desnivel)
                                })
                                .ToList();

            var listaDesnivel = new List<DesnivelFecha_DTO>();

            foreach (var item in resumenPorAño)
            {
                var obj = new DesnivelFecha_DTO();
                obj.añoOmes = item.Año;
                obj.kms = item.KmsAño;
                obj.desnivel = item.DesnivelAño;

                listaDesnivel.Add(obj);
            }

            return listaDesnivel;
        }


        public List<DesnivelFecha_DTO> dameDesnivelMes(int año)
        {
            var mesesPorAño = db.RegistroRutas
                                .Where(m=> m.Fecha.Year == año)
                                .GroupBy(r => r.Fecha.Month)
                                .Select(g => new
                                {
                                    Mes = g.Key,
                                    KmMeses = g.Sum(r => r.Km),
                                    DesnivelMeses = g.Sum(r => r.Desnivel)
                                })
                                .ToList();

            var listaDesnivelPorAño = new List<DesnivelFecha_DTO>();

            var culture = new CultureInfo("es-ES");

            foreach (var item in mesesPorAño)
            {
                var obj = new DesnivelFecha_DTO();
                obj.añoOmes = item.Mes;
                obj.desnivel = item.DesnivelMeses;
                obj.kms = item.KmMeses;
                var nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Mes);
                obj.nombreMes = culture.TextInfo.ToTitleCase(nombreMes);

                listaDesnivelPorAño.Add(obj);
            }

            return listaDesnivelPorAño;
        }


        public List<RegistroRutas> dameRegistrosDesnivel(int año, int mes)
        {
            return db.RegistroRutas.Where(x => x.Fecha.Year == año && x.Fecha.Month == mes).OrderByDescending(x=>x.Fecha).ToList();
        }
    }
}
