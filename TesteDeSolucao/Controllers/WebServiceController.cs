using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TesteDeSolucao.Controllers
{
    public class WebServiceController : ApiController
    {
        // GET api/<controller>
       

        // POST api/<controller>
        [HttpPost]
        [Route ("~/insereUsuario")]
        public string InsereUsuario([FromBody]Thiado.DataDll.Entidades.UsuarioEntidade usuario)
        {
            Thiado.DataDll.Services.UsuarioService UsuarioSalvar = new Thiado.DataDll.Services.UsuarioService();
            UsuarioSalvar.Salvar(usuario);
            return "Usuario inserido com Sucesso!";
        }

        // DELETE api/<controller>/5
        [HttpGet]
        [Route("~/deletaUsuario/{id}")]
        public string DeletaUsuario(int id)
        {
            Thiado.DataDll.Services.UsuarioService servicoData = new Thiado.DataDll.Services.UsuarioService();
            var retorno = servicoData.Deletar(id);
            if(retorno == true)
            {
                return "Usuario deletado com sucesso!";
            }
            else
            {
                return "Usuario não encontrado";
            }
        }

        [HttpGet]
        [Route("~/listaTodosUsuarios")]
        public List<Thiado.DataDll.Entidades.UsuarioEntidade> carregarUsuario()// o que acontece se eu por um parametro?
        {
            Thiado.DataDll.Services.UsuarioService ServicoData = new Thiado.DataDll.Services.UsuarioService();
            
            return ServicoData.ListarTodos();
        }

        [HttpGet]
        [Route("~/buscausuario/{id}")]
        public Thiado.DataDll.Entidades.UsuarioEntidade buscaUsuario(int id)
        {
            Thiado.DataDll.Services.UsuarioService serviceData = new Thiado.DataDll.Services.UsuarioService();
            var retorno = serviceData.CarregaUsuario(id);

            return retorno;
        }


    }
}