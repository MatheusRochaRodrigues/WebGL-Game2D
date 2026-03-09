using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("MOVE")]
    public float _playerWalk = 2.0f;
    public float _playerRun = 2.0f;
    private PlayerController _playerController;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.Setup(_playerWalk, _playerRun);
        //ChangeAnimation("idle");
    }

    //public void Update()
    //{
        //_playerController.Move();
    //}

    //bool mauseState = false;
    //public void Mause()
    //{
    //    //Mouse controller
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //        mauseState = !mauseState;
    //    if (mauseState)
    //        Cursor.lockState = CursorLockMode.None; // Restringe o cursor à janela do jogo
    //    else
    //        Cursor.lockState = CursorLockMode.Confined; // Restringe o cursor à janela do jogo

    //}
}
