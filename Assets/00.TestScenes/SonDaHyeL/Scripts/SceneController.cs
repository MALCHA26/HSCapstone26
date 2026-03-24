using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFade;

    void Start()
    {
        if (sceneFade != null)
        {
            sceneFade.FadeIn(); 
        }
    }
}