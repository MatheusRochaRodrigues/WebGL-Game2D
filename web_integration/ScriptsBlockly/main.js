var iframe = document.getElementById("unityIframe");
 
// Captura teclas pressionadas
window.addEventListener("keydown", function(event) {
  window.pressedKeys.add(event.key.toLowerCase()); // Marca a tecla como pressionada
 
  // Captura eventos do teclado e envia ao iframe
  sendEventToIframe(event);
});

// Remove teclas quando soltas
window.addEventListener("keyup", function(event) {
  window.pressedKeys.delete(event.key.toLowerCase()); // Remove do set

  if (window.pressedKeys.size === 0) {
    console.log("Nenhuma tecla está pressionada.");           // eval(idle());  // eval(restartBool());
  }   
  // Captura eventos do teclado e envia ao iframe
  sendEventToIframe(event);
});  

// Função para limpar todos os eventos de teclado adicionados
function removeAllKeyListeners() {
  window.keyEventHandlersList.forEach(handler => {
      document.removeEventListener("keydown", handler);
  });
  window.keyEventHandlersList = []; // Limpa a lista após remover os eventos
  console.log("Todos os eventos de teclado foram removidos!");
  window.keywordEvents = [];

  for (let id in window.whileLoopHandlers) {
    cancelAnimationFrame(window.whileLoopHandlers[id]);
  }
  window.whileLoopHandlers = {}; // Limpa tudo
}

// Verifica se já existe o mapa de teclas pressionadas
if (!window.pressedKeys) {
  window.pressedKeys = new Set(); // Armazena teclas ativamente pressionadas
}
if (!window.keywordEvents) {
  window.keywordEvents = []; // Vetor para armazenar todas as ações
}

if (!window.whileLoopHandlers) {
  window.whileLoopHandlers = {};
}


function handlePlay() { 
  //todos os listeners gerados por codigo no blockly
  if (!window.keyEventHandlersList) {
    window.keyEventHandlersList = [];
  } 

  //encerra todos os comandos executados anteriormente
  removeAllKeyListeners(); 

  //pego o iframe do unity
  const iframe = document.getElementById('unityIframe');
  console.log("Entrou em handlePlay");
  
  let code2 = javascript.javascriptGenerator.workspaceToCode(Blockly.getMainWorkspace());
  try {
    console.log(code2);
    eval(code2); 

    // Loop para verificar teclas pressionadas e rodar os statements em um intervalo de tempo
    setInterval(function() {
        const code = window.keywordEvents.join("\n"); // Junta todos os comandos
        eval(code); // Executa todos comandos
    }, 50); // Pequeno intervalo para não sobrecarregar o processador

  } catch (error) {
    console.error("Erro ao enviar mensagem para o Unity:", error);
  }
}


function sendMessageToUnity(message) {
  const iframe = document.getElementById('unityIframe');
  iframe.contentWindow.postMessage({
    type: "sendMessage",
    target: "Circle_Outlined",  // Nome do GameObject no Unity
    method: "ReceiveMessage",   // Nome do método no Unity
    message: message            // Mensagem que você deseja passar
  }, "*");
}

document.querySelector('#bt1').addEventListener('click', handlePlay);

