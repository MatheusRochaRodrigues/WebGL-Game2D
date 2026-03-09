
const toolboxFase1= {
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
              'name': 'Ações',
              'colour': 'blue',
              'contents': [ 
                    { 'kind': 'block', 'type': 'Sword' },
                      { 'kind': 'block', 'type': 'axe' },
                      { 'kind': 'block', 'type': 'Miner' }
  
            ]
        } 
    ]
  };

//PROGRESS OF TOOLBOX

// Fila que contém as coleções de blocos a serem adicionadas
let filaDeColecoes1 = [
    // ⭐ 1
    [
        {
            categoria: "Craftar",
            cor: 'rgb(54, 171, 210)',
            blocos: [
                { 'kind': 'block', 'type': 'Craft' },
                { 'kind': 'block', 'type': 'craft_object' } 
            ]
        }
    ],
    // ⭐ 2
    [
        {
            categoria: "Ações",
            cor: "blue",
            blocos: [ 
                { 'kind': 'block', 'type': 'PlaceTile' }
            ]
        },
        {
            categoria: "Alvos",
            cor: 'rgb(167, 19, 144)',
            blocos: [
                { 'kind': 'block', 'type': 'target_object_Object' }
            ] 
        } 
    ],
    // ⭐ 3
    [
        {
            categoria: "Ações",
            cor: "blue",
            blocos: [
                { 'kind': 'block', 'type': 'Hoop' },
                { 'kind': 'block', 'type': 'Plow' },
                { 'kind': 'block', 'type': 'WaterCan' }
            ]
        },
        {
            categoria: "Alvos",
            cor: 'rgb(167, 19, 144)',
            blocos: [
                { 'kind': 'block', 'type': 'target_object_Seed' }
            ] 
        } 
    ],
    // ⭐ 4
    [ 
        { 
            categoria: "Eventos",
            cor: "red",
            blocos: [
                { 'kind': 'block', 'type': 'my_repeat' } 
            ]
        },
        { 
            categoria: "Operadores",
            cor: '#18a74a',
            blocos: [
                { 'kind': 'block', 'type': 'math_number' }
            ]
        }
    ],
    // ⭐ 5 
    [ 
        { 
            categoria: "Eventos",
            cor: "red",
            blocos: [
                { 'kind': 'block', 'type': 'function_definition' },
                { 'kind': 'block', 'type': 'function_call' }
            ]
        }
    ] 
];

