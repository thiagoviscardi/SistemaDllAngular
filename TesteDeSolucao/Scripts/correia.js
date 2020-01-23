APP.controller('MinhaControllerCorreia', function ($scope) {// dentro desse scope tem tudo lá do html onde tem o angular?

    $scope.BuscarCorreiaNome = function () {
        aleatorios.loading();

        aleatorios.ajax("POST", { buscapeloNome: $scope.nome }, "/correia/Controller_Salvar_Correia", function (resposta) {
            $scope.lista = resposta.Data;
            // $scope.quantidade = resposta.Data.length;
            $scope.$apply();// isso faz aplicar a troca ou mudança do scope
            aleatorios.fimLoading();
        })
    }

    $scope.SalvaCorreia = function () {

        var json = $("FORM").serializeArray();
        aleatorios.ajax("POST", json, "/correia/Controller_Salvar_Correia", function (resposta) {
            $scope.Buscar();

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


        })
    };

    $scope.DeletarCorreia = function (id) {
        aleatorios.ajax("POST", { idDeletar: id }, "/correia/deletarCorreia", function (resposta) {
            $scope.Buscar();

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
        })
    };


    $scope.Buscar = function () {
        aleatorios.ajax("POST", {}, "/correia/Controller_Buscar", function (resposta) {
            $scope.lista = resposta.Data;
            $scope.$apply();
        })
    };

    $scope.Editar = function (item) {// por que não está achando os atributos do item la do html?? o idUsuario por exemplo

        $scope.id = item.Id;
        //$scope.IdResponsavel = item.IdResponsavel;
        $scope.nome = item.Nome;
        $scope.preco = item.Preco;// o primeiro preco é lá dos inputs onde via ser colocado e o segundo é do banco



    }
});