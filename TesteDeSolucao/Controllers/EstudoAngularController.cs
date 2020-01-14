using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteDeSolucao.Controllers
{
    public class EstudoAngularController : Controller
    {
        // GET: EstudoAngular
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Salvar(FormCollection form)//mapear e depois salvar. o que é o form collection??? 
        {//form collection acima recebe o json e já mapeia. deix apronto pra trabalharmos com ele.
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();

            Thiado.DataDll.Entidades.UsuarioEntidade usuario = new Thiado.DataDll.Entidades.UsuarioEntidade();

            int number1 = 0;

            bool canConvert = int.TryParse(form["Id"], out number1);// isso ta pegando o id que vem como json tentando transformar em inteiro
                                                                    //tryParse tem retorno boleano. isso quer dizer que se o id não for numérico ele nao vai converter e então o boleano será false

            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////
            if (form["Id"].Length > 0)
            {
                var critica = new Helper.Criticas();
                if (canConvert == false)
                {
                    critica.CampoId = "Id";
                    critica.Mensagem = "Id deve ser um inteiro.";
                    jsonRetorno.Criticas.Add(critica);
                }
            }
            if (string.IsNullOrEmpty(form["Nome"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Nome";
                critica.Mensagem = "Informe um nome.";
                jsonRetorno.Criticas.Add(critica);
            }
            if (string.IsNullOrEmpty(form["Idade"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Idade";
                critica.Mensagem = "Informe uma idade.";
                jsonRetorno.Criticas.Add(critica);
            }
            if (string.IsNullOrEmpty(form["Sexo"]))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Sexo";
                critica.Mensagem = "Informe um Sexo.";
                jsonRetorno.Criticas.Add(critica);
            }
            if (!string.IsNullOrEmpty(form["Sexo"]) && (form["Sexo"].ToUpper() != "M" && form["Sexo"].ToUpper() != "F"))
            {
                var critica = new Helper.Criticas();

                critica.CampoId = "Sexo";
                critica.Mensagem = "Informe M ou F";
                jsonRetorno.Criticas.Add(critica);
            }

            if (!string.IsNullOrEmpty(form["Ativo"]))
            {
                var critica = new Helper.Criticas();
                critica.CampoId = "Ativo";
                critica.Mensagem = "Informe um se o usuário está ativo ou inativo";
                jsonRetorno.Criticas.Add(critica);
            }

            if (jsonRetorno.Criticas.Count > 0)
            {
                return Json(jsonRetorno);
            }

            //////////////////////////////////////////////CRITICAS////////////////////////////////////////////////////
            usuario.Id = number1;
            usuario.Nome = form["Nome"].ToString();// esse nome vem de onde mesmo?acho que do name="Id" do input
            usuario.Sexo = form["Sexo"].ToString();
            usuario.Idade = Convert.ToInt32(form["Idade"]);
            usuario.Ativo = true;

            Thiado.DataDll.Services.UsuarioService service = new Thiado.DataDll.Services.UsuarioService();

            service.Salvar(usuario);

            return Json(jsonRetorno);

        }

        public JsonResult Buscare(FormCollection form)//mapear e depois salvar
        {
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();

            Thiado.DataDll.Services.UsuarioService usuario = new Thiado.DataDll.Services.UsuarioService();
            var usuarios = usuario.ListarTodos();
            jsonRetorno.Data = usuarios;

            System.Threading.Thread.Sleep(800);// aqui eu testo se as func de aparecer e desaparecer loading estao funcionando.
                                                // coloco esse sleep no metodo buscar pra quando chamar pelo ajax ele fica aqui por 3 segundos!

            return Json(jsonRetorno);
        }

        public JsonResult BuscarPorNome(FormCollection form)// pega mais de um parametro do form?? acho que sim
        {
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();

            Thiado.DataDll.Services.UsuarioService usuarioService = new Thiado.DataDll.Services.UsuarioService();
            var itemNome = form["buscaNome"];//ENTENDER MELHOR!!! pega os dados do js e deixa a gente trabalhar em c#
            var itemIdade = 0;
            
            if (form["buscaIdade"] == "0" || form["buscaIdade"] == null || form["buscaIdade"] == "")
            {
                itemIdade = 0;
            }
            else
            {
                itemIdade = Convert.ToInt32(form["buscaIdade"]);
            }
           
            var usuarios = usuarioService.CarregaUsuarioNome(itemNome, itemIdade);
            jsonRetorno.Data = usuarios;

            return Json(jsonRetorno);
        }


        public JsonResult Deletar(FormCollection form)//mapear e depois salvar
        {
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();

            Thiado.DataDll.Services.UsuarioService usuario = new Thiado.DataDll.Services.UsuarioService();
            var id = form["registroId"];// ENTENDER MELHOR ISSO AQUI AINDA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var usuarios = usuario.Deletar(Convert.ToInt32(id));
            jsonRetorno.Data = usuarios;

            return Json(jsonRetorno);
        }

        public JsonResult CarregarRegistro(FormCollection form)//mapear e depois salvar//neste caso aqui to recebendo o id nesse FormCollection form
        {
            Helper.JsonRetorno jsonRetorno = new Helper.JsonRetorno();

            var id = form["registroId"];

            //Vai no banco e carrega o usuario
            Thiado.DataDll.Services.UsuarioService usuarioService = new Thiado.DataDll.Services.UsuarioService();
            var usuario = usuarioService.CarregaUsuario(Convert.ToInt32(id));// 
            //jsonRetorno.Data = //Quando tiver o usuario colocar aqui;
            jsonRetorno.Data = usuario;

            return Json(jsonRetorno);
        }
    }
}