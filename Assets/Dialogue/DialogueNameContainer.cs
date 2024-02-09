using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNameContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private RawImage _nameImage;
    [SerializeField]
    private AnimationClip _nameAnimationClip;
    [SerializeField]
    private Animation _nameAnimationShow;

    private string _name = "";
    private Texture _profileImage = null;
    private bool _shown = false;

    private void Start()
    {
        // shouldn't have used legacy animation but whatever
        _nameAnimationShow.AddClip(_nameAnimationClip, "test");
    }

    public void TriggerShowName(string name, Texture profileImage = null)
    {
        _shown = true;
        _nameText.text = name;

        if (profileImage != null)
        {
            _nameImage.texture = profileImage;
        }

        this.gameObject.SetActive(true);
    }

    public void HideName()
    {
        this.gameObject.SetActive(false);
        _nameText.text = string.Empty;
    }

    // EVEN HANDLER DONT DELETE
    private void NameShownEventHandler()
    {
        _shown = true;
        _nameText.text = _name;

        if (_profileImage != null)
        {
            _nameImage.texture = _profileImage;
        }

        this.gameObject.SetActive(true);
    }
}
