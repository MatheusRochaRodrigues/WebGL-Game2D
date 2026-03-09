using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Item;
using FirstGearGames.SmoothCameraShaker;
using UnityEditor;
using System.Linq;
using static ManagerBlocksWorld;

public class PlayerController : MonoBehaviour, ILife
{
    public Rigidbody2D _playerRigidbody;
    public Animator _playerAnimator;
    public string currentAnimation = "";

    [Header("Moviment")]
    public float _speedInitial;
    public float _playerSpeed      = 2.0f;  //walk  
    public float _playerMultSpeed  = 2.0f;  //run
    public float _playerInitialSpeed;

    public float speed = 5f;
    Vector2 _playerDirection = Vector2.zero; 
    private Queue<string> keyMappings = new Queue<string>();

    
    [Header("Moviment Tile")]
    private bool isMoving = false;
    public float moveSpeed = 5f;
    private bool typeMovTile = false;
    private Queue<string> queueActions = new Queue<string>();

    [Space()]
    [Header("KnockBack")]
    //Damage KnockBack
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public Vector2 isKnockRight;

    [Header("Arrow")]
    public GameObject Arrow;
    public Transform ShooterLocation;

    private List<string> waitActions = new List<string>(){
        "Sword1",
        "Sword2",
        "Sword3",
        "Bow",
        "AutoShoot",
        "Hoop",
        "WaterCan",
        "Miner",
        "Axe"

    };

    //Attack
    private int currentAtackNb = 1;
    public AttackArea attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    [Header("Shake")]
    public ShakeData shake;
    // public ShakeData shakeLight;

    //Inventory
    [Header("Only IventoryManager")]
    [HideInInspector]
    public InventoryManager _inventoryManager;
    //objeto que esta na mao do player
    public static string _itemInHandle; 
    private bool stateInventory = false;

    //Tile States 
    public float tileSize = 1f; // Tamanho de um tile no Grid
    public InteractMap interactMap;

    //Dialogue System
    [Space()]
    [Header("Dialogue System")]
    DialogueSystem dialogueSystem;
    public Transform guide;

    //Events
    // public delegate void BuyHandler(string text);
    // public event BuyHandler buy;

    public AudioClip som; // Arraste o som aqui pelo Inspector
    private AudioSource fonte;
    public AudioClip guideSong; // Arraste o som aqui pelo Inspector

     
    void Start()
    {
        fonte = GetComponent<AudioSource>();
        if (fonte == null)
            fonte = gameObject.AddComponent<AudioSource>();


        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        _inventoryManager = FindObjectOfType<InventoryManager>();

        _playerDirection = Vector2.down;
        SetDirectionPlayer();
        _playerDirection = Vector2.zero;

        CheckAnimation("");

        // Chamando um script JavaScript no navegador
        Application.ExternalEval("console.log('Chamado do Unity!')");

        StartCoroutine(onFire());  
        SnapToGrid();

        // setKeyBidding("leftTile") ; setKeyBidding("leftTile") ; setKeyBidding("leftTile") ; setKeyBidding("leftTile") ; setKeyBidding("Sword") ; setKeyBidding("leftTile") ; setKeyBidding("Sword") ; setKeyBidding("Sword") ; setKeyBidding("Sword") ; setKeyBidding("leftTile") ; 
    } 

    public void Setup(float walk, float run)
    {
        _playerSpeed = walk;
        _playerInitialSpeed = walk;
        _playerMultSpeed = run;
    }

    public void Update() { 
        teste();            // if (Input.GetKeyDown(KeyCode.G))  _inventoryManager.AddItem(it2);   

        useGuide();
        Run(); 
        //Attacks
        if(attacking) {
            timer += Time.deltaTime;
            if(timer >= timeToAttack) {
                timer = 0;
                attacking = false;
                attackArea.gameObject.SetActive(attacking);
            }
        }
    } 

