APP.controller('MinhaControllerCorreia', function ($scope) {

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
            alert("Salvo");
        })
    }
});