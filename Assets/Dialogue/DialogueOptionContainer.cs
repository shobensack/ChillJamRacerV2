using TMPro;
using UnityEngine;

public class DialogueOptionContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _optionText;

    private int _gotoIndex = 0;

    public void ShowOption(string optionText, int gotoIndex)
    {
        _optionText.text = optionText;
        _gotoIndex = gotoIndex;

        this.gameObject.SetActive(true);
    }

    public void HideOption()
    {
        _optionText.text = string.Empty;
        _gotoIndex = 0;

        this.gameObject.SetActive(false);
    }
}
