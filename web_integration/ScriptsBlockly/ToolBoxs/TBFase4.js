


const toolboxFase4= {
    'kind': 'categoryToolbox',          
    'contents': [
        {
          'kind': 'category',
              'name': 'Movimento',
              'colour': '#5C81A6',
              'contents': [ 
                      //move
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
                      { 'kind': 'block', 'type': 'RandomLookAt'  },
                      //------------------------------------------------------------------
                      { 'kind': 'block', 'type': 'Up', "maxInstances": 1 }, 
                      { 'kind': 'block', 'type': 'Down'        }, 
                      { 'kind': 'block', 'type': 'Left'        },
                      { 'kind': 'block', 'type': 'Right'       }
            ]
        },
        {
          'kind': 'category',
              'name': 'Eventos',
              'colour': 'red',
              'contents': [
                      // customBlocks , 
                      { 'kind': 'block', 'type': 'my_repeat' },
                      { 'kind': 'block', 'type': 'controls_if' },
                      { 'kind': 'block', 'type': 'when_key_pressedLetterCondition' },
                      { 'kind': 'block', 'type': 'while_loop' },
                      { 'kind': 'block', 'type': 'infinite_loop' },
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
                      { 'kind': 'block', 'type': 'Bow' },
                      { 'kind': 'block', 'type': 'axe' },
                      { 'kind': 'block', 'type': 'ShootTarget' },
                      { 'kind': 'block', 'type': 'Miner' }, 
                      { 'kind': 'block', 'type': 'PlaceTile' },
                      { 'kind': 'block', 'type': 'Food' }
  
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
                      { 'kind': 'block', 'type': 'is_near_player' },
                      { 'kind': 'block', 'type': 'is_far_player'  },
                      { 'kind': 'block', 'type': 'target_object_Mob' },
                      { 'kind': 'block', 'type': 'target_object_Object' },
                      { 'kind': 'block', 'type': 'target_object_Seed' },
                      { 'kind': 'block', 'type': 'target_object_Food' },
                      { 'kind': 'block', 'type': 'is_inventory' },
                      { 'kind': 'block', 'type': 'count_inventory' },
                      { 'kind': 'block', 'type': 'DestroyItem' }
  
              ]
        },
        {
          'kind': 'category',
              'name': 'Operadores',
              'colour': '#18a74a',
              'contents': [
                      { 'kind': 'block', 'type': 'comparison' },
                      { 'kind': 'block', 'type': 'boolean_operator' },
                      { 'kind': 'block', 'type': 'variable_get' },
                      { 'kind': 'block', 'type': 'variable_set' }, 
                      { 'kind': 'block', 'type': 'math_number' },
                      { 'kind': 'block', 'type': 'logical_not' }
              ]
        }
    ]
  };