    public void teste(){
        // if(Input.GetKey("a")) setKeyBidding("leftTile") ;   
        // if(Input.GetKey("w")) setKeyBidding("upTile")   ;
        // if(Input.GetKey("s")) setKeyBidding("downTile") ;   
        // if(Input.GetKey("d")) setKeyBidding("rightTile");

        if(Input.GetKey("a")) setKeyBidding("left") ; 
        if(Input.GetKey("w")) setKeyBidding("up")   ;
        if(Input.GetKey("s")) setKeyBidding("down") ;   
        if(Input.GetKey("d")) setKeyBidding("right");

        if(Input.GetKeyDown("f")) setKeyBidding("Hoop");
        if(Input.GetKeyDown("h")) setKeyBidding("WaterCan");
        if(Input.GetKeyDown("e")) setKeyBidding("Axe");
        if(Input.GetKeyDown("r")) setKeyBidding("Sword");
        if(Input.GetKeyDown("t")) setKeyBidding("Pick");


        if(Input.GetKey("b")) setKeyBidding("AutoShoot:Skeleton");
        if(Input.GetKeyDown("g")) setKeyBidding("Plow:Wheat_Seed");
        if(Input.GetKeyDown("x")) setKeyBidding("setTile:Fence");
        // if(Input.GetKey("q")) setTile(it2);
        // if(Input.GetKeyDown("q")) setTile("Fence");
        // if(Input.GetKey("p")) setKeyBidding("inventory");
    }

    public void setKeyBidding(string data){
        keyMappings.Enqueue(data);             //keyMappings.Push(data);
    }
    
    private void CheckAnimation(string data){
        //verify if disponible
        if(waitActions.Contains(currentAnimation))
            return;

        if(data == "") {
            ChangeAnimation("Idle");    // Application.ExternalEval("console.log('Chamado do Unity2!')");
            return;
        }
        ChangeAnimation(data);
    }

