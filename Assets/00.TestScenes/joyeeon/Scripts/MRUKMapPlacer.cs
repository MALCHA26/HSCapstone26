using System.Collections;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class MRUKMapPlacer : MonoBehaviour
{
    public Transform mapRoot;
    public float heightOffset = 0.0f;
    public float forwardOffset = 0.0f;
    public float roomLoadTimeout = 5f;

    void Start()
    {
        if (mapRoot == null)
        {
            Debug.LogError("[MRUKMapPlacer] mapRoot가 설정되지 않았습니다.");
            return;
        }

        StartCoroutine(PlaceWhenRoomReady());
    }

    IEnumerator PlaceWhenRoomReady()
    {
        var mruk = MRUK.Instance;
        if (mruk == null)
        {
            Debug.LogError("[MRUKMapPlacer] MRUK.Instance 없음. MRUKManager가 씬에 있는지 확인.");
            yield break;
        }

        MRUKRoom room = null;
        float t = 0f;

        while (room == null && t < roomLoadTimeout)
        {
            room = mruk.GetCurrentRoom();
            if (room != null) break;

            t += Time.deltaTime;
            yield return null;
        }

        if (room == null)
        {
            Debug.LogWarning("[MRUKMapPlacer] Room을 로딩하지 못했습니다. MapRoot를 (0,0,0) 기본값으로 둡니다.");
            mapRoot.position = Vector3.zero;
            mapRoot.rotation = Quaternion.identity;
            yield break;
        }

        PlaceMap(room);
    }

    private void PlaceMap(MRUKRoom room)
    {
        //  FloorAnchors는 List<MRUKAnchor> → [0].transform.position으로 중앙 접근
        if (room.FloorAnchors == null || room.FloorAnchors.Count == 0)
        {
            Debug.LogError("[MRUKMapPlacer] FloorAnchor가 없습니다.");
            return;
        }

        Vector3 basePos = room.FloorAnchors[0].transform.position;
        basePos.y += heightOffset;

        // room.Forward 없음 → GetKeyWall()로 가장 긴 벽의 노말을 forward로 사용
        Vector3 forward = Vector3.forward; // fallback
        var keyWall = room.GetKeyWall(out _);
        if (keyWall != null)
        {
            // 벽의 forward(노말)가 방 안쪽을 향하므로 그대로 사용
            forward = keyWall.transform.forward;
        }
        else if (room.WallAnchors != null && room.WallAnchors.Count > 0)
        {
            // KeyWall 못 찾으면 첫 번째 벽 노말 사용
            forward = room.WallAnchors[0].transform.forward;
        }

        forward.y = 0f;
        forward.Normalize();

        basePos += forward * forwardOffset;

        Quaternion rot = Quaternion.LookRotation(forward, Vector3.up);
        mapRoot.SetPositionAndRotation(basePos, rot);

        Debug.Log($"[MRUKMapPlacer] MapRoot placed. pos={basePos}, forward={forward}");
    }
}