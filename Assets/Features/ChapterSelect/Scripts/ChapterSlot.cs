using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 챕터 슬롯(카드) 하나. 루트 오브젝트에 붙이고,
/// 하위의 제목·썸네일·입장 버튼·아이콘을 Inspector로 연결해 한 곳에서 관리한다.
/// </summary>
public class ChapterSlot : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] TMP_Text titleText;
    [SerializeField] Image thumbnailImage;
    [SerializeField] Button enterButton;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject clearIcon;

    [Header("Locked Look")]
    [Tooltip("잠김 시 카드 전체를 흐리게 (선택, 루트에 CanvasGroup)")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField, Range(0f, 1f)] float lockedAlpha = 0.5f;
    [SerializeField] Color lockedThumbnailColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    public void Setup(ChapterData data, ChapterState state, Action onClick)
    {
        bool isLocked = state == ChapterState.Locked;
        bool isCleared = state == ChapterState.Cleared;

        if (titleText != null)
            titleText.text = data != null ? data.title : "???";

        if (thumbnailImage != null)
        {
            thumbnailImage.sprite = data != null ? data.thumbnail : null;
            thumbnailImage.color = isLocked ? lockedThumbnailColor : Color.white;
        }

        if (lockIcon != null) lockIcon.SetActive(isLocked);
        if (clearIcon != null) clearIcon.SetActive(isCleared);
        if (canvasGroup != null) canvasGroup.alpha = isLocked ? lockedAlpha : 1f;

        if (enterButton != null)
        {
            enterButton.interactable = !isLocked;
            enterButton.onClick.RemoveAllListeners();
            if (onClick != null) enterButton.onClick.AddListener(() => onClick());
        }
    }
}