    public void ChangeAnimation(string animation, float crossfade = 0.2f, float time = 0){
        if(time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait(){
            yield return new WaitForSeconds(time - crossfade);
            Validate();
            
        }

        void Validate(){
            if(currentAnimation != animation)
            {
                currentAnimation = animation;

                if(currentAnimation == "")
                    CheckAnimation("");            //checkAnimation()
                else
                    _playerAnimator.CrossFade(animation, crossfade);
            }
            
        }
    } 
    [SerializeField] private float offset = 0.7f; // distância do collider à frente do player
    private void UpdateFrontCollider()
    {
        Vector2 dir = _playerDirection.normalized;
        attackArea.col.transform.localPosition = new Vector3(dir.x, dir.y, 0) * offset;
    }

    private void SetDirectionPlayer(){
        _playerAnimator.SetFloat("AxisX", _playerDirection.x);
        _playerAnimator.SetFloat("AxisY", _playerDirection.y);

        UpdateFrontCollider();
    }  
 
    void Run(){
        if (isMoving || typeMovTile) return; // Evita iniciar outro movimento antes de terminar o atual 

        _playerDirection = Vector2.zero;

        //  KnockBack
        if(KBCount > 0){ 
            CheckAnimation("Damage");
            return;  
        }else if (_playerRigidbody.velocity.magnitude < 0.1f)         //      _playerRigidbody.velocity.SqrMagnitude() > 0
            _playerRigidbody.velocity = Vector2.zero; // Para completamente
        
        //verify if disponible
        if(waitActions.Contains(currentAnimation))
            return; 

        //  Mapping actions
        if(keyMappings.Count == 0) CheckAnimation("");

        while(keyMappings.Count != 0){
            string mapping = keyMappings.Dequeue(); 
            switch (mapping)
            {
                //look at------------------------------------------------------------
                case "leftLookAt":
                    _playerDirection.x = -1; 
                    SetDirectionPlayer();
                    _playerDirection = Vector2.zero;
                    break;
                case "rightLookAt":
                    _playerDirection.x = 1; 
                    SetDirectionPlayer();
                    _playerDirection = Vector2.zero;
                    break;
                case "upLookAt":
                    _playerDirection.y = 1; 
                    SetDirectionPlayer();
                    _playerDirection = Vector2.zero;
                    break;
                case "downLookAt":
                    _playerDirection.y = -1; 
                    SetDirectionPlayer();
                    _playerDirection = Vector2.zero;
                    break;
                case "randomLookAt":
                    randomDirection(); 
                    SetDirectionPlayer();
                    _playerDirection = Vector2.zero;
                    break;

                //---------------------------------------------------------------Moviment
                case "left":
                    _playerDirection.x = -1; 
                    break;
                case "right":
                    _playerDirection.x = 1; 
                    break;
                case "up":
                    _playerDirection.y = 1; 
                    break;
                case "down":
                    _playerDirection.y = -1; 
                    break;

                //MovimentPerTile---------------------------------------------------------------------
                case "leftTile":
                    _playerDirection = Vector2.left; 
                    SnapToGrid();
                    break;
                case "rightTile":
                    _playerDirection = Vector2.right; 
                    SnapToGrid();
                    break;
                case "upTile":
                    _playerDirection = Vector2.up; 
                    SnapToGrid();
                    break;
                case "downTile":
                    _playerDirection = Vector2.down; 
                    SnapToGrid();
                    break;
                case "randomTile":
                    randomDirection(); 
                    SnapToGrid();
                    break; 


                //Actions----------------------------------------------------------------------------
                case "Sword":
                    CheckAnimation("Sword" + currentAtackNb++);  
                    _itemInHandle = mapping;
                    if(currentAtackNb > 3) currentAtackNb = 1;
                    Attack();
                    return; 

                case "Death":
                    CheckAnimation("Death");
                    _playerRigidbody.isKinematic = true;    //isso desativa a física do Rigidbody2D
                    break;

                case "Bow": 
                    arrow = true;
                    return;

                case "Hoop":
                    CheckAnimation(mapping);
                    StartCoroutine(HookPlow());   
                    return;

                case "Pick":
                    CheckAnimation("Sword3");  
                    _itemInHandle = mapping;
                    Pick();
                    return; 
                
                case "Axe":
                    CheckAnimation(mapping);
                    _itemInHandle = mapping;
                    Attack();
                    return;

                case "Miner":
                    CheckAnimation(mapping);
                    _itemInHandle = mapping;
                    Attack();
                    return;

                case "WaterCan":
                    StartCoroutine(WaterCan());  
                    CheckAnimation("WaterCan");
                    return;

                case "RunDirection":
                    _playerDirection = ArrowDirection();
                    break;


                // Manage care --------------------------------------------------------------------
                default:
                    if(mapping.StartsWith("AutoShoot")){
                        string[] values = mapping.Split(':');
                        string target = values[1];
                        TargetArrow = target;
                        AimAtNearestEnemy(TargetArrow); 
                        // currentAnimation = values[0];
                        _playerDirection = Vector2.zero;
                    }
                    if(mapping.StartsWith("Plow")){
                        string[] values = mapping.Split(':');
                        string target = values[1];
                        StartCoroutine(Plow(target));
                        return;
                    }
                    if(mapping.StartsWith("setTile")){
                        string[] values = mapping.Split(':');
                        string target = values[1];
                        setTile(target);
                        return;
                    }
                    if(mapping.StartsWith("DestroyItem")){
                        string[] values = mapping.Split(':');
                        string target = values[1];
                        _inventoryManager.getItem(target);
                        return;
                    }
                    if(mapping.StartsWith("Food")){
                        string[] values = mapping.Split(':');
                        string target = values[1]; 
                        //  Verificações iniciais   
                        if(_inventoryManager.getItem(target) == null) return;  
                        GetComponent<Food>().TryEat(target); 
                        return;
                    } 
                    //Inventory
                    // if(mapping.StartsWith("inventory")){ 
                    //     if (Input.anyKeyDown) {
                    //         _inventoryManager._inventoryMenu.SetActive(!stateInventory);
                    //         stateInventory = !stateInventory; 
                    //     } 
                    // } 
                    break; 
            }
            if(mapping.EndsWith("Tile") || mapping == "RunDirection") {
                typeMovTile = true; 
                CheckAnimation("Walk");
                SetDirectionPlayer();
                return;
            } else {
                typeMovTile = false;
            }
        }

        if (typeMovTile == false && _playerDirection != Vector2.zero) {
            CheckAnimation("Walk");
            SetDirectionPlayer();
        }  
    }  

    private void randomDirection(){
        int rand = UnityEngine.Random.Range(0,4);
        switch(rand){
            case 0:  _playerDirection = Vector2.left;  break;
            case 1:  _playerDirection = Vector2.right;   break;
            case 2:  _playerDirection = Vector2.up;  break;
            default: _playerDirection = Vector2.down;  break;  
        } 
    }
    

    private Vector2 targetPosition;
    void FixedUpdate() {
        if (isMoving) return;

        if (KBCount > 0) {   // Somente move se não estiver sofrendo knockback
            KnockLogic();
            return;
        }
        if(waitActions.Contains(currentAnimation)) return;
        if (_playerDirection.sqrMagnitude > 0.1f){
            if (!typeMovTile)
                _playerRigidbody.MovePosition(_playerRigidbody.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);
            else
                StartCoroutine(MoveToCell()); 
        }else {
            _playerRigidbody.velocity = Vector2.zero; // Para quando não há input
         }
    }  
    
    IEnumerator MoveToCell()
    {
        Tilemap tilemap = interactMap.tilemapInteractable;
        Vector3Int direction = new Vector3Int(
            Mathf.RoundToInt(_playerDirection.x), // Converte a direção X para inteiro
            Mathf.RoundToInt(_playerDirection.y), // Converte a direção Y para inteiro
            0 // Mantemos Z como 0 para jogos 2D
        );

        isMoving = true;
        
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + direction;
        Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f / moveSpeed)
        {
            _playerRigidbody.MovePosition(Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed));
            elapsedTime += Time.fixedDeltaTime; 
            yield return new WaitForFixedUpdate(); // Usa FixedUpdate para sincronizar com a física
        }