// const toolbox = {
//   'kind': 'categoryToolbox',                //'kind': 'flyoutToolbox',
//   'contents': [
//       {
//         'kind': 'category',
//             'name': 'Movimento',
//             'colour': '#5C81A6',
//             'contents': [
//                     // { 'kind': 'block', 'type': 'move_player' },
//                     // { 'kind': 'block', 'type': 'move_character' },
//                     //move
//                     { 'kind': 'block', 'type': 'Up', "maxInstances": 1 }, 
//                     { 'kind': 'block', 'type': 'Down'        }, 
//                     { 'kind': 'block', 'type': 'Left'        },
//                     { 'kind': 'block', 'type': 'Right'       },
//                     //------------------------------------------------------------------
//                     { 'kind': 'block', 'type': 'UpTile'      }, 
//                     { 'kind': 'block', 'type': 'DownTile'    },
//                     { 'kind': 'block', 'type': 'LeftTile'    },
//                     { 'kind': 'block', 'type': 'RightTile'   },
//                     { 'kind': 'block', 'type': 'RandomTile'  },
//                     //-------------------------------------------------------------------
//                     { 'kind': 'block', 'type': 'UpLookAt'      }, 
//                     { 'kind': 'block', 'type': 'DownLookAt'    },
//                     { 'kind': 'block', 'type': 'LeftLookAt'    },
//                     { 'kind': 'block', 'type': 'RightLookAt'   },
//                     { 'kind': 'block', 'type': 'RandomLookAt'  }
//                     // { 'kind': 'block', 'type': 'Color_Blue' },
//                     // { 'kind': 'block', 'type': 'set_key_action' }
//                     // { 'kind': 'block', 'type': 'Inventory' }
//           ]
//       },
//       {
//         'kind': 'category',
//             'name': 'Eventos',
//             'colour': 'red',
//             'contents': [
//                     // customBlocks , 
//                     { 'kind': 'block', 'type': 'my_repeat' },
//                     { 'kind': 'block', 'type': 'controls_if' },
//                     { 'kind': 'block', 'type': 'when_key_pressedLetterCondition' },
//                     { 'kind': 'block', 'type': 'while_loop' },
//                     { 'kind': 'block', 'type': 'infinite_loop' },
//                     { 'kind': 'block', 'type': 'function_definition' },
//                     { 'kind': 'block', 'type': 'function_call' }
                    
//           ]
//       },
//       {
//         'kind': 'category',
//             'name': 'Ações',
//             'colour': 'blue',
//             'contents': [
//                     { 'kind': 'block', 'type': 'Sword' },
//                     { 'kind': 'block', 'type': 'Hoop' },
//                     { 'kind': 'block', 'type': 'Plow' },
//                     { 'kind': 'block', 'type': 'WaterCan' },
//                     { 'kind': 'block', 'type': 'Bow' },
//                     { 'kind': 'block', 'type': 'axe' },
//                     { 'kind': 'block', 'type': 'ShootTarget' },
//                     { 'kind': 'block', 'type': 'Miner' }, 
//                     { 'kind': 'block', 'type': 'PlaceTile' }

//           ]
//       },
//       {
//         'kind': 'category',
//             'name': 'Craftar',
//             'colour': 'rgb(54, 171, 210)',
//             'contents': [
//                     { 'kind': 'block', 'type': 'Craft' },
//                     { 'kind': 'block', 'type': 'Cerca' }

//           ]
//       },
//       {
//         'kind': 'category',
//             'name': 'Alvos',
//             'colour': 'rgb(167, 19, 144)',
//             'contents': [
//                     { 'kind': 'block', 'type': 'is_near_player' },
//                     { 'kind': 'block', 'type': 'is_far_player'  },
//                     { 'kind': 'block', 'type': 'target_object_Mob' },
//                     { 'kind': 'block', 'type': 'target_object_Object' },
//                     { 'kind': 'block', 'type': 'target_object_Seed' },
//                     { 'kind': 'block', 'type': 'is_inventory' },
//                     { 'kind': 'block', 'type': 'count_inventory' }

//             ]
//       },
//       {
//         'kind': 'category',
//             'name': 'Operadores',
//             'colour': '#18a74a',
//             'contents': [
//                     { 'kind': 'block', 'type': 'comparison' },
//                     { 'kind': 'block', 'type': 'variable_get' },
//                     { 'kind': 'block', 'type': 'variable_set' }, 
//                     { 'kind': 'block', 'type': 'math_number' },
//                     { 'kind': 'block', 'type': 'logical_not' }
//             ]
//       }
//   ]
// };
 
