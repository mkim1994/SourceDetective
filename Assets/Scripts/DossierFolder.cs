using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DossierFolder : MonoBehaviour {

    public GameObject Dossier;
    public bool isOpen;
    public GameObject notificationSymbol;
    public List<ResearchNotes> researchNotes = new List<ResearchNotes>(5);

	// Use this for initialization
	void Start () {
        foreach(ResearchNotes note in researchNotes)
        {
            note.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < researchNotes.Count; i++)
        {
            if (researchNotes[i].researchType != -1)
            {
                researchNotes[i].gameObject.SetActive(true);
            }
        }

    }

    public void OpenAndCloseDossier()
    {
        if (!isOpen)
        {
            notificationSymbol.SetActive(false);
            isOpen = true;
            Dossier.SetActive(true);
        }
        else if (isOpen)
        {
            isOpen = false;
            Dossier.SetActive(false);
        }
    }
}
