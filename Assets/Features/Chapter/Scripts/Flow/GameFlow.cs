using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 현재 챕터 인덱스 보관 & 씬 전환 담당
/// </summary>
public static class GameFlow
{
    public const string SelectSceneName = "ChapterSelect";

    /// <summary>현재 진행 중인 챕터. 에디터에서 챕터 씬을 직접 실행해도 0으로 동작.</summary>
    public static int CurrentChapterIndex { get; private set; } = 0;

    public static void EnterChapter(int index, ChapterData data)
    {
        if (data == null)
        {
            Debug.LogError($"[GameFlow] ChapterData가 비어 있습니다 (index {index}).");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(data.sceneName))
        {
            Debug.LogError($"[GameFlow] 씬 '{data.sceneName}'을 로드할 수 없습니다. 이름 오타 또는 Build Settings 미등록을 확인하세요.");
            return;
        }

        CurrentChapterIndex = index;
        SceneManager.LoadScene(data.sceneName);
    }

    /// <summary>진행도 변경 없이 선택 화면으로 복귀 (포기/종료).</summary>
    public static void ReturnToSelect()
    {
        SceneManager.LoadScene(SelectSceneName);
    }

    /// <summary>현재 챕터 클리어 기록 후 선택 화면으로 복귀.</summary>
    public static void CompleteChapter()
    {
        ProgressService.MarkCleared(CurrentChapterIndex);
        ReturnToSelect();
    }
}
