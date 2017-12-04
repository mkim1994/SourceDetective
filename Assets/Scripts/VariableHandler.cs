using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableHandler : MonoBehaviour {

    DossierFolder dosScript;

    public List<ResearchNotes> researchNotes;

    private bool endSceneRan = false;

	// Use this for initialization
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(endSceneRan){
            Debug.Log("wht");
            GameObject prefab = Resources.Load("Prefabs/ResearchEvaluation") as GameObject;
            GameObject evaluation = GameObject.Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            Image[] notes = new Image[3];
            Text[] texts = new Text[3];
            notes[0] = evaluation.transform.GetChild(1).GetComponent<Image>();
            notes[1] = evaluation.transform.GetChild(2).GetComponent<Image>();
            notes[2] = evaluation.transform.GetChild(3).GetComponent<Image>();

            texts[0] = notes[0].transform.GetChild(0).GetComponent<Text>();
            texts[1] = notes[1].transform.GetChild(0).GetComponent<Text>();
            texts[2] = notes[2].transform.GetChild(0).GetComponent<Text>();
            Text final = evaluation.transform.GetChild(4).GetComponent<Text>();

            int count = 0;
            for (int i = 0; i < 3; ++i){
                notes[i].sprite = researchNotes[i].content.sprite;
               /* switch(i){
                    case 0:
                        note1.sprite = researchNotes[0].content.sprite;
                        break;
                    case 1:
                        note2.sprite = researchNotes[1].content.sprite;
                        break;
                    case 2:
                        note3.sprite = researchNotes[2].content.sprite;
                        break;
                    case 3:
                        note4.sprite = researchNotes[3].content.sprite;
                        break;
                    case 4:
                        note5.sprite = researchNotes[4].content.sprite;
                        break;
                }*/
            }



            endSceneRan = false;
        }
	}

    public void EvaluateResearch(List<ResearchNotes> r){
        endSceneRan = true;
        researchNotes = new List<ResearchNotes>();
        //dosScript.researchNotes;
        researchNotes = r;
        Debug.Log(researchNotes[0].content.sprite);
        Debug.Log(researchNotes[1].content.sprite);
        Debug.Log(researchNotes[2].content.sprite);
    }
}
