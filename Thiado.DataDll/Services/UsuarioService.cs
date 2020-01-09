using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Thiado.DataDll.Services
{
    public class UsuarioService
    {
        private Model.TreinamentoThiagoEntities1 _db = new Model.TreinamentoThiagoEntities1();//instanciando o banco

        ////////////////////////////////////METODO SALVAR//////////////////////////////// o que aconteceria se não retornasse o usuario?
        public Entidades.UsuarioEntidade Salvar (Entidades.UsuarioEntidade usuario)
        {
            Model.Usuarios usuarioDB = new Model.Usuarios();
            if (usuario.Id > 0)// ou seja, se já existe um usuario iremos fazer um select para editar posteriormente.
            {
                usuarioDB = (from n in _db.Usuarios where n.Id == usuario.Id select n).SingleOrDefault();// seleciona um usuario ou retorna null?
            }
            usuarioDB.Id = usuario.Id;
            usuarioDB.Nome = usuario.Nome;
           
            usuarioDB.Idade = usuario.Idade;
            
            usuarioDB.Sexo = usuario.Sexo;
            usuarioDB.Ativo = usuario.Ativo;


            if (usuario.Id == 0)//se o id que estamos adicionando é zero entao é usuario novo, entao adicionaremos um novo usuario
            {
                _db.Usuarios.Add(usuarioDB);// aqui abrimos a conexão com banco e adicionamos um novo usuarioDB que é instancia de usuar do banco
            }
            _db.SaveChanges();// salva as alterações no banco de dados

            usuario.Id = usuarioDB.Id; // o id que recebemos por parametro vai recebe ro id do usuario gravado no banco, para retornarmos
            //o id certo na funcao
            return usuario;// retornamos o usuario para podermos usar ele logo em seguida, por exemplo dar um append dele em uma lista
        }

        public bool Deletar(int id)
        {
            var usuarioDB = (from n in _db.Usuarios where n.Id == id select n).SingleOrDefault(); // fazer o select no usuario que queremos
            if(usuarioDB!=null)
            {
                _db.Usuarios.Remove(usuarioDB);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Entidades.UsuarioEntidade> ListarTodos()
        {
            List<Entidades.UsuarioEntidade> lista = new List<Entidades.UsuarioEntidade>();
            Entidades.UsuarioEntidade usuario = null;
            foreach (var item in from n in _db.Usuarios select n)
            {
                usuario = new Entidades.UsuarioEntidade();
                usuario.Id = item.Id;
                usuario.Nome = item.Nome;
                usuario.Idade = item.Idade;
                usuario.Sexo = item.Sexo;
                usuario.Ativo = item.Ativo;

                lista.Add(usuario);
            }
            return lista;
        }
        public Entidades.UsuarioEntidade CarregaUsuario(int id)
        {
            List<Entidades.UsuarioEntidade> lista = new List<Entidades.UsuarioEntidade>();
            Entidades.UsuarioEntidade usuario = null;
            var buscaUsuario = (from n in _db.Usuarios where n.Id == id select n).SingleOrDefault();
            if (buscaUsuario != null)
            {
                usuario = new Entidades.UsuarioEntidade();
                usuario.Id = buscaUsuario.Id;
                usuario.Nome = buscaUsuario.Nome;
                usuario.Idade = buscaUsuario.Idade;
                usuario.Sexo = buscaUsuario.Sexo;
                usuario.Ativo = buscaUsuario.Ativo;
            }

            
            return usuario;
        }

        public List<Entidades.UsuarioEntidade> CarregaUsuarioNome(string nome)
        {
            List<Entidades.UsuarioEntidade> lista = new List<Entidades.UsuarioEntidade>();
            Entidades.UsuarioEntidade usuario = null;
            foreach (var item in from n in _db.Usuarios where n.Nome.Contains(nome) select n)
            {
                usuario = new Entidades.UsuarioEntidade();
                usuario.Id = item.Id;
                usuario.Nome = item.Nome;
                usuario.Idade = item.Idade;
                usuario.Sexo = item.Sexo;
                usuario.Ativo = item.Ativo;

                lista.Add(usuario);
            }
            return lista;
        }
    }
}
