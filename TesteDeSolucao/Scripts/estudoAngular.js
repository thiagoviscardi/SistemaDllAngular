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

                $scope.lista = resposta.Data;
                $scope.$apply();
                aleatorios.fimLoading();
            });
    };

    $scope.Editar = function (item) {

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

    $scope.Deletar = function (id) {//DELETAR AJAX COM ANGULAR!
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

        aleatorios.ajax("POST", json, "/EstudoAngular/salvar", function (resposta) {

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
