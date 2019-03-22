using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanna_Studio
{
    [Serializable]
    public class Sequence
    {
        public string id { get; set; }
        public string type { get; set; }
        public string mainText { get; set; }
        public string secondaryText { get; set; }

        public Dictionary<string, Choice> choices { get; set; }

        public Sequence()
        {
            this.type = "ordinary";
            this.mainText = "Main Text Body";
            this.secondaryText = "Secondary Text Body";
            this.choices = new Dictionary<string, Choice>();
        }
    }

    [Serializable]
    public class Choice
    {

        public string letter { get; set; }
        public string type { get; set; }
        public List<string> condition { get; set; }
        // Condition has to have Container and the Value
        // Let's just store it as a List<string> with 2 values, index 1 container index 2 value
        public string text { get; set; }
        public string outcometext { get; set; }

        public string containerAdd { get; set; }
        public string containerAddValue { get; set; }


        public string nextSq { get; set; }

        public Choice(string letter, string type,List<string> condition, string text, string outcometext, string containerAdd, string containerAddValue, string nextSq)
        {
            this.letter = letter;
            this.type = type;
            this.condition = condition;
            this.text = text;
            this.outcometext = outcometext;

            // containerAdd stuff
            this.containerAdd = containerAdd;
            if (containerAdd != null) this.containerAddValue = containerAddValue;

            // thats it for containerAdd I guess?

            this.nextSq = nextSq;
        }

    }

        public class ChoiceLetters
        {
            public Dictionary<int, string> letters { get; set; }
            public ChoiceLetters()
            {
                letters = new Dictionary<int, string>();
                letters.Add(1, "A");
                letters.Add(2, "B");
                letters.Add(3, "C");
                letters.Add(4, "D");
            }
        }
   

}
