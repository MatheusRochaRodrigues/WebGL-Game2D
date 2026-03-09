// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Dynamic;
// using UnityEngine;
// using UnityEngine.Tilemaps;
// using static Item;
// using FirstGearGames.SmoothCameraShaker;
// using UnityEditor;
// using System.Linq;

// public class PlayerController : MonoBehaviour, ILife
// {
//     public Rigidbody2D _playerRigidbody;
//     public Animator _playerAnimator;
//     public string currentAnimation = "";

//     [Header("Moviment")]
//     public float _speedInitial;
//     public float _playerSpeed      = 2.0f;  //walk  
//     public float _playerMultSpeed  = 2.0f;  //run
//     public float _playerInitialSpeed;

//     public float speed = 5f;
//     Vector2 _playerDirection = Vector2.zero; 
//     private Stack<string> keyMappings = new Stack<string>();

//     [Space()]
//     [Header("KnockBack")]
//     //Damage KnockBack
//     public float KBForce;
//     public float KBCount;
//     public float KBTime;
//     public Vector2 isKnockRight;

//     [Header("Arrow")]
//     public GameObject Arrow;
//     public Transform ShooterLocation;

//     private List<string> waitActions = new List<string>(){
//         "Sword1",
//         "Sword2",
//         "Sword3",
//         "Bow",
//         "AutoShoot"

//     };

//     //Attack
//     public int currentAtackNb;
//     public AttackArea attackArea;
//     private bool attacking = false;
//     private float timeToAttack = 0.25f;
//     private float timer = 0f;

//     [Header("Shake")]
//     public ShakeData shake;

//     //Inventory
//     [Header("Only IventoryManager")]
//     public InventoryManager _inventoryManager;
//     //objeto que esta na mao do player
//     public static string _itemInHandle;
//     // public static Item _itemInHandle; 
//     public Item it2;
//     private bool stateInventory = false;

//     //Tile States 
//     public float tileSize = 1f; // Tamanho de um tile no Grid
//     public InteractMap interactMap;

//     //Dialogue System
//     [Space()]
//     [Header("Dialogue System")]
//     DialogueSystem dialogueSystem;
//     public Transform guide;

     
//     void Start()
//     {
//         _playerRigidbody = GetComponent<Rigidbody2D>();
//         _playerAnimator = GetComponent<Animator>();
//         dialogueSystem = FindObjectOfType<DialogueSystem>();

//         _playerDirection = Vector2.down;
//         SetDirectionPlayer();
//         _playerDirection = Vector2.zero;

//         CheckAnimation("");

//         // Chamando um script JavaScript no navegador
//         Application.ExternalEval("console.log('Chamado do Unity!')");

//         StartCoroutine(onFire());  
//         SnapToGrid();
//     } 

//     public void Setup(float walk, float run)
//     {
//         _playerSpeed = walk;
//         _playerInitialSpeed = walk;
//         _playerMultSpeed = run;
//     }

//     public void Update() { 
//         teste();            // if (Input.GetKeyDown(KeyCode.G))  _inventoryManager.AddItem(it2);  
 
//         useGuide();
//         Run();
//         keyMappings.Clear(); 
//         //Attacks
//         if(attacking) {
//             timer += Time.deltaTime;
//             if(timer >= timeToAttack) {
//                 timer = 0;
//                 attacking = false;
//                 attackArea.gameObject.SetActive(attacking);
//             }
//         }
//     } 

     

//     void OnDrawGizmos() {
//         Tilemap tilemap = interactMap.tilemapInteractable;
//         if (tilemap == null) return;
        
//         // Obtém a posição da célula e seu centro
//         Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
//         Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);

//         // Desenha um pequeno ponto no centro da célula
//         Gizmos.color = Color.red;
//         Gizmos.DrawSphere(cellCenter, 0.1f);
//     }

//     void SnapToGrid()
//     {
//         Tilemap tilemap = interactMap.tilemapInteractable;
//         Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
//         Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
//         transform.position = cellCenter;
//     }
 
//     public void setTile(string item){                                       // public void setTile(Item itemTile){ 
//         Item itemTile = _inventoryManager.getItem(item);
//         if(itemTile == null) return; 
        
//         if(itemTile.type != ItemType.Building ){ 
//             _inventoryManager.AddItem(itemTile);
//             return;
//         }

