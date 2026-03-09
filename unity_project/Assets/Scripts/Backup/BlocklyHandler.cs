// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;
// using static Item;

// public class BlocklyHandler : MonoBehaviour
// {
//     Rigidbody2D _playerRigidbody;
//     public Animator _playerAnimator;
//     public string currentAnimation = "";

//     public bool _attack = false;

//     //Inventory
//     [Header("Only IventoryManager")]
//     public InventoryManager _inventoryManager;
//     //objeto que esta na mao do player
//     Item _itemInHandle;
//     public Item it2;
//     private bool stateInventory = false;

//     //InteractMap
//     public InteractMap interactMap;

//     //referencia do objeto arrastavel
//     private TileItem itemDrag = null;
//     private Camera cam;

//     [Header("Only Prefabs")]
//     //prefab de item
//     public GameObject PrefabItemDrag;
//     public GameObject PrefabDrop;
//     [Header("Only drop")]
//     // public AppendDrops drops;


//     public Sprite[] spriteSlices;
//     public int sliceIndex = 2;// Arraste a textura no  Inspector
//     public Vector2 hotspot = Vector2.zero;  // Ponto de clique   no  cursor

//     DialogueSystem dialogueSystem;
//     public Transform guide;


//     public float _speedInitial = 2.0f;
//     public float _speedRun = 2.0f;
//     public float _playerSpeed;

//     Vector2 _playerDirection;

//     PlayerController _playerController;

//     bool mauseState = false;


//     void Start()
//     {
//         _playerRigidbody = GetComponent<Rigidbody2D>();
//         _playerAnimator = GetComponent<Animator>();

//         _playerController = GetComponent<PlayerController>();

//         dialogueSystem = FindObjectOfType<DialogueSystem>();

//         _playerSpeed = _speedInitial;

//         PrefabItemDrag = Instantiate(PrefabItemDrag, transform.root);
//         itemDrag = PrefabItemDrag.GetComponent<TileItem>();

//         cam = Camera.main;

//         hotspot = new Vector2(spriteSlices[sliceIndex].rect.width / 2, spriteSlices[sliceIndex].rect.height / 2);
//         if (spriteSlices.Length > 0 && sliceIndex < spriteSlices.Length)
//         {
//             Sprite selectedSprite = spriteSlices[sliceIndex];

//             // Converte o slice selecionado para uma textura
//             Texture2D cursorTexture = SpriteToTexture2D(selectedSprite);

//             // Configura o cursor com o slice convertido
//             Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
//         }
//         else
//         {
//             Debug.LogWarning("Sprite slice inválido ou índice fora do intervalo.");
//         }

//         //Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto); 
//         Cursor.lockState = CursorLockMode.Confined; // Restringe o cursor à janela do jogo
//                                                     //Cursor.visible = true; // Torna o cursor visível

//         ChangeAnimation("idle");
//     }


//     public void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.LeftShift))
//             _playerSpeed = _speedInitial * _speedRun;
//         if (Input.GetKeyUp(KeyCode.LeftShift))
//             _playerSpeed = _speedInitial;

//         //OnAttack();

//         //Inventory and Handle
//         openInventory();
//         useInventory();
//         useGuide();
//     }

//     void FixedUpdate()
//     {
//         _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

//         if (_playerDirection.sqrMagnitude > 0.1f)
//         {
//             _playerRigidbody.MovePosition(_playerRigidbody.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);

//             _playerAnimator.SetInteger("Move", 1);
//             _playerAnimator.SetFloat("AxisX", _playerDirection.x);
//             _playerAnimator.SetFloat("AxisY", _playerDirection.y);
//         }
//         else
//             _playerAnimator.SetInteger("Move", 0);

//         if (_attack)
//             _playerAnimator.SetInteger("Move", 2);

//     }



//     public void ChangeAnimation(string animation, float crossfade = 0.2f)
//     {
//         if (currentAnimation != animation)
//         {
//             currentAnimation = animation;
//             _playerAnimator.CrossFade(animation, crossfade);
//         }
//     }

//     public void ChangeCursor(int newSliceIndex)
//     {
//         if (newSliceIndex < spriteSlices.Length)
//         {
//             Sprite selectedSprite = spriteSlices[newSliceIndex];
//             Texture2D cursorTexture = SpriteToTexture2D(selectedSprite);
//             Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
//         }
//     }

//     private Texture2D SpriteToTexture2D(Sprite sprite)
//     {
//         if (sprite == null) return null;

//         Rect rect = sprite.rect;
//         Texture2D croppedTexture = new Texture2D((int)rect.width, (int)rect.height);
//         Color[] pixels = sprite.texture.GetPixels(
//             (int)rect.x,
//             (int)rect.y,
//             (int)rect.width,
//             (int)rect.height
//         );
//         croppedTexture.SetPixels(pixels);
//         croppedTexture.Apply();
//         return croppedTexture;
//     }



//     public void useGuide()
//     {
//         if (Mathf.Abs(transform.position.x - guide.position.x) < 2.0f)
//         {
//             if (Input.GetKeyDown(KeyCode.E))
//             {
//                 dialogueSystem.Next();
//             }
//         }
//     }

//     void useInventory()
//     {
//         //if (Input.GetKeyDown(KeyCode.Alpha3))
//         //{
//         //    _itemInHandle = _inventoryManager.SelectItem(3);
//         //}

