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

    [Header("Sounds")]
    [SerializeField] private string[] sounds;

    private GameObject mapObj;
    private MaterialFade mapMaterialFade;

    public IEnumerator PlaySequence()
    {
        // 1. 지도 생성
        mapObj = mapSpawner.Spawn();
        if (mapObj == null) yield break;

        mapObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        Transform mapWallTransform = mapObj.transform.Find("MapWall");
        AlphaChange mapWallAlpha = mapWallTransform?.GetComponent<AlphaChange>();
        mapMaterialFade = mapObj.GetComponentInChildren<MaterialFade>();

        // 2. MapWall AlphaChange
        if (mapWallAlpha != null)
        {
            mapWallAlpha.Play();
            yield return new WaitForSeconds(mapWallAlpha.Duration);
        }

        // 3. 머테리얼 전환 + 나레이션 진행
        for (int i = 0; i < sounds.Length; i++)
        {
            bool fadeDone = false;
            if (mapMaterialFade != null)
            {
                mapMaterialFade.onComplete = () => fadeDone = true;
                StartCoroutine(mapMaterialFade.FadeNext());
            }
            else fadeDone = true;

            // 나레이션 재생 + 종료 대기
            yield return StartCoroutine(SoundManager.Instance.PlayAndWait(sounds[i], 0.4f));

            // 머테리얼 전환 완료 대기
            yield return new WaitUntil(() => fadeDone);
        }

        // 4. 정리
        Destroy(mapObj);
    }

    public void End()
    {
        if (mapObj != null)
            Destroy(mapObj);
    }
}