        _playerRigidbody.MovePosition(targetPosition); // Garante que ele termine exatamente no centro do tile
        isMoving = false;

        typeMovTile = false;
    }
    

    void KnockLogic(){
        _playerRigidbody.velocity = new Vector2(KBForce, KBForce) * isKnockRight; 
        KBCount -= Time.fixedDeltaTime; 
    }     


    
    void SnapToGrid()
    {
        Tilemap tilemap = interactMap.tilemapInteractable;
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
        transform.position = cellCenter;
    }

    IEnumerator WaterCan(){    
        float duration = GetAnimationClipLength("WaterCan")+0.1f; 
        Tilemap tilemap = interactMap.tilemapPlow;  
        Vector3Int cellPosition = getTileFrontPositionCell(); 
 
        Tile tile = tilemap.GetTile(cellPosition) as Tile;
        if (tile != null) {
            Item item = TileSeeds[cellPosition];    

            if(item.stateGrown < item.seedGrown.Count-1){ 
                Debug.Log("regando.");     
                yield return new WaitForSeconds(duration);
                Tile newTile = ScriptableObject.CreateInstance<Tile>();  
                newTile.sprite = item.seedGrown[++item.stateGrown];
                tilemap.SetTile(cellPosition, newTile);   
            } 
        } else Debug.Log("Não há tile nessa posição.");  
    }


    // public void Pick(){ 
        // //Verificações iniciais {   
        // Vector3Int cellPosition = getTileFrontPositionCell(); 
        // Tilemap tilemap = interactMap.tilemapPlow; 
        // TileBase tile = tilemap.GetTile(cellPosition);
        // if (tile != null) {
        //     tilemap.SetTile(cellPosition, null);     
        //     interactMap.tilemapPlow.SetColliderType(cellPosition, Tile.ColliderType.None);  
        //     interactMap.availableTiles.Remove(cellPosition);   
        // }  
    // }


    void Pick()
    {
        // Vector3 cellPosition = getTileFrontPositionCell();  
        // Vector2 playerDirection = ArrowDirection().normalized;
        // Tilemap tilemap = interactMap.tilemapPlow; 
        // // ;

        // Vector2 position = transform.position; // Posição do jogador ou origem do disparo
        // Vector2 direction = playerDirection; // Mude para testar outras direções
        // float distance = 1; // Distância máxima do raio
        // RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
        // Debug.DrawLine(position, position + (direction * distance), Color.yellow, 2f);
        // if (hit.collider != null)
        // {
        //     // Verifica se o objeto atingido tem o script TileLogic (ou outro que lide com dano)
        //     TileLogic tileLogic = hit.collider.GetComponent<TileLogic>();

        //     if (tileLogic != null)
        //     {
        //         // Chama a função de dano passando os parâmetros necessários
        //         tileLogic.isDamage(1, hit.transform);
        //     }
        // }
    }


    IEnumerator Plow(string seed){ 
        //Verificações iniciais {
        Item itemTile = _inventoryManager.getItem(seed);
        if(itemTile == null) yield break; 
        Tilemap tilemap = interactMap.tilemapInteractable;  
        Vector3Int cellPosition = getTileFrontPositionCell();   
        if(itemTile.type != ItemType.Seed || ManagerBlocksWorld.TileSeeds.ContainsKey(cellPosition)){ 
            _inventoryManager.AddItem(itemTile);
            yield break;
        } 
        //} 

        float duration = GetAnimationClipLength("Hoop")+0.1f; 

        if (!interactMap.availableTiles.ContainsKey(cellPosition)){
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null) {
                if(tile is RuleTile t && t == ManagerBlocksWorld._groundPlow){       //if(tile is Tile t && t == ManagerBlocksWorld._groundPlow)
                    yield return new WaitForSeconds(duration);
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();  
                    newTile.sprite = itemTile.seedGrown[0];

                    if(ManagerBlocksWorld.TileSeeds.ContainsKey(cellPosition)) yield break;

                    TileSeeds.Add(cellPosition, itemTile.CreateCopy());

                    interactMap.tilemapPlow.SetTile(cellPosition, newTile);  
                    interactMap.tilemapPlow.SetColliderType(cellPosition, Tile.ColliderType.None); 

                    interactMap.availableTiles.Remove(cellPosition);  

                    CreateTileCollider(cellPosition, TileSeeds[cellPosition]); 
                    yield break;   
                } 
            } 
        }
        Debug.Log("Não há tile nessa posição."); 
        _inventoryManager.AddItem(itemTile);
    }
    
    IEnumerator HookPlow(){    
        float duration = GetAnimationClipLength("Hoop")+0.1f; 
        Tilemap tilemap = interactMap.tilemapInteractable;  
        Vector3Int cellPosition = getTileFrontPositionCell(); 
        if (!interactMap.availableTiles.ContainsKey(cellPosition))  yield break; 
        yield return new WaitForSeconds(duration);

        // Tile newTile = ScriptableObject.CreateInstance<Tile>();  
        // newTile.sprite = ManagerBlocksWorld._groundPlow;
        tilemap.SetTile(cellPosition, ManagerBlocksWorld._groundPlow);  
        tilemap.SetColliderType(cellPosition, Tile.ColliderType.None);

        interactMap.availableTiles.Remove(cellPosition);
    }

    Vector3Int getTileFrontPositionCell(){
        Tilemap tilemap = interactMap.tilemapInteractable;  
        Vector2 playerDirection = ArrowDirection();
        Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(this.transform.position);     // +  new Vector3(0.5f, 0.5f, 0)
        Vector3Int direction = new Vector3Int(
            Mathf.RoundToInt(playerDirection.x - 0.3f), // Converte a direção X para inteiro        // playerDirection.x
            Mathf.RoundToInt(playerDirection.y - 0.3f), // Converte a direção Y para inteiro        // playerDirection.y
            0 // Mantemos Z como 0 para jogos 2D
        );
        cellPosition += direction; 
        return cellPosition;
    }
 
    void CreateTileCollider(Vector3Int cellPosition, Item item)
    { 
        Vector3 worldPosition = interactMap.tilemapInteractable.GetCellCenterWorld(cellPosition);
        GameObject obj = new GameObject("TileScriptHolder" + worldPosition);
        obj.transform.position = worldPosition; 
        // Adiciona o Collider para colisões
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1); // Ajuste conforme necessário

        // Adiciona um Rigidbody2D para detectar colisões
        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
        // rb.bodyType = RigidbodyType2D.Static;
        rb.bodyType = RigidbodyType2D.Kinematic; // Não interage fisicamente, mas permite detectar triggers
        collider.isTrigger = true;

        TileLogic tlogic = obj.AddComponent<TileLogic>(); // Script que gerencia lógica do tile             // ItemInWorld itWrld = obj.AddComponent<ItemInWorld>(); // Script que gerencia lógica do tile
        obj.AddComponent<SpriteRenderer>(); // Script que gerencia lógica do tile             // ItemInWorld itWrld = obj.AddComponent<ItemInWorld>(); // Script que gerencia lógica do tile
        // tlogic.prefabItem = ManagerBlocksWorld.worldSeed.prefabItem;
        tlogic.SetTileLogic(cellPosition, item);

    }
 
    public void setTile(string item){                                       // public void setTile(Item itemTile){ 
        Item itemTile = _inventoryManager.getItem(item);
        if(itemTile == null) return; 
        
        if(itemTile.type != ItemType.Building ){ 
            _inventoryManager.AddItem(itemTile);
            return;
        }

        Tilemap tilemap = interactMap.tilemapInteractable;
        //Mouse 
            // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        //PlayerInput 
        Vector2 playerDirection = ArrowDirection();
        // Vector3  direction = new Vector3 (  playerDirection.x, playerDirection.y, 0  );   
        // direction = this.transform.position + direction;
        // Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(direction);     // +  new Vector3(0.5f, 0.5f, 0)
        Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(this.transform.position);     // +  new Vector3(0.5f, 0.5f, 0)
        Vector3Int direction = new Vector3Int(
            Mathf.RoundToInt(playerDirection.x), // Converte a direção X para inteiro
            Mathf.RoundToInt(playerDirection.y), // Converte a direção Y para inteiro
            0 // Mantemos Z como 0 para jogos 2D
        );
        cellPosition += direction; 

        if (!interactMap.availableTiles.ContainsKey(cellPosition)){
            _inventoryManager.AddItem(itemTile);
            return;
        }
        
        // RuleTile newTile = ScriptableObject.CreateInstance<RuleTile>();
        if(itemTile.ruleTile != null){
            tilemap.SetTile(cellPosition, itemTile.ruleTile);         
        }else{ 
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            newTile.sprite = itemTile.img;
            tilemap.SetTile(cellPosition, newTile);
        }

        // tilemap.SetColliderType(cellPosition, Tile.ColliderType.Sprite);        //newTile.colliderType = None;
        interactMap.availableTiles.Remove(cellPosition);

        _inventoryManager.ManagerLevels(itemTile ,true);
 





        //colider game object FOR DROP NEWWW
        Vector3 worldPosition = interactMap.tilemapInteractable.GetCellCenterWorld(cellPosition);
        GameObject obj = new GameObject("TileScriptHolder" + worldPosition);
        obj.transform.position = worldPosition; 
        // Adiciona o Collider para colisões
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1); // Ajuste conforme necessário

        // Adiciona um Rigidbody2D para detectar colisões
        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
        // rb.bodyType = RigidbodyType2D.Static;
        rb.bodyType = RigidbodyType2D.Kinematic; // Não interage fisicamente, mas permite detectar triggers
        collider.isTrigger = true;

        TileLogicInteract tlogic = obj.AddComponent<TileLogicInteract>(); // Script que gerencia lógica do tile             // ItemInWorld itWrld = obj.AddComponent<ItemInWorld>(); // Script que gerencia lógica do tile
        obj.AddComponent<SpriteRenderer>(); // Script que gerencia lógica do tile             // ItemInWorld itWrld = obj.AddComponent<ItemInWorld>(); // Script que gerencia lógica do tile
        tlogic.SetTileLogic(cellPosition, itemTile); 
    }


    public void collectItem(Item item){ _inventoryManager.AddItem(item); }

    public bool level = false;
    private void useGuide(){ 
        //Inventory 
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _inventoryManager._inventoryMenu.SetActive(!stateInventory);
            stateInventory = !stateInventory; 
        }  


        //DialogueSystem
        Vector2 diff = new Vector3(
            Mathf.Abs(transform.position.x - guide.position.x),
            Mathf.Abs(transform.position.y - guide.position.y)
        );

        if (diff.sqrMagnitude < 2.0f) {
            if (dialogueSystem.state == STATE.DISABLED && Input.GetKeyDown(KeyCode.Space)){  

                fonte.PlayOneShot(guideSong); // Toca o som uma vez
                
                dialogueSystem.Next(); 
 
                if(level) { 
                    FindObjectOfType<CheckList>().updateCount(); 
                    GameObject.FindGameObjectWithTag("List").SetActive(false);
                    FindObjectOfType<EventSystemManager>().RespawnEnemies(); 
                    level = false;
 
                }else {
                    GameObject.FindGameObjectWithTag("List").SetActive(false); 
                }
            } 
        }

    } 
    
    public void Attack(){
        attacking = true; 
        attackArea.gameObject.SetActive(attacking);
        
        //new  
        attackArea.col.enabled = false;
        attackArea.col.enabled = true;

    }

    public void isDamage(int Damage, Transform collision,  Item item = null){ 
        // if(isMoving || KBCount > 0)return;
        if(KBCount > 0)return;
        
        fonte.PlayOneShot(som); // Toca o som uma vez

        //set animation damage
        KBCount = KBTime; 

        Vector2 knockDirection = (collision.transform.position - this.transform.position).normalized; 
        if (Mathf.Abs(knockDirection.x) > Mathf.Abs(knockDirection.y)) 
            isKnockRight = new Vector2(Mathf.Sign(knockDirection.x), 0); 
        else  
            isKnockRight = new Vector2(0, Mathf.Sign(knockDirection.y)); 
        
        GetComponent<Life>().life -= Damage;
        CameraShakerHandler.Shake(shake);
    }
    
    //options
    public Transform GetNearest(string obj)
    { 
        GameObject[] objs = GameObject.FindGameObjectsWithTag(obj); 

        Transform nearestObj = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject choiseObj in objs)
        {
            float distance = Vector2.Distance(transform.position, choiseObj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObj = choiseObj.transform;
            }
        } 
        return nearestObj;
    }

    private Transform AimAtNearestEnemy(string obj)
    {
        Transform nearestEnemy = GetNearest(obj);
        if (nearestEnemy == null) return null;

        // Direção normalizada para o inimigo
        _playerDirection = (nearestEnemy.position - transform.position).normalized;

        // Atualiza a animação no Blend Tree
        SetDirectionPlayer();

        return nearestEnemy;
    }

    private bool arrow = false; 
    private string TargetArrow = ""; 
    public Vector2 ArrowDirection(){ return new Vector2(_playerAnimator.GetFloat("AxisX"), _playerAnimator.GetFloat("AxisY")); }
     
    private IEnumerator onFire()        //Shooter 
    {  
        float duration = GetAnimationClipLength("Bow");
        while(true){ 
            if(arrow){ 
                if(_inventoryManager.GetItemBool("Arrow")){
                    if(TargetArrow=="") {
                        GameObject obj = Instantiate(Arrow, ShooterLocation);
                        obj.transform.SetParent(this.transform);
                        obj.GetComponent<Arrow>().Run(ArrowDirection()); 
                        CheckAnimation("Bow"); 
                    }else{
                        Transform t = GetNearest(TargetArrow);       //AimAtNearestEnemy
                        if(t != null){ 
                            CheckAnimation("Bow");  
                            GameObject obj = Instantiate(Arrow, ShooterLocation);
                            obj.transform.SetParent(this.transform);
                            obj.GetComponent<Arrow>().Run(t); 
                        }
                    }
                }   
                // ChangeAnimation("");
                arrow = false;
            }
            if(currentAnimation == "Bow") yield return new WaitForSeconds(duration);     //  yield return new WaitForSeconds(1.5f);  
            else yield return null; // Aguarda o próximo frame
        }
    }

    float GetAnimationClipLength(string animationName)
    {
        foreach (AnimationClip clip in _playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                // Debug.Log("econcotriu" + clip.length);
                return clip.length; // Retorna a duração em segundos
            }
        }
        return 0f; // Retorna 0 se não encontrar a animação
    }  


    void OnDrawGizmos() {
        Tilemap tilemap = interactMap.tilemapInteractable;
        if (tilemap == null) return;
        
        // Obtém a posição da célula e seu centro
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);

        // Desenha um pequeno ponto no centro da célula
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(cellCenter, 0.1f);
    }
 
}
















