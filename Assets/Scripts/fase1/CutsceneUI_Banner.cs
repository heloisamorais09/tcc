// Assets/Scripts/CutsceneUI_Banner.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class CutsceneSlideBanner
{
    [TextArea] public string text;
    public Sprite character;
    public Sprite bg;
}

public class CutsceneUI_Banner : MonoBehaviour
{
    [Header("Referências")]
    public GameObject panel;              // CutscenePanel
    public Image backgroundImage;         // VisualLayer/BackgroundImage
    public Image characterImage;          // VisualLayer/CharacterImage
    public RectTransform bannerPanel;     // BannerPanel
    public Image bannerBackground;        // Image do BannerPanel
    public TextMeshProUGUI bannerText;    // BannerText
    public Button nextButton;             // NextButton

    [Header("Digitação")]
    public float typeSpeed = 0.035f;
    public bool forceUppercase = true;
    public float characterSpacing = 12f;
    public bool bold = true;
    public bool autoSize = true;
    public float autoMin = 36f, autoMax = 110f;

    List<CutsceneSlideBanner> slides; int index = -1; System.Action onEnd;
    bool typing, fast;

    void Awake()
    {
        if (nextButton) nextButton.onClick.AddListener(() => { if (typing) fast = true; else Next(); });
        if (panel && panel.activeSelf) panel.SetActive(false);
    }

    public void Show(List<CutsceneSlideBanner> list, System.Action onEnd)
    {
        if (!panel || !backgroundImage || !characterImage || !bannerText || !nextButton)
        {
            Debug.LogError("CutsceneUI_Banner: faltam referências no Inspector."); return;
        }
        slides = list; this.onEnd = onEnd; index = -1;
        panel.SetActive(true);
        Next();
    }

    void Next()
    {
        index++;
        if (slides == null || index >= slides.Count)
        {
            if (panel) panel.SetActive(false);
            onEnd?.Invoke();
            return;
        }
        var s = slides[index];

        backgroundImage.sprite = s.bg; backgroundImage.enabled = (s.bg != null);
        backgroundImage.color = Color.white;

        characterImage.sprite = s.character; characterImage.enabled = (s.character != null);
        characterImage.color = Color.white;

        // estilo texto
        bannerText.enableAutoSizing = autoSize;
        if (autoSize) { bannerText.fontSizeMin = autoMin; bannerText.fontSizeMax = autoMax; }
        bannerText.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
        bannerText.characterSpacing = characterSpacing;
        bannerText.enableWordWrapping = true;
        bannerText.overflowMode = TextOverflowModes.Overflow;

        string line = forceUppercase ? s.text.ToUpper() : s.text;
        StopAllCoroutines(); StartCoroutine(Type(line));
    }

    System.Collections.IEnumerator Type(string line)
    {
        typing = true; fast = false; bannerText.text = "";
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '<') { int c = line.IndexOf('>', i); if (c < 0) c = line.Length - 1; bannerText.text += line.Substring(i, c - i + 1); i = c; continue; }
            bannerText.text += line[i];
            if (fast) { bannerText.text += line.Substring(i + 1); break; }
            yield return new WaitForSeconds(typeSpeed);
        }
        typing = false;
    }
}
