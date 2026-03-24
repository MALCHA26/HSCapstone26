using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene2";
    [SerializeField] private SceneFade sceneFade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PRINTER"))
        {
            Destroy(gameObject);
            sceneFade.FadeOutAndLoad(sceneName);
        }
    }
}