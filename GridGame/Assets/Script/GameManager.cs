using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    GameObject BoardManager;
    BoardManager BoardManager_Script;
    public GameObject Player_Move;
    TextMeshPro Move_Text;
    public int MoveNumber = 6;
    public int Player_row = 2;
    public int Player_col = 3;
    int[] Player_Pos = new int[4];


    void Start()
    {
        BoardManager = GameObject.Find("BoardManager");
        BoardManager_Script = BoardManager.GetComponent<BoardManager>();

    }
  
    void Update()
    {
        if(BoardManager_Script.start && BoardManager_Script.move)
        {
            if ( Player_row > 0 && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)))
            {
                Player_Pos[0] = Player_row;
                Player_Pos[1] = Player_col;
                Player_row--;
                MoveNumber--;
                MoveCheck();
                //if (MoveNumber > 0)
                MovePlayer();
                
            }

            if (Player_row < 4 && (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
            {
                Player_Pos[0] = Player_row;
                Player_Pos[1] = Player_col;
                Player_row++;
                MoveNumber--;
                MoveCheck();
                //if (MoveNumber > 0)
                MovePlayer();
                

            }

            if (Player_col < 6 && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
            {
                Player_Pos[0] = Player_row;
                Player_Pos[1] = Player_col;
                Player_col++;
                MoveNumber--;
                MoveCheck();
                //if (MoveNumber > 0)
                MovePlayer();


            }

            if (Player_col > 0 && (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)))
            {
                Player_Pos[0] = Player_row;
                Player_Pos[1] = Player_col;
                Player_col--;
                MoveNumber--;
                MoveCheck();
                //if (MoveNumber > 0)
                MovePlayer();
                
            }

        }
        

    }



    void MoveCheck()
    {
        Player_Move = GameObject.Find("Move");
        Move_Text = Player_Move.GetComponent<TextMeshPro>();
        Move_Text.text = MoveNumber.ToString();
      
    }

    void MovePlayer()
    {
        Player_Pos[2] = Player_row;
        Player_Pos[3] = Player_col;
        BoardManager.SendMessage("SwapGem", Player_Pos);
        BoardManager.SendMessage("LoadBoard", Player_Pos);
        BoardManager.SendMessage("PlacePlayer", Player_Pos);
        if (MoveNumber == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        //BoardManager.SendMessage("CheckMatchVer", Player_Pos);
        //BoardManager.SendMessage("CheckMatchHor", Player_Pos);
    }
    
}
