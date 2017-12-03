using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCheckMark : MonoBehaviour {

    public GameObject checkMark;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckMark()
    {
        checkMark.SetActive(true);
    }
}
