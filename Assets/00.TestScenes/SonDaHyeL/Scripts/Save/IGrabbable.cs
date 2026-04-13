/*
 * 역할: 잡힐 수 있는 오브젝트 인터페이스
 *       VR 전환 시 PickUp.cs만 교체하면 되도록 분리
 */

public interface IGrabbable
{
    void OnGrab();
    void OnRelease();
}
