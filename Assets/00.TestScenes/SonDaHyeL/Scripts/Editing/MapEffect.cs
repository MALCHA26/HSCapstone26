using UnityEngine;
using System.Collections;

public class MapEffect : MonoBehaviour
{
    [SerializeField] private ObjectSpawn mapSpawner;

    public IEnumerator Play(string narration = null)
    {
        // 1. Map 프리팹 스폰
        GameObject mapObj = mapSpawner.Spawn();
        if (mapObj == null) yield break;

        // Y축 180도 회전
        mapObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        Transform mapWallTransform = mapObj.transform.Find("MapWall");
        AlphaChange mapWallAlpha = mapWallTransform?.GetComponent<AlphaChange>();
        MaterialFade mapMaterialFade = mapObj.GetComponentInChildren<MaterialFade>();

        // 2. MapWall AlphaChange
        if (mapWallAlpha != null)
        {
            mapWallAlpha.Play();
            yield return new WaitForSeconds(mapWallAlpha.Duration);
        }

        // 3. 나레이션 + MaterialFade 동시 시작, 둘 다 종료 대기
        bool fadeDone = false;
        bool narrationDone = narration == null; // 나레이션 없으면 바로 완료 처리

        if (mapMaterialFade != null)
        {
            mapMaterialFade.onComplete = () => fadeDone = true;
            StartCoroutine(mapMaterialFade.PlayAndWait());
        }
        else
        {
            fadeDone = true;
        }

        if (narration != null)
            TTSManager.Instance.Speak(narration, () => narrationDone = true);

        yield return new WaitUntil(() => fadeDone && narrationDone);

        Destroy(mapObj);
    }
}
