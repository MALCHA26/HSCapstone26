using UnityEngine;
using System.Collections;

public class Scene3_2Controller : AllSceneController
{
    [SerializeField] private Paper paper;

    protected override IEnumerator RunSequence()
    {
        yield return StartCoroutine(soundManager.PlayAndWait("Scene3_2", 0.4f));
    }

}