// IEnumerator MoveToTile(Vector2 target) {
//         isMoving = true;

//         while ((Vector2)_playerRigidbody.position != target) {
//             _playerRigidbody.position = Vector2.MoveTowards(_playerRigidbody.position, target, tileSize * Time.fixedDeltaTime * 5f);
//             yield return null;
//         }

//         _playerRigidbody.position = target;
//         isMoving = false;
//     }


// IEnumerator MoveToCell(Vector3Int direction)
// {
//     isMoving = true;
    
//     Vector3Int currentCell = tilemap.WorldToCell(transform.position);
//     Vector3Int targetCell = currentCell + direction;
//     Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

//     float elapsedTime = 0f;
//     Vector3 startPosition = transform.position;
    
//     while (elapsedTime < 1f / moveSpeed)
//     {
//         transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed);
//         elapsedTime += Time.deltaTime;
//         yield return null;
//     }

//     transform.position = targetPosition;
//     isMoving = false;
// }


























    // private void keyMoveWalk(string animation){
    //     ChangeAnimation(animation);
    //     _playerDirection = _playerDirection.normalized; 
    //     // _playerRigidbody.velocity = _playerDirection.normalized * speed;
    //     transform.position += new Vector3(_playerDirection.x * speed * Time.deltaTime, _playerDirection.y * speed * Time.deltaTime, 0);
    // }

