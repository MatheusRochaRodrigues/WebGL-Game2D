 const customBlocks = 
    {
        'kind': 'block',
        'type': 'controls_repeat_ext',
        'inputs': {
          'TIMES': {
            'shadow': {
              'type': 'math_number',
              'fields': {
                'NUM': 5
              }
            }
          }
        }
      }

      Blockly.common.defineBlocksWithJsonArray([
        {
          "type": "my_repeat",
          "message0": "repetir %1 vezes \n faca %2",
          "args0": [
            {
              "type": "input_value",
              "name": "TIMES",
              "check": "Number"
            },
            {
              "type": "input_statement",
              "name": "DO"
            }
          ],
          "inputsInline": true,
          "previousStatement": null,
          "nextStatement": null,
          "colour": 250,
          "tooltip": "Repete o comando X vezes",
          "helpUrl": ""
        }
      ]);
      
      
      
      javascript.javascriptGenerator.forBlock['my_repeat'] = function(block) {
        // Obtém o número de vezes que o bloco deve ser repetido (pode ser um valor numérico ou outro bloco)
        var times = Blockly.JavaScript.valueToCode(block, 'TIMES', Blockly.JavaScript.ORDER_NONE) || '5';  // Default para 5
        
        // Gera o código para o corpo do loop
        var branch = Blockly.JavaScript.statementToCode(block, 'DO');
        
        // Gera o código JavaScript para o loop 'for'
        return `for (var i = 0; i < ${times}; i++) {\n${branch}\n}\n`;
      };
      
      

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "when_key_pressed",
        "message0": "Quando tecla %1 for pressionada",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "KEY",
                "options": [
                    ["W", "w"],
                    ["A", "a"],
                    ["S", "s"],
                    ["D", "d"],
                    ["Left Shift", "left shift"],
                    ["Space", "space"]
                ]
            }
        ],
        "message1": "fazer %1",
        "args1": [
            {
                "type": "input_statement",
                "name": "DO"
            }
        ],
        "colour": 230,
        "tooltip": "Executa ações quando uma tecla for pressionada",
        "helpUrl": "",
        "inputsInline": false
    }
]);

javascript.javascriptGenerator.forBlock['when_key_pressed'] = function(block) {
    const key = block.getFieldValue('KEY');  
    const statements = javascript.javascriptGenerator.statementToCode(block, 'DO'); // Código dos blocos dentro do evento
    window.keywordEvents.push(`if (window.pressedKeys.has("${key}")) { ${statements} }`); 
    return ``; 
}; 


Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "when_key_pressedLetter",
        "message0": "Quando tecla %1 for pressionada",
        "args0": [
            {
                "type": "field_input",
                "name": "KEY",
                "text": "a" // Valor padrão
            }
        ],
        "message1": "fazer %1",
        "args1": [
            {
                "type": "input_statement",
                "name": "DO"
            }
        ],
        "colour": 230,
        "tooltip": "Executa ações quando uma tecla for pressionada",
        "helpUrl": "",
        "inputsInline": false,
        "extensions": ["validate_single_letter"] // Aplica a validação
    }
]);
// Define a extensão para validar a entrada
Blockly.Extensions.register("validate_single_letter", function() {
    this.getField("KEY").setValidator(function(value) {
        value = value.toLowerCase().trim(); // Converte para minúscula e remove espaços

        return /^[a-z]$/.test(value) ? value : null; // Só permite letras únicas
    });
});


javascript.javascriptGenerator.forBlock['when_key_pressedLetter'] = function(block) {
    const key = block.getFieldValue('KEY');
    const statements = javascript.javascriptGenerator.statementToCode(block, 'DO');
    window.keywordEvents.push(`if (window.pressedKeys.has("${key}")) { ${statements} }`);
    return ``;
}; 

//============================================================================================================================
Blockly.defineBlocksWithJsonArray([
    {
        "type": "set_key_action",
        "message0": "Quando pressionar %1 mover %2",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "KEY",
                "options": [
                    ["W", "W"], ["A", "A"], ["S", "S"], ["D", "D"]
                ]
            },
            {
                "type": "field_dropdown",
                "name": "DIRECTION",
                "options": [
                    ["para frente", "forward"],
                    ["para trás", "backward"],
                    ["para esquerda", "left"],
                    ["para direita", "right"]
                ]
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Define uma tecla para movimentar o jogador",
        "helpUrl": ""
    }
]);

Blockly.JavaScript['set_key_action'] = function(block) {
    var key = block.getFieldValue('KEY');
    var direction = block.getFieldValue('DIRECTION');

    var code = `ws.send("${key}:${direction}");\n`;
    return code;
};



//WHILE-----------------------------------------------------------------------------------------------------
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "while_loop",
        "message0": "enquanto %1",
        "args0": [
            {
                "type": "input_value",
                "name": "CONDITION",
                "check": "Boolean"
            }
        ],
        "message1": "fazer %1",
        "args1": [
            {
                "type": "input_statement",
                "name": "DO"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 120,
        "tooltip": "Executa ações enquanto a condição for verdadeira",
        "helpUrl": ""
    }
]);


