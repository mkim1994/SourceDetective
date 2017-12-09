using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {

    public AudioSource music;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(music);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame(){
        SceneManager.LoadScene("OpeningScene");
    }
    public void Quit(){
        Application.Quit();
    }
}
