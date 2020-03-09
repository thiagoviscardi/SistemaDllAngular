using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public JsonResult BuscarPorNomeMongo(Thiado.DataDll.Entidades.MongoUsuarioEntidade buscaNome)
        {
            //Thiado.DataDll.Entidades.MongoUsuarioEntidade MongoUsuarioEntidade = new Thiado.DataDll.Entidades.MongoUsuarioEntidade();
            List<Thiado.DataDll.Entidades.MongoUsuarioEntidade> MongoUsuarioTemp = null;

            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno(); // entender melhor este FilterDefinition abaixo!!
           
            List<FilterDefinition<Thiado.DataDll.Entidades.MongoUsuarioEntidade>> ListaFiltros = new List<FilterDefinition<Thiado.DataDll.Entidades.MongoUsuarioEntidade>>();
         
            if (buscaNome.Id > 0)// ENTENDER MELHOR ESTE BUILDERS ABAIXO! 
            {
                ListaFiltros.Add(Builders<Thiado.DataDll.Entidades.MongoUsuarioEntidade>.Filter.Eq(e => e.Id, (buscaNome.Id)));
            }
            else
            {
                if (!string.IsNullOrEmpty(buscaNome.Nome))
                {
                    ListaFiltros.Add(Builders<Thiado.DataDll.Entidades.MongoUsuarioEntidade>.Filter.Regex(e => e.Nome, BsonRegularExpression.Create(new Regex($".*{buscaNome.Nome}.*", RegexOptions.IgnoreCase))));
                }
            }

            if (ListaFiltros.Count > 0)
            {
                FilterDefinition<Thiado.DataDll.Entidades.MongoUsuarioEntidade> filtroAnd = Builders<Thiado.DataDll.Entidades.MongoUsuarioEntidade>.Filter.And(ListaFiltros);

                MongoUsuarioTemp = new TMSAMongo.Services.MongoService().Carregar(filtroAnd);
            }
            else
            {
                MongoUsuarioTemp = new TMSAMongo.Services.MongoService().CarregarTudo<Thiado.DataDll.Entidades.MongoUsuarioEntidade>();
            }

            var lista = (from n in MongoUsuarioTemp select new { n.Id, n.Nome, n.Idade, n.Sexo}).ToList();

            JsonRetorno.Data = lista;

            return Json(JsonRetorno);
        }
    }
}