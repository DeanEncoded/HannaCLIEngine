using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hanna_Studio
{
    /// <summary>
    /// Exports game data to JSON format for the Hanna CLI Engine.
    /// </summary>
    public class Exporter
    {
        private const string NullPlaceholder = "hnull";

        private readonly string _gameTitle;
        private readonly string _gameAuthor;
        private readonly string _gameDesc;
        private readonly List<string> _gameContainers;
        private readonly string _startSequence;
        private readonly Dictionary<string, Sequence> _sequences;

        public Exporter(
            string gameTitle,
            string gameAuthor,
            string gameDesc,
            Dictionary<string, Sequence> sequences,
            List<string> gameContainers,
            string startSequence)
        {
            _gameTitle = gameTitle ?? string.Empty;
            _gameAuthor = gameAuthor ?? string.Empty;
            _gameDesc = gameDesc ?? string.Empty;
            _sequences = sequences ?? new Dictionary<string, Sequence>();
            _gameContainers = gameContainers ?? new List<string>();
            _startSequence = startSequence ?? string.Empty;
        }

        /// <summary>
        /// Generates the JSON export string for the game.
        /// </summary>
        public string GenerateExportString()
        {
            var json = new JsonBuilder();

            json.BeginObject();
            json.AddProperty("gameTitle", _gameTitle);
            json.AddProperty("gameAuthor", _gameAuthor);
            json.AddProperty("gameDesc", _gameDesc);
            json.AddPropertyName("gameContainers");
            WriteContainersArray(json);
            json.AddProperty("startSq", _startSequence);
            json.AddPropertyName("sequences");
            WriteSequencesArray(json);
            json.EndObject();

            return json.ToString();
        }

        /// <summary>
        /// Exports the game data to a file.
        /// </summary>
        /// <param name="exportGame">If true, encrypts the output for game distribution (.hgm file).</param>
        /// <param name="exportLocation">The file path for the export.</param>
        /// <returns>True if export was successful, false otherwise.</returns>
        public bool ExportToFile(bool exportGame = false, string exportLocation = "export.json")
        {
            try
            {
                string exportString = GenerateExportString();

                if (exportGame)
                {
                    exportString = CrossPlatformAESEncryption.Helper.CryptoHelper.Encrypt(
                        exportString,
                        Helpers.KEYS.getExportKey());
                }

                File.WriteAllText(exportLocation, exportString, Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void WriteContainersArray(JsonBuilder json)
        {
            json.BeginArray();

            if (_gameContainers.Count > 0)
            {
                foreach (var container in _gameContainers)
                {
                    json.AddArrayValue(container);
                }
            }
            else
            {
                json.AddArrayValue(NullPlaceholder);
            }

            json.EndArray();
        }

        private void WriteSequencesArray(JsonBuilder json)
        {
            json.BeginArray();

            foreach (var sequenceEntry in _sequences)
            {
                WriteSequence(json, sequenceEntry.Value);
            }

            json.EndArray();
        }

        private void WriteSequence(JsonBuilder json, Sequence sequence)
        {
            json.BeginObject();
            json.AddProperty("sqId", sequence.id ?? string.Empty);
            json.AddProperty("sqType", sequence.type ?? string.Empty);
            json.AddProperty("mainText", sequence.mainText ?? string.Empty);
            json.AddProperty("secondaryText", sequence.secondaryText ?? string.Empty);
            json.AddPropertyName("choices");
            WriteChoicesArray(json, sequence.choices);
            json.EndObject();
        }

        private void WriteChoicesArray(JsonBuilder json, Dictionary<string, Choice> choices)
        {
            json.BeginArray();

            if (choices != null)
            {
                foreach (var choiceEntry in choices)
                {
                    WriteChoice(json, choiceEntry.Key, choiceEntry.Value);
                }
            }

            json.EndArray();
        }

        private void WriteChoice(JsonBuilder json, string choiceLetter, Choice choice)
        {
            json.BeginObject();
            json.AddProperty("choiceLetter", choiceLetter ?? string.Empty);
            json.AddProperty("choiceType", choice.type ?? string.Empty);

            json.AddPropertyName("choiceCondition");
            WriteCondition(json, choice.condition);

            json.AddProperty("choiceText", choice.text ?? string.Empty);
            json.AddProperty("outcomeText", choice.outcometext ?? string.Empty);

            json.AddPropertyName("containerAdd");
            WriteContainerAdd(json, choice.containerAdd, choice.containerAddValue);

            json.AddPropertyName("containerDispose");
            json.BeginObject();
            json.AddProperty("container", "null");
            json.EndObject();

            json.AddProperty("nextSq", choice.nextSq ?? string.Empty);
            json.EndObject();
        }

        private void WriteCondition(JsonBuilder json, List<string> condition)
        {
            json.BeginObject();

            if (condition != null && condition.Count >= 2)
            {
                json.AddProperty("container", condition[0] ?? NullPlaceholder);
                json.AddProperty("value", condition[1] ?? NullPlaceholder);
            }
            else
            {
                json.AddProperty("container", NullPlaceholder);
                json.AddProperty("value", NullPlaceholder);
            }

            json.EndObject();
        }

        private void WriteContainerAdd(JsonBuilder json, string container, string value)
        {
            json.BeginObject();

            if (!string.IsNullOrEmpty(container))
            {
                json.AddProperty("container", container);
                json.AddProperty("value", value ?? NullPlaceholder);
            }
            else
            {
                json.AddProperty("container", NullPlaceholder);
                json.AddProperty("value", NullPlaceholder);
            }

            json.EndObject();
        }

        #region Backward Compatibility

        [Obsolete("Use GenerateExportString() instead")]
        public string generateExportString() => GenerateExportString();

        [Obsolete("Use ExportToFile() instead")]
        public bool exportToFile(bool exportgame = false, string exportlocation = "export.json")
            => ExportToFile(exportgame, exportlocation);

        #endregion
    }

    /// <summary>
    /// A simple JSON builder that creates properly formatted JSON output.
    /// </summary>
    internal class JsonBuilder
    {
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly Stack<JsonContext> _contextStack = new Stack<JsonContext>();
        private int _indentLevel = 0;
        private const string IndentString = "  ";

        private enum JsonContext
        {
            Object,
            Array
        }

        private bool NeedsSeparator => _sb.Length > 0 &&
            !_sb.ToString().EndsWith("{") &&
            !_sb.ToString().EndsWith("[") &&
            !_sb.ToString().EndsWith("\n") &&
            !_sb.ToString().EndsWith(": ");

        public void BeginObject()
        {
            if (NeedsSeparator) AppendComma();
            AppendIndent();
            _sb.Append("{");
            _sb.AppendLine();
            _indentLevel++;
            _contextStack.Push(JsonContext.Object);
        }

        public void EndObject()
        {
            _indentLevel--;
            _sb.AppendLine();
            AppendIndent();
            _sb.Append("}");
            if (_contextStack.Count > 0) _contextStack.Pop();
        }

        public void BeginArray()
        {
            _sb.Append("[");
            _sb.AppendLine();
            _indentLevel++;
            _contextStack.Push(JsonContext.Array);
        }

        public void EndArray()
        {
            _indentLevel--;
            _sb.AppendLine();
            AppendIndent();
            _sb.Append("]");
            if (_contextStack.Count > 0) _contextStack.Pop();
        }

        public void AddProperty(string name, string value)
        {
            AddPropertyName(name);
            _sb.Append(EscapeString(value));
        }

        public void AddPropertyName(string name)
        {
            if (NeedsSeparator) AppendComma();
            AppendIndent();
            _sb.Append(EscapeString(name));
            _sb.Append(": ");
        }

        public void AddArrayValue(string value)
        {
            if (NeedsSeparator) AppendComma();
            AppendIndent();
            _sb.Append(EscapeString(value));
        }

        private void AppendComma()
        {
            _sb.Append(",");
            _sb.AppendLine();
        }

        private void AppendIndent()
        {
            for (int i = 0; i < _indentLevel; i++)
            {
                _sb.Append(IndentString);
            }
        }

        private static string EscapeString(string value)
        {
            if (value == null) return "null";

            var sb = new StringBuilder();
            sb.Append('"');

            foreach (char c in value)
            {
                switch (c)
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        if (c < ' ')
                        {
                            sb.AppendFormat("\\u{0:X4}", (int)c);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            sb.Append('"');
            return sb.ToString();
        }

        public override string ToString() => _sb.ToString();
    }
}
