APP.controller('VariavelControllerList', function ($scope) {

    var moduleName = "VariavelControle";
    $scope.variavelcontrole = [];
    //$scope.Filtro = { EquipamentoId: 0, ParteId: 0 };

    $scope.LoadList = function () {
        $scope.ButtonsSettings(true);


        aleatorios.ajax("POST", $scope.Filtro, "/Variavel/LoadList", function (response) {
            console.log(response.Data);
            $scope.variavelcontrole = response.Data;
            $scope.Equipamentos = response.Data2;
            $scope.$apply();

            $scope.ButtonsSettings(false);
        }, true);

    };

    $scope.ApagarFiltro = function () {
        $scope.Filtro = null;
    };

    $scope.ButtonsSettings = function (otpDisabled) {
        $("#btnSave").button({ disabled: otpDisabled });
    };

    $scope.LoadList();
});