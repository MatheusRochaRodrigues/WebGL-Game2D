Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Craft",
        "message0": "🛠️ Craftar %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Mira para o alvo determinado",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['Craft'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'TryCraft', // Nome do método no Unity 
        message:  target
    }; 

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};


//CRAFTS 
// Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "Cerca",
//         "message0": "Cerca [ 2x🪵 e 2x🪵 e 2x🪵]",
//         "previousStatement": null,
//         "nextStatement": null,
//         "colour": 230,
//         "tooltip": "Move the character to the specified position",
//         "helpUrl": ""
//     }
// ]);

// javascript.javascriptGenerator.forBlock['Cerca'] = function(block) {
//     const message = {
//         type: 'sendMessage',
//         target: 'Player', // Nome do objeto no Unity
//         method: 'setKeyBidding', // Nome do método no Unity
//         message: "right"
//     };
//  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//  };


 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "craft_object",
        "message0": " 🔨 Objeto %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [
                    ["🪵Cerca [2x🪵]", "Fence"],
                    ["🏹Flecha [🪵][🪨]", "Arrow"]
                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['craft_object'] = function(block) {
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
};