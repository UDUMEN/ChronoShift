using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        BuildUI();
    }

    void BuildUI()
    {
        var canvasGO = new GameObject("MenuCanvas");
        canvasGO.transform.SetParent(transform);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Full-screen dark background
        var bg = new GameObject("Background");
        bg.transform.SetParent(canvasGO.transform, false);
        var bgRect = bg.AddComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = bgRect.offsetMax = Vector2.zero;
        bg.AddComponent<Image>().color = new Color(0.04f, 0.03f, 0.10f, 1f);

        // Title
        var titleGO = new GameObject("Title");
        titleGO.transform.SetParent(canvasGO.transform, false);
        var tr = titleGO.AddComponent<RectTransform>();
        tr.anchorMin = new Vector2(0.1f, 0.62f);
        tr.anchorMax = new Vector2(0.9f, 0.82f);
        tr.offsetMin = tr.offsetMax = Vector2.zero;
        var title = titleGO.AddComponent<TextMeshProUGUI>();
        title.text = "<color=#C880FF>Chrono</color><color=#FFFFFF>Shift</color>";
        title.fontSize = 96f;
        title.alignment = TextAlignmentOptions.Center;
        title.fontStyle = FontStyles.Bold;
        title.richText = true;

        // Subtitle
        var subGO = new GameObject("Subtitle");
        subGO.transform.SetParent(canvasGO.transform, false);
        var sr = subGO.AddComponent<RectTransform>();
        sr.anchorMin = new Vector2(0.1f, 0.55f);
        sr.anchorMax = new Vector2(0.9f, 0.63f);
        sr.offsetMin = sr.offsetMax = Vector2.zero;
        var sub = subGO.AddComponent<TextMeshProUGUI>();
        sub.text = "Zamanı Yönet, Sırrı Çöz";
        sub.fontSize = 36f;
        sub.alignment = TextAlignmentOptions.Center;
        sub.color = new Color(0.78f, 0.65f, 1f, 0.85f);
        sub.fontStyle = FontStyles.Italic;

        // Decorative line
        var lineGO = new GameObject("Line");
        lineGO.transform.SetParent(canvasGO.transform, false);
        var lr = lineGO.AddComponent<RectTransform>();
        lr.anchorMin = new Vector2(0.3f, 0.535f);
        lr.anchorMax = new Vector2(0.7f, 0.540f);
        lr.offsetMin = lr.offsetMax = Vector2.zero;
        lineGO.AddComponent<Image>().color = new Color(0.8f, 0.45f, 1f, 0.5f);

        // Play button
        MakeButton(canvasGO.transform, "PlayBtn", "OYNA",
            new Vector2(0.35f, 0.38f), new Vector2(0.65f, 0.50f),
            new Color(0.45f, 0.15f, 0.80f, 1f),
            new Color(0.60f, 0.25f, 1.00f, 1f),
            () => StartCoroutine(LoadLevel()));

        // Exit button
        MakeButton(canvasGO.transform, "ExitBtn", "ÇIKIŞ",
            new Vector2(0.35f, 0.24f), new Vector2(0.65f, 0.36f),
            new Color(0.18f, 0.10f, 0.28f, 1f),
            new Color(0.30f, 0.15f, 0.45f, 1f),
            () => Application.Quit());
    }

    void MakeButton(Transform parent, string id, string label,
                    Vector2 anchorMin, Vector2 anchorMax,
                    Color normalColor, Color highlightColor,
                    UnityEngine.Events.UnityAction onClick)
    {
        var btnGO = new GameObject(id);
        btnGO.transform.SetParent(parent, false);
        var rect = btnGO.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = rect.offsetMax = Vector2.zero;

        var img = btnGO.AddComponent<Image>();
        img.color = normalColor;

        var btn = btnGO.AddComponent<Button>();
        var colors = btn.colors;
        colors.normalColor    = normalColor;
        colors.highlightedColor = highlightColor;
        colors.pressedColor   = new Color(highlightColor.r * 0.7f, highlightColor.g * 0.7f, highlightColor.b * 0.7f);
        colors.selectedColor  = highlightColor;
        colors.colorMultiplier = 1f;
        btn.colors = colors;
        btn.targetGraphic = img;
        btn.onClick.AddListener(onClick);

        var txtGO = new GameObject("Label");
        txtGO.transform.SetParent(btnGO.transform, false);
        var tr = txtGO.AddComponent<RectTransform>();
        tr.anchorMin = Vector2.zero; tr.anchorMax = Vector2.one;
        tr.offsetMin = tr.offsetMax = Vector2.zero;
        var txt = txtGO.AddComponent<TextMeshProUGUI>();
        txt.text = label;
        txt.fontSize = 40f;
        txt.fontStyle = FontStyles.Bold;
        txt.alignment = TextAlignmentOptions.Center;
        txt.color = Color.white;
    }

    IEnumerator LoadLevel()
    {
        yield return null;
        SceneManager.LoadScene("Level1");
    }
}
