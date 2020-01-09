var aleatorios = {
    mensagemErro: function (erro) {
        alert(erro);
    },
    ajax: function (type, data, url, success) {
        $.ajax({
            type: type,
            async: true,
            data: data, 
            url: url,  
            success: success,
            erro: function (resposta) {
                alert("Erro!");
            }
        })
    },
    loading: function () {
        document.getElementById("loading").style.display = "inline-block";
        //var i = setInterval(function () {
             
            //clearInterval(i);
            
            
            //document.getElementById("loading").style.display = "none";
            

       // }/*, 1000*/);
    },
    fimLoading: function () {
        document.getElementById("loading").style.display = "none";
    },

    calculaSomaIdade: function () {
    //tentar usar reduce
    }
};