// private void CheckAnimation(string data){
//         if(KBCount >= 0)
//             KnockLogic();

//         //verify if disponible
//         if(currentAnimation == "action" || wait)
//             return;

//         if(data == "") {
//             // Application.ExternalEval("console.log('Chamado do Unity2!')");
//             ChangeAnimation("Idle"); 
//             _playerRigidbody.velocity = Vector2.zero;
//             return;
//         }
//         string[] values = data.Split(':');
//         string key = values[0];
//         string action = values[1];

//         if(key == "mov"){
//             switch (action)
//             {
//                 case "up":    _playerDirection.y +=  1;         break;
//                 case "down":  _playerDirection.y += -1;       break;
//                 case "left":  _playerDirection.x += -1;       break;
//                 case "right": _playerDirection.x +=  1;     break;
//                 // case "run":   _playerDirection = new Vector2(-1,-1); break;
//             }

//             // switch (action)
//             // {
//             //     case "up":    _playerDirection = Vector2.up;         break;
//             //     case "down":  _playerDirection = Vector2.down;       break;
//             //     case "left":  _playerDirection = Vector2.left;       break;
//             //     case "right": _playerDirection = Vector2.right;      break;
//             //     // case "run":   _playerDirection = new Vector2(-1,-1); break;
//             // }

