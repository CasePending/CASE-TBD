using System;
using UnityEngine;

/// <summary>
/// 해금 규칙 판단과 진행도 저장/로드 담당
/// 진행도는 HighestClearedIndex 정수 하나로 표현 (-1 = 신규).
/// </summary>
public static class ProgressService
{
    const string Key = "HighestClearedIndex";
    const int None = -1;

    public static int HighestClearedIndex { get; private set; } = None;

    public static event Action OnProgressChanged;

    /// <summary>저장소에서 읽기. 저장값이 범위를 벗어나면 [-1, chapterCount-1]로 보정.</summary>
    public static void Load(int chapterCount)
    {
        int saved = PlayerPrefs.GetInt(Key, None);
        HighestClearedIndex = Mathf.Clamp(saved, None, chapterCount - 1);
    }

    public static bool IsUnlocked(int index) => index >= 0 && index <= HighestClearedIndex + 1;
    public static bool IsCleared(int index)  => index >= 0 && index <= HighestClearedIndex;

    public static ChapterState GetState(int index)
    {
        if (IsCleared(index))  return ChapterState.Cleared;
        if (IsUnlocked(index)) return ChapterState.Available;
        return ChapterState.Locked;
    }

    /// <summary>클리어 기록. Max 적용으로 진행도는 후퇴X, 즉시 저장</summary>
    public static void MarkCleared(int index)
    {
        int next = Mathf.Max(HighestClearedIndex, index);
        if (next == HighestClearedIndex) return;

        HighestClearedIndex = next;
        Save();
    }

    /// <summary>개발/테스트용 초기화.</summary>
    public static void ResetProgress()
    {
        HighestClearedIndex = None;
        PlayerPrefs.DeleteKey(Key);
        PlayerPrefs.Save();
        OnProgressChanged?.Invoke();
    }

    /// <summary>개발/테스트용 강제 설정 (예: 전체 해금).</summary>
    public static void DebugSetHighestCleared(int index)
    {
        HighestClearedIndex = Mathf.Max(None, index);
        Save();
    }

    static void Save()
    {
        PlayerPrefs.SetInt(Key, HighestClearedIndex);
        PlayerPrefs.Save();
        OnProgressChanged?.Invoke();
    }
}
