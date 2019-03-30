using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    int row = 4;
    int col = 6;
    public int[,] Gems_List = new int[4, 6];
    public Sprite Gem_Image;
    public GameObject Gems;
    public GameObject Player;

    void Start()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Gems_List[i,j] = Random.Range(1, 7);
                GameObject Gem = Instantiate(Gems);
                SpriteRenderer SpriteRender = Gem.GetComponent<SpriteRenderer>();
                switch(Gems_List[i, j])
                {
                    case 1:
                        SpriteRender.color = Color.red;
                        break;
                    case 2:
                        SpriteRender.color = Color.blue;
                        break;
                    case 3:
                        SpriteRender.color = Color.green;
                        break;
                    case 4:
                        SpriteRender.color = Color.yellow;
                        break;
                    case 5:
                        SpriteRender.color = new Color (1,0.6f,0,1);
                        break;
                    case 6:
                        SpriteRender.color = new Color (0.6f,0.3f,0.9f,1);
                        break;
                }
                Gem.transform.position = new Vector3 (i, j-1, 0);
            }

        }
    }

    void Update()
    {
        
    }

    void PlacePlayer()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if(Gems_List[i,j] == 7)
                {
                    Player.transform.position = new Vector3(i, j - 1, 0);
                }
            }

        }

    }
}
