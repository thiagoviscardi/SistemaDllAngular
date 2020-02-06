APP.controller('MinhaControllerTambor', function ($scope) {



    $scope.SalvarTambor = function () {
        aleatorios.loading();
        var json = $("FORM").serializeArray();
        aleatorios.ajax("POST", json, "Tambor/SalvarTambor", function (resposta) {
            console.log(resposta);
            
        })
        aleatorios.fimLoading();                
    }

    $scope.BuscarTodosTambores = function () {
        aleatorios.loading();

        aleatorios.ajax("POST", {}, "Tambor/BuscaTambor", function (resposta) {
            console.log(resposta);

        })

        aleatorios.fimLoading();                
    }
    
})