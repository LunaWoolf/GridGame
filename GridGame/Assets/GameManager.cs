using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    GameObject BoardManager;
    BoardManager BoardManager_Script;

    void Start()
    {
        BoardManager = GameObject.Find("BoardManager");
        BoardManager_Script = BoardManager.GetComponent<BoardManager>();
        BoardManager_Script.Gems_List[2,3] = 7;
        BoardManager.SendMessage("PlacePlayer");
    }


    void Update()
    {

        
    }
}
