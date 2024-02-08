using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Dialogue
{
    /// <summary>
    /// Class that loads dialogue script.
    /// 
    /// There will be multiple dialogue scripts, one for each "conversation" after race.
    /// </summary>
    public class DialogueController : MonoBehaviour
    {
        [SerializeField]
        private string _dialogueScriptPath;
        [SerializeField]
        private DialogueBox _dialogueBox;

        private Dictionary<int, Message> _script;

        private Message _currentScriptDialogue = null;
        private int _currentDialogueIndex = 1;
        private bool _waitingOnInput = false;

        // Start is called before the first frame update
        void Start()
        {
            if (string.IsNullOrWhiteSpace(_dialogueScriptPath))
                throw new System.Exception("Missing dialogue script path!");

            ParseScript();

            if (_script == null || _script.Count == 0)
                throw new System.Exception("Script is empty!");
        }

        void Update()
        {
            // no dialogue, start new one
            if (_currentScriptDialogue == null)
            {
                _currentScriptDialogue = _script[_currentDialogueIndex];
            }


        }

        private void ParseScript()
        {
            var scriptFileContents = File.ReadAllText(_dialogueScriptPath);
            _script = JsonConvert.DeserializeObject<Dictionary<int, Message>>(scriptFileContents);

            //DebugScript();
        }

        private void DebugScript()
        {
            if (_script == null || _script.Count == 0)
                return;

            Debug.Log("!! Remember to click into log entry, to see full text output.");

            foreach (var test in _script)
            {
                var str = "";
                str += "\n|-- " + test.Key + ". " + test.Value.Speaker + " --|\n";
                str += "\"" + test.Value?.Text + "\"\n\n";

                if (test.Value?.Options != null)
                {
                    str += "\n|| Options: ";

                    var optionIndex = 1;
                    foreach (var option in test.Value.Options)
                    {
                        str += optionIndex + ". " + option.Text + " (goes to " + option.Goto + " )\n";
                        optionIndex++;
                    }
                }

                str += "\n";

                Debug.Log(str);
            }
        }
    }
}

