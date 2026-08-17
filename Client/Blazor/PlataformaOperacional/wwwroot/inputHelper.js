window.inputHelper = {
    selecionarTextoAtivo: function () {
        var elemento = document.activeElement;
        if (elemento && typeof elemento.select === "function") {
            elemento.select();
        }
    },
    definirValorInput: function (elemento, texto) {
        if (elemento) {
            elemento.value = texto;
        }
    }
};