//         Tilemap tilemap = interactMap.tilemapInteractable;
//         //Mouse 
//             // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
//         //PlayerInput 
//         Vector2 playerDirection = ArrowDirection();
//         // Vector3  direction = new Vector3 (  playerDirection.x, playerDirection.y, 0  );   
//         // direction = this.transform.position + direction;
//         // Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(direction);     // +  new Vector3(0.5f, 0.5f, 0)
//         Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(this.transform.position);     // +  new Vector3(0.5f, 0.5f, 0)
//         Vector3Int direction = new Vector3Int(
//             Mathf.RoundToInt(playerDirection.x), // Converte a direção X para inteiro
//             Mathf.RoundToInt(playerDirection.y), // Converte a direção Y para inteiro
//             0 // Mantemos Z como 0 para jogos 2D
//         );
//         cellPosition += direction; 

//         if (!interactMap.availableTiles.ContainsKey(cellPosition)){
//             _inventoryManager.AddItem(itemTile);
//             return;
//         }
        
//         Tile newTile = ScriptableObject.CreateInstance<Tile>();
//         newTile.sprite = itemTile.img;

//         tilemap.SetTile(cellPosition, newTile);
//         // tilemap.SetColliderType(cellPosition, Tile.ColliderType.Sprite);        //newTile.colliderType = None;
//         interactMap.availableTiles.Remove(cellPosition);
//     }

//     public void collectItem(Item item){ _inventoryManager.AddItem(item); }

//     private void useGuide(){ 
//         //Inventory 
//         if (Input.GetKeyDown(KeyCode.LeftShift)) {
//             _inventoryManager._inventoryMenu.SetActive(!stateInventory);
//             stateInventory = !stateInventory; 
//         }  
//         //DialogueSystem
//         if (Mathf.Abs(transform.position.x - guide.position.x) < 2.0f)
//         {
//             if (Input.GetKeyDown(KeyCode.Space))  dialogueSystem.Next(); 
//         }
//     }

//     public void teste(){
//         if(Input.GetKey("a")) setKeyBidding("left") ;   //addAction
//         if(Input.GetKey("w")) setKeyBidding("up")   ;
//         if(Input.GetKey("s")) setKeyBidding("down") ;   
//         if(Input.GetKey("d")) setKeyBidding("right");
//         if(Input.GetKey("f")) setKeyBidding("Sword");

//         if(Input.GetKey("b")) setKeyBidding("AutoShoot:Skeleton");
//         if(Input.GetKey("g")) setKeyBidding("Bow");
//         // if(Input.GetKey("q")) setTile(it2);
//         if(Input.GetKeyDown("q")) setTile("Chest");
//         // if(Input.GetKey("p")) setKeyBidding("inventory");
//     }

//     public void setKeyBidding(string data){
//         keyMappings.Push(data);
//     }

//     public void Attack(){
//         attacking = true; 
//         attackArea.gameObject.SetActive(attacking);
        
//         //new  
//         attackArea.col.enabled = false;
//         attackArea.col.enabled = true;

//     }

//     public void isDamage(int Damage, Transform collision,  Item item = null){ 
//         // if(isMoving || KBCount > 0)return;
//         if(KBCount > 0)return;
//         //set animation damage
//         KBCount = KBTime; 

//         Vector2 knockDirection = (collision.transform.position - this.transform.position).normalized; 
//         if (Mathf.Abs(knockDirection.x) > Mathf.Abs(knockDirection.y)) 
//             isKnockRight = new Vector2(Mathf.Sign(knockDirection.x), 0); 
//         else  
//             isKnockRight = new Vector2(0, Mathf.Sign(knockDirection.y)); 
        
//         GetComponent<Life>().life -= Damage;
//         CameraShakerHandler.Shake(shake);
//     }
 
//     private bool typeMovTile = false;
//     void Run(){
//         if (isMoving) return; // Evita iniciar outro movimento antes de terminar o atual 

//         _playerDirection = Vector2.zero;

//         //  KnockBack
//         if(KBCount > 0){ 
//             CheckAnimation("Damage");
//             return;  
//         }else if (_playerRigidbody.velocity.magnitude < 0.1f)         //      _playerRigidbody.velocity.SqrMagnitude() > 0
//             _playerRigidbody.velocity = Vector2.zero; // Para completamente
        
//         //verify if disponible
//         if(waitActions.Contains(currentAnimation))
//             return;

