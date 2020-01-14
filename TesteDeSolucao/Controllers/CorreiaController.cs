using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteDeSolucao.Controllers
{
    public class CorreiaController : Controller
    {
        // GET: Correia
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Controller_Salvar_Correia(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servico = new Thiado.DataDll.Services.CorreiaService();
            Thiado.DataDll.Entidades.CorreiaEntidade correia = new Thiado.DataDll.Entidades.CorreiaEntidade();

            //correia.Id = 0; // falo que o valor zero t aduplicado
            correia.IdResponsavel = Convert.ToInt32(form["IdUsuario"]);// esse IdResponsavel vem de onde mesmo???????
            correia.Nome = form["Nome"].ToString();// esse Nome vem de onde mesmo?acho que do name="Id" do input
            correia.Preco = Convert.ToInt32(form["Preco"]);
            servico.SalvarCorreia(correia);
            return Json(JsonRetorno);
        }
    }
}