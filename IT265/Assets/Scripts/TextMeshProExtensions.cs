using TMPro;

public static class TextMeshProExtensions
{
    public static void SetText(this TextMeshProUGUI tmp, int num) {
        tmp.text = $"{num}";
    }
}
