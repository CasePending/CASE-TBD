#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>상단 메뉴 Tools/Progress 에서 진행도 초기화/강제 해금.</summary>
public static class ProgressDebugMenu
{
    [MenuItem("Tools/Progress/Reset Progress")]
    static void ResetProgress()
    {
        ProgressService.ResetProgress();
        Debug.Log("[Progress] 진행도 초기화 완료");
    }

    [MenuItem("Tools/Progress/Unlock All (Clear 1~4)")]
    static void UnlockAll()
    {
        ProgressService.DebugSetHighestCleared(3); // 5챕까지 입장 가능
        Debug.Log("[Progress] 5챕까지 해금");
    }

    [MenuItem("Tools/Progress/Clear All")]
    static void ClearAll()
    {
        ProgressService.DebugSetHighestCleared(4);
        Debug.Log("[Progress] 전체 클리어 처리");
    }

    [MenuItem("Tools/Progress/Log Current")]
    static void LogCurrent()
    {
        Debug.Log($"[Progress] HighestClearedIndex = {PlayerPrefs.GetInt("HighestClearedIndex", -1)}");
    }
}
#endif
