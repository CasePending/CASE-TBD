using UnityEngine;

[CreateAssetMenu(fileName = "ChapterDatabase", menuName = "Game/Chapter Database")]
public class ChapterDatabase : ScriptableObject
{
    [Tooltip("챕터 목록. 배열 순서가 곧 챕터(해금) 순서")]
    public ChapterData[] chapters;

    public int Count => chapters != null ? chapters.Length : 0;
}
