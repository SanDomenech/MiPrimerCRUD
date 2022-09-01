using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerCRUD.Models;
using MiPrimerCRUD.Services;

namespace MiPrimerCRUD.Controllers
{
    public class LinqController : Controller
    {
        private MiContexto ctx;
        private IGeneralService general;

        public LinqController(MiContexto ctx, IGeneralService general)
        {
            this.ctx = ctx;
            this.general = general;
        }

        public IActionResult Index(string? filtro)
        {
            var listado = ctx.Asignaturas
                .Where(asig => asig.Nombre.Contains(filtro))
                .OrderByDescending(asig => asig.Id)
                .Take(2);
            ViewBag.filtro = filtro;
            return View(listado);
        }
        public IActionResult ListaCursos(string? filtro, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var cursos = ctx.Cursos
                 .Where(cur => cur.Activo);
            if (fechaDesde != null)
            {
                cursos = cursos.Where(cur => DateTime.Compare(cur.FechaInicio.Value, fechaDesde.Value) >= 0);
            }
            if (fechaHasta != null)
            {
                cursos = cursos.Where(cur => DateTime.Compare(cur.FechaInicio.Value, fechaHasta.Value) <= 0);
            }
            if (filtro != null)
            {
                cursos = cursos.Where(cur => cur.Nombre.Contains(filtro));
            }
            cursos =cursos.OrderByDescending(cur=> cur.Id)
                 .Take(10);

            ViewBag.filtro = filtro;
            ViewBag.fechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.fechaHasta = fechaHasta?.ToString("yyyy-MM-dd");
            ViewBag.autor = general.GetAutor();

            return View(cursos);
        }
    }
}
