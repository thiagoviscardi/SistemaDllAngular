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

        public JsonResult deletarCorreia(FormCollection form)
        {
            var id = form["idDeletar"];
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService correiaService = new Thiado.DataDll.Services.CorreiaService();
            Thiado.DataDll.Entidades.CorreiaEntidade correia = new Thiado.DataDll.Entidades.CorreiaEntidade();
            correiaService.Deletar(Convert.ToInt32(id));
            return Json(jsonRetorno);
        }

        public JsonResult Controller_Buscar(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servicoCorreia = new Thiado.DataDll.Services.CorreiaService();
            var correia = servicoCorreia.ListarCorreias();
            JsonRetorno.Data = correia;
            return Json(JsonRetorno);
        }

        public JsonResult BuscarCorreiaPorNome(FormCollection form)
        {
            
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servicoCorreia = new Thiado.DataDll.Services.CorreiaService();
            
            var NomeCorreia = form["buscapeloNome"];
            var correia = servicoCorreia.ListarCorreiasPorNome(NomeCorreia);
            JsonRetorno.Data = correia;
            return Json(JsonRetorno);
        }



        public JsonResult Controller_Salvar_Correia(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servico = new Thiado.DataDll.Services.CorreiaService();
            Thiado.DataDll.Entidades.CorreiaEntidade correia = new Thiado.DataDll.Entidades.CorreiaEntidade();

            int number1 = 0;

            bool canConvert = int.TryParse(form["Id"], out number1);

            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////
            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////

            if (string.IsNullOrEmpty(form["IdResponsavel"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "IdResponsavel";
                critica.Mensagem = "Informe um IdResponsavel.";
                JsonRetorno.Criticas.Add(critica);
            }

            if (form["Id"].Length > 0)
            {
                var critica = new Helper.Criticas();
                if (canConvert == false)
                {
                    critica.CampoId = "Id";
                    critica.Mensagem = "Id deve ser um inteiro.";
                    JsonRetorno.Criticas.Add(critica);
                }
            }


            if (JsonRetorno.Criticas.Count > 0)// aqui sai da funcao se houverem criticas
            {
                return Json(JsonRetorno);
            }




            if (String.IsNullOrEmpty(form["id"]))// isso aqui faz editar a correia
            {
                correia.Id = 0;
            } else
            {
                correia.Id = Convert.ToInt32(form["id"]);// da problema se colocarem um id que ainda não existe
            }

           

            correia.IdResponsavel = Convert.ToInt32(form["IdResponsavel"]);
            correia.Nome = form["Nome"].ToString();// esse Nome vem de onde mesmo?acho que do name="Id" do input
            correia.Preco = Convert.ToInt32(form["Preco"]);

            if (new Thiado.DataDll.Services.UsuarioService().VerificaExistencia(correia.IdResponsavel))
            {
                servico.SalvarCorreia(correia);
                JsonRetorno.Mensagem = "Correia cadastrada com sucesso!!";
            } else
            {

                JsonRetorno.Criticas.Add(new Helper.Criticas() { CampoId = "IdResponsavel", Mensagem = "usuario inexistente" });
                //JsonRetorno.Mensagem="usuario inexistente";
            }
            
            return Json(JsonRetorno);
        }

    }
}