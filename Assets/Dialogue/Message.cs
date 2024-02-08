
using System.Collections.Generic;

namespace Assets.Dialogue
{
    public class Message
    {
        public string Speaker { get; set; }
        public string Text { get; set; }
        public string RenderSpeed { get; set; }
        public List<Option> Options { get; set; }
        public int Goto { get; set; }
    }
}
