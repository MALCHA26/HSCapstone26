/*
 * 작성자: 손다혜
 * 작성일: 2026.03.27
 * 역할: 지정한 위치에 프리팹을 생성
 */

using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [Header("Spawn Target Prefab")]
    public GameObject prefab;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = Vector3.zero;

    public GameObject Spawn()
    {
        if (prefab == null)
        {
            Debug.LogError("ObjectSpawn: Prefab is not assigned.");
            return null;
        }

        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}