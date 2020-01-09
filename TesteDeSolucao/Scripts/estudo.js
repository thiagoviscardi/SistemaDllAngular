
$(document).ready(function () {
    $("#btnTeste").click(function () {
        estudos.teste();
    });
    $("#btnAjax").click(function () {
        estudos.salvar();
        aleatorios.fimLoading();
    });
    $("#btnBuscar").click(function () {
        estudos.buscar();
        
        //aleatorios.mensagemErro("Não deu erro!");// só para mostrar o teste de usar o js de outro arquivo. um arquivo de metodos
    });

    //if ($(this).value == $("#tblBody tr td").value){ // só vai chamar a funcao se clicar na classe tblBody que é a classe do tbody
    //    console.log("Clicou em " + this.value);
    //    $(this).click(function () { //aqui ao clicar nele mesmo quero editar ele mesmo
    //    estudos.editar(this)
    //});
    //}
});

var estudos = {
    teste: function () {
        master.MensagemPadrao("Funcionou!");
    },
    salvar: function () {

        var json = $("FORM").serializeArray();

        //console.log(json);

        $.ajax({
            type: "POST",
            async: true,
            data: json,
            url: "/estudo/salvar",
            success: function (resposta) {
                console.log(resposta);
                $("input").removeClass("formErro");
                $("input").removeAttr("Title");
                if (resposta.Criticas.length > 0) {

                    $(resposta.Criticas).each(function () {
                        $("#" + this.CampoId).addClass("formErro");
                        $("#" + this.CampoId).attr("Title", this.Mensagem);
                    });
                }

                estudos.buscar();
            },
            error: function (resposta) {
                alert("Erro.");
            }
        });
    },
    deletar: function (id) {
        $.ajax({
            type: "POST",
            async: true,
            data: { registroId: id }, //aqui eu coloco o nome que eu quiser : o id que recebi por paramtro. para enviar esse id ao servidor
            url: "/estudo/deletar",
            success: function (resposta) {
                estudos.buscar();
                //alert("Usuário removido com sucesso!");// aqui cai no problema que o Beis falou. como ele demora pra buscar, acaba dando o alert primeiro mesmo estando em baixo poisé assincrono
            }, erro: function (resposta) {
                alert("Erro.");
            }
        })
    },

    carregarRegistro: function (id) {
        $.ajax({
            type: "POST",
            async: true,
            data: { registroId: id }, //aqui eu coloco o nome que eu quiser : o id que recebi por parametro. para enviar esse id ao servidor
            url: "/estudo/carregarRegistro",
            success: function (resposta) {
                //$("#Nome").val(resposta.Data.Nome)//resposta.Data.Nome;
                $("#Nome").val(resposta.Data.Nome)//resposta.Data.Nome;
                $("#Id").val(resposta.Data.Id)//resposta.Data.Nome;
                $("#Idade").val(resposta.Data.Idade)//resposta.Data.Nome;
                $("#Sexo").val(resposta.Data.Sexo)//resposta.Data.Nome;

            }, erro: function (resposta) {
                alert("Erro.");
            }
        })
    },

    buscar: function () {

        aleatorios.loading();

        $.ajax({
            type: "POST",
            async: true,
            data: {}, //quando usar vazia e quando usar json?
            url: "/estudo/buscare", //ESSA URL PEGA O METODO BUSCARE DA CLASSE ESTUDO. 
            success: function (resposta) { //esse parametro de resposta tras a resposta do servidor. é como um return resposta
                //console.log(resposta);
                
                
                var html = "";
                $(resposta.Data).each(function () {// esse resposta.data é o retorno da função buscare
                    html += "<tr>";

                    html += "<td>";
                    //html += this.Id;
                    html += "<a class=\"lnkEdit\" data-id=\"" + this.Id + "\" href=\"javascript:;\">Editar</a>"; //javascript:; isso faz com que o link não faça nada!!
                    html += "</td>";

                     html += "<td>";
                    //html += this.Id;
                    html += "<a class=\"lnkDelete\" data-id=\"" + this.Id + "\" href=\"javascript:;\">Deletar</a>";// esse data-id receberá o o valor do atributo id que eu clicar
                    html += "</td>";

                    html += "<td>";
                    html += this.Id;
                    html += "</td>";

                    html += "<td>";
                    html += this.Nome;
                    html += "</td>";

                    html += "<td>";
                    html += this.Idade;
                    html += "</td>";

                    html += "<td>";
                    html += this.Sexo;
                    html += "</td>";

                    html += "</tr>";
                });

                $("#tblLista tbody").html(html);

                $(".lnkEdit").unbind("click");//retira o evento clique antes de dar o clique para não empilhar cliques
                $(".lnkEdit").click(function () {
                    var id = $(this).attr("data-id"); //pega o id do atributo que coloquei nas tag a
                    //alert(id);
                    estudos.carregarRegistro(id);

                });
                $(".lnkDelete").click(function () {
                    var idDel = $(this).attr("data-id");
                    //alert(idDel);
                    estudos.deletar(idDel);
                });

                aleatorios.fimLoading();


            },
            erro: function (resposta) {
                alert("Erro ao buscar lista!");
            }
        })
    }

};
