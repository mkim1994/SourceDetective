using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AC;

public class DossierFolder : MonoBehaviour
{

    public GameObject Dossier;
    public bool isOpen;
    public GameObject notificationSymbol;
    public List<ResearchNotes> researchNotes = new List<ResearchNotes>(5);
    private static int clickCount_;
    private static int clickCount
    {
        get { return clickCount_; }
        set
        {
            clickCount_ = value;
            Debug.Log("Setting click count to " + value);
        }
    }

    // Use this for initialization
    void Start()
    {
        foreach (ResearchNotes note in researchNotes)
        {
            note.gameObject.SetActive(false);
        }
        clickCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < researchNotes.Count; i++)
        {
            if (researchNotes[i].researchType != -1)
            {
                researchNotes[i].gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            clickCount += 1;
            //Debug.Log("ClickCount = " + clickCount);
        }

    }

    public void OpenAndCloseDossier()
    {
        if (!AC.KickStarter.stateHandler.IsInCutscene())
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
}