//             keyMoveWalk("Walk");
//         }else if(key == "action")
//         {
//             wait = true;
//             switch (action)
//             {
//                 case "axe": ChangeAnimation("Axe");             break;
//             }
//         }
//         else{
//             ChangeAnimation("Idle");
//         }
//         SetDirectionPlayer();
//     }

    // int contTrues = 0;
    // public void setKeyBidding(string data){
    //     if (!data.Contains(":")) {
    //         keyMappings["true" + contTrues] = data;
    //         contTrues++;
    //     }else{
    //         string[] values = data.Split(':');
    //         string key = values[0];
    //         string action = values[1];
            
    //         keyMappings[key] = action; // Usa '=' para atualizar caso a tecla já exista
    //     }
    // }



        
        // setKeyBidding("a:left");
        // setKeyBidding("w:up");
        // setKeyBidding("d:right");
        // setKeyBidding("s:down");


    // void WalkMap()
    // {
    //     _playerDirection = Vector2.zero;

    //     foreach (var mapping in keyMappings)
    //     {
    //         if (mapping.Key.StartsWith("true") || Input.GetKey(mapping.Key)) // Verifica se a tecla está pressionada
    //         {
    //             switch (mapping.Value)
    //             {
    //                 case "left":
    //                     _playerDirection.x = -1;
    //                     break;
    //                 case "right":
    //                     _playerDirection.x = 1;
    //                     break;
    //                 case "up":
    //                     _playerDirection.y = 1;
    //                     break;
    //                 case "down":
    //                     _playerDirection.y = -1;
    //                     break;
    //                 default:
    //                     CheckAnimation("");
    //                     // Executa a ação desejada, como animação ou ataque
    //                     break;
    //             }
    //         }
    //     }
    // }











