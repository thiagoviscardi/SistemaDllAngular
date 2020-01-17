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
            //alert("Salvo");
        })
    }

    $scope.Buscar = function () {
        aleatorios.ajax("POST", {}, "/correia/Controller_Buscar", function (resposta) {
            $scope.lista = resposta.Data;
            $scope.$apply();
        })
    }
});