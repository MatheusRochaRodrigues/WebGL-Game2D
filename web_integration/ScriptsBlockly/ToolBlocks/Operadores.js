//VARIAVEL
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "variable_get",
        "message0": "variável %1",
        "args0": [
            {
                "type": "field_variable",
                "name": "VAR",
                "variable": "%{BKY_VARIABLES_DEFAULT_NAME}"
            }
        ],
        "output": "Number",  // Pode ser null, "Number", "String", etc.
        "colour": 330,
        "tooltip": "Obtém o valor de uma variável",
        "helpUrl": ""
    },
    {
        "type": "variable_set",
        "message0": "definir variável %1 para %2",
        "args0": [
            {
                "type": "field_variable",
                "name": "VAR",
                "variable": "x"
            },
            {
                "type": "input_value",
                "name": "VALUE"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 330,
        "tooltip": "Define uma variável",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['variable_get'] = function(block) {
    const variable = block.getFieldValue('VAR');  // Obtém o nome da variável selecionada.
    console.log("Variável selecionada: " + variable);  // Verifica o nome no console
    return [variable, javascript.Order.ATOMIC];  // Retorna o nome da variável
};
// Definir o comportamento de variáveis no set
javascript.javascriptGenerator.forBlock['variable_set'] = function(block) {
    const variable = block.getFieldValue('VAR');  // Obtém o nome da variável
    const value = javascript.javascriptGenerator.valueToCode(block, 'VALUE', javascript.Order.ASSIGNMENT) || '0';  // Valor da variável

    return `${variable} = ${value};\n`;  // Define a variável
};


//condiçao
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "comparison",
        "message0": "%1 %2 %3",
        "args0": [
            {
                "type": "input_value",
                "name": "A"
            },
            {
                "type": "field_dropdown",
                "name": "OP",
                "options": [
                    ["=", "=="],
                    ["≠", "!="],
                    [">", ">"],
                    ["<", "<"],
                    ["≥", ">="],
                    ["≤", "<="]
                ]
            },
            {
                "type": "input_value",
                "name": "B"
            }
        ],
        "inputsInline": true,   // Deixa os campos na mesma linha, igual ao "logic_compare"
        "output": "Boolean",    // Retorna um valor booleano
        "colour": 210,          // Cor padrão do bloco lógico (igual ao "logic_compare")
        "tooltip": "Compara dois valores e retorna verdadeiro ou falso",
        "helpUrl": ""
    }
]);


Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "comparison",
        "message0": "%1 %2 %3",
        "args0": [
            {
                "type": "input_value",
                "name": "A"
            },
            {
                "type": "field_dropdown",
                "name": "OP",
                "options": [
                    ["=", "=="],
                    ["≠", "!="],
                    [">", ">"],
                    ["<", "<"],
                    ["≥", ">="],
                    ["≤", "<="]
                ]
            },
            {
                "type": "input_value",
                "name": "B"
            }
        ],
        "inputsInline": true,   // Deixa os campos na mesma linha, igual ao "logic_compare"
        "output": "Boolean",    // Retorna um valor booleano
        "colour": 210,          // Cor padrão do bloco lógico (igual ao "logic_compare")
        "tooltip": "Compara dois valores e retorna verdadeiro ou falso",
        "helpUrl": ""
    }
]);



javascript.javascriptGenerator.forBlock['comparison'] = function(block) {
    const A = javascript.javascriptGenerator.valueToCode(block, 'A', javascript.Order.RELATIONAL) || '0';
    const B = javascript.javascriptGenerator.valueToCode(block, 'B', javascript.Order.RELATIONAL) || '0';
    const op = block.getFieldValue('OP');

    const code = `${A} ${op} ${B}`;
    return [code, javascript.Order.RELATIONAL];
};


Blockly.common.defineBlocksWithJsonArray([
    {
      "type": "boolean_operator",
      "message0": "%1 %2 %3",
      "args0": [
        {
          "type": "input_value",
          "name": "A",
          "check": "Boolean"
        },
        {
          "type": "field_dropdown",
          "name": "OP",
          "options": [
            ["e", "&&"],
            ["ou", "||"]
          ]
        },
        {
          "type": "input_value",
          "name": "B",
          "check": "Boolean"
        }
      ],
      "inputsInline": true,
      "output": "Boolean",
      "colour": 210,
      "tooltip": "Retorna verdadeiro se ambas ou uma das condições forem verdadeiras.",
      "helpUrl": ""
    }
  ]);

  javascript.javascriptGenerator.forBlock['boolean_operator'] = function(block) {
    const A = javascript.javascriptGenerator.valueToCode(block, 'A', javascript.Order.LOGICAL_AND) || 'false';
    const B = javascript.javascriptGenerator.valueToCode(block, 'B', javascript.Order.LOGICAL_AND) || 'false';
    const op = block.getFieldValue('OP');

    const code = `${A} ${op} ${B}`;
    return [code, javascript.Order.LOGICAL_AND];
};




Blockly.common.defineBlocksWithJsonArray([
    {
      "type": "variable_declaration",
      "message0": "Defina a variável %1 com valor %2",
      "args0": [
        {
          "type": "field_input",
          "name": "VAR_NAME",
          "text": "myVar"
        },
        {
          "type": "field_input",
          "name": "VAR_VALUE",
          "text": "0"
        }
      ],
      "colour": 230,
      "tooltip": "Declara uma variável",
      "helpUrl": ""
    }
  ]);
javascript.javascriptGenerator.forBlock['variable_declaration'] = function(block) {
    const varName = block.getFieldValue('VAR_NAME');
    const varValue = block.getFieldValue('VAR_VALUE'); 
    return `let ${varName} = ${varValue};\n`;
};
  




//NUmeros

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "math_number",
        "message0": "%1",
        "args0": [
            {
                "type": "field_number",
                "name": "NUM",
                "value": 0
            }
        ],
        "output": "Number",
        "colour": 230,
        "tooltip": "Um número",
        "helpUrl": ""
    }
    
]);
javascript.javascriptGenerator.forBlock['math_number'] = function(block) {
    const number = block.getFieldValue('NUM');
    return [number, javascript.Order.ATOMIC];
};


Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "logical_not",
        "message0": "Não %1",
        "args0": [
            {
                "type": "input_value",
                "name": "BOOL",
                "check": "Boolean"
            }
        ],
        "output": "Boolean",
        "colour": 210,
        "tooltip": "Retorna a negação do valor booleano.",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['logical_not'] = function(block) {
    var argument = Blockly.JavaScript.valueToCode(block, 'BOOL', Blockly.JavaScript.ORDER_LOGICAL_NOT) || 'false';
    var code = '!' + argument;
    return [code, Blockly.JavaScript.ORDER_LOGICAL_NOT];
};
