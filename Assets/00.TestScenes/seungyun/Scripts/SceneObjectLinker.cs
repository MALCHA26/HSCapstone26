using UnityEngine;

public class SceneObjectLinker : MonoBehaviour
{
    public static SceneObjectLinker Instance; 
    public GameObject guideCanvas;
    public GameObject gaugeCanvas;
    public GaugeManager gaugeManager;
    public STTManager sttManager;

    void Awake() { Instance = this; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
