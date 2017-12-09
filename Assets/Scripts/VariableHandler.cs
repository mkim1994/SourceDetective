using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableHandler : MonoBehaviour {

    DossierFolder dosScript;

    public List<ResearchNotes> researchNotes;

    private bool endSceneRan = false;
    public bool runEvaluations;

    public Sprite[] correctSprites = new Sprite[6];

    // Use this for initialization
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        //correctSprites[0] = Resources.Load("Art/Research/" + "Case1Correct1.png") as Sprite;
        //correctSprites[1] = Resources.Load("Art/Research/" + "Case1Correct2.png") as Sprite;
        //correctSprites[2] = Resources.Load("Art/Research/" + "Case1Correct3.png") as Sprite;
        //correctSprites[3] = Resources.Load("Art/Research/" + "Case2Correct1.png") as Sprite;
        //correctSprites[4] = Resources.Load("Art/Research/" + "Case2Correct2.png") as Sprite;
        //correctSprites[5] = Resources.Load("Art/Research/" + "Case2Correct3.png") as Sprite;
        runEvaluations = false;
    }
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(endSceneRan && runEvaluations){
            runEvaluations = false;
            int correctCount = 0;
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

            for (int i = 0; i < 3; ++i){
                notes[i].sprite = researchNotes[i].content.sprite;
                bool foundMatch = false;
                for (int researchSprites = 0; researchSprites < correctSprites.Length; researchSprites++)
                {
                    if (!foundMatch)
                    {
                        if (notes[i].sprite == correctSprites[researchSprites])
                        {
                            Debug.Log("right");
                            texts[i].text = "This is very interesting indeed! Good find!";
                            foundMatch = true;
                            correctCount += 1;
                        }
                        else
                        {
                            Debug.Log("wrong");
                            texts[i].text = "This doesn't seem like it's applicable to the investigation.";
                        }
                    }
                }
            }
            if (correctCount < 3) final.text = "I'm not sure you were very thorough. Maybe I shouldn't have called you.";
            else final.text = "Amazing work detective! I'll make sure everyone knows how thorough you are.";
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

    public void RunEvalutation()
    {
        runEvaluations = true;
    }
}
