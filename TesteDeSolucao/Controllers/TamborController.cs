using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteDeSolucao.Controllers
{
    public class TamborController : Controller
    {
        // GET: Tambor
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SalvarTambor(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Entidades.TamborEntidade tambor = new Thiado.DataDll.Entidades.TamborEntidade();
            Thiado.DataDll.Services.TamborService tamborService = new Thiado.DataDll.Services.TamborService();
            Thiado.DataDll.Services.UsuarioService usuario = new Thiado.DataDll.Services.UsuarioService();
            
            var verificado = usuario.VerificaExistencia(tambor.IdResponsavel);

            if(form["Id"] == null || form["Id"] == "")
            {
                tambor.Id = 0;
            } else
            {
                tambor.Id = Convert.ToInt32(form["Id"]);
            }
            
            tambor.IdResponsavel = Convert.ToInt32(form["IdResponsavel"]);
            tambor.Nome = form["Nome"];
            tambor.Preco = Convert.ToInt32(form["Preco"]);

            if (verificado)
            {
                tamborService.SalvarTambor(tambor);
            } else
            {
                return Json(JsonRetorno); 
            }
            

            return Json(JsonRetorno);
        }

        public JsonResult BuscaTambor(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.TamborService tamborService = new Thiado.DataDll.Services.TamborService();

            var tambor = tamborService.Buscar();
            JsonRetorno.Data = tambor;
            return Json(JsonRetorno);
        }
    }
   
}