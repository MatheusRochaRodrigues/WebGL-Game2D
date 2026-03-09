
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "is_near_player",
        "message0": "Esta perto do jogador %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "output": "Boolean",
        "colour": 210,
        "tooltip": "Retorna verdadeiro se o alvo estiver a até 5 metros do jogador",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['is_near_player'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ['false', Blockly.JavaScript.ORDER_ATOMIC]; // Se não houver alvo, retorna false
        // return ``;
    }

    return [`window.nearDistance.has("${target}")`, Blockly.JavaScript.ORDER_NONE];
    // return [`Vector3.Distance(player.transform.position, ${target}.transform.position) <= 5`, Blockly.JavaScript.ORDER_NONE];
};

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "is_far_player",
        "message0": "Esta longe do jogador %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "output": "Boolean",
        "colour": 210,
        "tooltip": "Retorna verdadeiro se o alvo estiver a até 5 metros do jogador",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['is_far_player'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ['false', Blockly.JavaScript.ORDER_ATOMIC]; // Se não houver alvo, retorna false
    }

    return [`window.farDistance.has("${target}")`, Blockly.JavaScript.ORDER_NONE];};

//--------------------------------------------------------------------Alvos
Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "target_object_Mob",
        "message0": "💀 mob %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [
                    ["Slime", "Slime"],
                    ["Skeleton", "Skeleton"]
                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['target_object_Mob'] = function(block) {
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
};

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "target_object_Animal",
        "message0": "🐾 Animal %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [
                    ["🐷 Porco", "Pig"],
                    ["🐔 Galinha", "Chicken"]
                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['target_object_Animal'] = function(block) {
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
};

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "target_object_Object",
        "message0": " 🔨 Objeto %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [ 
                    ["🪵 Arvore", "Tree"],
                    ["🪨 Pedra", "Stone"],
                    ["🪵 Cerca", "Fence"],
                    ["🏹 Flecha", "Arrow"]
                    // ["Pedra", "stone"],
                    // ["Bau", "Chest"]
                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['target_object_Object'] = function(block) { 
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
    // return [`"${objectName}"`, Blockly.JavaScript.ORDER_ATOMIC];
};

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "target_object_Seed",
        "message0": "🌿 Semente de %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [
                    ["Trigo", "Wheat_Seed"], 
                    ["Tomate", "Tomato_Seed"] ,
                    ["Cenoura", "Carrot_Seed"]
                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['target_object_Seed'] = function(block) {
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
};


Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "target_object_Food",
        "message0": " 🍒 Comida %1",
        "args0": [
            {
                "type": "field_dropdown",
                "name": "OBJECT",
                "options": [ 
                    ["Amora", "Blackberry"],
                    ["Blueberry", "Blueberry"] ,
                    ["Cenoura", "Carrot"],
                    ["Pipoca", "Corn"],
                    ["Tomate", "Tomato"]

                ]
            }
        ],
        "output": "String",
        "colour": 120,
        "tooltip": "Especifica um alvo (inimigo ou objeto)",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['target_object_Food'] = function(block) { 
    // Verifica se o bloco está conectado a algo
    if (!block.outputConnection || !block.outputConnection.targetConnection) {
        return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
    }

    var objectName = block.getFieldValue('OBJECT');
    return [`${objectName}`, Blockly.JavaScript.ORDER_ATOMIC];
    // return [`"${objectName}"`, Blockly.JavaScript.ORDER_ATOMIC];
};



Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "is_inventory",
        "message0": "Tem no Inventario %1",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "output": "Boolean",
        "colour": 210,
        "tooltip": "Retorna verdadeiro se o alvo estiver a até 5 metros do jogador",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['is_inventory'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ['false', Blockly.JavaScript.ORDER_ATOMIC]; // Se não houver alvo, retorna false
        // return ``;
    }

    return [`window.inventory.has("${target}")`, Blockly.JavaScript.ORDER_NONE];
    // return [`Vector3.Distance(player.transform.position, ${target}.transform.position) <= 5`, Blockly.JavaScript.ORDER_NONE];
};

Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "count_inventory",
        "message0": "Quantos %1 no Inventario?",
        "args0": [
            {
                "type": "input_value",
                "name": "TARGET",
                "check": "String"
            }
        ],
        "output": "Number",
        "colour": 210,
        "tooltip": "Retorna verdadeiro se o alvo estiver a até 5 metros do jogador",
        "helpUrl": ""
    }
]);
javascript.javascriptGenerator.forBlock['count_inventory'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if (target === '""') {
        return ['0', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna 0 se não houver alvo
    }

    return [`(window.inventory.get("${target}") || 0)`, Blockly.JavaScript.ORDER_ATOMIC];
};



Blockly.common.defineBlocksWithJsonArray([
    {
        "type": "DestroyItem",
        "message0": "❌ Destroir Item no Inventario %1",
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
javascript.javascriptGenerator.forBlock['DestroyItem'] = function(block) {
    var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
    if(target === '""'){
        return ``; // Se não houver alvo, retorna false
    }
    
    const message = {
        type: 'sendMessage',
        target: 'Player', // Nome do objeto no Unity
        method: 'setKeyBidding', // Nome do método no Unity 
        message: "DestroyItem:" + target
    }; 

    return `iframe.contentWindow.postMessage(${JSON.stringify(message)}, '*');\n`;      
};













//-===========================================================================================================
// Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "pedra",
//         "message0": "Pedra",
//         "output": "String",
//         "colour": 120,
//         "tooltip": "Retorna um alvo fixo: 'pedra'",
//         "helpUrl": "",
//         "args0": []
//     }
// ]);

// javascript.javascriptGenerator.forBlock['pedra'] = function(block) {
//     // Verifica se o bloco está conectado a algo
//     if (!block.outputConnection || !block.outputConnection.targetConnection) {
//         return ['""', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna string vazia se estiver solto
//     }

//     return ['"enemy"', Blockly.JavaScript.ORDER_ATOMIC]; // Retorna "enemy" ou "object"
// };




// Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "is_far_player",
//         "message0": "Esta longe do jogador",         //Em até 5 metros de visão do jogador?
//         "output": "Boolean",
//         "colour": 210,
//         "tooltip": "Retorna verdadeiro se o inimigo estiver a até 5 metros do jogador",
//         "helpUrl": ""
//     }
// ]);
// javascript.javascriptGenerator.forBlock['is_far_player'] = function(block) {
//     return [`Vector3.Distance(player.transform.position, enemy.transform.position) <= 5`, Blockly.JavaScript.ORDER_NONE];
// };


// Blockly.common.defineBlocksWithJsonArray([
//     {
//         "type": "is_far_player",
//         "message0": "Esta longe do jogador %1",
//         "args0": [
//             {
//                 "type": "input_value",
//                 "name": "TARGET",
//                 "check": "String"
//             }
//         ],
//         "output": "Boolean",
//         "colour": 210,
//         "tooltip": "Retorna verdadeiro se o alvo estiver a até 5 metros do jogador",
//         "helpUrl": ""
//     }
// ]);
// javascript.javascriptGenerator.forBlock['is_far_player'] = function(block) {
//     var target = Blockly.JavaScript.valueToCode(block, 'TARGET', Blockly.JavaScript.ORDER_NONE) || '""';
//     if(target === '""'){
//         return ``;
//     }

//     return [`Vector3.Distance(player.transform.position, ${target}.transform.position) <= 5`, Blockly.JavaScript.ORDER_NONE];
// };
