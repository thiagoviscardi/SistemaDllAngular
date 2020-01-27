APP.controller('MinhaController', function ($scope) {//  tudo que variavel que eu criar no html vem nesse scope


    $scope.BuscarPorNome = function () {// poderia passar como parametro o nome e o buscaNome:nome

        aleatorios.loading();
        aleatorios.ajax("POST", { buscaNome: $scope.nome, buscaIdade: $scope.idade }, "/EstudoAngular/BuscarPorNome",
            function (resposta) { //esse parametro de resposta tras a resposta do servidor. é como um return resposta

                $scope.lista = resposta.Data;
                $scope.quantidade = resposta.Data.length;
                $scope.$apply();// isso faz aplicar a troca ou mudança do scope
                aleatorios.fimLoading();
        })
    };

    $scope.Buscar = function () {

        aleatorios.loading();
        aleatorios.ajax("POST", {}, "/EstudoAngular/buscare",
            function (resposta) { 
                $scope.quantidade = resposta.Data.length;
                $scope.lista = resposta.Data;
                $scope.$apply();
                aleatorios.fimLoading();
            });
    };

    $scope.Editar = function (item) {// de onde vem esse item?

        $scope.id = item.Id;
        $scope.nome = item.Nome;
        $scope.idade = item.Idade;
        $scope.sexo = item.Sexo;

        if (resposta.Criticas.length > 0) {// COLOCAF ISSO SEMPR ENO SALVAR EDITAR E DELETAR

            $(resposta.Criticas).each(function () {
                $("#" + this.CampoId).addClass("formErro");
                $("#" + this.CampoId).attr("Title", this.Mensagem);
            });
        } else {
            if (resposta.Mensagem.length > 0) {
                alert(resposta.Mensagem);
            }
        }

    };

    $scope.Deletar = function (id) {// de onde vem esse id? quem t aenviando esse parametro pra essa funcao??
        aleatorios.ajax("POST", { registroId: id }, "/EstudoAngular/deletar", function (resposta) {
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
        aleatorios.ajax("POST", json, "/EstudoAngular/salvar", function (resposta) {

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

    $scope.Quantidade = function () {
        aleatorios.ajax("POST", {}, "/EstudoAngular/buscare", function (resposta) {
            $scope.quantidade = resposta.Data.length;
            $scope.$apply();
        });
    }

});
