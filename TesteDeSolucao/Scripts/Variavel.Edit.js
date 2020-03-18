APP.controller('VariavelControllerEdit', function ($scope) {

    var moduleName = "VariavelControle";
    //$scope.PartesEquipamento = [];
    //$scope.Equipamentos = [];
    $scope.resultadoTeste = 0;
    $scope.reNew = false;
    $("#btnTestar").button({ disabled: true });

    $scope.LoadData = function () { // le os itens quando clica no edit e poe em tela
        $scope.ButtonsSettings(true);
        aleatorios.ajax("POST", { "Id": $("#_id").val() }, "/LoadData", function (response) {
            //console.log(response);
            $scope.Data = response.Data;
            if ($scope.Data.Id == 0) {
                $scope.Data.Tipo = "";
            }
            console.log($scope.Data);
            //$scope.Data.Tipo = "";
            $scope.Equipamentos = response.Data2;
            $scope.$apply();
            $scope.ButtonsSettings(false);
        }, true)
    };


    $scope.Save = function () {
        $(".inputError").removeClass("inputError");

        console.log($scope.Data);
        TMSA.Ajax.GetData(moduleName + "/Save", $scope.Data, function (response) {
            console.log($scope.Data.Tipo);
            if (response.Criticas.length > 0) {
                $(response.Criticas).each(function () {
                    $("#" + this.FieldId).addClass("inputError");
                    TMSA.AddToolTip($("#" + this.FieldId), this.Message);
                });
                TMSA.ShowMessage(response.Message, "error");
            }
            else {
                TMSA.ShowMessage(response.Message, response.MessageType);
                $scope.Data = response.Data;
                $scope.reNew = true;
                $scope.$apply();
            }
            $scope.ButtonsSettings(false);
        }, true);
    };


    $scope.AdicionaInput = function () {
        $("#IdInsereInput").addClass("aumentaDiv");
        if ($scope.Data.Opcoes == null) {
            $scope.Data.Opcoes = [];
        }
        $scope.Data.Opcoes.push({ "Valor": "", "Descricao": "" });
    }
    $scope.RemoveServico = function (arr, item) {
        var index = arr.indexOf(item);
        arr.splice(index, 1);
        console.log(arr);
    };


    $scope.TestarCalculo = function () {
        $scope.ButtonsSettings(true);
        $scope.Data.VariaveisValores = $scope.Parte.VariaveisControle;
        TMSA.Ajax.GetData(moduleName + "/TestarCalculo", $scope.Data, function (response) {
            $scope.resultadoTeste = response.Data;
            $scope.$apply();
            $scope.ButtonsSettings(false);
        }, true);
    };

    $scope.$watch('reNew', function (newValue, oldValue) {
        if (newValue) {
            $scope.Data.Id = 0;
            $scope.Data.EquipamentoId = 0;
            $scope.Data.RoteiroEstimado = false;
            $scope.Data.ParteId = 0;
            $scope.Data.CodigoRoteiro = "";
            $scope.Data.Obs = "";
            $scope.Data.VariaveisValores = [];
            $($scope.Data.RoteirosCustMec).each(function () {
                this.Valor = 0;
            });
            $scope.Data.ServicosAdicionais = [];
            if ($scope.Parte != null && $scope.Parte.VariaveisControle != null) {
                for (var i = 0; i < $scope.Parte.VariaveisControle.length; i++) {
                    $scope.Parte.VariaveisControle[i].Valor = 0;
                }
            }
            $scope.reNew = false;
        }
    });

    $scope.$watch('Data.EquipamentoId', function (newValue, oldValue) {
        if (newValue != undefined && newValue != null) {
            $scope.PartesEquipamento = [];
            //$scope.Data.ParteId = 0;
            $scope.Parte = null;
            if ($scope.Equipamentos != null && $scope.Equipamentos.length > 0) {
                let equipamentosFiltrados = $scope.Equipamentos.filter((equip) => {
                    return equip.Id === newValue;
                });
                if (equipamentosFiltrados != null && equipamentosFiltrados.length > 0) {
                    $scope.PartesEquipamento = equipamentosFiltrados[0].Partes;
                }
            }
        }
    });

    $scope.$watch('Data.ParteId', function (newValue, oldValue) {
        if (newValue != undefined && newValue != null) {
            if ($scope.PartesEquipamento != null && $scope.PartesEquipamento.length > 0) {
                let parteFiltrada = $scope.PartesEquipamento.filter((parte) => {
                    return parte.Id === newValue;
                });
                $scope.Parte = parteFiltrada[0];
                if ($scope.Parte != null && $scope.Parte.VariaveisControle != null && $scope.Data.VariaveisValores != null) {
                    for (var i = 0; i < $scope.Parte.VariaveisControle.length; i++) {
                        if ($scope.Data.VariaveisValores.length > 0) {
                            $scope.Parte.VariaveisControle[i].Valor = $scope.Data.VariaveisValores[i].Valor;
                        }
                    }
                }

                $("#btnTestar").button({ disabled: false });
            }
        }
    });

    $scope.ButtonsSettings = function (otpDisabled) {
        $("#btnSave").button({ disabled: otpDisabled });
    };

    $scope.RemoveServico = function (arr, item) {
        var index = arr.indexOf(item);
        arr.splice(index, 1);
        console.log(arr);
    };

    //$("#txtServicos").autocomplete({
    //    minLength: 3,
    //    source: function (request, response) {
    //        var dados = new Array();
    //        TMSA.Ajax.GetData(moduleName + "/BuscaServicos", { "valor": this.term }, function (retorno) {
    //            for (var i = 0; i < retorno.Data.length; i++) {
    //                dados[i] = { label: retorno.Data[i].Codigo + " - " + retorno.Data[i].Nome, Id: retorno.Data[i].Id, Item: retorno.Data[i] };
    //            }
    //            response(dados);
    //        }, true);
    //    },
    //    focus: function (event, ui) {
    //        return false;
    //    },
    //    select: function (event, ui) {
    //        $(this).val("");
    //        //console.log($scope.Data.ServicosAdicionais);
    //        if ($scope.Data.ServicosAdicionais === undefined || $scope.Data.ServicosAdicionais === null) {
    //            $scope.Data.ServicosAdicionais = [];
    //        }
    //        let itens = $scope.Data.ServicosAdicionais.filter((item) => {
    //            return item.Codigo === ui.item.Item.Codigo;
    //        });
    //        if (itens.length === 0) {
    //            $scope.Data.ServicosAdicionais.push(ui.item.Item);
    //            $scope.$apply();
    //            //console.log(itens);
    //        }

    //        return false;
    //    }
    //});

    //$scope.LoadData();
});