using UnityEngine;
using System.Collections;

public class Scene3_2Controller : AllSceneController
{
    [SerializeField] protected Paper paper;
    protected override IEnumerator RunSequence()
    {
        //bool grabbed = false;
        //paper.onGrabbed = () => grabbed = true;
        //yield return new WaitUntil(() => grabbed);
        yield return 0;
    }
    
}
