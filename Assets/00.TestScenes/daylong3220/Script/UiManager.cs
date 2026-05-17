using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject Dictation;
    private OpenAIRequester ai;
    public TextMeshProUGUI re_box;
    public TextMeshProUGUI an_box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dictation = GameObject.Find("[BuildingBlock] Dictation");
        ai = GameObject.Find("aimanager").GetComponent<OpenAIRequester>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        re_box.text = Dictation.GetComponent<VoiceListener>().Player_text;
        //an_box.text = ai.answer;
    }
    */
}
