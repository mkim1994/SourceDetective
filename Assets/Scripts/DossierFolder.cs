using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DossierFolder : MonoBehaviour {

    public GameObject Dossier;
    public bool isOpen;

	// Use this for initialization
	void Start () {

        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenAndCloseDossier()
    {
        if (!isOpen)
        {
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
