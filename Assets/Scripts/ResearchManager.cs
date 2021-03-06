﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AC;
using UnityEngine.SceneManagement;

public class ResearchManager : MonoBehaviour {

    public GameObject researchButton;
    public GameObject dossier;
    DossierFolder dosScript;
    public float researchCount = 0;
    public GameObject researchEvaluation;

    public GameObject variableHandlerPrefab;
	// Use this for initialization
	void Start () {

        dosScript = dossier.GetComponent<DossierFolder>();
        if(GameObject.Find("VariableHandler(Clone)") == null){
            GameObject.Instantiate(variableHandlerPrefab, variableHandlerPrefab.transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndScene()
    {
        Debug.Log("end scene"); 
        GameObject.Find("VariableHandler(Clone)").GetComponent<VariableHandler>().EvaluateResearch(dosScript.researchNotes);
    }

    public void ActuallyEndScene()
    {
        GameObject.Find("VariableHandler(Clone)").GetComponent<VariableHandler>().RunEvalutation();
    }

    public void Restart()
    {
        AC.KickStarter.variablesManager.GetVariable(0).SetValue(0);
        GameObject.Find("VariableHandler(Clone)").GetComponent<VariableHandler>().DestroyThis();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Titlescreen");

    }

    public void ActivateResearchButton()
    {
        researchButton.SetActive(true);
    }

    void SetResearchType(int research)
    {
        for (int i = 0; i < dosScript.researchNotes.Count; i++)
        {
            if (dosScript.researchNotes[i].researchType == -1 && researchCount < 3)
            {
                dosScript.researchNotes[i].researchType = research;
                dosScript.researchNotes[i].OutsideUpdate();
                dosScript.notificationSymbol.SetActive(true);
                Debug.Log("adding research!");

                break;
            }
        }
    }

    public void AddAnyoneCanRegister()
    {
        int researchType = 0;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddNermWorkInfo()
    {
        int researchType = 1;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddOtherOpenDomains()
    {
        int researchType = 2;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddWhatAreCOMS()
    {
        int researchType = 3;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddNermDegreeInfo()
    {
        int researchType = 4;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddWhatIsURL()
    {
        int researchType = 5;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddMelvinWorkInfo()
    {
        int researchType = 6;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddUniversityURLInfo()
    {
        int researchType = 7;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddMelvinExperience()
    {
        int researchType = 8;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddRobotBlogInfo()
    {
        int researchType = 9;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddMelvinDegreeInfo()
    {
        int researchType = 10;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddRootOfURLInfo()
    {
        int researchType = 11;
        SetResearchType(researchType);
        researchCount += 1;

    }

    public void AddNermHobbie()
    {
        int researchType = 12;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddUpToDate()
    {
        int researchType = 13;
        SetResearchType(researchType);
        researchCount += 1;
    }

    public void AddVerifiedUni()
    {
        int researchType = 14;
        SetResearchType(researchType);
        researchCount += 1;
    }

}