//         //  Mapping actions
//         foreach (var mapping in keyMappings) {
//             switch (mapping)
//             {
//                 //look at------------------------------------------------------------
//                 case "leftLookAt":
//                     _playerDirection.x = -1; 
//                     SetDirectionPlayer();
//                     _playerDirection = Vector2.zero;
//                     break;
//                 case "rightLookAt":
//                     _playerDirection.x = 1; 
//                     SetDirectionPlayer();
//                     _playerDirection = Vector2.zero;
//                     break;
//                 case "upLookAt":
//                     _playerDirection.y = 1; 
//                     SetDirectionPlayer();
//                     _playerDirection = Vector2.zero;
//                     break;
//                 case "downLookAt":
//                     _playerDirection.y = -1; 
//                     SetDirectionPlayer();
//                     _playerDirection = Vector2.zero;
//                     break;
//                 case "randomLookAt":
//                     randomDirection(); 
//                     SetDirectionPlayer();
//                     _playerDirection = Vector2.zero;
//                     break;

//                 //---------------------------------------------------------------Moviment
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

//                 //MovimentPerTile---------------------------------------------------------------------
//                 case "leftTile":
//                     _playerDirection = Vector2.left; 
//                     SnapToGrid();
//                     break;
//                 case "rightTile":
//                     _playerDirection = Vector2.right; 
//                     SnapToGrid();
//                     break;
//                 case "upTile":
//                     _playerDirection = Vector2.up; 
//                     SnapToGrid();
//                     break;
//                 case "downTile":
//                     _playerDirection = Vector2.down; 
//                     SnapToGrid();
//                     break;
//                 case "randomTile":
//                     randomDirection(); 
//                     SnapToGrid();
//                     break;
 
//                 //Actions----------------------------------------------------------------------------
//                 case "Sword":
//                     CheckAnimation("Sword" + currentAtackNb++);
//                     // _itemInHandle = mapping;
//                     if(currentAtackNb > 3) currentAtackNb = 1;
//                         Attack();
//                     break;

//                 case "Death":
//                     CheckAnimation("Death");
//                     _playerRigidbody.isKinematic = true;    //isso desativa a física do Rigidbody2D
//                     break;

//                 case "Bow":
//                     // _itemInHandle = mapping;
//                     arrow = true;
//                     break;

//                 // Manage care --------------------------------------------------------------------
//                 default:
//                     if(mapping.StartsWith("AutoShoot")){
//                         string[] values = mapping.Split(':');
//                         string target = values[1];
//                         TargetArrow = target;
//                         AimAtNearestEnemy(TargetArrow); 
//                         // currentAnimation = values[0];
//                         _playerDirection = Vector2.zero;
//                     }
//                     //Inventory
//                     // if(mapping.StartsWith("inventory")){ 
//                     //     if (Input.anyKeyDown) {
//                     //         _inventoryManager._inventoryMenu.SetActive(!stateInventory);
//                     //         stateInventory = !stateInventory; 
//                     //     } 
//                     // }
//                     if(mapping == ""){

//                     }
//                     break;
//             }
//             if(mapping.EndsWith("Tile")) typeMovTile = true; else typeMovTile = false;
//         }

//         if (_playerDirection != Vector2.zero) {
//             CheckAnimation("Walk");
//             SetDirectionPlayer();
//         }
        
//         if(keyMappings.Count == 0) CheckAnimation("");
//     }  

//     private void randomDirection(){
//         int rand = UnityEngine.Random.Range(0,4);
//         switch(rand){
//             case 0:  _playerDirection = Vector2.left;  break;
//             case 1:  _playerDirection = Vector2.right;   break;
//             case 2:  _playerDirection = Vector2.up;  break;
//             default: _playerDirection = Vector2.down;  break;  
//         } 
//     }
    
//     private void CheckAnimation(string data){
//         //verify if disponible
//         if(waitActions.Contains(currentAnimation))
//             return;

//         if(data == "") {
//             ChangeAnimation("Idle");    // Application.ExternalEval("console.log('Chamado do Unity2!')");
//             return;
//         }
//         ChangeAnimation(data);
//     }

//     public void ChangeAnimation(string animation, float crossfade = 0.2f, float time = 0){
//         if(time > 0) StartCoroutine(Wait());
//         else Validate();

