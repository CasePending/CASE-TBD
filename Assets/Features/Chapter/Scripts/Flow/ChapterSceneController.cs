using TMPro;
using UnityEngine;

/// <summary>
/// 챕터 씬 측 연결 예시. 각 Chapter 씬에 하나 배치
/// 클리어 조건 달성 시 Complete(), 포기 시 Quit()을 호출
/// </summary>
public class ChapterSceneController : MonoBehaviour
{

    [Tooltip("선택: 진입한 챕터 번호 표시 (테스트 씬용)")]
    [SerializeField] TMP_Text chapterLabel;

    void Start()
    {
        int index = GameFlow.CurrentChapterIndex;
        Debug.Log($"[Chapter] {index + 1}챕터 시작");
        // 챕터별 초기화가 필요하면 여기서 index 기준으로 수행

        // 테스트씬 용 코드
        if (chapterLabel != null) chapterLabel.text = $"Chapter {index + 1}";
    }

    /// <summary>클리어 조건 달성 시 호출 (UI 버튼/트리거 등에서 연결).</summary>
    public void Complete() => GameFlow.CompleteChapter();

    /// <summary>도중 포기/종료. 진행도 변경 없음.</summary>
    public void Quit() => GameFlow.ReturnToSelect();
}
