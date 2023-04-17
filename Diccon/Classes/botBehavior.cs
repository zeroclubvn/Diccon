﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccon
{
    internal class BotBehavior
    {
        string Setting_ShortView = Properties.Settings.Default["shortView"].ToString();
        /// <summary>
        /// Create a bubble message base on the example design pattern
        /// </summary>
        /// <param name="answerText"></param>
        /// <param name="exampleRichTextBox"></param>
        /// <param name="exampleColoredPanel"></param>
        /// <param name="exampleParentPanel"></param>
        /// <param name="targetFlowLayout"></param>
        public void BotAnswerLongMessage(string answerText, Label exampleLabel /*RichTextBox exampleRichTextBox*/, Panel exampleColoredPanel, Panel exampleParentPanel, FlowLayoutPanel targetFlowLayout)
        {
            Panel newAnswerPanel = new Panel();
            RoundedPanel newColoredPanel = new RoundedPanel();
            newColoredPanel.Dock = exampleColoredPanel.Dock;
            newColoredPanel.BackColor = exampleColoredPanel.BackColor;
            newColoredPanel.Width = exampleColoredPanel.Width;

            Label newLabel = new Label();
            newLabel.Text = answerText;
            newLabel.MaximumSize = exampleLabel.MaximumSize;
            newLabel.Location = exampleLabel.Location;
            newLabel.AutoSize = false;
            newLabel.Height = newColoredPanel.Height - 20;
            newLabel.Width = exampleLabel.MaximumSize.Width;
            newLabel.Font = exampleLabel.Font;
            newLabel.ForeColor = exampleLabel.ForeColor;
            newLabel.Click += NewLabel_Click1;
            newLabel.Cursor = Cursors.Hand;
            newColoredPanel.Controls.Add(newLabel);
            newLabel.Parent = newColoredPanel;

            newAnswerPanel.Width = exampleParentPanel.Width;
            newAnswerPanel.Controls.Add(newColoredPanel);

            targetFlowLayout.Controls.Add(newAnswerPanel);
            targetFlowLayout.ScrollControlIntoView(newAnswerPanel);
        }

        private void NewLabel_Click1(object sender, EventArgs e)
        {

            Label lb = (sender as Label);
            FlowLayoutPanel grandFlowLayout = (lb.Parent.Parent.Parent as FlowLayoutPanel);
            lb.AutoSize = true;
            lb.Parent.Height = lb.Height + 20;
            lb.Parent.Parent.Height = lb.Height + 20;
            lb.Cursor = Cursors.Default;
            // refresh to get rid of unrendered design
            grandFlowLayout.Refresh();
        }
        /// <summary>
        /// Create a rounded bubble chat with a play button. These parameters bellow is the pattern design for function to clone. 
        /// </summary>
        /// <param name="textToPlay"></param>
        /// <param name="examplePlayButton"></param>
        /// <param name="exampleColoredPanel"></param>
        /// <param name="examplePlayAlignPanel"></param>
        /// <param name="examplePanel"></param>
        /// <param name="targetFlowPanel"></param>
        public void BotSoundMessage(String textToPlay, Label exampleTextHolder, PictureBox examplePlayButton, RoundedPanel exampleColoredPanel, Panel examplePlayAlignPanel, Panel examplePanel, FlowLayoutPanel targetFlowPanel)
        {
            Panel newPanel = new Panel();
            Panel newAlignPanel = new Panel();
            RoundedPanel newColoredPanel = new RoundedPanel();
            PictureBox playButton = new PictureBox();
            Label textHolder = new Label();

            playButton.Location = examplePlayButton.Location;
            playButton.Size = examplePlayButton.Size;
            playButton.Image = examplePlayButton.Image;
            playButton.Tag = textToPlay;
            playButton.Cursor = examplePlayButton.Cursor;
            playButton.Click += PlayButton_Click;

            textHolder.Text = textToPlay;
            textHolder.MaximumSize = examplePlayButton.MaximumSize;
            textHolder.Location = exampleTextHolder.Location;
            textHolder.Font = exampleTextHolder.Font;
            textHolder.AutoSize = true;
            // change text color equal to its parent background to invisible it
            newColoredPanel.BackColor = exampleColoredPanel.BackColor;
            textHolder.ForeColor = newColoredPanel.BackColor;


            newColoredPanel.Controls.Add(playButton);
            newColoredPanel.Controls.Add(textHolder);
            // add control before resize panel
            newColoredPanel.AutoSize = true;
            newColoredPanel.Size = exampleColoredPanel.Size;

            newColoredPanel.Location = exampleColoredPanel.Location;

            newAlignPanel.Dock = examplePlayAlignPanel.Dock;
            newAlignPanel.Size = examplePlayAlignPanel.Size;

            newPanel.Size = examplePanel.Size;


            newPanel.Refresh();
            newAlignPanel.Controls.Add(newColoredPanel);
            newPanel.Controls.Add(newAlignPanel);
            targetFlowPanel.Controls.Add(newPanel);

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            string text = (sender as PictureBox).Tag.ToString();
            BotSoundPlay(text, "us");
        }

        /// <summary>
        /// Play sound of a word in a area ("us"/"uk")
        /// </summary>
        /// <param name="text"></param>
        /// <param name="area"></param>
        public void BotSoundPlay(string text, string area)
        {
            SoundRelated player = new SoundRelated();
            WordRelated word = new WordRelated(text);
            word.SearchWordProcess();
            player.OfflinePlay(word.OutWord);
        }
        public void BotSynonym(List<string> synonymList, RoundedLabel exampleItem, FlowLayoutPanel examplePanelHaveSynonyms, FlowLayoutPanel targetFlowLayout)
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
                newLabel.Cursor = exampleItem.Cursor;
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
            DicconProp.WordFromSynonym = (sender as RoundedLabel).Text;
        }

        /// <summary>
        /// Using Microsoft Translator API to translate string to a translated JSON-formated string.
        /// Key is hold by RapidAPI website.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<string> GetEnViTranslatedTextAsync(string text)
        {
            try
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

                        return DicconProp.ErrorInternet;

                    }

                }
            }
            catch (Exception)
            {
                return DicconProp.ErrorInternet;
            }


        }

        public async Task<string> GetViEnTranslatedTextAsync(string text)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/translate?to=en&api-version=3.0&profanityAction=NoAction&textType=plain&suggestedFrom=vi"),
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

                        return DicconProp.ErrorInternet;

                    }

                }
            }
            catch (Exception)
            {
                return DicconProp.ErrorInternet;
            }


        }

        public async void BotImageAnswer(PictureBox examplePixabayLogo, RoundedPictureBox examplePictureBox, RoundedPanel exampleColoredPanel, Panel examplePanel, FlowLayoutPanel targetFlowLayout)
        {
            RoundedPictureBox pictureBox = new RoundedPictureBox();
            RoundedPanel roundedPanel = new RoundedPanel();
            PictureBox pixabayLogo = new PictureBox();
            Panel panel = new Panel();
            ImageRelated img = new ImageRelated();

            pixabayLogo.Image = examplePixabayLogo.Image;
            pixabayLogo.Location = examplePixabayLogo.Location;
            pixabayLogo.Size = examplePixabayLogo.Size;
            pixabayLogo.Click += PixabayLogo_Click;
            pixabayLogo.Cursor = examplePixabayLogo.Cursor;
            pixabayLogo.Visible = DicconProp.IsFromPixabay;

            roundedPanel.Size = exampleColoredPanel.Size;
            roundedPanel.Location = exampleColoredPanel.Location;
            roundedPanel.BackColor = exampleColoredPanel.BackColor;

            pictureBox.Size = examplePictureBox.Size;
            pictureBox.Location = examplePictureBox.Location;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderColor = roundedPanel.BackColor;
            pictureBox.BackColor = examplePictureBox.BackColor;
            pictureBox.ErrorImage = examplePictureBox.ErrorImage;
            pictureBox.InitialImage = examplePictureBox.InitialImage;

            panel.Size = examplePanel.Size;

            pictureBox.ImageLocation = await img.GetImageUrl(DicconProp.CurrentWord);
            pictureBox.Click += PictureBox_Click;
            pictureBox.Tag = DicconProp.CurrentWord;

            roundedPanel.Controls.Add(pictureBox);

            panel.Controls.Add(pixabayLogo);
            panel.Controls.Add(roundedPanel);

            panel.Visible = true;

            targetFlowLayout.Controls.Add(panel);

            targetFlowLayout.ScrollControlIntoView(panel);
        }

        private void PixabayLogo_Click(object sender, EventArgs e)
        {

        }

        private async void PictureBox_Click(object sender, EventArgs e)
        {

            ImageRelated img = new ImageRelated();
            (sender as PictureBox).ImageLocation = await img.GetImageUrl((sender as PictureBox).Tag.ToString());
        }
    }
}
