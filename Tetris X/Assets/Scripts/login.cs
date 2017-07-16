using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;

public class login : MonoBehaviour {
	protected Firebase.Auth.FirebaseAuth auth;

	public InputField logname;
	public InputField logpass;
	public InputField regnam;
	public InputField regpass;

	private string uEmail;
	private string uPassword;
	

	// Use this for initialization
	void Start () {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		//uEmail = logname.text; 
		//uPassword = logpass.text;
		//Button logB = logButton.GetComponent<Button> ();
		//Button noL = noLog.GetComponent<Button> ();
		//logButton.onClick.AddListener (()=>signIn());
		//noLog.onClick.AddListener (() => jumptogame ());

		
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
		

	public void signIn(){
		
		uEmail = logname.text; 
		uPassword = logpass.text;
		auth.SignInWithEmailAndPasswordAsync (uEmail, uPassword).ContinueWith((authTask) => {
			if (authTask.IsCompleted){
				jumptogame();
			}
			else{
			}
		});
			
			
	}


	public void register(){
		uEmail = regnam.text;
		uPassword = regnam.text;
		auth.CreateUserWithEmailAndPasswordAsync(uEmail, uPassword).ContinueWith((authTask) => {
			if (authTask.IsCompleted){
				jumptogame();
			}
			else{
			}
		});
	}

	public void jumptogame(){
		Application.LoadLevel("GameStart");
	}










}
