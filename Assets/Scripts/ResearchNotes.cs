using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNotes : MonoBehaviour {

    public int researchType = -1;
    public Image[] contentArray;
    public Image content;
    private int prevResearchType;
  

	// Use this for initialization
	void Start () {
        prevResearchType = -1;
        foreach(Image img in contentArray)
        {
            img.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(researchType >= 0 && prevResearchType != researchType)
        {
            prevResearchType = researchType;
            Debug.Log(researchType);
            content = contentArray[researchType];
        }

    }

    public void OnMouseOver()
    {
        content.enabled = true;
    }

    public void OnMouseExit()
    {
        content.enabled = false;
    }
}
