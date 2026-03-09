
//============================================================  UP
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "axe",
        "message0": "🪓 UsarMachado",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 230,
        "tooltip": "Move the character to the specified position",
        "helpUrl": "",
        "extensions": ["limit_one_instance"]
    }
]);

javascript.javascriptGenerator.forBlock['axe'] = function(block) {
    const message = {
     type: 'sendMessage',
     target: 'Player', // Nome do objeto no Unity
     method: 'setKeyBidding', // Nome do método no Unity
     message: 'Axe'
 };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };



 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Sword",
        "message0": "🗡️ UsarEspada",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 730,
        "tooltip": "Move the character to the specified position",
        "helpUrl": "",
        "extensions": ["limit_one_instance"]
    }
]);

javascript.javascriptGenerator.forBlock['Sword'] = function(block) {
    const message = {
     type: 'sendMessage',
     target: 'Player', // Nome do objeto no Unity
     method: 'setKeyBidding', // Nome do método no Unity
     message: 'Sword'
 };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };



 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Bow",
        "message0": "🏹 UsarArco",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 500,
        "tooltip": "Move the character to the specified position",
        "helpUrl": "",
        "extensions": ["limit_one_instance"]
    }
]);

javascript.javascriptGenerator.forBlock['Bow'] = function(block) {
    const message = {
     type: 'sendMessage',
     target: 'Player', // Nome do objeto no Unity
     method: 'setKeyBidding', // Nome do método no Unity
     message: 'Bow'
 };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };


 //=======================================================================================================
 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "ShootTarget",
        "message0": "🎯 Mirar para %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 170,
        "tooltip": "Mira para o alvo determinado",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['ShootTarget'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity 
        message: "AutoShoot:" + target
    };
    
    // Certifique-se de que a mensagem gerada é a esperada
    // console.log('Mensagem enviada: ', message);

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};



Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "PlaceTile",
        "message0": "✋ Coloque %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 810,
        "tooltip": "Mira para o alvo determinado",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['PlaceTile'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity 
        message: "setTile:" + target
    }; 

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};



//plow ----------------------------------------------------------

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Hoop",
        "message0": "UsarFoice",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 110,
        "tooltip": "Move the character to the specified position",
        "helpUrl": "",
        "extensions": ["limit_one_instance"]
    }
]);

javascript.javascriptGenerator.forBlock['Hoop'] = function(block) {
    const message = {
     type: 'sendMessage',
     target: 'Player', // Nome do objeto no Unity
     method: 'setKeyBidding', // Nome do método no Unity
     message: 'Hoop'
 };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Plow",
        "message0": "🌱 Plantar %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 690,
        "tooltip": "Mira para o alvo determinado",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['Plow'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity 
        message: "Plow:" + target  // parametro do metodo compactado em uma string
    }; 

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};



 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "WaterCan",
        "message0": "💧 Regar",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 590,
        "tooltip": "Move the character to the specified position",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['WaterCan'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "WaterCan"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };



 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Miner",
        "message0": "⛏️ Picareta",
        "previousStatement": null,
        "nextStatement": null,
        "colour": 590,
        "tooltip": "Use a picareta para quebrar pedras",
        "helpUrl": ""
    }
]);

javascript.javascriptGenerator.forBlock['Miner'] = function(block) {
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity
        message: "Miner"
    };
 return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
 };


 Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "Food",
        "message0": "🥫 Comer %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 810,
        "tooltip": "Mira para o alvo determinado",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['Food'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity 
        message: "Food:" + target
    }; 

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};



//  Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "ShootTarget",
//         "message0": "Mirar",
//         "previousStatement": null,
//         "nextStatement": null,
//         "colour": 230,
//         "tooltip": "Move the character to the specified position",
//         "helpUrl": "",
//         "extensions": ["limit_one_instance"]
//     }
// ]);

// javascript.javascriptGenerator.forBlock['Sword'] = function(block) {
//     const message = {
//      type: 'sendMessage',
//      target: 'Player', // Nome do objeto no Unity
//      method: 'setKeyBidding', // Nome do método no Unity
//      message: 'Sword'
//  };
//  return "iframe.contentWindow.postMessage(" + JSON.stringify(message) + ", '*');";
//  };