APP.controller('MinhaControllerMongo', function ($scope) {//  tudo que variavel que eu criar no html vem nesse scope

    $scope.Filtro = new Object(); // preciso instanciar o Filtro se não da undefined la no editar
    
    console.log($scope.Filtro);
    $scope.BuscarPorNome = function () {// poderia passar como parametro o nome e o buscaNome:nome

        aleatorios.loading();
        console.log($scope.Filtro);
        aleatorios.ajax("POST", $scope.Filtro, "/MongoUsuario/BuscarPorNomeMongo", /*usando $scope.Filtro, ele busca no html o Filtro.nome e etc. ou seja busca um objeto, por isso funciona la na controller, pois ela ta esperando
* um objeto. se eu passasse somente o scope.nome ele enviaria uma string e não conseguiria pegar na controle por estar esperando um objeto*/
            function (resposta) { 

                $scope.lista = resposta.Data;
                $scope.quantidade = resposta.Data.length;
                $scope.$apply();// isso faz aplicar a troca ou mudança do scope
                aleatorios.fimLoading();
            })
    };

    $scope.Buscar = function () {

        aleatorios.loading();
        aleatorios.ajax("POST", {}, "/MongoUsuario/BuscarTodosMongo",
            function (resposta) {
                console.log(resposta.Data);
                $scope.lista = resposta.Data;
                $scope.$apply();
                aleatorios.fimLoading();
            });
    };


    $scope.Editar = function (item) {// este item vem co item do foreach do angular la no html
        

        $scope.Filtro.Nome = item.Nome;
        $scope.Filtro.Id = item.Id;
        $scope.Filtro.Idade = item.Idade;
        $scope.Filtro.Sexo = item.Sexo;
        console.log($scope.Filtro);
        //$scope.$apply();
        
    };

    $scope.Deletar = function (id) {// Este id ta vindo lá do html. e é usado de novo para popular o registroId
        aleatorios.ajax("POST", { registroId: id }, "/MongoUsuario/DeletarMongo", function (resposta) {
            $scope.Buscar();

            if (resposta.Criticas.length > 0) {// COLOCAF ISSO SEMPRE NO SALVAR EDITAR E DELETAR

                $(resposta.Criticas).each(function () {
                    //$("#" + this.CampoId).addClass("formErro");
                    //$("#" + this.CampoId).attr("Title", this.Mensagem);
                    alert(resposta.Mensagem);
                });
            } else {
                if (resposta.Mensagem.length > 0) {
                    alert(resposta.Mensagem);
                }
            }

        });
    };


    $scope.Salvar = function () {
        var json = $("FORM").serializeArray();
        $("#Nome").removeClass("formErro");
        $("#Idade").removeClass("formErro");
        $("#Sexo").removeClass("formErro");
        $("#Id").removeClass("formErro");
        //$("#" + this.CampoId).removeClass("formErro");// por que nao ta removendo a classe?
        aleatorios.ajax("POST", json, "/MongoUsuario/SavaMongoUsuario", function (resposta) {

            if (resposta.Criticas.length > 0) {// COLOCA ISSO SEMPRE NO SALVAR EDITAR E DELETAR

                $(resposta.Criticas).each(function () {
                    $("#" + this.CampoId).addClass("formErro");
                    $("#" + this.CampoId).attr("Title", this.Mensagem);// isso coloca a mensagem de erro no input em questão.
                    console.log(resposta);
                    console.log(resposta.Mensagem);
                    //alert(resposta.Mensagem);

                });

            } else {
                if (resposta.Mensagem.length > 0) {// posso apagar isso. nao ta servindo pra nada.
                    alert(resposta.Mensagem);
                    console.log("entrou aqui??");
                }
            }

            $scope.Buscar();
        });


    }

    $scope.Quantidade = function () {// serve pra alguma coisa essa barra antes da url?
        aleatorios.ajax("POST", {}, "/MongoUsuario/BuscarTodosMongo", function (resposta) {
            $scope.quantidade = resposta.Data.length;
            $scope.$apply();
        });
    }

});