//         IEnumerator Wait(){
//             yield return new WaitForSeconds(time - crossfade);
//             Validate();
            
//         }

//         void Validate(){
//             if(currentAnimation != animation)
//             {
//                 currentAnimation = animation;

//                 if(currentAnimation == "")
//                     CheckAnimation("");            //checkAnimation()
//                 else
//                     _playerAnimator.CrossFade(animation, crossfade);
//             }
            
//         }
//     } 

//     private void SetDirectionPlayer(){
//         _playerAnimator.SetFloat("AxisX", _playerDirection.x);
//         _playerAnimator.SetFloat("AxisY", _playerDirection.y);
//     } 

//     //options
//     public Transform GetNearest(string obj)
//     {
//         // Debug.Log("D + " + obj);
//         GameObject[] objs = GameObject.FindGameObjectsWithTag(obj); 

//         Transform nearestObj = null;
//         float minDistance = Mathf.Infinity;

//         foreach (GameObject choiseObj in objs)
//         {
//             float distance = Vector2.Distance(transform.position, choiseObj.transform.position);
//             if (distance < minDistance)
//             {
//                 minDistance = distance;
//                 nearestObj = choiseObj.transform;
//             }
//         } 
//         return nearestObj;
//     }

//     private Transform AimAtNearestEnemy(string obj)
//     {
//         Transform nearestEnemy = GetNearest(obj);
//         if (nearestEnemy == null) return null;

//         // Direção normalizada para o inimigo
//         _playerDirection = (nearestEnemy.position - transform.position).normalized;

//         // Atualiza a animação no Blend Tree
//         SetDirectionPlayer();

//         return nearestEnemy;
//     }

//     private bool arrow = false; 
//     private string TargetArrow = ""; 
//     private Vector2 ArrowDirection(){ return new Vector2(_playerAnimator.GetFloat("AxisX"), _playerAnimator.GetFloat("AxisY")); }
     
//     private IEnumerator onFire()        //Shooter 
//     {  
//         float duration = GetAnimationClipLength("Bow");
//         while(true){ 
//             if(arrow){ 
//                 if(_inventoryManager.GetItemBool("Arrow")){
//                     if(TargetArrow=="") {
//                         GameObject obj = Instantiate(Arrow, ShooterLocation);
//                         obj.GetComponent<Arrow>().Run(ArrowDirection());
//                         Damage dam = obj.GetComponent<Damage>();
//                         dam.objNotHit = this.gameObject;
//                         dam.enabled = true;
//                         dam.damage = 3;
//                         CheckAnimation("Bow"); 
//                     }else{
//                         Transform t = GetNearest(TargetArrow);       //AimAtNearestEnemy
//                         if(t != null){ 
//                             CheckAnimation("Bow");  
//                             GameObject obj = Instantiate(Arrow, ShooterLocation);
//                             obj.GetComponent<Arrow>().Run(t);
//                             Damage dam = obj.GetComponent<Damage>();
//                             dam.objNotHit = this.gameObject;
//                             dam.enabled = true;
//                             dam.damage = 3;
//                         }
//                     }
//                 }   
//                 // ChangeAnimation("");
//                 arrow = false;
//             }
//             if(currentAnimation == "Bow") yield return new WaitForSeconds(duration);     //  yield return new WaitForSeconds(1.5f);  
//             else yield return null; // Aguarda o próximo frame
//         }
//     }

//     float GetAnimationClipLength(string animationName)
//     {
//         foreach (AnimationClip clip in _playerAnimator.runtimeAnimatorController.animationClips)
//         {
//             if (clip.name == animationName)
//             {
//                 // Debug.Log("econcotriu" + clip.length);
//                 return clip.length; // Retorna a duração em segundos
//             }
//         }
//         return 0f; // Retorna 0 se não encontrar a animação
//     }  

//     private Vector2 targetPosition;
//     void FixedUpdate() {
//         if (isMoving) return;

//         if (KBCount > 0) {   // Somente move se não estiver sofrendo knockback
//             KnockLogic();
//             return;
//         }
//         if(waitActions.Contains(currentAnimation)) return;
//         if (_playerDirection.sqrMagnitude > 0.1f){
//             if (!typeMovTile)
//                 _playerRigidbody.MovePosition(_playerRigidbody.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);
//             else
//                 StartCoroutine(MoveToCell()); 
//         }else {
//             _playerRigidbody.velocity = Vector2.zero; // Para quando não há input
//          }
//     } 
    
