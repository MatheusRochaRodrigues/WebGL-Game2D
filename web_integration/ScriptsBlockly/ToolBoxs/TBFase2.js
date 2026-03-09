const toolboxFase2= {
    'kind': 'categoryToolbox',          
    'contents': [
        {
          'kind': 'category',
              'name': 'Movimento',
              'colour': '#5C81A6',
              'contents': [ 
                      //move 
                      //------------------------------------------------------------------
                      { 'kind': 'block', 'type': 'UpTile'      }, 
                      { 'kind': 'block', 'type': 'DownTile'    },
                      { 'kind': 'block', 'type': 'LeftTile'    },
                      { 'kind': 'block', 'type': 'RightTile'   },
                      { 'kind': 'block', 'type': 'RandomTile'  },
                      { 'kind': 'block', 'type': 'RunDirection'  },
                      //-------------------------------------------------------------------
                      { 'kind': 'block', 'type': 'UpLookAt'      }, 
                      { 'kind': 'block', 'type': 'DownLookAt'    },
                      { 'kind': 'block', 'type': 'LeftLookAt'    },
                      { 'kind': 'block', 'type': 'RightLookAt'   },
                      { 'kind': 'block', 'type': 'RandomLookAt'  } 
            ]
        },
        {
          'kind': 'category',
              'name': 'Eventos',
              'colour': 'red',
              'contents': [ 
                      { 'kind': 'block', 'type': 'my_repeat' },
                      { 'kind': 'block', 'type': 'controls_if' },  
                      { 'kind': 'block', 'type': 'function_definition' },
                      { 'kind': 'block', 'type': 'function_call' }
                      
            ]
        },
        {
          'kind': 'category',
              'name': 'Ações',
              'colour': 'blue',
              'contents': [
                      { 'kind': 'block', 'type': 'Sword' },
                      { 'kind': 'block', 'type': 'Hoop' },
                      { 'kind': 'block', 'type': 'Plow' },
                      { 'kind': 'block', 'type': 'WaterCan' },
                      { 'kind': 'block', 'type': 'axe' },
                      { 'kind': 'block', 'type': 'Bow' },
                      { 'kind': 'block', 'type': 'ShootTarget' },
                      { 'kind': 'block', 'type': 'Miner' }, 
                      { 'kind': 'block', 'type': 'PlaceTile' }
  
            ]
        },
        {
          'kind': 'category',
              'name': 'Craftar',
              'colour': 'rgb(54, 171, 210)',
              'contents': [
                      { 'kind': 'block', 'type': 'Craft' },
                      { 'kind': 'block', 'type': 'craft_object' }
  
            ]
        },
        {
          'kind': 'category',
              'name': 'Alvos',
              'colour': 'rgb(167, 19, 144)',
              'contents': [ 
                      { 'kind': 'block', 'type': 'target_object_Animal' },
                      { 'kind': 'block', 'type': 'target_object_Object' }
              ]
        },
        {
          'kind': 'category',
              'name': 'Operadores',
              'colour': '#18a74a',
              'contents': [ 
                      { 'kind': 'block', 'type': 'math_number' } 
              ]
        }
    ]
  };

//PROGRESS OF TOOLBOX

// Fila que contém as coleções de blocos a serem adicionadas
let filaDeColecoes2 = [
    // ⭐ 1
    [  
        { 
            categoria: "Operadores",
            cor: "#18a74a",
            blocos: [
                { 'kind': 'block', 'type': 'comparison' },
                { 'kind': 'block', 'type': 'boolean_operator' }
            ]
        }
    ],
    // ⭐ 2
    [
        { 
            categoria: "Eventos",
            cor: "red",
            blocos: [
                { 'kind': 'block', 'type': 'infinite_loop' }
            ]
        },
        { 
            categoria: "Alvos",
            cor: 'rgb(167, 19, 144)',
            blocos: [
                { 'kind': 'block', 'type': 'is_near_player' },
                { 'kind': 'block', 'type': 'is_far_player'  }
            ]
        }
    ],
    // ⭐ 3 
    [ 
        { 
            categoria: "Eventos",
            cor: "red",
            blocos: [                      
                { 'kind': 'block', 'type': 'while_loop' } 
            ]
        } 
    ] 
];
 