using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanna_Studio
{
    class Exporter
    {

        //TODO: DON'T ALLOW EXPORT WHEN CERTAIN CONDITIONS ARE MET

        string gameTitle;
        string gameAuthor;
        string gameDesc;

        List<String> gameContainers;
        string startSequence;

        Dictionary<string, Sequence> sequences;
        public Exporter(string gameTitle, string gameAuthor, string gameDesc, Dictionary<string, Sequence> sqs, List<string> gameContainers, string startSequence)
        {
            // init export stuff
            this.gameTitle = gameTitle;
            this.gameAuthor = gameAuthor;
            this.gameDesc = gameDesc;
            sequences = sqs;
            this.gameContainers = gameContainers;
            this.startSequence = startSequence;
        }


        public string generateExportString() {


            string estring_gameContainers = @" ""hnull"" ";
            // generate containers string!
            if (gameContainers.Count > 0) {
                string tempString = "";
                bool firstIn = false;
                foreach (String c in gameContainers) {
                    if (!firstIn)
                    { // don't add a comma!
                        tempString += @" """ + c + @"""";
                        firstIn = true;
                    }
                    else { // add a comma to the beginning
                        tempString += @",""" + c + @"""";
                    }
                }
                estring_gameContainers = tempString;
            }

            string estring_gameSequences = @"";
            bool isFirstSq = true;
            // loop through all the sequences!
            foreach (KeyValuePair<string, Sequence> sequence in sequences) {

                //make a meta string for this sequence
                string tempsqMeta = @"";
                if (!isFirstSq)
                { // put a comma on tempsqMeta
                    tempsqMeta += @",";
                }
                else {
                    isFirstSq = false;
                }
                tempsqMeta += @"{""sqId"": """+ sequence.Value.id +@""",
                                ""sqType"": """+ sequence.Value.type +@""",
                                ""mainText"": """+ sequence.Value.mainText.Replace("\"","\\\"") +@""",
                                ""secondaryText"": """+ sequence.Value.secondaryText.Replace("\"", "\\\"") + @""",
                                ""choices"":[";
                // now lets loop through the choices of this sequence

                // what about if it doesn't have choices????
                if (sequence.Value.choices.Count < 1) {
                    // just close the bracket
                }
                bool isFirstChoice = true;
                foreach (KeyValuePair<string,Choice> choice in sequence.Value.choices) {
                    // generate choice condition string
                    string estringChoiceCondition = @"
                                    {
                                    ""container"": ""hnull"",
                                    ""value"": ""hnull"" }";
                    if (choice.Value.condition.Count > 0) {
                        estringChoiceCondition = @"
                                    {
                                    ""container"": """ + choice.Value.condition[0] + @""",
                                    ""value"": """ + choice.Value.condition[1] + @""" }";
                    }

                    // generate choice containerAdd strings
                    // if containrAdd container has nothing then it's null
                    string estringChoiceContainerAdd = @"
                                    {
                                    ""container"": ""hnull"",
                                    ""value"": ""hnull""}";
                    if (choice.Value.containerAdd != null)
                    {
                        estringChoiceContainerAdd = @"
                                    {
                                    ""container"": """ + choice.Value.containerAdd + @""",
                                    ""value"": """ + choice.Value.containerAddValue + @"""}";
                    }


                    // check whether it's our first choice. So as to put a comma at the start
                    string tempChoiceMeta = @"";
                    if (isFirstChoice){
                        // dont add the comma
                        isFirstChoice = false;
                    }
                    else {
                        // add the comma
                        tempChoiceMeta += @",";
                    }
             
            
                    tempChoiceMeta += @"{
                                ""choiceLetter"": """+ choice.Key +@""",
                                ""choiceType"": """+ choice.Value.type +@""",
                                ""choiceCondition"": "+ estringChoiceCondition +@",
                                ""choiceText"": """+ choice.Value.text.Replace("\"", "\\\"") + @""",
                                ""outcomeText"": """+ choice.Value.outcometext.Replace("\"", "\\\"") + @""",
                                ""containerAdd"": "+ estringChoiceContainerAdd +@",
                                ""containerDispose"": {
                                ""container"": ""null""
                                },
                                ""nextSq"": """+ choice.Value.nextSq +@"""
                                }";
                    // push tempChoiceMeta the sequence string
                    tempsqMeta += tempChoiceMeta;
                }
                tempsqMeta += @"]"; // close the choices bracket???

                // puth tempsqMeta to estring_gameSequences
                estring_gameSequences += tempsqMeta + @"}";

            }

            // generate sequences string!

            string estring_closers = @"
                ]
            }";

            string meta = @"
               {
              ""gameTitle"": """ + gameTitle + @""",
              ""gameAuthor"":  """ + gameAuthor + @""",
              ""gameDesc"": """ + gameDesc + @""",
              ""gameContainers"": [ " + estring_gameContainers + @" ],
              ""startSq"": """+ startSequence +@""",
              ""sequences"":[" + estring_gameSequences + estring_closers;
            return meta;

        }


        public bool exportToFile()
        {

            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter("export.json"); //open the file for writing.
                writer.Write(generateExportString()); //write the current date to the file. change this with your date or something.
                writer.Close(); //remember to close the file again.
                writer.Dispose(); //remember to dispose it from the memory.
                return true;
            }
            catch (Exception e) {
                return false;
            }
            

        

        }
}

}
