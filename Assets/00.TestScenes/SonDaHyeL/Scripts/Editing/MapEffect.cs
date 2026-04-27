/*
 * 작성자: 손다혜
 * 작성일: 2026.04.18
 * 역할: Scene1 지도 연출 제어 
 */

using UnityEngine;
using System.Collections;

public class MapEffect : MonoBehaviour
{
    [SerializeField] private ObjectSpawn mapSpawner;

    private GameObject mapObj;
    private MaterialFade mapMaterialFade;

    // 프리팹 생성, 알파값 조정
    public IEnumerator Begin()
    {
        mapObj = mapSpawner.Spawn();
        if (mapObj == null) yield break;

        mapObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        Transform mapWallTransform = mapObj.transform.Find("MapWall");
        AlphaChange mapWallAlpha = mapWallTransform?.GetComponent<AlphaChange>();
        mapMaterialFade = mapObj.GetComponentInChildren<MaterialFade>();

        if (mapWallAlpha != null)
        {
            mapWallAlpha.Play();
            yield return new WaitForSeconds(mapWallAlpha.Duration);
        }
    }

    // 머테리얼 단계 전환
    public IEnumerator PlayStep(string narration)
    {
        bool fadeDone = false;
        if (mapMaterialFade != null)
        {
            mapMaterialFade.onComplete = () => fadeDone = true;
            StartCoroutine(mapMaterialFade.FadeNext());
        }
        else fadeDone = true;

        yield return Narrate(narration);
        yield return new WaitUntil(() => fadeDone);
    }

    public void End()
    {
        if (mapObj != null)
            Destroy(mapObj);
    }

    private IEnumerator Narrate(string text)
    {
        bool done = false;
        TTSManager.Instance.Speak(text, () => done = true);
        yield return new WaitUntil(() => done);
    }
}
