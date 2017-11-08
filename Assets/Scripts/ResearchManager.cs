using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour {

    public GameObject researchButton;
    public GameObject dossier;
    DossierFolder dosScript;
	// Use this for initialization
	void Start () {

        dosScript = dossier.GetComponent<DossierFolder>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateResearchButton()
    {
        researchButton.SetActive(true);
    }

    void SetResearchType(int research)
    {
        for (int i = 0; i < dosScript.researchNotes.Count; i++)
        {
            if (dosScript.researchNotes[i].researchType == -1)
            {
                dosScript.researchNotes[i].researchType = research;
                break;
            }
        }
    }

    public void AddHostSiteResearch()
    {
        int researchType = 0;
        SetResearchType(researchType);
    }

    public void AddSiteTrafficResearch()
    {
        int researchType = 1;
        SetResearchType(researchType);
    }

    public void AddBlogCategoriesResearch()
    {
        int researchType = 2;
        SetResearchType(researchType);
    }

}
