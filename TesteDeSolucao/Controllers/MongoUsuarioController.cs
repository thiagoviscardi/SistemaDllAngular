using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteDeSolucao.Controllers
{
    public class MongoUsuarioController : Controller
    {
        // GET: MongoUsuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SavaMongoUsuario(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            TMSAMongo.Services.MongoService mongoService = new TMSAMongo.Services.MongoService();
            Thiado.DataDll.Entidades.MongoUsuarioEntidade mongoDB = new Thiado.DataDll.Entidades.MongoUsuarioEntidade();
            mongoDB.Nome = form["Nome"];
            mongoDB.Idade = Convert.ToInt32(form["Idade"]);
            mongoDB.Sexo = form["Sexo"];

            mongoService.Save(mongoDB);
            JsonRetorno.Data = mongoDB;
            return Json(JsonRetorno);
        }

        public JsonResult BuscarTodosMongo(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            TMSAMongo.Services.MongoService mongoService = new TMSAMongo.Services.MongoService();
            Thiado.DataDll.Entidades.MongoUsuarioEntidade mongoDB = new Thiado.DataDll.Entidades.MongoUsuarioEntidade();
            var lista = mongoService.CarregarTudo<Thiado.DataDll.Entidades.MongoUsuarioEntidade>();
            JsonRetorno.Data = lista;
            return Json(JsonRetorno);
        }

        public JsonResult DeletarMongo (FormCollection form)
        {
            string idADeletar = form["registroId"]; // no mongo sempre deletaremos como id visto que é tudo json?
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Entidades.MongoUsuarioEntidade mongoDB = new Thiado.DataDll.Entidades.MongoUsuarioEntidade();
            TMSAMongo.Services.MongoService mongoService = new TMSAMongo.Services.MongoService();
            mongoService.RemoverItem<Thiado.DataDll.Entidades.MongoUsuarioEntidade>(idADeletar);
            return Json(JsonRetorno);
        }
    }
}