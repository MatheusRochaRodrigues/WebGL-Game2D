Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "move_player",
        "message0": "Mover jogador para %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "DIRECTION",
                "options": [
                    ["Cima", "up"],
                    ["Baixo", "down"],
                    ["Esquerda", "left"],
                    ["Direita", "right"]
                ]
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 120,
        "tooltip": "Move o jogador na direção escolhida",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['move_player'] = function(block) {
    const direction = block.getFieldValue('DIRECTION');

    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "a:" + direction
    };

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;
};







//============================================================  UP
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Up",
        "message0": "🚶AndarParaCima",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Move the character to the specified position",
        "helpUrl": "",
        "extensions": ["limit_one_instance"]
    }
]);

javascript.javascriptGenerator.forBlock['Up'] = function(block) { 
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "up"
    };

 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.Extensions.register("limit_one_instance", function() {
    this.setOnChange(function(changeEvent) {
        if (changeEvent.type === Blockly.Events.BLOCK_CREATE) {
            const blocks = this.workspace.getBlocksByType("Up", false);
            if (blocks.length > 1) {
                this.dispose(); // Remove o bloco extra
                alert("Só é permitido um bloco 'Up'!");
            }
        }
    });
});


//============================================================================================================================
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Down",
        "message0": "🚶 AndarParaBaixo",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['Down'] = function(block) {  
 const message = {
    type: 'sendMessage',
    target: 'Player', // Nome do objeto no Unity
    method: 'setKeyBidding', // Nome do método no Unity
    message: "down"
};
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

//============================================================================================================================
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Left",
        "message0": "🚶AndarParaEsquerda",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['Left'] = function(block) {
//     let previousBlock = block.getPreviousBlock(); // Obtém o bloco anterior
//     let key = ""; // Armazena a tecla se houver um bloco válido antes
//     if(previousBlock){
//         if (previousBlock.type === "when_key_pressedLetter") {
//             key = previousBlock.getFieldValue("KEY"); // Obtém a tecla escolhida no bloco anterior
//         }
//         //if

//         // // Percorre os blocos anteriores até encontrar um "controls_if"
//         // // while (currentBlock) {
//         // if(previousBlock){          
//         //     if (previousBlock.type === "when_key_pressedLetter") {
//         //         key = previousBlock.getFieldValue("KEY"); // Obtém a tecla escolhida no bloco anterior
//         //     }
//         //     //if

//         //     if (previousBlock.type === "controls_if") {
//         //         let conditionBlock = previousBlock.getInputTargetBlock("IF0"); // Pega a condição do primeiro IF
//         //         if (conditionBlock) {
//         //             condition = javascript.javascriptGenerator.blockToCode(conditionBlock)[0]; // Gera código da condição
                    
//         //         }
//         //         // break;
//         //     }
//         //     previousBlock = previousBlock.getPreviousBlock(); // Vai para o próximo bloco anterior
        
//         // }
//     }


//     const message = {
//      type: 'sendMessage',
//      target: 'Player', // Nome do objeto no Unity
//      method: 'setKeyBidding', // Nome do método no Unity
//     //  message: 'left'
//     message: key ? `${key}:left` : 'left' // Se houver tecla, modifica a mensagem
//  };

    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "left"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

//============================================================================================================================
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Right",
        "message0": "🚶AndarParaDireita",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['Right'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "right"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

//============================================STEP TILE================================================================================
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "UpTile",
        "message0": "🚶UmPassoParaCima",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 280,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['UpTile'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "upTile"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "LeftTile",
        "message0": "🚶UmPassoParaEsquerda",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 280,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['LeftTile'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "leftTile"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "RightTile",
        "message0": "🚶UmPassoParaDireita",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 280,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['RightTile'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "rightTile"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.common.defineBlocksWithJsonArray([
     {
         "type": "DownTile",
         "message0": "🚶UmPassoParaBaixo",
         "previousStatement": null,
         "nextStatement": null,
         "colour": 280,
         "tooltip": "Move the character to the specified position",
         "helpUrl": ""
     }
 ]); 
 javascript.javascriptGenerator.forBlock['DownTile'] = function(block) {
     const message = {
         type: 'sendMessage',
         target: 'Player', // Nome do objeto no Unity
         method: 'setKeyBidding', // Nome do método no Unity
         message: "downTile"
     };
  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
  };
  Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "RandomTile",
        "message0": "🚶UmPassoParaDireçãoAleatoria",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 280,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['RandomTile'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "randomTile"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "RunDirection",
        "message0": "🚶UmPassoParaDireçãoAtual",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 200,
        "tooltip": "Se move na Atual Direção",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['RunDirection'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "RunDirection"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };



//=============================================LOOK AT=======================================================================
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "UpLookAt",
        "message0": "👁️ OlharParaCima",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 700,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['UpLookAt'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "upLookAt"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "LeftLookAt",
        "message0": "👁️ OlharParaEsquerda",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 700,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['LeftLookAt'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "leftLookAt"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "RightLookAt",
        "message0": "👁️ OlharParaDireita",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 700,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['RightLookAt'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "rightLookAt"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

 Blockly.common.defineBlocksWithJsonArray([
     {
         "type": "DownLookAt",
         "message0": "👁️ OlharParaBaixo",
         "previousStatement": null,
         "nextStatement": null,
         "colour": 700,
         "tooltip": "Move the character to the specified position",
         "helpUrl": ""
     }
 ]); 
 javascript.javascriptGenerator.forBlock['DownLookAt'] = function(block) {
     const message = {
         type: 'sendMessage',
         target: 'Player', // Nome do objeto no Unity
         method: 'setKeyBidding', // Nome do método no Unity
         message: "downLookAt"
     };
  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
  };
  Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "RandomLookAt",
        "message0": "👁️ OlharParaDireçãoAleatoria",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 700,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]); 
javascript.javascriptGenerator.forBlock['RandomLookAt'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "randomLookAt"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };




//============================================================================================================
//  Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "AmountItem",
//         "message0": "Quantidade DSS",
//         "previousStatement": null,
//         "nextStatement": null,
//         "colour": 230,
//         "tooltip": "Move the character to the specified position",
//         "helpUrl": ""
//     }
// ]); 
// javascript.javascriptGenerator.forBlock['RandomTile'] = function(block) {
//     const message = {
//         type: 'sendMessage',
//         target: 'Player', // Nome do objeto no Unity
//         method: 'setKeyBidding', // Nome do método no Unity
//         message: "randomTile"
//     };
//  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//  };
 



//============================================================================================================================


//  function idle(){
//     {
//       const message = {
//       type: 'sendMessage',
//       target: 'Player', // Nome do objeto no Unity
//       method: 'CheckAnimation', // Nome do método no Unity
//       message: ''
//   };
//   return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//   };
// }
// function restartBool(){
//     {
//       const message = {
//       type: 'sendMessage',
//       target: 'Player', // Nome do objeto no Unity
//       method: 'restartBool', // Nome do método no Unity
//       message: ''
//   };
//   return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//   };
// }


//============================DEMAIS COISAS
// Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "Inventory",
//         "message0": "Abrir/Fechar o Inventario",
//         "previousStatement": null,
//         "nextStatement": null,
//         "colour": 230,
//         "tooltip": "Move the character to the specified position",
//         "helpUrl": ""
//     }
// ]);

// javascript.javascriptGenerator.forBlock['Inventory'] = function(block) {
//     const message = {
//         type: 'sendMessage',
//         target: 'Player', // Nome do objeto no Unity
//         method: 'setKeyBidding', // Nome do método no Unity
//         message: "inventory"
//     };
//  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//  };

 