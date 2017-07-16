using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public void PlayAgainmethod() {
        Application.LoadLevel("GameContent");
    }

	public void PlayAgainmethod1() {
		Application.LoadLevel("GameContent 1");
	}

	public void PlayAgainmethod2() {
		Application.LoadLevel("GameContent 2");
	}

	public void GotoStartMenumethod(){
		Application.LoadLevel ("GameStart");
	}

}
