using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Game/Chapter Data")]
public class ChapterData : ScriptableObject
{
    [Tooltip("챕터 표시 이름")]
    public string title;

    [Tooltip("선택 버튼에 표시할 이미지")]
    public Sprite thumbnail;

    [Tooltip("로드할 씬 이름 (Build Settings 등록 필수)")]
    public string sceneName;
}
