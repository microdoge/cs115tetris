﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocks1 : MonoBehaviour {

	float fall1 = 0;
	public double falling_speed1 = 0.5;
	public bool allowedrotation = true;
	public bool limitedrotation = false;
    public Button rightbutton1;
    public Button leftbutton1;
    public Button rotatebutton1;
    public Button downbutton1;
	public Button easy_mode_button;
	public Button normal_mode_button;
	public Button hard_mode_button;
	int helpernum;

    // Use this for initialization
    void Start() {

        rightbutton1 = GameObject.FindGameObjectWithTag("Rightbutton").GetComponent<Button>(); 
        rightbutton1.onClick.AddListener(() => movetoright1());

        leftbutton1 = GameObject.FindGameObjectWithTag("Leftbutton").GetComponent<Button>();
        leftbutton1.onClick.AddListener(() => movetoleft1());

        rotatebutton1 = GameObject.FindGameObjectWithTag("Rotatebutton").GetComponent<Button>();
        rotatebutton1.onClick.AddListener(() => buttonrotatefunc1());

        downbutton1 = GameObject.FindGameObjectWithTag("Downbutton").GetComponent<Button>();
        downbutton1.onClick.AddListener(() => movedownbutton1());

//		easy_mode_button = GameObject.FindGameObjectWithTag("Easymodebutton").GetComponent<Button>();
//		easy_mode_button.onClick.AddListener(() => seteasyspeed());
//
//		normal_mode_button = GameObject.FindGameObjectWithTag("Normalmodebutton").GetComponent<Button>();
//		normal_mode_button.onClick.AddListener(() => setnormalspeed());

    }
	
	// Update is called once per frame
	void Update () {
		func_move_block1 ();
    }
		
	//code to move the blocks and make block falling
	void func_move_block1(){

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3 (1,0,0);
			if(validposition1()){
				FindObjectOfType<Game1> ().update_boundary1 (this);
			}else{
				transform.position += new Vector3 (-1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){

			transform.position += new Vector3 (-1,0,0);
			if(validposition1()){
				FindObjectOfType<Game1> ().update_boundary1 (this);
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

			if(validposition1()){
				FindObjectOfType<Game1> ().update_boundary1 (this);
			}else{
				if(transform.rotation.eulerAngles.z >= 90){
					transform.Rotate (0, 0, -90);
				}else{
					transform.Rotate (0, 0, 90);
				}
			}
			
		}else if(Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - fall1) >= falling_speed1){

			transform.position += new Vector3 (0,-1,0);
			if(validposition1()){
				FindObjectOfType<Game1> ().update_boundary1 (this);
			}else{
				
				transform.position += new Vector3 (0,1,0);
				FindObjectOfType<Game1> ().removerow1 ();

                if (FindObjectOfType<Game1>().isoverlimit1(this)) {
                    FindObjectOfType<Game1>().Gameend();
                }

				enabled = false;
				FindObjectOfType<Game1> ().generatenextblock1 ();
				
			}
			fall1 = Time.time;

		}
	}

    

	public bool validposition1(){

		foreach (Transform mino in transform){
			Vector2 pos = FindObjectOfType<Game1> ().Round1 (mino.position);
			if(FindObjectOfType<Game1>().checkwithinboundary1(pos) == false){
				return false;
			}
			if(FindObjectOfType<Game1>().TransBlockHeplerFunc1(pos) != null && FindObjectOfType<Game1>().TransBlockHeplerFunc1(pos).parent != transform){
				return false;
			}
		}
		return true;
	}

    public void movetoright1() {
        //if (FindObjectOfType<Game>().istouchbotton(this) == false)
		if(enabled == true)	
        {
            transform.position += new Vector3(1, 0, 0);
            if (validposition1())
            {
                FindObjectOfType<Game1>().update_boundary1(this);
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

    public void movetoleft1() {
		if (enabled == true)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (validposition1())
            {
                FindObjectOfType<Game1>().update_boundary1(this);
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

    public void buttonrotatefunc1() {
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

            if (validposition1())
            {
                FindObjectOfType<Game1>().update_boundary1(this);
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

    public void movedownbutton1() {
        transform.position += new Vector3(0, -1, 0);
        if (validposition1())
        {
            FindObjectOfType<Game1>().update_boundary1(this);
        }
        else
        {

            transform.position += new Vector3(0, 1, 0);
            

            if (FindObjectOfType<Game1>().isoverlimit1(this))
            {
                FindObjectOfType<Game1>().Gameend();
            }

            
            

        }
        
    }
   
}
