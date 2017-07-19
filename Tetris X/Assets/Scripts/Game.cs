using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int boundaryheight = 20;
    public static int boundarywidth = 10;
    public static Transform[,] boundaryaxis = new Transform[boundarywidth, boundaryheight];

    //variables for generating score
    private int number_of_canceled_lines = 0;
    public int score_cancel_oneline = 10;
    public int score_cancel_twoline = 30;
    public int score_cancel_threeline = 70;
    public int score_cancel_fourline = 250;
    private int easymode_current_score = 0;
    public Text easymode_score;


    // Use this for initialization
    void Start() {
        generatenextblock();
    }

    // Update is called once per frame
    void Update() {
        gamescore_function();
        gamescore_helper_function();
        //update the score during the game
    }

    public void gamescore_helper_function() {

        easymode_score.text = easymode_current_score.ToString();
        //convert the current score to text format
    }

    public void gamescore_function()
    {
        if (number_of_canceled_lines > 0)
        {
            if (number_of_canceled_lines == 4) {
                easymode_current_score = easymode_current_score + score_cancel_fourline;
            } else if (number_of_canceled_lines == 3) {
                easymode_current_score = easymode_current_score + score_cancel_threeline;
            } else if (number_of_canceled_lines == 2) {
                easymode_current_score = easymode_current_score + score_cancel_twoline;
            } else if (number_of_canceled_lines == 1) {
                easymode_current_score = easymode_current_score + score_cancel_oneline;
            }

            number_of_canceled_lines = 0;
        }
    }

    public bool checkwithinboundary(Vector2 pos) {

        return ((int)pos.x >= 1 && (int)pos.x <= 9 && (int)pos.y >= 1);

    }

    public Vector2 Round(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void generatenextblock() {
        GameObject next_block = (GameObject)Instantiate(Resources.Load(get_block_names(), typeof(GameObject)), new Vector2(5.0f, 22.0f), Quaternion.identity);
    }

    string get_block_names() {
        int random_num = Random.Range(1, 8);
        string random_block_name = "Prefabs/theBLOCK_J";
        switch (random_num) {

            case 1:
                random_block_name = "Prefabs/theBLOCK_J";
                break;
            case 2:
                random_block_name = "Prefabs/theBLOCK_L";
                break;
            case 3:
                random_block_name = "Prefabs/theBLOCK_long";
                break;
            case 4:
                random_block_name = "Prefabs/theBLOCK_s";
                break;
            case 5:
                random_block_name = "Prefabs/theBLOCK_square";
                break;
            case 6:
                random_block_name = "Prefabs/theBLOCK_T";
                break;
            case 7:
                random_block_name = "Prefabs/theBLOCK_z";
                break;
        }
        return random_block_name;
    }

    public void update_boundary(Blocks block) {

        for (int y = 1; y < 20; y++) {
            for (int x = 1; x < 10; x++) {

                if (boundaryaxis[x, y] != null) {
                    if (boundaryaxis[x, y].parent == block.transform) {
                        boundaryaxis[x, y] = null;
                    }
                }

            }
        }

        foreach (Transform mino in block.transform) {
            Vector2 pos = Round(mino.position);
            if (pos.y < boundaryheight) {
                boundaryaxis[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    public Transform TransBlockHeplerFunc(Vector2 pos) {
        if (pos.y >= boundaryheight - 1) {
            return null;
        } else {
            return boundaryaxis[(int)pos.x, (int)pos.y];
        }
    }

    public bool isrowfull(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            if (boundaryaxis[x, y] == null) {
                return false;
            }
        }
        number_of_canceled_lines++;
        return true;
    }

    public void deletetherow(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            Destroy(boundaryaxis[x, y].gameObject);
            boundaryaxis[x, y] = null;
        }

    }

    public void movetherowdown(int y) {

        for (int x = 1; x < boundarywidth; ++x) {

            if (boundaryaxis[x, y] != null) {

                boundaryaxis[x, y - 1] = boundaryaxis[x, y];
                boundaryaxis[x, y] = null;
                boundaryaxis[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }

    }

    public void moveentirerowdown(int y) {

        for (int j = y; j < boundaryheight; ++j) {
            movetherowdown(j);
        }

    }

    public void removerow() {

        for (int y = 1; y < boundaryheight; ++y) {

            if (isrowfull(y)) {
                deletetherow(y);
                moveentirerowdown(y + 1);
                --y;
            }

        }

    }

    public bool isoverlimit(Blocks theblock){

        for (int x = 0; x < boundarywidth; ++x) {

            foreach (Transform mino in theblock.transform) {
                Vector2 theposition = Round(mino.position);
                if (theposition.y >= 19 ) {
                    return true;
                }
            }

        }
        return false;
    }

    public void Gameend() {
        Application.LoadLevel("GameOver");
    }

    
}
