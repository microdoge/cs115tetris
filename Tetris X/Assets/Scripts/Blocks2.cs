﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocks2 : MonoBehaviour {

	float fall1 = 0;
	public double falling_speed1 = 0.2;
	public bool allowedrotation = true;
	public bool limitedrotation = false;
    public Button rightbutton2;
    public Button leftbutton2;
    public Button rotatebutton2;
    public Button downbutton2;
	public Button easy_mode_button;
	public Button normal_mode_button;
	public Button hard_mode_button;
	int helpernum;

    // Use this for initialization
    void Start() {

        rightbutton2 = GameObject.FindGameObjectWithTag("Rightbutton").GetComponent<Button>(); 
        rightbutton2.onClick.AddListener(() => movetoright2());

        leftbutton2 = GameObject.FindGameObjectWithTag("Leftbutton").GetComponent<Button>();
        leftbutton2.onClick.AddListener(() => movetoleft2());

        rotatebutton2 = GameObject.FindGameObjectWithTag("Rotatebutton").GetComponent<Button>();
        rotatebutton2.onClick.AddListener(() => buttonrotatefunc2());

        downbutton2 = GameObject.FindGameObjectWithTag("Downbutton").GetComponent<Button>();
        downbutton2.onClick.AddListener(() => movedownbutton2());

//		easy_mode_button = GameObject.FindGameObjectWithTag("Easymodebutton").GetComponent<Button>();
//		easy_mode_button.onClick.AddListener(() => seteasyspeed());
//
//		normal_mode_button = GameObject.FindGameObjectWithTag("Normalmodebutton").GetComponent<Button>();
//		normal_mode_button.onClick.AddListener(() => setnormalspeed());

    }
	
	// Update is called once per frame
	void Update () {
		func_move_block2 ();
    }
		
	//code to move the blocks and make block falling
	void func_move_block2(){

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3 (1,0,0);
			if(validposition2()){
				FindObjectOfType<Game2> ().update_boundary2 (this);
			}else{
				transform.position += new Vector3 (-1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){

			transform.position += new Vector3 (-1,0,0);
			if(validposition2()){
				FindObjectOfType<Game2> ().update_boundary2 (this);
			}else{
				transform.position += new Vector3 (1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.UpArrow)){
			
			if(allowedrotation){
				if (limitedrotation) {
					if (transform.rotation.eulerAngles.z >= 90) {
						transform.Rotate (0, 0, -90);
					} else {
						transform.Rotate (0, 0, 90);
					}
				} else {
					transform.Rotate (0, 0, 90);
				}	
			}

			if(validposition2()){
				FindObjectOfType<Game2> ().update_boundary2 (this);
			}else{
				if(transform.rotation.eulerAngles.z >= 90){
					transform.Rotate (0, 0, -90);
				}else{
					transform.Rotate (0, 0, 90);
				}
			}
			
		}else if(Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - fall1) >= falling_speed1){

			transform.position += new Vector3 (0,-1,0);
			if(validposition2()){
				FindObjectOfType<Game2> ().update_boundary2 (this);
			}else{
				
				transform.position += new Vector3 (0,1,0);
				FindObjectOfType<Game2> ().removerow2 ();

                if (FindObjectOfType<Game2>().isoverlimit2(this)) {
                    FindObjectOfType<Game2>().Gameend();
                }

				enabled = false;
				FindObjectOfType<Game2> ().generatenextblock2 ();
				
			}
			fall1 = Time.time;

		}
	}

    

	public bool validposition2(){

		foreach (Transform mino in transform){
			Vector2 pos = FindObjectOfType<Game2> ().Round2 (mino.position);
			if(FindObjectOfType<Game2>().checkwithinboundary2(pos) == false){
				return false;
			}
			if(FindObjectOfType<Game2>().TransBlockHeplerFunc2(pos) != null && FindObjectOfType<Game2>().TransBlockHeplerFunc2(pos).parent != transform){
				return false;
			}
		}
		return true;
	}

    public void movetoright2() {
        //if (FindObjectOfType<Game>().istouchbotton(this) == false)
		if(enabled == true)	
        {
            transform.position += new Vector3(1, 0, 0);
            if (validposition2())
            {
                FindObjectOfType<Game2>().update_boundary2(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else {
            //enabled = false;
        }
        
    }

    public void movetoleft2() {
		if (enabled == true)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (validposition2())
            {
                FindObjectOfType<Game2>().update_boundary2(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else {
            //enabled = false;
        }
    }

    public void buttonrotatefunc2() {
		if (enabled == true) {
            if (allowedrotation)
            {
                if (limitedrotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            if (validposition2())
            {
                FindObjectOfType<Game2>().update_boundary2(this);
            }
            else
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }

    public void movedownbutton2() {
        transform.position += new Vector3(0, -1, 0);
        if (validposition2())
        {
            FindObjectOfType<Game2>().update_boundary2(this);
        }
        else
        {

            transform.position += new Vector3(0, 1, 0);
            

            if (FindObjectOfType<Game2>().isoverlimit2(this))
            {
                FindObjectOfType<Game2>().Gameend();
            }

            
            

        }
        
    }
   
}
