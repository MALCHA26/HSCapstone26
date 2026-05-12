/*
 * 작성자: 손다혜
 * 작성일: 2026.04.08
 * 수정 : 2026.04.27
 * 역할: Scene1 흐름 제어
 */

using UnityEngine;
using System.Collections;

public class Scene1Controller : AllSceneController
{
    [Header("Scene1 Components")]
    [SerializeField] private PaperDrop paperDrop;
    [SerializeField] private TextureChange textureChange;
    [SerializeField] private MapEffect mapEffect;
    [SerializeField] private ObjectSpawn printerSpawner;
    [SerializeField] protected Paper paper;


    [Header("캔버스 제어")]
    [SerializeField] private GameObject videoCanvas;

    private bool screenActive = false;

    protected override IEnumerator RunSequence()
    {

        
        // 1. 영상 재생
        bool videoDone = false;
        videoPlayer.onComplete = () => videoDone = true;

        yield return Narrate("지금 보이는 장면은 우리가 알고 있는 역사와는 사뭇 다른 모습입니다. 만약 우리의 외침이 세상에 닿지 못했다면, 실제 역사 또한 이처럼 어둡게 남았을지도 모릅니다.이러한 암울한 가능성을 밀어낼 수 있었던 것은, 역사를 바꾼 작은 불씨가 있었기 때문입니다."); //Scene1_1
        yield return new WaitUntil(() => videoDone);
        yield return VideoFadeTransition();

        //비디오 캔버스 비활성화
        videoCanvas.SetActive(false);

       yield return Narrate("1919년 3월 1일. 전국 곳곳에서 울려 퍼진 '대한독립만세'의 함성.");

        // 2. 종이 FallPaper 애니메이션 실행
        bool dropDone = false;
        paperDrop.onComplete = () => dropDone = true;
        paperDrop.Drop();
        soundManager.Play("PaperFall", 0.5f);
        yield return Narrate("이 거대한 역사의 물결은 오늘 밤 이곳에서 찍어낸 3만 5천 장의 '기미독립선언서'가 없었다면 시작되기 어려웠을 것입니다.");

        yield return new WaitUntil(() => dropDone);
        
        // 3. 종이 잡기 대기
        bool grabbed = false;
        paper.onGrabbed = () => grabbed = true;
        yield return new WaitUntil(() => grabbed);

        
        // 4. 스크린 연출
        yield return Narrate("기미독립선언서는 이렇게 시작합니다.");
        soundManager.Play("PageFlip", 1f);
        yield return PlayScreen("우리는 이에 우리 조선이 독립국임과 조선인이 이 나라의 주인임을 선언한다.");
        yield return Narrate("여러분이 손에 쥐고 계신 가벼운 종이 한 장에는, 우리 민족이 스스로의 정당한 권리를 회복하고 후손들에게 억압이 아닌 온전한 자유와 행복을 물려주고자 했던 간절한 염원이 담겨 있습니다.");

        yield return Narrate("그렇다면 어떻게 일제의 엄중한 감시를 피해, 선언서를 무려 3만 5천 장이나 인쇄할 수 있었던 것일까요?");
        soundManager.Play("PageFlip", 1f);
        yield return PlayScreen("그 중심에는 인쇄소 '보성사'의 사장이자 민족대표 33인 중 한 분이었던 이종일 선생의 결단이 있었습니다.");
        yield return Narrate("선언서의 인쇄를 맡기로 한 이종일 선생은 2월 20일부터 천도교 인쇄소인 보성사에서 독립선언문 인쇄를 시작했습니다.");
        yield return EndScreen();

        // 5. 지도 연출
        soundManager.Play("PageFlip", 1f);
        yield return StartCoroutine(mapEffect.Begin());
        yield return PlayMap("좁은 인쇄소 안에서 문을 굳게 닫은 채, 평판 인쇄기로 한 장 한 장 찍어내어 25일까지 1차로 2만 5천 장, 이어 2차로 1만 장이 더 인쇄되었습니다.");
        yield return PlayMap("이렇게 인쇄된 선언서는 비밀리에 경운동에 있던 이종일 선생의 자택으로 옮겨졌습니다.");
        yield return PlayMap("선언서는 이후 여러 독립운동가들의 손을 거쳐 2월 28일 아침부터 전국 각지로 운반될 수 있었습니다.");
        yield return PlayMap("이 모든 노력이 모여 3·1운동 당일, 태화관에서 민족대표 33인이 독립선언식을 거행할 수 있는 기반이 마련될 수 있었던 것입니다.");
        mapEffect.End();

        

        // 6. 인쇄기 스폰
        soundManager.Play("Typing", 0.1f);
        yield return Narrate("이제 여러분은 과거로 돌아가, 이종일 선생의 시점에서 그 긴박한 순간을 직접 체험하시게 될 겁니다. 부디 이 거대한 역사의 불꽃이 무사히 세상 밖으로 나갈 수 있도록 도와주세요.");
        printerSpawner.Spawn();
    }

    // 스크린 FadeIn + 나레이션
    private IEnumerator PlayScreen(string narration)
    {
        if (screenActive)
        {
            bool fadeOutDone = false;
            textureChange.onFadeOutComplete = () => fadeOutDone = true;
            textureChange.FadeOut();
            yield return new WaitUntil(() => fadeOutDone);
        }

        bool fadeInDone = false;
        textureChange.onFadeInComplete = () => fadeInDone = true;
        textureChange.PlayNext();
        yield return new WaitUntil(() => fadeInDone);
        screenActive = true;

        yield return Narrate(narration);
    }

    // 스크린 종료
    private IEnumerator EndScreen()
    {
        bool fadeOutDone = false;
        textureChange.onFadeOutComplete = () => fadeOutDone = true;
        textureChange.FadeOut();
        yield return new WaitUntil(() => fadeOutDone);
        screenActive = false;
    }

    // 지도 이펙트 + 나레이션
    private IEnumerator PlayMap(string narration)
    {
        yield return StartCoroutine(mapEffect.PlayStep(narration));
    }

    // 씬 전환 (사운드 포함)
    public override void LoadNextScene()
    {
        sceneFade.FadeOut();
        soundManager.Play("Printer", 0.7f);
        Invoke(nameof(LoadNextSceneDelayed), 3f);
    }

    private void LoadNextSceneDelayed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
