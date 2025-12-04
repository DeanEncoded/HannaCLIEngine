using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Hanna_Studio.NodeEditor;

namespace Hanna_Studio
{
    [Serializable]
    public class StudioObject
    {

        public string projectName { get; set; }
        public string projectAuthor { get; set; }

        public List<String> projectContainers { get; set; }

        public string projectDescription { get; set; }

        public Dictionary<string, Sequence> sequences { get; set; }

        // Node Editor state for saving/loading visual layout
        public NodeEditorState nodeEditorState { get; set; }

        public StudioObject(string projectName, string projectAuthor, string projectDescription, List<String> projectContainers, Dictionary<string, Sequence> sequences, NodeEditorState nodeEditorState = null) {
            this.projectName = projectName;
            this.projectAuthor = projectAuthor;
            this.projectContainers = projectContainers;
            this.projectDescription = projectDescription;
            this.sequences = sequences;
            this.nodeEditorState = nodeEditorState;
        }

    }


    // serializer class
    public class BinarySystem{


        public BinarySystem()
        {

        }
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
                Console.WriteLine("WROTE TO FILE!");
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

    }
}
