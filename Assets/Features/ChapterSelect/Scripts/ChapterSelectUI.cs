using UnityEngine;

/// <summary>
/// 선택 화면. ProgressService에 상태를 물어 버튼에 전달하고 클릭을 GameFlow로 넘긴다.
/// 해금 규칙은 직접 판단하지 않는다.
/// </summary>
public class ChapterSelectUI : MonoBehaviour
{
    [SerializeField] ChapterDatabase database;
    [SerializeField] ChapterSlot[] slots;

    void OnEnable()  => ProgressService.OnProgressChanged += Refresh;
    void OnDisable() => ProgressService.OnProgressChanged -= Refresh;

    void Start()
    {
        if (database == null)
        {
            Debug.LogError("[ChapterSelectUI] ChapterDatabase가 연결되지 않았습니다.");
            return;
        }

        if (slots.Length != database.Count)
        {
            Debug.LogWarning($"[ChapterSelectUI] 버튼 수({slots.Length})와 챕터 수({database.Count})가 다릅니다.");
        }

        ProgressService.Load(database.Count);
        Refresh();
    }

    public void Refresh()
    {
        if (database == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            if (i >= database.Count)
            {
                slots[i].gameObject.SetActive(false);
                continue;
            }

            int index = i; // 클로저 캡처용
            slots[i].gameObject.SetActive(true);
            slots[i].Setup(database.chapters[i], ProgressService.GetState(i), () => OnButtonClicked(index));
        }
    }

    void OnButtonClicked(int index)
    {
        // interactable=false로 막혀 있어도 코드에서 한 번 더 검증
        if (!ProgressService.IsUnlocked(index)) return;

        GameFlow.EnterChapter(index, database.chapters[index]);
    }
}
