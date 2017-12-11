using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {

    public AudioSource music;

	// Use this for initialization
	void Start () {
        GameObject instance = GameObject.Find("Music(Clone)");
        if (instance == null)
        {
            GameObject prefab = Resources.Load("Prefabs/Music") as GameObject;
            instance = GameObject.Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            music = instance.GetComponent<AudioSource>();
            DontDestroyOnLoad(instance);
        }
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