//         if (Input.inputString != null)
//         {
//             bool isNumber = int.TryParse(Input.inputString, out int number);
//             if (isNumber && number >= 0 && number <= 4)
//             {
//                 _itemInHandle = _inventoryManager.SelectItem(number - 1);

//                 if (_itemInHandle != null)
//                 {
//                     Debug.Log(_itemInHandle.type);
//                     itemDrag.setScripItem(_itemInHandle);
//                 }
//             }
//         }

//         if (_itemInHandle == null)
//             return;

//         OnInteract();
//     }

//     private Vector3Int lastCellPos; // Última posição 
//     private TileBase lastTile; // Último tile 
//     void OnInteract()
//     {
//         if (_itemInHandle.type == ItemType.Building)
//         {
//             Tilemap tilemap = interactMap.tilemapInteractable;
//             Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
//             Vector3Int cellPosition = interactMap.tilemapInteractable.WorldToCell(mouseWorldPos);

//             if (!interactMap.availableTiles.ContainsKey(cellPosition))
//                 return;

//             Tile newTile = ScriptableObject.CreateInstance<Tile>();
//             newTile.sprite = _itemInHandle.img;

//             if (cellPosition != lastCellPos)
//             {
//                 tilemap.SetTile(lastCellPos, lastTile);

//                 lastTile = tilemap.GetTile(cellPosition);


//                 tilemap.SetTile(cellPosition, newTile);
//                 tilemap.SetColliderType(cellPosition, Tile.ColliderType.None);

//                 lastCellPos = cellPosition;
//             }

//             if (Input.GetMouseButtonDown(0))
//             {
//                 lastTile = newTile;

//                 tilemap.SetTile(cellPosition, newTile);
//                 tilemap.SetColliderType(cellPosition, Tile.ColliderType.Sprite);        //newTile.colliderType = None;
//                 interactMap.availableTiles.Remove(cellPosition);
//             }
//             //PrefabItemDrag.transform.position = Input.mousePosition;
//         }

//         if (_itemInHandle.type == ItemType.Tool)
//         {
//             if (Input.GetMouseButtonDown(0))
//             {
//                 Tilemap tilemap = interactMap.trigger;

//                 Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
//                 Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

//                 TileBase tile = tilemap.GetTile(cellPosition);

//                 if (tile == null)
//                     return;

//                 //-------------MINER----------------

//                 //----Rock
//                 for (int i = 15; i <= 16; i++)
//                 {
//                     string name = "Decor_" + i;
//                     if (tile.name == name)
//                         tilemap.SetTile(cellPosition, null);
//                 }

//                 for (int i = 21; i <= 24; i++)
//                 {
//                     string name = "Decor_" + i;
//                     if (tile.name == name)
//                         tilemap.SetTile(cellPosition, null);
//                 }

//                 //Debug.Log("Passei aq");
//                 Vector2 _playerDirection = this.transform.position - Input.mousePosition;
//                 //Vector3 Direction = cam.ScreenToWorldPoint(Input.mousePosition);

//                 //_playerAnimator.SetInteger  ("Move", 3);
//                 _playerAnimator.SetFloat("AxisX", _playerDirection.x);
//                 _playerAnimator.SetFloat("AxisY", _playerDirection.y);

//                 //GameObject collectible = Instantiate(collectiblePrefab, position, Quaternion.identity);
//                 GameObject g = Instantiate(PrefabDrop, transform.parent);
//                 g.transform.position = cellPosition;

//                 // g.GetComponent<ItemMagnetic>().myCoroutine =
//                 // StartCoroutine(ItemMagnetic.JumpCollectible(drops.drops[0], g));     // Quaternion.identity

//                 //---Gold



//                 //------------AXE-------------------
//             }
//         }


//     }

//     //explosao
//     public IEnumerator animationDrop(Sprite drop, GameObject objDrop)
//     {
//         Destroy(objDrop, 30f);

//         float jumpForce = 2.0f;

//         Rigidbody2D rb = objDrop.GetComponent<Rigidbody2D>();
//         objDrop.GetComponent<SpriteRenderer>().sprite = drop;

//         if (rb != null)
//         {
//             Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
//             rb.AddForce(randomDirection * jumpForce, ForceMode2D.Impulse);
//             //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

//         }
//         else
//             Debug.Log("rigd vazio");

//         new WaitForSeconds(1f);
//         rb.gravityScale = 0;

//         yield return null;
//         // Opcional: Destroi o sprite após um tempo
//     }




//     void OnAttack()
//     {
//         if ((Input.GetKeyDown(KeyCode.Space)) && !_attack)
//         {
//             _attack = true;
//             _playerSpeed = 0;

//         }

//         if (Input.GetKeyUp(KeyCode.Space))
//         {
//             _attack = false;
//             _playerSpeed = _speedInitial;
//         }
//     }

//     void openInventory()
//     {
//         if (Input.GetKeyDown(KeyCode.G))
//         {
//             _inventoryManager.AddItem(it2);
//         }

//         if (Input.GetKeyDown(KeyCode.F))
//         {
//             _inventoryManager._inventoryMenu.SetActive(!stateInventory);
//             stateInventory = !stateInventory;
//         }

//     }


//     //void flip()
//     //{
//     //    if (_playerDirection.x > 0)
//     //        transform.eulerAngles = new Vector2(0, 0);
//     //    else if (_playerDirection.x < 0)
//     //        transform.eulerAngles = new Vector2(0, 180.0f);
//     //}
// }