Blockly.Msg["CONTROLS_IF_MSG_IF"] = "se";
Blockly.Msg["CONTROLS_IF_MSG_ELSE"] = "senão";
Blockly.Msg["CONTROLS_IF_MSG_ELSEIF"] = "senão se";
Blockly.Msg["CONTROLS_IF_TOOLTIP_1"] = "Se a condição for verdadeira, então execute algumas instruções.";
Blockly.Msg["CONTROLS_IF_TOOLTIP_2"] = "Se a condição for verdadeira, execute o primeiro bloco de instruções. Caso contrário, execute o segundo bloco de instruções.";
Blockly.Msg["CONTROLS_IF_TOOLTIP_3"] = "Se a primeira condição for verdadeira, execute o primeiro bloco de instruções. Caso contrário, se a segunda condição for verdadeira, execute o segundo bloco de instruções.";
Blockly.Msg["CONTROLS_IF_TOOLTIP_4"] = "Se a primeira condição for verdadeira, execute o primeiro bloco de instruções. Caso contrário, se a segunda condição for verdadeira, execute o segundo bloco de instruções. Se nenhuma condição for verdadeira, execute o último bloco de instruções.";

Blockly.Msg.CONTROLS_IF_MSG_IF = "se";
Blockly.Msg.CONTROLS_IF_MSG_ELSEIF = "senão se";
Blockly.Msg.CONTROLS_IF_MSG_ELSE = "senão";

Blockly.Msg.CONTROLS_IF_TOOLTIP_1 = "Se a condição for verdadeira, executa instruções.";
Blockly.Msg.CONTROLS_IF_TOOLTIP_2 = "Se a condição for verdadeira, executa o primeiro bloco de instruções. Senão, executa o segundo.";
Blockly.Msg.CONTROLS_IF_TOOLTIP_3 = "Se a primeira condição for verdadeira, executa o primeiro bloco de instruções. Senão, se a segunda condição for verdadeira, executa o segundo bloco de instruções.";
Blockly.Msg.CONTROLS_IF_TOOLTIP_4 = "Se a primeira condição for verdadeira, executa o primeiro bloco de instruções. Senão, se a segunda condição for verdadeira, executa o segundo bloco de instruções. Se nenhuma condição for verdadeira, executa o último bloco de instruções.";
  
Blockly.Msg["CONTROLS_IF_IF_TITLE_IF"] = "se";
Blockly.Msg["CONTROLS_IF_ELSEIF_TITLE_ELSEIF"] = "senão se";
Blockly.Msg["CONTROLS_IF_ELSE_TITLE_ELSE"] = "senão";
Blockly.Msg["CONTROLS_IF_IF_TOOLTIP"] = "Adicionar, remover ou reordenar seções para reconfigurar este bloco if.";
Blockly.Msg["CONTROLS_IF_ELSEIF_TOOLTIP"] = "Adicione uma condição ao bloco if.";
Blockly.Msg["CONTROLS_IF_ELSE_TOOLTIP"] = "Adicione uma condição final que captura todas as situações.";

Blockly.Msg["CONTROLS_IF_MSG_THEN"] = "faca";


  const workspace = Blockly.inject('blocklyDiv', {
        toolbox: toolboxFase1,        //toolbox: toolbox,
        scrollbars: true,           // Permite a rolagem no workspace
        zoom: {
            controls: true,         // Habilita os controles de zoom na interface (botões)
            wheel: true,            // Permite o zoom usando o scroll do mouse
        },
        move: {
            scrollbars: true,       // Permite o movimento do workspace com o mouse
        },
        horizontalLayout: false,
        toolboxPosition: "end",
    });


//a partir dq esta todo o gerenciamento associado ao bloco principal onde tudo nasce

// Função para garantir que o bloco principal (program_start) seja sempre criado
function createMainBlock(workspace) {
  // Verifica se já existe um bloco principal
  if (workspace.getAllBlocks().some(block => block.type === 'program_start')) {
      return;  // Não cria o bloco se já existir
  }

  // Cria um novo bloco "program_start"
  var block = workspace.newBlock('program_start');
  block.initSvg();
  block.render();

  // Posiciona no canto superior esquerdo
  block.moveBy(50, 50);
}

