using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemManager : MonoBehaviour
{
    PopSystem pop;
    PlayerController player;
    InventoryManager inventory;

    BehaviourVideo vd;
    DialogueSystem dialogue;


    //fase3
    [Header("Fase 2 e 3")]
    public bool faseAllHunter = false;
    
    public List<GameObject> EnemysFase = new List<GameObject>();

    private List<EnemyData> savedEnemiesData = new List<EnemyData>();

    // Prefabs dos dois tipos de inimigos (pode ter mais se necessário)
    public string tagNamePrefab1 = "Slime";
    public GameObject mobPrefab1;
    public GameObject mobPrefab2;

    // Classe para armazenar as informações dos inimigos (posição e tipo/prefab)
    [System.Serializable]
    public class EnemyData
    {
        public Vector3 position;  // Posição do inimigo
        public GameObject prefab; // Prefab do tipo de inimigo
    }
   
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pop = FindObjectOfType<PopSystem>();
        inventory = FindObjectOfType<InventoryManager>(); 
        
        vd = FindObjectOfType<BehaviourVideo>();

        inventory.nextLevel += pop.PopUpNotification;



        
        EnemysFase.AddRange(GameObject.FindGameObjectsWithTag("Pig"));
        EnemysFase.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
        EnemysFase.AddRange(GameObject.FindGameObjectsWithTag("Slime"));
        EnemysFase.AddRange(GameObject.FindGameObjectsWithTag("Skeleton"));
         

        SaveEnemiesPositions();
    }  




    //FASE 3 E 2
    void Update(){
        if(faseAllHunter){
            // Filtra a lista removendo qualquer referência a objetos nulos
            EnemysFase.RemoveAll(enemy => enemy == null);

            if(!player.level && EnemysFase.Count == 0){
                pop.PopUpNotification("Parabens, voce desbloqueou novos blocos, Volte A cerca de Instruções");
                // CallJS();
                inventory.up();
                player.level = true;
            } 
        }
    }  




    // Função para respawnar os inimigos nas posições salvas
    public void RespawnEnemies()
    {
        // Verifica se há dados salvos para respawnar
        if (savedEnemiesData.Count > 0)
        {
            foreach (var enemyData in savedEnemiesData)
            {
                // Respawn do inimigo usando o prefab salvo e na posição salva
                GameObject newEnemy = Instantiate(enemyData.prefab, enemyData.position, Quaternion.identity);
                newEnemy.name = enemyData.prefab.name;
                
                EnemysFase.Add(newEnemy);  // Adiciona o novo inimigo à lista
            } 
        }
    }

    // Função para salvar as posições dos inimigos antes de destruí-los
    public void SaveEnemiesPositions()
    { 
        // Salva as posições de todos os inimigos
        foreach (var enemy in EnemysFase)
        {
            if (enemy != null)  // Verifica se o inimigo ainda está na cena
            {
                EnemyData enemyData = new EnemyData
                {
                    position = enemy.transform.position,
                    prefab = enemy.tag == tagNamePrefab1 ? mobPrefab1 : mobPrefab2  // Verifica o tipo do inimigo pela tag
                };
                savedEnemiesData.Add(enemyData);  // Armazena as informações 
            }
        }
    }


    public void CallJS( ){

        // Chamando um script JavaScript no navegador
        #if UNITY_WEBGL && !UNITY_EDITOR 
            Application.ExternalEval("parent.postMessage({ type: 'Level', data: 'Level', id: 'Level' }, '*');");
        #endif
    }
}