//start
        
         // Define direções padrão (pode ser reconfigurado pelo Blockly)
        // keyMappings["w"] = Vector2.up;
        // keyMappings["s"] = Vector2.down;
        // keyMappings["a"] = Vector2.left;
        // keyMappings["d"] = Vector2.right;

        // keyMappings["left shift"] = new Vector2(-1,-1);

        // keyMappingsActions["leftshift"] = 1;


//update
        // if(Input.GetKey(KeyCode.D))
        //     MoveRight();
        // if(Input.GetKey(KeyCode.A))
        //     MoveLeft();
        // if(Input.GetKey(KeyCode.W))
        //     MoveUp();
        // if(Input.GetKey(KeyCode.S))
        //     MoveDown();

        // _playerDirection = Vector2.zero;
        // //Analyze the change keys
        // foreach (var key in keyMappings.Keys)
        // {
        //     //For Run
        //     if(keyMappings[key] == new Vector2(-1,-1)){     //Vector2(-1,-1) for Run
        //         if (Input.GetKeyDown(key))  {
        //             _playerSpeed = _speedInitial * _playerMultSpeed;
        //         }
        //         if (Input.GetKeyUp(key))  {
        //             _playerSpeed = _speedInitial;
        //         }
        //         continue;
        //     }

        //     //For Walk
        //     if (Input.GetKey(key))   
        //     {
        //         _playerDirection.x += keyMappings[key].x;
        //         _playerDirection.y += keyMappings[key].y;
        //         // transform.position += new Vector3(keyMappings[key].x * speed * Time.deltaTime, keyMappings[key].y * speed * Time.deltaTime, 0);;
        //     }
        // }




    // public void SetKeyBinding(string data)
    // {
    //     string[] values = data.Split(':');
    //     string direction = values[0];
    //     string key = values[1];

    //     Vector3 newDirection = Vector3.zero;

    //     switch (direction)
    //     {
    //         case "up":    newDirection = Vector3.forward;  break;
    //         case "down":  newDirection = Vector3.back;     break;
    //         case "left":  newDirection = Vector3.left;     break;
    //         case "right": newDirection = Vector3.right;    break;

    //         case "run": newDirection = new Vector2(-1,-1); break;
    //     }

    //     keyMappings[key] = newDirection;
    //     Debug.Log($"Tecla {key} configurada para mover {direction}");
    // }