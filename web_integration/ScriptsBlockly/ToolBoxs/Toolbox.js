
// Função que retira a primeira coleção da fila e a adiciona ao workspace
function adicionarPrimeiraColecao(filaDeColecoes) { 

    if (filaDeColecoes.length > 0) { 
        let colecoes = filaDeColecoes.shift(); // Retira o primeiro grupo da fila
        
        colecoes.forEach(colecao => {
            console.log("Adicionando a coleção:", colecao);
            desbloquearNovosBlocos(colecao.blocos, colecao.categoria, colecao.cor);
        });
    } else { 
        console.warn("Não há mais coleções para adicionar."); 
    }
}


// Função que desbloqueia os blocos da coleção e a adiciona no workspace
function desbloquearNovosBlocos(novosBlocos, categoriaNome = "Movimento", cor = "#5C81A6", posicao = "fim") {
    console.log(`Desbloqueando blocos na categoria '${categoriaNome}' com cor ${cor}`);
    
    // Copia a estrutura da toolbox atual
    let novaToolbox = JSON.parse(JSON.stringify(currentToolbox));

    if (!novaToolbox) {
        console.error("❌ ERRO: A novaToolbox está indefinida!");
        return;
    }

    // Procura a categoria desejada
    let categoria = novaToolbox.contents.find(cat => cat.name === categoriaNome);

    if (!categoria) {
        console.warn(`Categoria '${categoriaNome}' não encontrada. Criando uma nova com cor ${cor}!`);
        categoria = {
            'kind': 'category',
            'name': categoriaNome,
            'colour': cor, // Define a cor da nova categoria
            'contents': []
        };
        novaToolbox.contents.push(categoria); // Adiciona a nova categoria
    }

    // Adiciona os novos blocos na posição especificada
    if (posicao === "inicio") {
        categoria.contents.unshift(...novosBlocos);
    } else {
        categoria.contents.push(...novosBlocos);
    }

    // Atualiza a toolbox do workspace
    workspace.updateToolbox(novaToolbox);
    currentToolbox = novaToolbox;

    console.log(`✅ Blocos adicionados à categoria '${categoriaNome}' com sucesso!`);
}


 

function updateToolbox(toolboxName) { 
    if (toolboxName === "fase1") { 
        CurrentCollection = JSON.parse(JSON.stringify(filaDeColecoes1)) ;
        currentToolbox = toolboxFase1;
        workspace.updateToolbox(toolboxFase1); 
        // adicionarPrimeiraColecao(CurrentCollection);           //Debug
    } else if (toolboxName === "fase2") {
        CurrentCollection = JSON.parse(JSON.stringify(filaDeColecoes2)) ;
        currentToolbox = toolboxFase2;
        workspace.updateToolbox(toolboxFase2); 
        // adicionarPrimeiraColecao(CurrentCollection);           //Debug

    } 
    else if (toolboxName === "fase3") {
        CurrentCollection = JSON.parse(JSON.stringify(filaDeColecoes3)) ;
        currentToolbox = toolboxFase3;
        workspace.updateToolbox(toolboxFase3); 
    } else if (toolboxName === "fase4") {
        // CurrentCollection = JSON.parse(JSON.stringify(filaDeColecoes4)) ;
        currentToolbox = toolboxFase4;
        workspace.updateToolbox(toolboxFase4); 
    }
}


// Armazenando a referência da toolboxFase1 em uma variável
let currentToolbox = toolboxFase1;
// let ultimateToolFase = "fase1";
let CurrentCollection = JSON.parse(JSON.stringify(filaDeColecoes1));





// Função que retira a primeira coleção da fila e a adiciona ao workspace
// function adicionarPrimeiraColecao() {
//     if (filaDeColecoes.length > 0) { 
//         let colecao = filaDeColecoes.shift();   // Retira o primeiro item da fila
//         console.log("Adicionando a coleção:", colecao);
        
//         // Chama a função que adiciona os blocos na categoria correta
//         desbloquearNovosBlocos(colecao.blocos, colecao.categoria, colecao.cor);
//     } else { 
//         console.warn("Não há mais coleções para adicionar."); 
//     }
// }




        // desbloquearNovosBlocos([
        //     { 'kind': 'block', 'type': 'Left' },
        //     { 'kind': 'block', 'type': 'Right' }
        // ], "inicio");

// function desbloquearNovoBloco(novoBloco, posicao = "fim") {
//     console.log("Desbloqueando novo bloco:", novoBloco);

//     // Copia a estrutura da toolbox atual
//     let novaToolbox = JSON.parse(JSON.stringify(toolboxFase1)); 
    
//     console.log("📌 Toolbox clonada:", novaToolbox);

//     if (!novaToolbox) {
//         console.error("❌ ERRO: A novaToolbox está indefinida!");
//         return;
//     }
    
//     // Encontra a categoria "Movimento"
//     let categoriaMovimento = novaToolbox.contents.find(cat => cat.name === "Movimento");

//     if (categoriaMovimento) {
//         // Adiciona o novo bloco à categoria
//         // categoriaMovimento.contents.push(novoBloco);

//         // Adiciona o novo bloco na posição especificada
//         if (posicao === "inicio") {
//             categoriaMovimento.contents.unshift(novoBloco); // Adiciona no início
//         } else {
//             categoriaMovimento.contents.push(novoBloco); // Adiciona no final
//         }
        
//         // Atualiza a toolbox do workspace
//         workspace.updateToolbox(novaToolbox);
//         console.log("Novo bloco adicionado com sucesso!");
//     } else {
//         console.warn("Categoria 'Movimento' não encontrada!");
//     }
// }




















//   function updateToolbox(toolboxName) {
//     console.log("MUDANDO A CENA PARA:", toolboxName);
//     setTimeout(() => {
//         if (toolboxName === "fase1") { 
//             workspace.updateToolbox(toolboxFase1);
//         } else if (toolboxName === "fase2") {
//             workspace.updateToolbox(toolboxFase4);
//         }
//     }, 100);
// }






// function desbloquearNovoBloco(novoBloco) {
//     // Pega a toolbox atual
//     let toolbox = workspace.getToolbox();
//     let categoriaMovimento = toolbox.getToolboxItemByName("Movimento"); // Nome da categoria
    
//     if (categoriaMovimento) {
//         categoriaMovimento.addBlock(novoBloco);
//     }
    
//     workspace.updateToolbox(toolbox);
// } 
