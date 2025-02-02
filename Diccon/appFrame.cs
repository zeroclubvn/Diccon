﻿using Diccon.Pages;
using QuoteBank;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Diccon
{
    public partial class appFrame : Form
    {

        Form currentForm = null;
        Form dictionaryForm = null;
        Form aboutForm = null;
        Form donateForm = null;
        Form timelineForm = null;
        Form settingForm = null;
        Form yawaForm = new yawa();
        Form loginForm = null;
        private readonly SQLHandler _sqlHandler = new SQLHandler();
        public appFrame()
        {
            switch (Properties.Settings.Default["language"])

            {
                case "english":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    break;
                case "vietnamese":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VI");
                    break;
                default:
                    break;
            }
            InitializeComponent();
        }
        private void AppFrame_Load(object sender, EventArgs e)
        {
            // Stack up quotation
            switch (DicconProp.Language)
            {

                case DicconProp.LanguageType.English:
                    englishToolStripMenuItem.Checked = true;
                    vietnameseToolStripMenuItem.Checked = false;
                    Quotes.English quote = new Quotes.English();
                    lbQuotation.Text = quote.Wisdom;
                    break;
                case DicconProp.LanguageType.Vietnamese:
                    englishToolStripMenuItem.Checked = false;
                    vietnameseToolStripMenuItem.Checked = true;
                    Quotes.Vietnamese quote_vi = new Quotes.Vietnamese();
                    lbQuotation.Text = quote_vi.Wisdom;
                    break;
                default:
                    break;
            }
            //Determine data folder existen in ApplicationData
            if (!Directory.Exists(DicconProp.DicconApplicationDataPath))
            {
                Directory.CreateDirectory(DicconProp.DicconApplicationDataPath);
            };
            switch (Properties.Settings.Default["staredForm"])
            {
                case "Dictionary":
                    dictionaryForm = new dictionary();
                    openForm(dictionaryForm);
                    break;
                case "Timeline":
                    timelineForm = new timeline();
                    openForm(timelineForm);
                    break;
                default:

                    break;
            }
            //get userId and Email in UserInfoFile
            DicconProp.UserID = getUserInfo()[0];
            DicconProp.UserEmail = getUserInfo()[1];
            if (DicconProp.UserID != "")
            {
                accountToolStripMenuItem.Visible = true;
                backUpSyncToolStripMenuItem.Visible = true;
                logInWithGoogleToolStripMenuItem.Visible = false;
            }



            //topControlPanel.BackColor = dicconProp.AccentColor;
            btDictionary.BackColor = DicconProp.ColorA8;
            btDonate.BackColor = DicconProp.ColorA8;
            btTimeline.BackColor = DicconProp.ColorA8;
            btCommunity.BackColor = DicconProp.ColorA8;
            topPanel.BackColor = DicconProp.AccentColor;
        }


        private string[] getUserInfo()
        {
            string[] userInfo = new string[2];
            userInfo[0] = "";
            userInfo[1] = "";
            if (File.Exists(DicconProp.UserInfoFileName))
            {
                string userInfoFile = File.ReadAllText(DicconProp.UserInfoFileName);
                userInfo[0] = userInfoFile.Split('#')[0];
                userInfo[1] = userInfoFile.Split('#')[1];
            }
            return userInfo;
        }










        /// <summary>
        /// Change Red and Green background of Rounded-Label to a darker shade of  color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoundedLabel_MouseEnter(object sender, EventArgs e)
        {
            DicconProp.RoundedLabel_MouseEnter(sender, e);
        }
        /// <summary>
        /// Change Red and Green background of Rounded-Label to a lighter shade of  color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoundedLabel_MouseLeave(object sender, EventArgs e)
        {
            DicconProp.RoundedLabel_MouseLeave(sender, e);
        }
        /// <summary>
        /// Change position of PictureBox to create a illusion that the box is lift up a little bit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Change position of PictureBox to create a illusion that the box is push down a little bit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {

        }

        private void btDictionary_Click(object sender, EventArgs e)
        {
            if (dictionaryForm != null)
            {

                openForm(dictionaryForm);
            }
            else
            {
                dictionaryForm = new dictionary();
                openForm(dictionaryForm);
            }
        }
        private void openForm(Form targetForm)
        {
            if (targetForm == timelineForm)
            {
                timeLineDetector.Enabled = true;
            }
            else
            {
                timeLineDetector.Enabled = false;
            }
            {
                currentForm = targetForm;
                currentForm.TopLevel = false;
                currentForm.FormBorderStyle = FormBorderStyle.None;
                currentForm.Dock = DockStyle.Fill;
                this.Controls.Add(currentForm);
                currentForm.Show();
                currentForm.BringToFront();

            }

        }

        private void logo_Click(object sender, EventArgs e)
        {

        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            int X = Cursor.Position.X - 155;
            int Y = Cursor.Position.Y + 15;
            contextMenu.Show(X, Y);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm != null)
            {

                openForm(aboutForm);
            }
            else
            {
                aboutForm = new about();
                openForm(aboutForm);
            }

        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutForm != null)
            {

                openForm(aboutForm);
            }
            else
            {
                aboutForm = new about();
                openForm(aboutForm);
            }
        }




        private void playGroundPanel_ControlAdded(object sender, ControlEventArgs e)
        {

        }


        private void appFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default["autoBackup"].ToString() != "False")

                    backUpTimeline();

            }
            catch (Exception)
            {

            }
            Properties.Settings.Default.Save();
        }

        private void btDonate_Click(object sender, EventArgs e)
        {


            if (donateForm != null)
            {

                openForm(donateForm);
            }
            else
            {
                donateForm = new donate();
                openForm(donateForm);
            }
        }

        private void btTimeline_Click(object sender, EventArgs e)
        {
            if (timelineForm != null)
            {

                openForm(timelineForm);
            }
            else
            {
                timelineForm = new timeline();
                openForm(timelineForm);
            }
        }

        private void buttonYourNote_Click(object sender, EventArgs e)
        {

        }

        private void roundedLabel7_Click(object sender, EventArgs e)
        {
            if (DicconProp.UserID != "")
            {
                if (yawaForm != null)
                {

                    openForm(yawaForm);
                }
                else
                {
                    yawaForm = new yawa();
                    openForm(yawaForm);
                }
            }
            else
            {
                MessageBox.Show(DicconProp.PromptLogin, DicconProp.Caption);
            }
        }

        private void timeLineDetector_Tick(object sender, EventArgs e)
        {
            if (DicconProp.WordFromTimeline != "")
            {
                btDictionary_Click(null, null);
            }
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (settingForm != null)
            {

                openForm(settingForm);
            }
            else
            {
                settingForm = new settings();
                openForm(settingForm);
            }
        }

        private void notificationDetector_Tick(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.alert_wav);
            soundPlayer.Play();
        }

        private void logInWithGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loginForm != null)
            {

                openForm(loginForm);
            }
            else
            {
                loginForm = new login();
                openForm(loginForm);
            }
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loginForm != null)
            {

                openForm(loginForm);
            }
            else
            {
                loginForm = new login();
                openForm(loginForm);
            }
        }

        private void backUpSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                backUpTimeline();
                MessageBox.Show(DicconProp.PromptSyncedSuccess, DicconProp.Caption);
            }
            catch (Exception error)
            {
                //MessageBox.Show(dicconProp.errorBackup, dicconProp.caption);
                MessageBox.Show(error.ToString(), DicconProp.Caption);


            }

        }

        private async void backUpTimeline()
        {

            string timelineLocalContents = "";
            string timelineOnlineContents = "";
            var connectivity = new Connectivity();
            if (connectivity.IsOnline())
            {

                if (File.Exists(DicconProp.HistoryFileName))
                {
                    // Get both local and online data together, and then mix them up, filter out doulicate and push online and local
                    timelineLocalContents = File.ReadAllText(DicconProp.HistoryFileName);
                    string getOnlineQueryString = @"Select Timeline from dbo.DicconUser where Id=" + DicconProp.UserID;
                    DataTable dataTable = await _sqlHandler.SelectAsync(getOnlineQueryString);
                    timelineOnlineContents = dataTable.Rows[0][0].ToString();
                    // Combine two string
                    string combinedContents = timelineLocalContents + "#" + timelineOnlineContents;
                    string[] rawList = combinedContents.Split('#');
                    List<string> rawList_1 = new List<string>();
                    foreach (var item in rawList)
                    {
                        rawList_1.Add(item.ToString());
                    }
                    // Remove doublicate by Distinct() function in LINQ
                    List<string> rawList_2 = rawList_1.Distinct().ToList();
                    string outList = string.Join("#", rawList_2);
                    // Update new data to online disk
                    string updateQueryString = "UPDATE dbo.DicconUser  SET Timeline = '" + outList + "' Where Id=" + DicconProp.UserID;
                    await _sqlHandler.UpdateAsync(updateQueryString);
                    // Update new data to local disk
                    StreamWriter history = new StreamWriter(DicconProp.HistoryFileName);
                    history.Write(outList);
                    history.Close();
                }
                else
                {
                    // If local file not exist, we get online data and write it to local
                    string getOnlineQueryString = @"Select Timeline from dbo.DicconResources where Id=" + DicconProp.UserID;
                    DataTable dataTable = await _sqlHandler.SelectAsync(getOnlineQueryString);
                    timelineOnlineContents = dataTable.Rows[0][1].ToString();
                    // Update new data to local disk
                    StreamWriter history = new StreamWriter(DicconProp.HistoryFileName);
                    history.Write(timelineOnlineContents);
                    history.Close();
                }



            }
            else
            {
                MessageBox.Show(DicconProp.ErrorInternet, DicconProp.Caption);
            }

        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["language"] = "english";
            Properties.Settings.Default.Save();
            Application.Restart();

        }

        private void vietnameseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["language"] = "vietnamese";
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lbQuotation_Click(object sender, EventArgs e)
        {
            DicconProp.WordFromTimeline = lbQuotation.Text.Replace("\"", "");
            btDictionary_Click(null, null);
        }
    }
}
