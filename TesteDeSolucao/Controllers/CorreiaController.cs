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
            
            var NomeCorreia = form["buscapeloNome"];// esse nome aqui par4ece errado
            var correia = servicoCorreia.ListarCorreiasPorNome(NomeCorreia);
            JsonRetorno.Data = correia;
            return Json(JsonRetorno);
        }

          public JsonResult BuscaPeloUsuario(FormCollection form)
        {

            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servicoCorreia = new Thiado.DataDll.Services.CorreiaService();

            var usuarioCorreia = Convert.ToInt32(form["buscaPeloUsuario"]);
            var correia = servicoCorreia.ListarCorreiaPorUsuario(usuarioCorreia);
            JsonRetorno.Data = correia;
            return Json(JsonRetorno);
        }



        public JsonResult Controller_Salvar_Correia(FormCollection form)
        {
            Helper.JsonRetorno JsonRetorno = new Helper.JsonRetorno();
            Thiado.DataDll.Services.CorreiaService servico = new Thiado.DataDll.Services.CorreiaService();
            Thiado.DataDll.Entidades.CorreiaEntidade correia = new Thiado.DataDll.Entidades.CorreiaEntidade();

            int number1 = 0;
            int number2 = 0;
            int number3= 0;

            bool canConvert = int.TryParse(form["Id"], out number1);//tryParse tem retorno boleano. isso quer dizer que se o id não for numérico ele nao vai converter e então o boleano será false
            bool canConvertIdade = int.TryParse(form["Preco"], out number2);
            bool canConvertIdResponsavel = int.TryParse(form["IdResponsavel"], out number3);


            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////
            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////

            if (string.IsNullOrEmpty(form["IdResponsavel"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "IdResponsavel";
                critica.Mensagem = "Informe um IdResponsavel.";
                JsonRetorno.Criticas.Add(critica);
            }

            if (string.IsNullOrEmpty(form["Preco"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Preco";
                critica.Mensagem = "Informe um Preco.";
                JsonRetorno.Criticas.Add(critica);
            }

            if (string.IsNullOrEmpty(form["Nome"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Nome";
                critica.Mensagem = "Informe um nome.";
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

            if (form["IdResponsavel"].Length > 0)
            {
                var critica = new Helper.Criticas();
                if (canConvertIdResponsavel == false)
                {
                    critica.CampoId = "IdResponsavel";
                    critica.Mensagem = "IdResponsavel deve ser um inteiro.";
                    JsonRetorno.Criticas.Add(critica);
                }
            }

            if (form["Preco"].Length > 0)
            {
                var critica = new Helper.Criticas();
                if (canConvertIdade == false)
                {
                    critica.CampoId = "Preco";
                    critica.Mensagem = "Preco deve ser um inteiro.";
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

            if (correia.Id == -1)
            {
                var critica = new Helper.Criticas();
                critica.CampoId = "Id";
                critica.Mensagem = "Informe um id válido";
                JsonRetorno.Mensagem = "Informe um id válido!";
                JsonRetorno.Criticas.Add(critica);

                critica.CampoId = "Id";


            }

            return Json(JsonRetorno);
        }

    }
}