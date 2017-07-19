using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocks : MonoBehaviour {

	float fall = 0;
	public float falling_speed = 1;
	public bool allowedrotation = true;
	public bool limitedrotation = false;
    public Button rightbutton;
    public Button leftbutton;
    public Button rotatebutton;
    public Button downbutton;
	public Button easy_mode_button;
	public Button normal_mode_button;
	public Button hard_mode_button;
	int helpernum;

    // Use this for initialization
    void Start() {

        rightbutton = GameObject.FindGameObjectWithTag("Rightbutton").GetComponent<Button>(); 
        rightbutton.onClick.AddListener(() => movetoright());

        leftbutton = GameObject.FindGameObjectWithTag("Leftbutton").GetComponent<Button>();
        leftbutton.onClick.AddListener(() => movetoleft());

        rotatebutton = GameObject.FindGameObjectWithTag("Rotatebutton").GetComponent<Button>();
        rotatebutton.onClick.AddListener(() => buttonrotatefunc());

        downbutton = GameObject.FindGameObjectWithTag("Downbutton").GetComponent<Button>();
        downbutton.onClick.AddListener(() => movedownbutton());

    }
	
	// Update is called once per frame
	void Update () {
		func_move_block ();
    }
		
	//code to move the blocks and make block falling
	void func_move_block(){

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3 (1,0,0);
			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				transform.position += new Vector3 (-1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){

			transform.position += new Vector3 (-1,0,0);
			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
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

			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				if(transform.rotation.eulerAngles.z >= 90){
					transform.Rotate (0, 0, -90);
				}else{
					transform.Rotate (0, 0, 90);
				}
			}
			
		}else if(Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - fall) >= falling_speed){

			transform.position += new Vector3 (0,-1,0);
			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				
				transform.position += new Vector3 (0,1,0);
				FindObjectOfType<Game> ().removerow ();

                if (FindObjectOfType<Game>().isoverlimit(this)) {
                    FindObjectOfType<Game>().Gameend();
                }

				enabled = false;
				FindObjectOfType<Game> ().generatenextblock ();
				
			}
			fall = Time.time;

		}
	}

    

	public bool validposition(){

		foreach (Transform mino in transform){
			Vector2 pos = FindObjectOfType<Game> ().Round (mino.position);
			if(FindObjectOfType<Game>().checkwithinboundary(pos) == false){
				return false;
			}
			if(FindObjectOfType<Game>().TransBlockHeplerFunc(pos) != null && FindObjectOfType<Game>().TransBlockHeplerFunc(pos).parent != transform){
				return false;
			}
		}
		return true;
	}

    public void movetoright() {
        //if (FindObjectOfType<Game>().istouchbotton(this) == false)
		if(enabled == true)	
        {
            transform.position += new Vector3(1, 0, 0);
            if (validposition())
            {
                FindObjectOfType<Game>().update_boundary(this);
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

    public void movetoleft() {
		if (enabled == true)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (validposition())
            {
                FindObjectOfType<Game>().update_boundary(this);
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

    public void buttonrotatefunc() {
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

            if (validposition())
            {
                FindObjectOfType<Game>().update_boundary(this);
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

    public void movedownbutton() {
        transform.position += new Vector3(0, -1, 0);
        if (validposition())
        {
            FindObjectOfType<Game>().update_boundary(this);
        }
        else
        {

            transform.position += new Vector3(0, 1, 0);
            

            if (FindObjectOfType<Game>().isoverlimit(this))
            {
                FindObjectOfType<Game>().Gameend();
            }

            
            

        }
        
    }
   
}
