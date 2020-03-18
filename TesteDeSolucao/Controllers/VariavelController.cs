using Microsoft.ApplicationInsights.AspNetCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace TesteDeSolucao.Controllers
{
    public class VariavelController : Controller
    {

        #region "LIST"

        [HttpGet]
        public ActionResult Editar() // por que não pode ser Edit??? thiago beis e aonde ta esse editar?
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        

        [HttpPost]
        public JsonResult LoadList(Thiado.DataDll.Entidades.Variaveis variavel)
        {
            Helper.JsonRetorno jsonRetornoJS = new Helper.JsonRetorno();

           
             //Data.Entities.VariavelControle VariavelControle = new VariavelControle();
            List<Thiado.DataDll.Entidades.Variaveis> variavelcontroleTemp = null;

            List<FilterDefinition<Thiado.DataDll.Entidades.Variaveis>> filtros = new List<FilterDefinition<Thiado.DataDll.Entidades.Variaveis>>();

            if (variavel.Id > 0)
            {
                filtros.Add(Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Eq(e => e.Id, (variavel.Id)));
            }
            else
            {

                if (!string.IsNullOrEmpty(variavel.Descricao))
                {
                    filtros.Add(Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Regex(e => e.Descricao, BsonRegularExpression.Create(new Regex($".*{variavel.Descricao}.*", RegexOptions.IgnoreCase))));
                }
                if (!string.IsNullOrEmpty(variavel.UnidadeMedida))
                {
                    filtros.Add(Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Eq(e => e.UnidadeMedida, variavel.UnidadeMedida));
                }
                if (!string.IsNullOrEmpty(variavel.VariavelLetra))
                {
                    filtros.Add(Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Eq(e => e.VariavelLetra, variavel.VariavelLetra));
                }
                if (!string.IsNullOrEmpty(variavel.VariavelWDL))
                {
                    filtros.Add(Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Regex(e => e.VariavelWDL, BsonRegularExpression.Create(new Regex($".*{variavel.VariavelWDL}.*", RegexOptions.IgnoreCase))));
                }
            }

            if (filtros.Count > 0)
            {
                FilterDefinition<Thiado.DataDll.Entidades.Variaveis> filtroAnd = Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.And(filtros);

                variavelcontroleTemp = new TMSAMongo.Services.MongoService().Carregar(filtroAnd);
            }
            else
            {
                variavelcontroleTemp = new TMSAMongo.Services.MongoService().CarregarTudo<Thiado.DataDll.Entidades.Variaveis>();
            }


            var lista = (from n in variavelcontroleTemp
                         select new
                         {
                             n.Id,
                             n.Descricao,
                             n.UnidadeMedida,
                             n.VariavelLetra,
                             n.VariavelWDL
                         }).ToList();

            jsonRetornoJS.Data = lista;

            return Json(jsonRetornoJS);

        }
        #endregion

        #region "Edit"

        [HttpGet]
        public ActionResult Edit(int? parameter)
        {
            if (parameter == null)
            {
                parameter = 0;
            }

            ViewData["_id"] = parameter;

            return View();
        }

        public JsonResult LoadData(Thiado.DataDll.Entidades.Variaveis variavelcontrole)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();

            // buscar os itens pelo id que já temos 
            Thiado.DataDll.Entidades.Variaveis variaveisDB = new Thiado.DataDll.Entidades.Variaveis(); //Sintec.Data.Entities.VariavelControle variavelControleBD = new VariavelControle();
            Thiado.DataDll.Services.VariavelService VariavelService = new Thiado.DataDll.Services.VariavelService(); //Sintec.Data.ADO.VariavelControleADO variavelControleService = new Data.ADO.VariavelControleADO();
            TMSAMongo.Services.MongoService mongoservices = new TMSAMongo.Services.MongoService(); //Sintec.Data.ADO.MongoADO mongoservices = new Data.ADO.MongoADO();

            var itemEditVariavelControle = VariavelService.CerragarPeloId(variavelcontrole);


            JsonRetorno.Data = itemEditVariavelControle;




            return Json(JsonRetorno);
        }



        public JsonResult Save(Thiado.DataDll.Entidades.Variaveis variavelcontrole)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.VariavelService VariavelService = new Thiado.DataDll.Services.VariavelService(); //Data.ADO.VariavelControleADO VariavelControleADO = new Data.ADO.VariavelControleADO();
            TMSAMongo.Services.MongoService ServicoMongo = new TMSAMongo.Services.MongoService();

            if (variavelcontrole.Descricao.Equals("null"))
            {
                JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "txtDescricaoVariavelControle", Mensagem = "Campo Obrigatório." });
            }
            if (variavelcontrole.UnidadeMedida.Equals("null"))
            {
                JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "txtUnidadeMedidaVariavelControle", Mensagem = "Campo Obrigatório." });
            }
            if (variavelcontrole.VariavelLetra.Equals("null"))
            {
                JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "txtVariavelLetraVariavelControle", Mensagem = "Campo Obrigatório." });
            }
            if (variavelcontrole.VariavelWDL.Equals("null"))
            {
                JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "txtVariavelWDLVariavelControle", Mensagem = "Campo Obrigatório." });
            }
            if (variavelcontrole.Tipo.Equals("0"))
            {
                JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "txtTipoVariavelControle", Mensagem = "Campo Obrigatório." });
            }

            if (JsonRetorno.Criticas.Any())
            {
                JsonRetorno.Data = variavelcontrole;
                //JsonRetorno.Mensagem = Resources.Geral.CriticalVerify;
                return Json(JsonRetorno);
            }

            FilterDefinition<Thiado.DataDll.Entidades.Variaveis> filtro = Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Eq(e => e.VariavelWDL, variavelcontrole.VariavelWDL);

            if (variavelcontrole.Id > 0)
            {
                filtro &= Builders<Thiado.DataDll.Entidades.Variaveis>.Filter.Ne(e => e.Id, variavelcontrole.Id);
            }

            List<Thiado.DataDll.Entidades.Variaveis> VariavelControleTemp = new TMSAMongo.Services.MongoService().Carregar(filtro);/* Data.ADO.MongoADO().Carregar(filtro);*/

            foreach (Thiado.DataDll.Entidades.Variaveis item in VariavelControleTemp)
            {
                var arr1 = from n in variavelcontrole.VariavelWDL select n;
                var arr2 = from n in item.VariavelWDL select n;
                bool isEqual = Enumerable.SequenceEqual(arr1, arr2);
                if (isEqual)
                {
                    JsonRetorno.Criticas.Add(new Helper.Criticas() { FieldId = "X", Mensagem = "Campo Obrigatório." });
                    if (JsonRetorno.Criticas.Any())
                    {
                        JsonRetorno.Data = variavelcontrole;
                        JsonRetorno.Mensagem = "Já existe uma variável de controle com mesmo WDL cadastrado no sistema.";
                        return Json(JsonRetorno);
                    }
                }
            }

            //JsonRetorno.Message = Resources.Geral.OperationSuccess;
            JsonRetorno.Mensagem = "success";// thiago talvez retirar isso


            //var vemoq1 = VariavelControleADO.CarragarTudo(); // redundante
            var vemoq2 = ServicoMongo.Save<Thiado.DataDll.Entidades.Variaveis>(variavelcontrole);

            JsonRetorno.Data = variavelcontrole;

            return Json(JsonRetorno);
        }

        #endregion
    }
}