//     private bool isMoving = false;
//     public float moveSpeed = 5f;
//     IEnumerator MoveToCell()
//     {
//         Tilemap tilemap = interactMap.tilemapInteractable;
//         Vector3Int direction = new Vector3Int(
//             Mathf.RoundToInt(_playerDirection.x), // Converte a direção X para inteiro
//             Mathf.RoundToInt(_playerDirection.y), // Converte a direção Y para inteiro
//             0 // Mantemos Z como 0 para jogos 2D
//         );

//         isMoving = true;
        
//         Vector3Int currentCell = tilemap.WorldToCell(transform.position);
//         Vector3Int targetCell = currentCell + direction;
//         Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

//         float elapsedTime = 0f;
//         Vector3 startPosition = transform.position;

//         while (elapsedTime < 1f / moveSpeed)
//         {
//             _playerRigidbody.MovePosition(Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed));
//             elapsedTime += Time.fixedDeltaTime; 
//             yield return new WaitForFixedUpdate(); // Usa FixedUpdate para sincronizar com a física
//         }

//         _playerRigidbody.MovePosition(targetPosition); // Garante que ele termine exatamente no centro do tile
//         isMoving = false;
//     }
    

//     void KnockLogic(){
//         _playerRigidbody.velocity = new Vector2(KBForce, KBForce) * isKnockRight; 
//         KBCount -= Time.fixedDeltaTime; 
//     }   
 
// }
















// // IEnumerator MoveToTile(Vector2 target) {
// //         isMoving = true;

// //         while ((Vector2)_playerRigidbody.position != target) {
// //             _playerRigidbody.position = Vector2.MoveTowards(_playerRigidbody.position, target, tileSize * Time.fixedDeltaTime * 5f);
// //             yield return null;
// //         }

// //         _playerRigidbody.position = target;
// //         isMoving = false;
// //     }


// // IEnumerator MoveToCell(Vector3Int direction)
// // {
// //     isMoving = true;
    
// //     Vector3Int currentCell = tilemap.WorldToCell(transform.position);
// //     Vector3Int targetCell = currentCell + direction;
// //     Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

// //     float elapsedTime = 0f;
// //     Vector3 startPosition = transform.position;
    
// //     while (elapsedTime < 1f / moveSpeed)
// //     {
// //         transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed);
// //         elapsedTime += Time.deltaTime;
// //         yield return null;
// //     }

// //     transform.position = targetPosition;
// //     isMoving = false;
// // }


























//     // private void keyMoveWalk(string animation){
//     //     ChangeAnimation(animation);
//     //     _playerDirection = _playerDirection.normalized; 
//     //     // _playerRigidbody.velocity = _playerDirection.normalized * speed;
//     //     transform.position += new Vector3(_playerDirection.x * speed * Time.deltaTime, _playerDirection.y * speed * Time.deltaTime, 0);
//     // }

// // private void CheckAnimation(string data){
// //         if(KBCount >= 0)
// //             KnockLogic();

// //         //verify if disponible
// //         if(currentAnimation == "action" || wait)
// //             return;

// //         if(data == "") {
// //             // Application.ExternalEval("console.log('Chamado do Unity2!')");
// //             ChangeAnimation("Idle"); 
// //             _playerRigidbody.velocity = Vector2.zero;
// //             return;
// //         }
// //         string[] values = data.Split(':');
// //         string key = values[0];
// //         string action = values[1];

// //         if(key == "mov"){
// //             switch (action)
// //             {
// //                 case "up":    _playerDirection.y +=  1;         break;
// //                 case "down":  _playerDirection.y += -1;       break;
// //                 case "left":  _playerDirection.x += -1;       break;
// //                 case "right": _playerDirection.x +=  1;     break;
// //                 // case "run":   _playerDirection = new Vector2(-1,-1); break;
// //             }

// //             // switch (action)
// //             // {
// //             //     case "up":    _playerDirection = Vector2.up;         break;
// //             //     case "down":  _playerDirection = Vector2.down;       break;
// //             //     case "left":  _playerDirection = Vector2.left;       break;
// //             //     case "right": _playerDirection = Vector2.right;      break;
// //             //     // case "run":   _playerDirection = new Vector2(-1,-1); break;
// //             // }

