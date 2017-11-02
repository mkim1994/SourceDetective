using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchButton : MonoBehaviour {

    public GameObject researchButton;
    bool researching;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Research()
    {
        if (!researching)
        {
            Debug.Log("AH SHIT I'M RESEARCHING");
            researchButton.SetActive(true);
            researching = true;
        }
        else if (researching)
        {
            researchButton.SetActive(false);
        }

    }
}
