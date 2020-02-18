APP.controller('MinhaControllerTambor', function ($scope) {



    $scope.SalvarTambor = function () {

        aleatorios.loading();
        var json = $("FORM").serializeArray();

        $("#Nome").removeClass("formErro");
        //$scope.nome.removeClass("formErro"); // por que nao da pra fazer assim??
        //$scope.$apply();
        $("#IdResponsavel").removeClass("formErro");
        $("#Preco").removeClass("formErro");
        $("#Id").removeClass("formErro");

        aleatorios.ajax("POST", json, "Tambor/SalvarTambor", function (resposta) {
            console.log(resposta);
            $scope.BuscarTodosTambores();
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
        aleatorios.fimLoading();                
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    $scope.BuscarTodosTambores = function () {
        aleatorios.loading();

        aleatorios.ajax("POST", {}, "Tambor/BuscaTambor", function (resposta) {
            console.log(resposta);
            $scope.lista = resposta.Data;
            $scope.$apply();
        })

        aleatorios.fimLoading();                
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    $scope.Quantidade = function () {
        aleatorios.ajax("POST", {}, "Tambor/BuscaTambor", function (resposta) {
            
            $scope.quantidade = resposta.Data.length;
            console.log($scope.quantidade);
            $scope.$apply();
        })
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $scope.Deletar = function (id) {
        aleatorios.ajax("POST", { idDeletar: id }, "Tambor/DeletarTambor", function (resposta) {
            $scope.BuscarTodosTambores();               
        })
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $scope.EditarTambor = function (item) {
        $scope.Id = item.Id; // esse item.id vem de onde mesmo? acho que vem la do foreach do html em angularjs
        $scope.IdResponsavel = item.IdResponsavel;
        $scope.Nome = item.Nome;
        $scope.Preco = item.Preco;
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $scope.BuscaNomeTambor = function () {
        aleatorios.ajax("POST", { nomeTambor: $scope.Nome }, "Tambor/BuscatamborNomeController", function (resposta) {
 
            $scope.lista = resposta.Data;
            $scope.$apply();
        })
    }

    $scope.BuscaTamborUsuario = function () {
        aleatorios.ajax("POST", { IdUsuTambor: $scope.IdResponsavel }, "Tambor/BuscaTamborUsuarioController", function (resposta) {
            console.log(resposta);
            $scope.lista = resposta.Data;
            $scope.$apply();
        })
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    
})