// Lógica para converter o código do workspace para JavaScript
javascript.javascriptGenerator.workspaceToCode = function(workspace) {
  var topBlocks = workspace.getTopBlocks(true);
  var mainBlock = topBlocks.find(block => block.type === 'program_start');
  
  if (!mainBlock) {
      return "// Erro: O código deve começar com o bloco 'Iniciar Programa'";
  }

  // Converte o código do bloco principal
  var code = javascript.javascriptGenerator.blockToCode(mainBlock);
  
  // Agora, verifica se há funções definidas fora do bloco principal
  var functionBlocks = topBlocks.filter(block => block.type === 'function_definition');
  
  // Converte as funções definidas fora do bloco principal
  functionBlocks.forEach(function(block) {
      code += javascript.javascriptGenerator.blockToCode(block);
  });

  return code;
};



// Criação do bloco principal se não existir
createMainBlock(workspace);

// Listener para garantir que o bloco principal seja recriado se deletado
workspace.addChangeListener(function(event) {
  if (event.type === Blockly.Events.BLOCK_DELETE) {
      const blocks = workspace.getAllBlocks();
      const hasMainBlock = blocks.some(block => block.type === 'program_start');

      // Se o bloco principal for deletado, recria ele
      if (!hasMainBlock) {
          setTimeout(() => createMainBlock(workspace), 0);
      }
  }
});


// função para limpar todo o espaço de trabalho mantendo apenas o bloco program_start
function resetWorkspace(workspace) {
  // Remove todos os blocos
  workspace.clear(); 
  // Recria o bloco principal
  createMainBlock(workspace);
} 
// Exemplo de uso: chamar resetWorkspace(workspace) sempre que quiser resetar


// Adiciona novos blocos na categoria "Ações"
// desbloquearNovosBlocos([
//   { 'kind': 'block', 'type': 'Miner' },
//   { 'kind': 'block', 'type': 'Hoop' }
// ], "Ações", "#FF5733");
 

//SEM CONSIDERAR FUNCAO

//   //bloco PRINCIPAL ONDE TUDO COMECA
//     function createMainBlock(workspace) {
//       // Verifica se já existe um bloco principal
//       if (workspace.getAllBlocks().some(block => block.type === 'program_start')) {
//           return;
//       }
  
//       // Cria um novo bloco "function_definition"
//       var block = workspace.newBlock('program_start');
//       block.initSvg();
//       block.render();
  
//       // Posiciona no canto superior esquerdo
//       block.moveBy(50, 50);
//   }


// //bloco main
// javascript.javascriptGenerator.workspaceToCode = function(workspace) {
//   var topBlocks = workspace.getTopBlocks(true);
//   var mainBlock = topBlocks.find(block => block.type === 'program_start');

//   if (!mainBlock) {
//       return "// Erro: O código deve começar com o bloco 'Iniciar Programa'";
//   }

//   return javascript.javascriptGenerator.blockToCode(mainBlock);
// };


// createMainBlock(workspace);

// //CUIDADO PODE SOBRESCREVER OUTRO E AINDA NAO VERIFIQUEI
// workspace.addChangeListener(function(event) {
//   if (event.type === Blockly.Events.BLOCK_DELETE) {
//       const blocks = workspace.getAllBlocks();
//       const hasMainBlock = blocks.some(block => block.type === 'program_start');

//       // Se o bloco principal foi deletado, recria ele
//       if (!hasMainBlock) {
//           setTimeout(() => createMainBlock(workspace), 0);
//       }
//   }
// });







  // const workspace = Blockly.inject('blocklyDiv', {
  //   toolbox: toolbox,
  //   scrollbars: false,
  //   horizontalLayout: false,
  //   toolboxPosition: "end",
  //   move: {
  //     scrollbars: true,      // Desabilita as barras de rolagem internas
  //     drag: true,             // Permite a movimentação do workspace com o mouse
  //   }
  // });