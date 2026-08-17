

window.layoutHelper = {
    alterarBackground: function (cor) {
        console.log("alterarBackground", cor);
        document.body.style.backgroundColor = cor;
    },

    restaurarBackground: function () {
        console.log("restaurarBackground");
        document.body.style.backgroundColor = "";
    }
};