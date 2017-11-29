﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHandler : MonoBehaviour {

    DossierFolder dosScript;

    public List<ResearchNotes> researchNotes;

	// Use this for initialization
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EvaluateResearch(){
        dosScript = GameObject.Find("ResearchManager").GetComponent<DossierFolder>();
        //dosScript.researchNotes;
        researchNotes = dosScript.researchNotes;
    }
}
