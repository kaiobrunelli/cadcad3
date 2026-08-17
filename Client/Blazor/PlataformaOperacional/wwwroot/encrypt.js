window.criptografiaInterop = {
    criptografarComChavePublica: async function (texto, chavePublicaPem) {
        const encoder = new TextEncoder();
        const dados = encoder.encode(texto);

        // Converte a chave PEM para ArrayBuffer
        const chaveImportada = await window.crypto.subtle.importKey(
            "spki",
            pemParaArrayBuffer(chavePublicaPem),
            {
                name: "RSA-OAEP",
                hash: "SHA-256"
            },
            false,
            ["encrypt"]
        );

        const dadosCriptografados = await window.crypto.subtle.encrypt(
            { name: "RSA-OAEP" },
            chaveImportada,
            dados
        );

        return btoa(String.fromCharCode(...new Uint8Array(dadosCriptografados)));
    }
};

// Função auxiliar para converter PEM em ArrayBuffer

function pemParaArrayBuffer(pem) {
    // Remove cabeçalhos, rodapés e quebras de linha
    const base64 = pem
        .replace(/-----BEGIN PUBLIC KEY-----/, "")
        .replace(/-----END PUBLIC KEY-----/, "")
        .replace(/\\r/g, "")
        .replace(/\\n/g, "")
        .replace(/\\s+/g, "");

    try {
        const bin = atob(base64);
        const buffer = new ArrayBuffer(bin.length);
        const view = new Uint8Array(buffer);
        for (let i = 0; i < bin.length; i++) {
            view[i] = bin.charCodeAt(i);
        }
        return buffer;
    } catch (e) {
        console.error("Erro ao decodificar base64 da chave pública:", e);
        throw e;
    }
}