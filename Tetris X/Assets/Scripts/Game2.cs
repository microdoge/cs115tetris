﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2 : MonoBehaviour {

    public static int boundaryheight = 20;
    public static int boundarywidth = 10;
    public static Transform[,] boundaryaxis = new Transform[boundarywidth, boundaryheight];

    // Use this for initialization
    void Start() {
        generatenextblock2();
    }

    // Update is called once per frame
    void Update() {

    }

    public bool checkwithinboundary2(Vector2 pos) {

        return ((int)pos.x >= 1 && (int)pos.x <= 9 && (int)pos.y >= 1);

    }

    public Vector2 Round2(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void generatenextblock2() {
		int random_x = Random.Range (3,8);
		float x_value = 1.0f * random_x;
		GameObject next_block1 = (GameObject)Instantiate(Resources.Load(get_block_names2(), typeof(GameObject)), new Vector2(x_value, 22.0f), Quaternion.identity);
    }

//	public void generatenextblock1() {
//		GameObject next_block = (GameObject)Instantiate(Resources.Load(get_block_names1(), typeof(GameObject)), new Vector2(5.0f, 22.0f), Quaternion.identity);
//	}

    string get_block_names2() {
        int random_num = Random.Range(1, 8);
        string random_block_name = "Prefabs 2/theBLOCK_J";
        switch (random_num) {

            case 1:
                random_block_name = "Prefabs 2/theBLOCK_J";
                break;
            case 2:
                random_block_name = "Prefabs 2/theBLOCK_L";
                break;
            case 3:
                random_block_name = "Prefabs 2/theBLOCK_long";
                break;
            case 4:
                random_block_name = "Prefabs 2/theBLOCK_s";
                break;
            case 5:
                random_block_name = "Prefabs 2/theBLOCK_square";
                break;
            case 6:
                random_block_name = "Prefabs 2/theBLOCK_T";
                break;
            case 7:
                random_block_name = "Prefabs 2/theBLOCK_z";
                break;
        }
        return random_block_name;
    }

//	string get_block_names1() {
//		int random_num = Random.Range(1, 8);
//		string random_block_name = "Prefabs 1/BLOCK_J";
//		switch (random_num) {
//
//		case 1:
//			random_block_name = "Prefabs 1/BLOCK_J";
//			break;
//		case 2:
//			random_block_name = "Prefabs 1/BLOCK_L";
//			break;
//		case 3:
//			random_block_name = "Prefabs 1/BLOCK_long";
//			break;
//		case 4:
//			random_block_name = "Prefabs 1/BLOCK_S";
//			break;
//		case 5:
//			random_block_name = "Prefabs 1/BLOCK_square";
//			break;
//		case 6:
//			random_block_name = "Prefabs 1/BLOCK_T";
//			break;
//		case 7:
//			random_block_name = "Prefabs 1/BLOCK_Z";
//			break;
//		}
//		return random_block_name;
//	}

    public void update_boundary2(Blocks2 block) {

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
            Vector2 pos = Round2(mino.position);
            if (pos.y < boundaryheight) {
                boundaryaxis[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    public Transform TransBlockHeplerFunc2(Vector2 pos) {
        if (pos.y >= boundaryheight - 1) {
            return null;
        } else {
            return boundaryaxis[(int)pos.x, (int)pos.y];
        }
    }

    public bool isrowfull2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            if (boundaryaxis[x, y] == null) {
                return false;
            }
        }
        return true;
    }

    public void deletetherow2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            Destroy(boundaryaxis[x, y].gameObject);
            boundaryaxis[x, y] = null;
        }

    }

    public void movetherowdown2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {

            if (boundaryaxis[x, y] != null) {

                boundaryaxis[x, y - 1] = boundaryaxis[x, y];
                boundaryaxis[x, y] = null;
                boundaryaxis[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }

    }

    public void moveentirerowdown2(int y) {

        for (int j = y; j < boundaryheight; ++j) {
            movetherowdown2(j);
        }

    }

    public void removerow2() {

        for (int y = 1; y < boundaryheight; ++y) {

            if (isrowfull2(y)) {
                deletetherow2(y);
                moveentirerowdown2(y + 1);
                --y;
            }

        }

    }

//    public bool istouchbotton(Blocks theblock) {
//        for (int x = 0; x < boundarywidth; ++x)
//        {
//
//            foreach (Transform mino in theblock.transform)
//            {
//                Vector2 theposition = Round(mino.position);
//                if (theposition.y == 1)
//                {
//                    return true;
//                }
//            }
//
//        }
//        return false;
//    }

    public bool isoverlimit2(Blocks2 theblock){

        for (int x = 0; x < boundarywidth; ++x) {

            foreach (Transform mino in theblock.transform) {
                Vector2 theposition = Round2(mino.position);
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
