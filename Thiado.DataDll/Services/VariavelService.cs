using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSAMongo.Services;

namespace Thiado.DataDll.Services
{
    public class VariavelService
    {
        //TMSAMongo.Services.MongoService MongoService = new TMSAMongo.Services.MongoService();

        public Entidades.Variaveis Salvar(Entidades.Variaveis variavel)
        {
            TMSAMongo.Services.MongoService MongoService = new TMSAMongo.Services.MongoService();
            try
            {
                if (variavel.Id > 0)
                {


                    //var variavelcontroleTemp = MongoService.CarregarPorId<Entidades.Variaveis>(variavel.Id);
                    var variavelcontroleTemp = new MongoService().CarregarPorId<Entidades.Variaveis>(variavel.Id);

                    variavel.Id = variavelcontroleTemp.Id;
                    variavel.Descricao = variavelcontroleTemp.Descricao;
                    variavel.UnidadeMedida = variavelcontroleTemp.UnidadeMedida;
                    variavel.VariavelLetra = variavelcontroleTemp.VariavelLetra;
                    variavel.VariavelWDL = variavelcontroleTemp.VariavelWDL;

                    MongoService.Save(variavel);//new MongoADO().Save(variavel);


                }
                return variavel;
            }
            catch
            {
                return null;
            }
        }

        public Entidades.Variaveis CerragarPeloId(Entidades.Variaveis variavelcontrole)
        {
            //TMSAMongo.Services.MongoService MongoService = new TMSAMongo.Services.MongoService();
            try
            {
                //var VariavelControleDB = MongoService.CarregarPorId<Entidades.Variaveis>(variavelcontrole.Id); 
                var VariavelControleDB = new MongoService().CarregarPorId<Entidades.Variaveis>(variavelcontrole.Id);
                if (VariavelControleDB == null)
                {
                    return new Entidades.Variaveis();
                }

                return VariavelControleDB;
            }
            catch
            {
                return null;
            }
        }
        //public Entidades.Variaveis CerragarPeloFiltro(Entidades.Variaveis variaveiscontrole)
        //{
        //    TMSAMongo.Services.MongoService MongoService = new TMSAMongo.Services.MongoService();

        //    List<Entidades.Variaveis> lista = new List<Entidades.Variaveis>();
        //    Entidades.Variaveis variavel = null;

        //    var BuscaVariavelControle = MongoDB.Driver.Builders<Entidades.Variaveis>.Filter.Eq(e => e.Descricao, variaveiscontrole.Descricao);
        //    variavel = new Entidades.Variaveis();
        //    Entidades.Variaveis Variaveis = new Entidades.Variaveis(); //Data.Entities.VariavelControle VariavelControle = new Entidades.Variaveis();
        //     //Data.ADO.MongoADO mongoADO = new MongoADO();
           

        //    return variavel;

        //}

        public List<Entidades.Variaveis> CarragarTudo()
        {
            //TMSAMongo.Services.MongoService MongoService = new TMSAMongo.Services.MongoService();

            try
            {
                return new MongoService().CarregarTudo<Entidades.Variaveis>();
                //return new MongoADO().CarregarTudo<Entidades.Variaveis>();
            }
            catch
            {
                return null;
            }
        }


    }
}
