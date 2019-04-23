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
    public int MoveNumber;
    public int Player_row = 2;
    public int Player_col = 3;
    public int Enemy_row = 0;
    public int Enemy_col = 6;
    int[] Player_Pos = new int[4];
    int[] Enemy_Pos = new int[2];
    bool left = true;
    SpriteRenderer SpriteRender;




    void Start()
    {
        MoveNumber = 8;
        BoardManager = GameObject.Find("BoardManager");
        BoardManager_Script = BoardManager.GetComponent<BoardManager>();
        Enemy_Pos[0] = Enemy_row;
        Enemy_Pos[1] = Enemy_col;

    }
  
    void Update()
    {
        if(BoardManager_Script.start && BoardManager_Script.move)
        {
            if(Player != null)
            {
                Player = GameObject.Find("Player(Clone)");
                SpriteRender = Player.GetComponent<SpriteRenderer>();
            }
           

            if ( Player_row > 0 && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)))
            {
                Player_Pos[0] = Player_row;
                Player_Pos[1] = Player_col;
                Player_row--;
                MoveNumber--;
                MoveCheck();
                //if (MoveNumber > 0)
                MovePlayer();
                left = true;


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
                left = false;



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

            if(left)
            {
                SpriteRender.flipX = false;
            }else
            {
                SpriteRender.flipX = true;
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
        if (Player_row == Enemy_row && Player_col == Enemy_col)
        {
            SceneManager.LoadScene("GameOver");
        }
        MoveEnemy();
        if (MoveNumber == 0)
        {
            SceneManager.LoadScene("GameOver2");
        }
        if (Player_row == Enemy_row && Player_col == Enemy_col)
        {
            SceneManager.LoadScene("GameOver");
        }


    }

    void MoveEnemy()
    {
        int random;
        random = Random.Range(1, 5);

        if (Enemy_row == 0)
        {
            random = 1;
        }
        else if (Enemy_row == 4)
        {
            random = 2;
        }
        else if (Enemy_col == 6)
        {
            random = 4;
        }
        else if (Enemy_col == 0)
        {
            random = 3;
        }


        if (random == 1)
        {
            Enemy_row ++;

        }
        else if (random == 2)
        {
            Enemy_row --;

        }
        else if (random == 3)
        {
            Enemy_col ++;
        }
        else if (random == 4)
        {
            Enemy_col --;
        }

        Enemy_Pos[0] = Enemy_row;
        Enemy_Pos[1] = Enemy_col;

        BoardManager.SendMessage("PlaceEnemy", Enemy_Pos);

    }
    
}