// //             keyMoveWalk("Walk");
// //         }else if(key == "action")
// //         {
// //             wait = true;
// //             switch (action)
// //             {
// //                 case "axe": ChangeAnimation("Axe");             break;
// //             }
// //         }
// //         else{
// //             ChangeAnimation("Idle");
// //         }
// //         SetDirectionPlayer();
// //     }

//     // int contTrues = 0;
//     // public void setKeyBidding(string data){
//     //     if (!data.Contains(":")) {
//     //         keyMappings["true" + contTrues] = data;
//     //         contTrues++;
//     //     }else{
//     //         string[] values = data.Split(':');
//     //         string key = values[0];
//     //         string action = values[1];
            
//     //         keyMappings[key] = action; // Usa '=' para atualizar caso a tecla já exista
//     //     }
//     // }



        
//         // setKeyBidding("a:left");
//         // setKeyBidding("w:up");
//         // setKeyBidding("d:right");
//         // setKeyBidding("s:down");


//     // void WalkMap()
//     // {
//     //     _playerDirection = Vector2.zero;

//     //     foreach (var mapping in keyMappings)
//     //     {
//     //         if (mapping.Key.StartsWith("true") || Input.GetKey(mapping.Key)) // Verifica se a tecla está pressionada
//     //         {
//     //             switch (mapping.Value)
//     //             {
//     //                 case "left":
//     //                     _playerDirection.x = -1;
//     //                     break;
//     //                 case "right":
//     //                     _playerDirection.x = 1;
//     //                     break;
//     //                 case "up":
//     //                     _playerDirection.y = 1;
//     //                     break;
//     //                 case "down":
//     //                     _playerDirection.y = -1;
//     //                     break;
//     //                 default:
//     //                     CheckAnimation("");
//     //                     // Executa a ação desejada, como animação ou ataque
//     //                     break;
//     //             }
//     //         }
//     //     }
//     // }











// //start
        
//          // Define direções padrão (pode ser reconfigurado pelo Blockly)
//         // keyMappings["w"] = Vector2.up;
//         // keyMappings["s"] = Vector2.down;
//         // keyMappings["a"] = Vector2.left;
//         // keyMappings["d"] = Vector2.right;

//         // keyMappings["left shift"] = new Vector2(-1,-1);

//         // keyMappingsActions["leftshift"] = 1;


// //update
//         // if(Input.GetKey(KeyCode.D))
//         //     MoveRight();
//         // if(Input.GetKey(KeyCode.A))
//         //     MoveLeft();
//         // if(Input.GetKey(KeyCode.W))
//         //     MoveUp();
//         // if(Input.GetKey(KeyCode.S))
//         //     MoveDown();

//         // _playerDirection = Vector2.zero;
//         // //Analyze the change keys
//         // foreach (var key in keyMappings.Keys)
//         // {
//         //     //For Run
//         //     if(keyMappings[key] == new Vector2(-1,-1)){     //Vector2(-1,-1) for Run
//         //         if (Input.GetKeyDown(key))  {
//         //             _playerSpeed = _speedInitial * _playerMultSpeed;
//         //         }
//         //         if (Input.GetKeyUp(key))  {
//         //             _playerSpeed = _speedInitial;
//         //         }
//         //         continue;
//         //     }

//         //     //For Walk
//         //     if (Input.GetKey(key))   
//         //     {
//         //         _playerDirection.x += keyMappings[key].x;
//         //         _playerDirection.y += keyMappings[key].y;
//         //         // transform.position += new Vector3(keyMappings[key].x * speed * Time.deltaTime, keyMappings[key].y * speed * Time.deltaTime, 0);;
//         //     }
//         // }




//     // public void SetKeyBinding(string data)
//     // {
//     //     string[] values = data.Split(':');
//     //     string direction = values[0];
//     //     string key = values[1];

//     //     Vector3 newDirection = Vector3.zero;

//     //     switch (direction)
//     //     {
//     //         case "up":    newDirection = Vector3.forward;  break;
//     //         case "down":  newDirection = Vector3.back;     break;
//     //         case "left":  newDirection = Vector3.left;     break;
//     //         case "right": newDirection = Vector3.right;    break;

//     //         case "run": newDirection = new Vector2(-1,-1); break;
//     //     }

//     //     keyMappings[key] = newDirection;
//     //     Debug.Log($"Tecla {key} configurada para mover {direction}");
//     // }