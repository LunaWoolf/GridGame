using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    GameObject BoardManager;
    BoardManager BoardManager_Script;
    public int Player_row = 2;
    public int Player_col = 3;
    int[] Player_Pos = new int[4];
    //int[] Swap_Pos = new int[4];

    void Start()
    {
        BoardManager = GameObject.Find("BoardManager");
        BoardManager_Script = BoardManager.GetComponent<BoardManager>();
       
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Player_Pos[0] = Player_row;
            Player_Pos[1] = Player_col;
            //Debug.Log("A");
            Player_row--;
            MovePlayer();
        }

        if (Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.RightArrow))
        {
            Player_Pos[0] = Player_row;
            Player_Pos[1] = Player_col;
            //Debug.Log("D");
            Player_row++;
            MovePlayer();

        }

        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.UpArrow))
        {
            Player_Pos[0] = Player_row;
            Player_Pos[1] = Player_col;
            //Debug.Log("W");
            Player_col++;
            MovePlayer();

        }

        if (Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.DownArrow))
        {
            Player_Pos[0] = Player_row;
            Player_Pos[1] = Player_col;
            //Debug.Log("S");
            Player_col--;
            MovePlayer();

        }
    }

    void MovePlayer()
    {
        Player_Pos[2] = Player_row;
        Player_Pos[3] = Player_col;
        BoardManager.SendMessage("SwapGem", Player_Pos);
        BoardManager.SendMessage("LoadBoard", Player_Pos);
        BoardManager.SendMessage("PlacePlayer", Player_Pos);
        BoardManager.SendMessage("CheckMatchVer", Player_Pos);
        BoardManager.SendMessage("CheckMatchHor", Player_Pos);
    }
}
