using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public partial class dlgNewChoice : Form
    {
        frmWorkspace refWorkspace;
        bool editChoice = false;
        string editLetter;
        Choice choiceToEdit;
        public dlgNewChoice(frmWorkspace workspace, string letter = "nothing", Choice choiceToEdit = null)
        {
            InitializeComponent();
            refWorkspace = workspace;
            if (letter != "nothing")
            {

                labelHeader.Text = "Edit Choice";
                this.Text = "Edit existing choice";

                editChoice = true;
                editLetter = letter;
                this.choiceToEdit = choiceToEdit;

            }
        }

        private void DlgNewChoice_Load(object sender, EventArgs e)
        {

            // load types into combobox
            List<string> choiceTypes = new List<string>() { "set", "conditional" };
            comboBoxChoiceType.DataSource = choiceTypes;


            // lets load available sequences into sequenceSelectComboBox
            // loop through the controls in our sequence flow panel
            Panel fp = refWorkspace.getSequencesPanel();
            // loop through all the controls, getting the tag!
            List<string> sequences = new List<string>();
            foreach (Control c in fp.Controls) {
                // you can't show current sequence as a sequence (or maybe you can I don't know I'm confused now)
                if (c.Tag.ToString() != refWorkspace.getCurrentSequence().id) {
                    sequences.Add(c.Tag.ToString());
                }
            }

            // now we have our list! Use that as a datasource for our combobox
            sequenceSelectComboBox.DataSource = sequences;

            // if this an edit loop through the sequences finding the sequence that matches nextsq of the editchoice
            if (editChoice) {
                int index = 0;
                foreach (string s in sequences) {
                    if (s == choiceToEdit.nextSq) {
                        // this be the index
                        sequenceSelectComboBox.SelectedIndex = index;
                        // can we exit this for loop??
                    }
                    index++;
                }
            }


            // now get the current sequence and look into it's choices.
            // this is just to determine which choice letters to show.
            // Hanna accepts 4 choices right now.... but that can change as well!
            // min 1 choice max 4 choices
            Sequence currentSequence = refWorkspace.getCurrentSequence();


            // get choices
            Dictionary<string, Choice> choices = currentSequence.choices;
            ChoiceLetters cLetters = new ChoiceLetters();
            List<int> toRemove = new List<int>();
            if (choices.Count > 0 && choices.Count != 4)
            {
                // there are some choices in there!
                foreach (KeyValuePair<string, Choice> choice in choices)
                {
                    // dont display the letter if we find it in here
                    // I don't even know why I'm using a dictinary for storing these letters to be honest
                    foreach (KeyValuePair<int, string> letter in cLetters.letters)
                    {
                        if (choice.Key == letter.Value)
                        {
                            // add to tRemove
                            toRemove.Add(letter.Key);
                        }
                    }
                }
                // remove those key-value pars from cLetters.letters
                foreach (int i in toRemove)
                {
                    cLetters.letters.Remove(i);
                }
            }
            else if(choices.Count == 4){
                // check if we're editing
                if (!editChoice) {
                    this.Close();
                    MessageBox.Show("You can't create more choices");
                    return;
                }
          
            }
            choiceLetterComboBox.DataSource = new BindingSource(cLetters.letters, null);
            choiceLetterComboBox.DisplayMember = "Value";
            choiceLetterComboBox.ValueMember = "Key";

            // load containers
            if (refWorkspace.projectContainers.Count > 0)
            {
                comboBoxContainers.DataSource = refWorkspace.projectContainers;
                comboBoxConditionalContainer.DataSource = refWorkspace.projectContainers;
            }
            else { panelContainerAdd.Enabled = false; }

            // Dark theme for MaterialSkin items
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);

            // if we are editing a choice! // ----------------------------------------------------------------------------
            if (editChoice)
            {
                btnCreate.Text = "Update Choice";
                // Display choice proporties for this choice
                // disable choice letter combobox
                List<string> dummyDS = new List<string>() { editLetter };
                choiceLetterComboBox.DataSource = dummyDS;
                choiceLetterComboBox.Enabled = false;

                // how to we display the letter now???
                if (choiceToEdit.type == "set")
                {
                    comboBoxChoiceType.SelectedIndex = 0;
                    panelConditionalConfig.Enabled = false;
                }
                else
                {
                    // its conditional
                    comboBoxChoiceType.SelectedIndex = 1;
                    // enable the panel
                    panelConditionalConfig.Enabled = true;

                    // take the conditional value and display it in its textbox
                    textBoxConditionalValues.Text = choiceToEdit.condition[1];
                    // what about the condition container?????
                    // are we going to loop through all the containers and find a matching one????
                    // I guess so
                    int index = 0;
                    foreach (string s in refWorkspace.projectContainers)
                    {
                        if (s == choiceToEdit.condition[0])
                        {
                            // this be the index
                            comboBoxConditionalContainer.SelectedIndex = index;
                            // can we exit this for loop??
                        }
                        index++;
                    }

                }
                // show the Choice Text and Outcome text in their textboxes
                textBoxChoiceText.Text = choiceToEdit.text;
                textBoxOutcometext.Text = choiceToEdit.outcometext.Replace("\\n", "\n");

                // check for a containerAdd as well
                if (choiceToEdit.containerAddValue != null)
                {
                    // there is container add.
                    checkBoxContainerAdd.Checked = true;

                    // how do we display this container???
                    textBoxContainerAddValue.Text = choiceToEdit.containerAddValue;

                    // but now we should also display the correct containers that's being added to!
                    // I seem to be reusing a lot of code.... ¯\_(ツ)_/¯
                    int index = 0;
                    foreach (string s in refWorkspace.projectContainers)
                    {
                        if (s == choiceToEdit.containerAdd)
                        {
                            // this be the index
                            comboBoxContainers.SelectedIndex = index;
                            // can we exit this for loop??
                        }
                        index++;
                    }

                }


            }

        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            // get text from the textboxes
            string choiceLetter = choiceLetterComboBox.Text;
            string choiceText = textBoxChoiceText.Text;
            string outcometext = Regex.Replace(textBoxOutcometext.Text.ToString(), @"\r\n?|\n", "\\n");

            // containerAdd stuff
            string containerAdd = null;
            string containerAddValue = textBoxContainerAddValue.Text;

            if (comboBoxContainers.Text.Length > 0 && checkBoxContainerAdd.Checked) {
                containerAdd = comboBoxContainers.Text;
                // if the textboxes don't contain anything... then don't do anything.
                if (containerAddValue.Length < 1) {
                    MessageBox.Show("You didn't enter a value for in ContainerAdd!");
                    return;
                }
            }

            List<string> choiceCondition = new List<string>();
            // if type is conditional.... get the condition container and value and pass that instead of a an empty one.
            if (comboBoxChoiceType.SelectedIndex == 1) {
                // its a conditional
                if (comboBoxConditionalContainer.Text.Length > 0 && textBoxConditionalValues.Text.Length > 0)
                { // i don't even know why I'm checking the comboBoxConditionalContainer LOL. Too lazy to change for now,  but lets go ahead.
                    // a conditional container is selected
                    // where are the values
                    choiceCondition.Add(comboBoxConditionalContainer.Text);
                    choiceCondition.Add(textBoxConditionalValues.Text);
                }
                else {
                    MessageBox.Show("You didn't enter a value for your conditional choice");
                    return;
                }
            }


            string nextSq = sequenceSelectComboBox.Text;

            // if we're not updating a choice. Just create the choice. But if we are... Update the choice

            this.Close();

            if (editChoice)
            {
                refWorkspace.editChoice(choiceLetter, comboBoxChoiceType.Text, choiceCondition, choiceText, outcometext, containerAdd, containerAddValue, nextSq);
            }
            else {
                refWorkspace.createChoice(choiceLetter, comboBoxChoiceType.Text, choiceCondition, choiceText, outcometext, containerAdd, containerAddValue, nextSq);

            }
        }

        private void CheckBoxContainerAdd_CheckedChanged(object sender, EventArgs e)
        {
            // Enable/Disable containeradd panel
            if (checkBoxContainerAdd.Checked) {
                // enable it.. if there are anyproject containers
                if (refWorkspace.projectContainers.Count > 0)
                {
                    // Enable
                    panelContainerAdd.Enabled = true;
                    // if there is no value in containainer add value... and theres a value in Choice text automatically add that
                    if (textBoxContainerAddValue.Text.Length < 1 && textBoxChoiceText.Text.Length > 0) textBoxContainerAddValue.Text = textBoxChoiceText.Text;
                }
                else { MessageBox.Show("There are no containers in your project");checkBoxContainerAdd.Checked = false; }
            }
            else
            {
                panelContainerAdd.Enabled = false;
            }
        }

        private void ComboBoxChoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxChoiceType.SelectedIndex == 0)
            {
                // it's set.... disable the conditional config panel
                panelConditionalConfig.Enabled = false;
            }
            else {
                // enable it.. if there are anyproject containers
                if (refWorkspace.projectContainers.Count > 0)
                {
                    panelConditionalConfig.Enabled = true;
                }
                else { MessageBox.Show("There are no containers in your project");comboBoxChoiceType.SelectedIndex = 0; }
       
            }
        }
    }
}