javascript.javascriptGenerator.forBlock['while_loop'] = function(block) {
    const blockId = block.id;
    const safeId = blockId.replace(/[^a-zA-Z0-9_]/g, "_"); // Sanitize para uso como variável
    const condition = javascript.javascriptGenerator.valueToCode(block, 'CONDITION', javascript.Order.NONE) || 'false';
    const statements = javascript.javascriptGenerator.statementToCode(block, 'DO');
    // const delay = 200; // Delay entre execuções (em ms)
    const delay = 400; // Tempo em ms


    return `
        if (window.whileLoopHandlers["${blockId}"]) {
            cancelAnimationFrame(window.whileLoopHandlers["${blockId}"]);
        }

        let lastExecution_${safeId} = 0;

        (function loop_${safeId}(timestamp) {
            if (!(${condition})) {
                delete window.whileLoopHandlers["${blockId}"];
                return;
            }

            if (!lastExecution_${safeId} || timestamp - lastExecution_${safeId} >= ${delay}) {
                lastExecution_${safeId} = timestamp;
                ${statements}
            }

            window.whileLoopHandlers["${blockId}"] = requestAnimationFrame(loop_${safeId});
        })();
    `;
};



Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "infinite_loop",
        "message0": "Para Sempre",
        "message1": "fazer %1",
        "args1": [
            {
                "type": "input_statement",
                "name": "DO"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 120,
        "tooltip": "Executa ações indefinidamente",
        "helpUrl": ""
    }
]);
// javascript.javascriptGenerator.forBlock['infinite_loop'] = function(block) {
//     const blockId = block.id; // Identificador único do bloco
//     const statements = javascript.javascriptGenerator.statementToCode(block, 'DO');
    

//     return `
//         if (window.whileLoopHandlers["${blockId}"]) {
//             cancelAnimationFrame(window.whileLoopHandlers["${blockId}"]);
//         }
        
//         (function loop() {
//             ${statements}
//             window.whileLoopHandlers["${blockId}"] = requestAnimationFrame(loop);
//         })();
//     `; 
// };

// javascript.javascriptGenerator.forBlock['infinite_loop'] = function(block) {
//     const blockId = block.id;
//     const statements = javascript.javascriptGenerator.statementToCode(block, 'DO');
//     const delay = 200; // delay em milissegundos entre iterações

//     return `
//         if (window.whileLoopHandlers["${blockId}"]) {
//             clearTimeout(window.whileLoopHandlers["${blockId}"]);
//         }

//         (function loop_${blockId}() {
//             ${statements}
//             window.whileLoopHandlers["${blockId}"] = setTimeout(loop_${blockId}, ${delay});
//         })();
//     `;
// };


javascript.javascriptGenerator.forBlock['infinite_loop'] = function(block) {
    const blockId = block.id;
    const safeId = blockId.replace(/[^a-zA-Z0-9_]/g, "_"); // Sanitize block ID
    const statements = javascript.javascriptGenerator.statementToCode(block, 'DO');
    // const delay = 200; // Tempo em ms
    const delay = 400; // Tempo em ms

    return `
        if (window.whileLoopHandlers["${blockId}"]) {
            cancelAnimationFrame(window.whileLoopHandlers["${blockId}"]);
        }

        let lastExecution_${safeId} = 0;

        (function loop_${safeId}(timestamp) {
            if (!lastExecution_${safeId} || timestamp - lastExecution_${safeId} >= ${delay}) {
                lastExecution_${safeId} = timestamp;
                ${statements}
            }
            window.whileLoopHandlers["${blockId}"] = requestAnimationFrame(loop_${safeId});
        })();
    `;
};





Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "when_key_pressedLetterCondition",
        "message0": "Tecla %1 está pressionada",
        "args0": [
            {
                "type": "field_input",
                "name": "KEY",
                "text": "a" // Valor padrão
            }
        ],
        "output": "Boolean", // Define o bloco como saída booleana
        "colour": 230,
        "tooltip": "Retorna verdadeiro se a tecla especificada estiver pressionada",
        "helpUrl": "",
        "extensions": ["validate_single_letter"] // Extensão opcional para validar letras
    }
]);

javascript.javascriptGenerator.forBlock['when_key_pressedLetterCondition'] = function(block) {
    const key = block.getFieldValue('KEY'); 
    return [`window.pressedKeys.has("${key}")`, Blockly.JavaScript.ORDER_ATOMIC]; 
};
 

//function and main
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "function_definition",
        "message0": "função %1 \n corpo %2",
        "args0": [
            {
                "type": "field_input",
                "name": "FUNC_NAME",
                "text": "minhaFuncao"
            },
            {
                "type": "input_statement",
                "name": "BODY"
            }
        ],
        "colour": 290,
        "tooltip": "Define uma nova função sem parâmetros",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['function_definition'] = function(block) {
    var funcName = block.getFieldValue('FUNC_NAME');
    var body = Blockly.JavaScript.statementToCode(block, 'BODY');

    return `function ${funcName}() {\n${body}\n}\n`;
};


Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "function_call",
        "message0": "📌 chamar %1",
        "args0": [
            {
                "type": "field_input",
                "name": "FUNC_NAME",
                "text": "minhaFuncao"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 290,
        "tooltip": "Chama uma função sem parâmetros",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['function_call'] = function(block) {
    var funcName = block.getFieldValue('FUNC_NAME');

    return `${funcName}();\n`;
};


//main 🔷 🌟 ⭐ ✦ ✪ ➤ 🚀
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "program_start",
        "message0": " ⭐ Programa Principal \n %1",
        "args0": [
            {
                "type": "input_statement",
                "name": "BODY"
            }
        ],
        "colour": 900,
        "tooltip": "Bloco principal do programa",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['program_start'] = function(block) {
    var body = Blockly.JavaScript.statementToCode(block, 'BODY');
    return `(function main() {\n${body}\n})();\n`;
};
