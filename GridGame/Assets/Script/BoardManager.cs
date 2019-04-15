using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardManager : MonoBehaviour
{
    int row = 5;
    int col = 7;
    int score = 0;
    public bool start = false;
    public bool move = false;
    public int[,] Gems_List = new int[5, 7];
    public Sprite Gem_Image;
    public GameObject Gems;
    public GameObject Player_Perfab;
    public GameObject GameManager;
    public GameObject Score;
    public GameObject Particles;
    TextMeshProUGUI Score_Text;
    GameManager GameManager_Script;
    GameObject Player;
    int[] Player_Pos_Start = new int[4];

    void Start()
    {
        Player_Pos_Start[0] = 2;
        Player_Pos_Start[1] = 3;
        Player_Pos_Start[2] = 2;
        Player_Pos_Start[3] = 3;
        Score = GameObject.Find("Score_Number");
        Score_Text = Score.GetComponent<TextMeshProUGUI>();
        GameManager_Script = GameManager.GetComponent<GameManager>();

        LoadStartBoard();

    }

    //__________________________________________________________________________

    void LoadStartBoard()
    {

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {

                Gems_List[i, j] = Random.Range(1, 6);
                Gems_List[2, 3] = 7;
                if (Gems_List[i, j] != 7)
                {
                    GameObject Gem = Instantiate(Gems);
                    SpriteRenderer SpriteRender = Gem.GetComponent<SpriteRenderer>();
                    switch (Gems_List[i, j])
                    {
                        case 1:
                            SpriteRender.color = new Color(1, 0.6f, 0.6f, 1); // red
                            break;
                        case 2:
                            SpriteRender.color = new Color(0.7f, 1, 1, 1);
                            break;
                        case 3:
                            SpriteRender.color = new Color(0.6f, 1, 0.7f, 1);
                            break;
                        case 4:
                            SpriteRender.color = new Color(1, 1, 0.5f, 1);
                            break;
                        case 5:
                            SpriteRender.color = new Color(1, 0.7f, 0.3f, 1); // orange
                            break;
                    

                    }
                    Gem.transform.position = new Vector3(i, j - 1, 0);

                }

            }

        }
        CheckMatchVer(Player_Pos_Start);
        CheckMatchHor(Player_Pos_Start);


        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Gems_List[i, j] == 0)
                {
                    GameObject Gem = Instantiate(Gems);
                    SpriteRenderer SpriteRender = Gem.GetComponent<SpriteRenderer>();
                    SpriteRender.color = new Color(0.7f, 0.6f, 1, 1); 
                    Gem.transform.position = new Vector3(i, j - 1, 0);
                }

            }
        }
        Player = Instantiate(Player_Perfab);
        Player.transform.position = new Vector3(2, 3 - 1, 0);
        score = 0;
        start = true;
        move = true;
        Score_Text.text = score.ToString();

    }

    //__________________________________________________________________________

    void LoadBoard(int[] Player_Pos)
    {
        GameObject[] OldGems;

        OldGems = GameObject.FindGameObjectsWithTag("Gem");

        foreach (GameObject Gem in OldGems)
        {
            Destroy(Gem);
        }
        //-------------------------------------------------------
        Gems_List[Player_Pos[2], Player_Pos[3]] = 7;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Gems_List[i, j] != 7)
                {
                    GameObject Gem = Instantiate(Gems);
                    SpriteRenderer SpriteRender = Gem.GetComponent<SpriteRenderer>();
                    switch (Gems_List[i, j])
                    {
                        case 1:
                            SpriteRender.color = new Color(1, 0.6f, 0.6f, 1); // red
                            break;
                        case 2:
                            SpriteRender.color = new Color(0.7f, 1, 1, 1);
                            break;
                        case 3:
                            SpriteRender.color = new Color(0.6f, 1, 0.7f, 1);
                            break;
                        case 4:
                            SpriteRender.color = new Color(1, 1, 0.5f, 1);
                            break;
                        case 5:
                            SpriteRender.color = new Color(1, 0.7f, 0.3f, 1); // orange
                            break;
                        case 6:
                            SpriteRender.color = new Color(0.7f, 0.6f, 1, 1); // purple
                            break;
                    }
                    Gem.transform.position = new Vector3(i, j - 1, 0);

                }

            }

        }
        CheckMatchVer(Player_Pos);
        CheckMatchHor(Player_Pos);
    }


    //__________________________________________________________________________

    
    void PlacePlayer(int[] Player_Pos)
    {

        //Debug.Log("call");
        int row = Player_Pos[2];
        int col = Player_Pos[3] - 1;
        Player.transform.position = new Vector3(row, col, 0);

    }

    //__________________________________________________________________________

    void SwapGem(int[] Player_Pos)
    {
        Gems_List[Player_Pos[0], Player_Pos[1]] = Gems_List[Player_Pos[2], Player_Pos[3]];

    }

    //__________________________________________________________________________

    void CheckMatchVer(int[] Player_Pos)
    {
        int match = 0;
        int oldnumber = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                int newnumber = Gems_List[i, j];
                if (newnumber == oldnumber)
                {
                    match++;
                    if (j == col - 1)
                    {
                        if (match >= 2)
                        {
                            int[] Particle_Pos = new int[2];
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j- 2;
                            ParticlesAnimation(Particle_Pos);
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j- 3;
                            ParticlesAnimation(Particle_Pos);

                            Debug.Log("match");
                            Debug.Log(match);
                            Gems_List[i, j] = 0;
                            Gems_List[i, j - 1] = 0;
                            Gems_List[i, j - 2] = 0;
                            score += 3;
                            if (match == 3)
                            {
                                Gems_List[i, j - 3] = 0;
                                score += 1;
                                Particle_Pos[0] = i;
                                Particle_Pos[1] = j - 4;
                                ParticlesAnimation(Particle_Pos);
                            }

                            if (match == 4)
                            {
                                Gems_List[i, j - 4] = 0;
                                score += 2;
                                Particle_Pos[0] = i;
                                Particle_Pos[1] = j - 5;
                                ParticlesAnimation(Particle_Pos);
                            }

                            if (match == 5)
                            {
                                Gems_List[i, j - 5] = 0;
                                score += 3;
                                Particle_Pos[0] = i;
                                Particle_Pos[1] = j - 6;
                                ParticlesAnimation(Particle_Pos);
                            }

                            if (match == 6)
                            {
                                Gems_List[i, j - 6] = 0;
                                score += 4;
                                Particle_Pos[0] = i;
                                Particle_Pos[1] = j - 7;
                                ParticlesAnimation(Particle_Pos);
                            }

                            match = 0;
                            GemsFall(Player_Pos);

                            GameManager_Script.MoveNumber = 6;
                            if(start)
                            GameManager.SendMessage("MoveCheck");

                        }
                        match = 0;
                        Score_Text.text = score.ToString();
                    }
                }
                else
                {
                    if (match >= 2)
                    {
                        int[] Particle_Pos = new int[2];
                        Particle_Pos[0] = i;
                        Particle_Pos[1] = j - 2;
                        ParticlesAnimation(Particle_Pos);
                        Particle_Pos[0] = i;
                        Particle_Pos[1] = j - 3;
                        ParticlesAnimation(Particle_Pos);
                        Particle_Pos[0] = i;
                        Particle_Pos[1] = j - 4;
                        ParticlesAnimation(Particle_Pos);

                        //_____________________________________

                        Debug.Log("match");
                        Debug.Log(match);
                        Gems_List[i, j - 1] = 0;
                        Gems_List[i, j - 2] = 0;
                        Gems_List[i, j - 3] = 0;
                        score += 3;

                        if (match == 3)
                        {
                            Gems_List[i, j - 4] = 0;
                            score += 1;
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 5;
                            ParticlesAnimation(Particle_Pos);
                        }

                        if (match == 4)
                        {
                            Gems_List[i, j - 5] = 0;
                            score += 2;
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 6;
                            ParticlesAnimation(Particle_Pos);
                        }

                        if (match == 5)
                        {
                            Gems_List[i, j - 6] = 0;
                            score += 3;
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 7;
                            ParticlesAnimation(Particle_Pos);
                        }

                        if (match == 6)
                        {
                            Gems_List[i, j - 7] = 0;
                            score += 4;
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 8;
                            ParticlesAnimation(Particle_Pos);
                        }

                        match = 0;
                        GemsFall(Player_Pos);

                        GameManager_Script.MoveNumber = 6;
                        if (start)
                            GameManager.SendMessage("MoveCheck");
                    }
                    match = 0;
                    Score_Text.text = score.ToString();

                   
                }
                oldnumber = Gems_List[i, j];
            }

            match = 0;
            oldnumber = 0;
        }

    }

    //_______________________________________________________________

    void CheckMatchHor(int[] Player_Pos)
    {
        int match_Hor = 0;
        int oldnumber_Hor = 0;
        for (int j = 0; j < col; j++)
        {
            for (int i = 0; i < row; i++)
            {
                int newnumber_Hor = Gems_List[i, j];
                if (newnumber_Hor == oldnumber_Hor)
                {
                    match_Hor++;

                    if (i == row - 1)
                    {
                        if (match_Hor >= 2)
                        {

                            int[] Particle_Pos = new int[2];
                            Particle_Pos[0] = i;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                            Particle_Pos[0] = i - 1;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                            Particle_Pos[0] = i - 2;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);

                            //___________________________________

                            Debug.Log("match");
                            Debug.Log(match_Hor);
                            Gems_List[i - 1, j] = 0;
                            Gems_List[i - 2, j] = 0;
                            Gems_List[i - 3, j] = 0;
                            score += 3;
                            if (match_Hor == 3)
                            {
                                Gems_List[i - 4, j] = 0;
                                score += 1;
                                Particle_Pos[0] = i - 3;
                                Particle_Pos[1] = j - 1;
                                ParticlesAnimation(Particle_Pos);
                            }
                            if (match_Hor == 4)
                            {
                                Gems_List[i - 5, j] = 0;
                                score += 2;
                                Particle_Pos[0] = i - 4;
                                Particle_Pos[1] = j - 1;
                                ParticlesAnimation(Particle_Pos);
                            }

                            if (match_Hor == 5)
                            {
                                Gems_List[i - 6, j] = 0;
                                score += 3;
                                Particle_Pos[0] = i - 5;
                                Particle_Pos[1] = j - 1;
                                ParticlesAnimation(Particle_Pos);
                            }
                            match_Hor = 0;
                            GemsFall(Player_Pos);

                            GameManager_Script.MoveNumber = 6;
                            if (start)
                                GameManager.SendMessage("MoveCheck");
                        }
                        match_Hor = 0;
                        Score_Text.text = score.ToString();


                    }
                }
                else
                {
                    if (match_Hor >= 2)
                    {

                        int[] Particle_Pos = new int[2];
                        Particle_Pos[0] = i - 1;
                        Particle_Pos[1] = j - 1;
                        ParticlesAnimation(Particle_Pos);
                        Particle_Pos[0] = i - 2;
                        Particle_Pos[1] = j - 1;
                        ParticlesAnimation(Particle_Pos);
                        Particle_Pos[0] = i - 3;
                        Particle_Pos[1] = j - 1;
                        ParticlesAnimation(Particle_Pos);

                        //___________________________________

                        Debug.Log("match");
                        Debug.Log(match_Hor);
                        Gems_List[i - 1, j] = 0;
                        Gems_List[i - 2, j] = 0;
                        Gems_List[i - 3, j] = 0;
                        score += 3;
                        if (match_Hor == 3)
                        {
                            Gems_List[i - 4, j] = 0;
                            score += 1;
                            Particle_Pos[0] = i - 4;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                        }
                        if (match_Hor == 4)
                        {
                            Gems_List[i - 5, j] = 0;
                            score += 2;
                            Particle_Pos[0] = i - 5;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                        }

                        if (match_Hor == 5)
                        {
                            Gems_List[i - 6, j] = 0;
                            score += 3;
                            Particle_Pos[0] = i - 6;
                            Particle_Pos[1] = j - 1;
                            ParticlesAnimation(Particle_Pos);
                        }
                        match_Hor = 0;
                        GemsFall(Player_Pos);

                        GameManager_Script.MoveNumber = 6;
                        if (start)
                            GameManager.SendMessage("MoveCheck");
                    }
                    match_Hor = 0;
                    Score_Text.text = score.ToString();
                }
                oldnumber_Hor = Gems_List[i, j];
            }

            match_Hor = 0;
            oldnumber_Hor = 0;
        }

    }

    //__________________________________________________________________________

    void GemsFall(int[] Player_Pos)
    {
        if (start)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Gems_List[i, j] == 0)
                    {
                        if (j + 1 < col && Gems_List[i, j + 1] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 1];
                            Gems_List[i, j + 1] = 0;
                        }
                        else if (j + 2 < col && Gems_List[i, j + 2] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 2];
                            Gems_List[i, j + 2] = 0;
                        }
                        else if (j + 3 < col && Gems_List[i, j + 3] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 3];
                            Gems_List[i, j + 3] = 0;
                        }
                        else if (j + 4 < col && Gems_List[i, j + 4] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 4];
                            Gems_List[i, j + 4] = 0;
                        }
                        else if (j + 5 < col && Gems_List[i, j + 5] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 5];
                            Gems_List[i, j + 5] = 0;
                        }
                        else if (j + 6 < col && Gems_List[i, j + 6] != 0)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 6];
                            Gems_List[i, j + 6] = 0;
                        }

                    }
                }

            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Gems_List[i, j] == 7)
                    {
                        GameManager_Script.Player_row = i;
                        GameManager_Script.Player_col = j;
                        Player_Pos[0] = i;
                        Player_Pos[1] = j;
                        Player_Pos[2] = i;
                        Player_Pos[3] = j;
                    }
                }
            }
          
            GemsRenew(Player_Pos);

        }

        if (!start)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Gems_List[i, j] == 0)
                    {
                        if (j + 1 < col && Gems_List[i, j + 1] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 1];
                            Gems_List[i, j + 1] = 0;
                        }
                        else if (j + 2 < col && Gems_List[i, j + 2] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 2];
                            Gems_List[i, j + 2] = 0;
                        }
                        else if (j + 3 < col && Gems_List[i, j + 3] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 3];
                            Gems_List[i, j + 3] = 0;
                        }
                        else if (j + 4 < col && Gems_List[i, j + 4] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 4];
                            Gems_List[i, j + 4] = 0;
                        }
                        else if (j + 5 < col && Gems_List[i, j + 5] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 5];
                            Gems_List[i, j + 5] = 0;
                        }
                        else if (j + 6 < col && Gems_List[i, j + 6] != 0 && Gems_List[i, j + 1] != 7)
                        {
                            Gems_List[i, j] = Gems_List[i, j + 6];
                            Gems_List[i, j + 6] = 0;
                        }

                    }
                }

            }
          
            GemsRenew(Player_Pos);

        }

    }

    //__________________________________________________________________________

    void GemsRenew(int[] Player_Pos)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (Gems_List[i, j] == 0)
                {
                    Gems_List[i, j] = Random.Range(1, 7);
                }
            }
        }

        if (start)
        {
            StartCoroutine(wait(Player_Pos));
        }

        if (!start)
        {
            LoadBoard(Player_Pos);
        }

    }


    IEnumerator wait(int[] Player_Pos)
    {
        move = false;
        yield return new WaitForSeconds(1.0f);
        move = true;
        LoadBoard(Player_Pos);
      
    }

    //______________________________________________________________

    void ParticlesAnimation(int[] Particle_Pos)
    {
        if (start)
        {
            GameObject Particle = Instantiate(Particles);
            Particle.transform.position = new Vector3(Particle_Pos[0], Particle_Pos[1], 0);
        }

    }





}

