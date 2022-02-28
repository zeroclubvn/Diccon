﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccon
{
    internal class botBehavior
    {
        string Setting_ShortView = Properties.Settings.Default["shortView"].ToString();
        public void botAnswerLongMessage(string answerText, RichTextBox exampleRichTextBox, Panel exampleColoredPanel, Panel exampleParentPanel, FlowLayoutPanel targetFlowLayout)
        {
            Panel newAnswerPanel = new Panel();
            RoundedPanel newColoredPanel = new RoundedPanel();
            newColoredPanel.Dock = exampleColoredPanel.Dock;
            newColoredPanel.BackColor = exampleColoredPanel.BackColor;
            newColoredPanel.Width = exampleColoredPanel.Width;

            RichTextBox newRichTextBox = new RichTextBox();
            if (Setting_ShortView == "True") newRichTextBox.Text = answerText;
            newRichTextBox.Click += newRichTextBox_ClickToExpand;
            newRichTextBox.TextChanged += newRichTextBox_TextChanged;
            newRichTextBox.BackColor = exampleRichTextBox.BackColor;
            newRichTextBox.ForeColor = exampleRichTextBox.ForeColor;
            newRichTextBox.Font = exampleRichTextBox.Font;
            newRichTextBox.Width = exampleRichTextBox.Width;
            newRichTextBox.Dock = exampleRichTextBox.Dock;
            newRichTextBox.Location = exampleRichTextBox.Location;
            newRichTextBox.BorderStyle = BorderStyle.None;
            newRichTextBox.ScrollBars = RichTextBoxScrollBars.None;
            newRichTextBox.Cursor = exampleRichTextBox.Cursor;

            newColoredPanel.Controls.Add(newRichTextBox);
            newRichTextBox.Parent = newColoredPanel;

            newAnswerPanel.Width = exampleParentPanel.Width;
            newAnswerPanel.Height = newRichTextBox.Height + 28;

            newAnswerPanel.Controls.Add(newColoredPanel);

            targetFlowLayout.Controls.Add(newAnswerPanel);

            // these if condition is a way to display short and long message when click (original from a bug :))) )
            if (Setting_ShortView == "False") newRichTextBox.Text = answerText;

            targetFlowLayout.ScrollControlIntoView(newAnswerPanel);
        }

        private void newRichTextBox_ClickToExpand(object sender, EventArgs e)
        {
            RichTextBox textMessage = (sender as RichTextBox);
            FlowLayoutPanel grandFlowLayout = (textMessage.Parent.Parent.Parent as FlowLayoutPanel);
            Panel panel = (textMessage.Parent.Parent as Panel);
            int lineHeight = 20;
            int lineCount = textMessage.GetLineFromCharIndex(textMessage.TextLength);
            textMessage.Height = lineHeight * lineCount;
            textMessage.Parent.Parent.Height = textMessage.Height + 20;
            grandFlowLayout.ScrollControlIntoView(panel);
            // refresh to get rid of unrendered design
            grandFlowLayout.Refresh();
        }

        private void newRichTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox textMessage = (sender as RichTextBox);
            var parentPanel = textMessage.Parent.Parent;
            parentPanel.Visible = false;

            int lineHeight = 20;
            int lineCount = textMessage.GetLineFromCharIndex(textMessage.TextLength);
            textMessage.Height = lineHeight * lineCount;
            parentPanel.Height = textMessage.Height + 20;
            parentPanel.Visible = true;

        }
        /// <summary>
        /// Create a rounded bubble chat with a play button. These parametter bellow is the partern design for function to clone. 
        /// </summary>
        /// <param name="textToPlay"></param>
        /// <param name="examplePlayButton"></param>
        /// <param name="exampleColoredPanel"></param>
        /// <param name="examplePlayAlignPanel"></param>
        /// <param name="examplePanel"></param>
        /// <param name="targetFlowPanel"></param>
        public void botSoundMessage(String textToPlay, PictureBox examplePlayButton, RoundedPanel exampleColoredPanel, Panel examplePlayAlignPanel, Panel examplePanel, FlowLayoutPanel targetFlowPanel)
        {
            Panel newPanel = new Panel();
            Panel newAlignPanel = new Panel();
            RoundedPanel newColoredPanel = new RoundedPanel();
            PictureBox playButton = new PictureBox();

            playButton.Location = examplePlayButton.Location;
            playButton.Size = examplePlayButton.Size;
            playButton.Image = examplePlayButton.Image;
            playButton.Tag = textToPlay;
            playButton.Cursor = examplePlayButton.Cursor;
            playButton.Click += PlayButton_Click;

            newColoredPanel.Size = exampleColoredPanel.Size;
            newColoredPanel.BackColor = exampleColoredPanel.BackColor;
            newColoredPanel.Location = exampleColoredPanel.Location;

            newAlignPanel.Dock = examplePlayAlignPanel.Dock;
            newAlignPanel.Size = examplePlayAlignPanel.Size;

            newPanel.Size = examplePanel.Size;

            newColoredPanel.Controls.Add(playButton);
            newAlignPanel.Controls.Add(newColoredPanel);
            newPanel.Controls.Add(newAlignPanel);
            targetFlowPanel.Controls.Add(newPanel);

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            string text = (sender as PictureBox).Tag.ToString();
            botSoundPlay(text, "us");
        }

        /// <summary>
        /// Play sound of a word in a area ("us"/"uk")
        /// </summary>
        /// <param name="text"></param>
        /// <param name="area"></param>
        public void botSoundPlay(string text, string area)
        {
            soundRelated player = new soundRelated();
            wordRelated word = new wordRelated(text);
            word.SearchWordProcess();
            player.OfflinePlay(word.OutWord);
        }
        /// <summary>
        /// Play sound of a given word 
        /// </summary>
        /// <param name="text"></param>
        public void botSoundPlay(string text)
        {
            soundRelated player = new soundRelated();
            wordRelated word = new wordRelated(text);
            word.SearchWordProcess();
            player.OfflinePlay(word.OutWord);
        }
        public void botSynonym(List<string> synonymList, RoundedLabel exampleItem, FlowLayoutPanel examplePanelHaveSynonyms, FlowLayoutPanel targetFlowLayout)
        {
            FlowLayoutPanel panelHaveSynonyms = new FlowLayoutPanel();
            panelHaveSynonyms.AutoSize = true;
            panelHaveSynonyms.MaximumSize = panelHaveSynonyms.MaximumSize;
            panelHaveSynonyms.MinimumSize = panelHaveSynonyms.MinimumSize;
            panelHaveSynonyms.Size = examplePanelHaveSynonyms.Size;
            panelHaveSynonyms.Padding = examplePanelHaveSynonyms.Padding;

            targetFlowLayout.Controls.Add(panelHaveSynonyms);
            foreach (string word in synonymList)
            {
                // if (++index >= 10) break;
                RoundedLabel newLabel = new RoundedLabel();
                newLabel.BackColor = exampleItem.BackColor;
                newLabel.ForeColor = exampleItem.ForeColor;
                newLabel.Padding = exampleItem.Padding;
                newLabel.Text = word;
                newLabel.AutoSize = true;
                newLabel.Font = exampleItem.Font;
                newLabel.Click += NewLabel_Click;

                panelHaveSynonyms.Controls.Add(newLabel);

            }

            targetFlowLayout.ScrollControlIntoView(panelHaveSynonyms);
        }

        private void NewLabel_Click(object sender, EventArgs e)
        {
            Label label = (sender as Label);
            dicconProp.currentWord = label.Text;

        }
        /// <summary>
        /// Using Microsoft Translator API to translate string to a translated JSON-formated string.
        /// Key is hold by RapidAPI website.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<string> getTranslatedTextAsync(string text)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/translate?to=vi&api-version=3.0&profanityAction=NoAction&textType=plain&suggestedFrom=en"),
                Headers ={
                            { "x-rapidapi-host", "microsoft-translator-text.p.rapidapi.com" },
                            { "x-rapidapi-key", "a10d63c67cmshd79f69a2d87629ap1e586djsna7cdee48e5de" },
                         },
                Content = new StringContent("[\r\n    {\r\n        \"Text\": \"" + text + "\"\r\n    }\r\n]")
                {
                    Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                }
            };
            string body;
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    body = await response.Content.ReadAsStringAsync();
                    JsonNode note = JsonNode.Parse(body);
                    string translatedWord = note[0]["translations"][0]["text"].GetValue<string>();
                    return translatedWord;
                }
                else
                {
                    MessageBox.Show("ok");
                    return "-1";

                }
                
            }
            
        }

    }
}
