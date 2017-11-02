using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour {

    public GameObject researchButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateResearchButton()
    {
        researchButton.SetActive(true);
    }

    public void AddResearch(int i)
    {

    }
}
