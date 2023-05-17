using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptPortal.Vegas;

//TO- DO Set up cases for intro checked, outro checked, and both checked. Add setting for the fade timers. Add Delete button. Maybe option to save outside of exit. Reset Button?

namespace Highlights
{
    public partial class Form1 : Form
    {
        public int highlightCounter;
        public string Location;//ie E:\Videos\Test 
        public string path; 
        
        public int folderCounter = 1;
        public string LocationHighlight; //E:\Videos\Test\Highlights SHARE
        public string insideHighlights; 
        public string intro; 
        public string outro;
        public int createdFolders;

        public bool introChecker = false;

        public bool outroChecker = false;

        
        public double afterIntroFadeOne { get; set; }
        public double beforeOutroFadeOne { get; set; }

        public int sonyImportOne { get; set; }

        public int timelineHighlightOne { get; set; }



        






        public Form1()
        {
            InitializeComponent();
            //Location = @"E:\Videos"; // Have to use @ because of the backslash;
            //path = System.IO.Path.Combine(Location, "Highlights"); //Creates folder name highlight, might need to have a try and catch
            afterIntroFadeOne = 1.5; //Might cause an issue because they are doubles. Maybe change to int if issues arrive. Also could try putting it in form.
            beforeOutroFadeOne = 2;
            sonyImportOne = 30000;
            timelineHighlightOne = 120;

    }

        public void ResetVariables()
        {
            afterIntroFadeOne = 1.5; //Might cause an issue because they are doubles. Maybe change to int if issues arrive. Also could try putting it in form.
            beforeOutroFadeOne = 2;
            sonyImportOne = 30000;
            timelineHighlightOne = 120;
            Location = null;
            path = null;
            folderCounter = 1;
            insideHighlights = null;
            intro = null;
            outro = null;
            createdFolders = 0;
            introChecker = false;
            outroChecker = false;
        }

        

        
        //Think I want global variables of Location/path



        private void button1_Click(object sender, EventArgs e)
        {
            //mention your location
            //Location = @"E:\Videos"; // Have to use @ because of the backslash;
            path = System.IO.Path.Combine(Location, "Highlights"); //Creates folder name highlight, might need to have a try and catch ie E:\Videos\Test\Highlights
            System.IO.Directory.CreateDirectory(path); //E:\Videos\Test\Highlights
            LocationHighlight = path;  //E:\Videos\Test\Highlights
            MessageBox.Show("Created folder named Highlights to store cut up videos!");
            //MessageBox.Show(sux);

            DialogResult dialogResult = MessageBox.Show("Will now move all your mp4 files in this directory to the highlights folder!", "Move mp4s to Highlights folder", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var dam = new DirectoryInfo(Location);
                foreach (FileInfo fi in dam.GetFiles())
                {
                    if (fi.Extension == ".mp4")
                    {
                        string destFilePath = Path.Combine(path, fi.Name);
                        if (!File.Exists(destFilePath))
                        {
                            fi.MoveTo(destFilePath);
                        }
                    }
                }
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Start from top to bottom. This program's first buttom prompts you to first select a folder with your highlights in it.\n The next button creates a folder name -Highlights- in that directory." +
                "\nThe next button MOVES any non-mp4 files into a folder named -Junk- within the Highlights Directory.\nThe following rows lets you select how many mp4s make up a video, then you hit submit." +
                "\n Select/Mark whether you have an intro or outro video and follow the prompt. The next button groups up all your videos into their own folder for importing.\nThe final button begins the process of putting the videos into Sony Vegas. ");
        }

        private void button2_Click(object sender, EventArgs e) //Seperate Videos into their own folders button.
        {
            //if(radioButton1.Checked)
            //{

            //}

            //else if(radioButton2.Checked)
            //{
                int counter = 0;
                int counterfolder = 0;
                //string location = @"E:\Videos\Highlights";
                string fileExtension = ".mp4";
                string[] files = Directory.GetFiles(LocationHighlight, "*" + fileExtension);
                if (files.Length < 1)
                {
                    MessageBox.Show("There are no mp4 files here.");
                }
                try
                {
                    foreach (string file in files)
                    {
                        if (counter < folderCounter)
                        {
                            //MessageBox.Show(file); //Shows the name of the file
                            string destinationDirectory = Path.Combine(LocationHighlight, "Highlights" + counterfolder); //Gets Directory of where the file should go
                            Directory.CreateDirectory(destinationDirectory); //Creates the directory of where the file should go
                            string sourceFilePath = file; //Sets source file to the name of the file it is currently on from the array
                            string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(file));
                            File.Move(sourceFilePath, destinationFilePath);
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                            counterfolder++;
                            //MessageBox.Show(file); //Shows the name of the file
                            string destinationDirectory = Path.Combine(LocationHighlight, "Highlights" + counterfolder); //Gets Directory of where the file should go
                            Directory.CreateDirectory(destinationDirectory); //Creates the directory of where the file should go
                            string sourceFilePath = file; //Sets source file to the name of the file it is currently on from the array
                            string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(file));
                            File.Move(sourceFilePath, destinationFilePath);
                            counter++;
                        }
                    }
                    createdFolders = counterfolder;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error moving files: " + ex.Message);
                }

            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //Intro Checker
        {
            try
            {
                if (checkBox1.Checked)
                {
                    //string Location = @"E:\Videos\Highlights"; // Have to use @ because of the backslash;
                    string path = System.IO.Path.Combine(LocationHighlight, "Intro"); //Creates folder name highlight, might need to have a try and catch
                    System.IO.Directory.CreateDirectory(path);
                    intro = path;
                    MessageBox.Show("Created a folder to hold the Intro");
                    //Maybe create folder dialog that selects and moves the intro to the folder
                    DialogResult dialogResult = MessageBox.Show("Do you want to select your intro video now to move it in the new folder or would you rather do it manually? \n NOTE: Only 1 video should be in the intro folder.", "Select Video", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "MP4 files (*.mp4)|*.mp4";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog1.FileName;
                            File.Copy(filePath, Path.Combine(intro, Path.GetFileName(filePath)));
                            // Do something with the file path
                        }
                    }
                    introChecker = true;
                }
                else
                {
                    introChecker = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Did you not set the base Location of where the highlight files are?");
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) //Outro Checker
        {
            try
            {
                if (checkBox2.Checked)
                {
                    //string Location = @"E:\Videos\Highlights"; // Have to use @ because of the backslash;
                    string path = System.IO.Path.Combine(LocationHighlight, "Outro"); //Creates folder name highlight, might need to have a try and catch
                    System.IO.Directory.CreateDirectory(path);
                    outro = path;
                    MessageBox.Show("Created a folder to hold the Outro");
                    //Maybe create folder dialog that selects and moves the intro to the folder
                    DialogResult dialogResult = MessageBox.Show("Do you want to select your outro video now to move it in the new folder or would you rather do it manually? \n NOTE: Only 1 video should be in the outro folder.", "Select Video", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "MP4 files (*.mp4)|*.mp4";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog1.FileName;
                            File.Copy(filePath, Path.Combine(outro, Path.GetFileName(filePath)));
                            // Do something with the file path
                        }
                    }
                    outroChecker = true;
                }
                else
                {
                    outroChecker = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Did you not set the base Location of where the highlight files are?");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.Combine(LocationHighlight, "Junk");
            System.IO.Directory.CreateDirectory(path);
            var dam = new DirectoryInfo(LocationHighlight);
            foreach (FileInfo fi in dam.GetFiles())
            {
                if (fi.Extension != ".mp4")
                {
                    string destFilePath = Path.Combine(path, fi.Name);
                    if (!File.Exists(destFilePath))
                    {
                        fi.MoveTo(destFilePath);
                    }
                }
            }
            MessageBox.Show("Created a folder to hold all files that were not mp4's");
        }


        private void button5_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //label1.Name = "How many highlights make up a video?";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            folderCounter = (int)numericUpDown1.Value;
            MessageBox.Show(folderCounter + " highlights make up a video.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    Location = selectedPath; //ie E:\Videos\Test
                    MessageBox.Show("Selected location - "+ Location + " - that has all your video files.");
                    // Do something with the selected folder path
                }
            }

            //path = System.IO.Path.Combine(Location, "Highlights");
        }

        

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you ABSOLUTELY sure that you want to delete the Highlights Folder? \n NOTE: You should backup your INTRO and OUTRO mp4's along with making sure your videos rendered completely!", "Delete Folder?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if(Directory.Exists(LocationHighlight))
                {
                    Directory.Delete(LocationHighlight, true); // Test this please
                }
            }
        }

        private void SettingsForm_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(this);
            settingsForm.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }



    public class EntryPoint
    {

        private int numberHighlights;
        private string position; //E:\Videos\Test
        private string position2; //E:\Videos\Test\Highlights
        private string entryIntro; //E:\Videos\Test\Intro
        private string entryOutro; //E:\Videos\Test\Outro
        public int folderNumber;
        public double afterIntroFade; //Might cause an issue because they are doubles. Maybe change to int if issues arrive. Also could try putting it in form.
        public double beforeOutroFade;
        public int sonyImport;
        public int timelineHighlight;
        public bool checkerIntro;
        public bool checkerOutro;

        public void FromVegas(Vegas myVegas)
        {
            //Timecode cursorPosition = myVegas.Transport.CursorPosition;
            //image

            // audio
            //string audioPath = @"E:\Videos\Highlights\Highlights0";

            // video
            //string videoPath = @"E:\Videos\Highlights\Highlights0\something.mp4"; //path directly to video
            //VideoEvent v1Event = (VideoEvent)AddMedia(myVegas.Project, videoPath, 0, cursorPosition, Timecode.FromSeconds(20));
            //AudioEvent v2Event = (AudioEvent)AddMedia(myVegas.Project, videoPath, 0, cursorPosition, Timecode.FromSeconds(20));
            
            using (Form1 form1 = new Form1()) //This creates the Windows GUI that we used to manage everything
            {
                DialogResult result = form1.ShowDialog();
                numberHighlights = form1.folderCounter;
                position = form1.Location; //E:\Videos\Test
                position2 = form1.LocationHighlight; //E:\Videos\Test\Highlights
                entryIntro = form1.intro;
                entryOutro = form1.outro;
                folderNumber = form1.createdFolders;
                afterIntroFade = form1.afterIntroFadeOne;
                beforeOutroFade = form1.beforeOutroFadeOne;
                sonyImport = form1.sonyImportOne;
                timelineHighlight = form1.timelineHighlightOne;
                checkerOutro = form1.outroChecker;
                checkerIntro = form1.introChecker;

                if (result == DialogResult.OK)
                {

                }
                else
                {
                    return;
                }
            }


            try
            {
               if(checkerIntro && checkerOutro) //Both are checked
                {
                    Timecode cursorPosition = new Timecode();// new Timecode() I could just change this to 000000 usign the constuctor
                                                             //cursorPosition++;
                                                             //string imagePath = "E:\\Videos\\Highlights\\Highlights0\\Bridget.PNG";
                                                             //VideoEvent imageEvent = (VideoEvent)AddMedia(myVegas.Project, imagePath, 0, cursorPosition, Timecode.FromSeconds(10));
                    int counter = 0;
                    int dumbcounter = 0;
                    string basicPath = position2 + @"\" + "Highlights";
                    string intro = null;
                    string outro = null;
                    if (!string.IsNullOrEmpty(entryIntro))
                    {
                        intro = Directory.GetFiles(entryIntro, "*.mp4").FirstOrDefault();
                    }
                    if (!string.IsNullOrEmpty(entryOutro))
                    {
                        outro = Directory.GetFiles(entryOutro, "*.mp4").FirstOrDefault();
                    }


                    //Implement Outro later




                    while (counter < folderNumber+1) //NEEDS TO BE CHANGED
                    {
                        string currentFolder = basicPath + counter; // ie Highlights0 Highlights1 Highlights2 Highlights3 stops there but shouldn't
                        string[] fileNames = Directory.GetFiles(currentFolder, "*.mp4").Select(Path.GetFileName).ToArray();
                        Timecode initialStart = new Timecode();
                        Timecode endPosition = new Timecode();
                        VideoEvent v1EventPrevious = null;
                        AudioEvent v2EventPrevious = null;




                        foreach (string fileName in fileNames) //Need to only get files of mp4 because vegas will autocreate those terrible files. NOTE FOR THIS TO WORK A VIDEO TRACK AND AUDIO TRACK SHOULD ALREADY BE MADE
                        {


                            if (dumbcounter == 0) //Adds Intro to the beginning
                            {
                                initialStart = cursorPosition;
                                //MessageBox.Show("The value of intro is " + intro);
                                //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                                Media mediaIntro = Media.CreateInstance(myVegas.Project, intro);
                                Timecode lengthIntro = mediaIntro.Length;
                                VideoEvent v1EventIntro = (VideoEvent)AddMedia(myVegas.Project, intro, 0, cursorPosition, lengthIntro); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                                AudioEvent v2EventIntro = (AudioEvent)AddMedia(myVegas.Project, intro, 1, cursorPosition, lengthIntro);
                                cursorPosition = cursorPosition + lengthIntro;

                                dumbcounter++;
                            }





                            string specificFile = currentFolder + @"\" + fileName;
                            //MessageBox.Show("The value of specificFile is " + specificFile);
                            //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                            Media media = Media.CreateInstance(myVegas.Project, specificFile);
                            Timecode length = media.Length;
                            VideoEvent v1Event = (VideoEvent)AddMedia(myVegas.Project, specificFile, 0, cursorPosition, length); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                            AudioEvent v2Event = (AudioEvent)AddMedia(myVegas.Project, specificFile, 1, cursorPosition, length); //Adds audio but video has no audio file. Should work

                            cursorPosition = cursorPosition + length;
                            endPosition = cursorPosition;

                            if (dumbcounter == 1) //Adds fade to the clip after the intro
                            {
                                v1Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v1Event.FadeIn.Curve = CurveType.Smooth;
                                v2Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v2Event.FadeIn.Curve = CurveType.Smooth;
                                dumbcounter++;
                            }

                            v1EventPrevious = v1Event;
                            v2EventPrevious = v2Event;
                        }

                        v1EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v1EventPrevious.FadeOut.Curve = CurveType.Smooth;
                        v2EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v2EventPrevious.FadeOut.Curve = CurveType.Smooth;

                        Media mediaOutro = Media.CreateInstance(myVegas.Project, outro);
                        Timecode lengthOutro = mediaOutro.Length;
                        VideoEvent v1EventOutro = (VideoEvent)AddMedia(myVegas.Project, outro, 0, cursorPosition, lengthOutro);
                        AudioEvent v2EventOutro = (AudioEvent)AddMedia(myVegas.Project, outro, 1, cursorPosition, lengthOutro);
                        cursorPosition = cursorPosition + lengthOutro;

                        //MessageBox.Show("The value of initialStart is " + initialStart.ToString());
                        //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                        endPosition = cursorPosition;
                        //MessageBox.Show("I am ADDING A REGION THAT IS FROM " + initialStart + " to " + cursorPosition);
                        ScriptPortal.Vegas.Region region1 = new ScriptPortal.Vegas.Region(initialStart, cursorPosition - initialStart); //The second argument is LENGTH not just a timeframe
                        myVegas.Project.Regions.Add(region1);
                        cursorPosition = cursorPosition + Timecode.FromSeconds(timelineHighlight);
                        counter++;
                        dumbcounter = 0;

                        Thread.Sleep(sonyImport); // 30 second wait timer

                    }
                }
               else if(!checkerIntro && !checkerOutro) // Both are UNCHECKED
                {
                    Timecode cursorPosition = new Timecode();// new Timecode() I could just change this to 000000 usign the constuctor
                                                             //cursorPosition++;
                                                             //string imagePath = "E:\\Videos\\Highlights\\Highlights0\\Bridget.PNG";
                                                             //VideoEvent imageEvent = (VideoEvent)AddMedia(myVegas.Project, imagePath, 0, cursorPosition, Timecode.FromSeconds(10));
                    int counter = 0;
                    int dumbcounter = 0;
                    string basicPath = position2 + @"\" + "Highlights";



                    //Implement Outro later




                    while (counter < folderNumber+1) //NEEDS TO BE CHANGED
                    {
                        string currentFolder = basicPath + counter; // ie Highlights0 Highlights1 Highlights2 Highlights3 stops there but shouldn't
                        string[] fileNames = Directory.GetFiles(currentFolder, "*.mp4").Select(Path.GetFileName).ToArray();
                        Timecode initialStart = new Timecode();
                        Timecode endPosition = new Timecode();
                        VideoEvent v1EventPrevious = null;
                        AudioEvent v2EventPrevious = null;




                        foreach (string fileName in fileNames) //Need to only get files of mp4 because vegas will autocreate those terrible files. NOTE FOR THIS TO WORK A VIDEO TRACK AND AUDIO TRACK SHOULD ALREADY BE MADE
                        {








                            string specificFile = currentFolder + @"\" + fileName;
                            //MessageBox.Show("The value of specificFile is " + specificFile);
                            //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                            Media media = Media.CreateInstance(myVegas.Project, specificFile);
                            Timecode length = media.Length;
                            VideoEvent v1Event = (VideoEvent)AddMedia(myVegas.Project, specificFile, 0, cursorPosition, length); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                            AudioEvent v2Event = (AudioEvent)AddMedia(myVegas.Project, specificFile, 1, cursorPosition, length); //Adds audio but video has no audio file. Should work

                            cursorPosition = cursorPosition + length;
                            endPosition = cursorPosition;

                            if (dumbcounter == 0) //Adds fade to the clip after the intro
                            {
                                v1Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v1Event.FadeIn.Curve = CurveType.Smooth;
                                v2Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v2Event.FadeIn.Curve = CurveType.Smooth;
                                dumbcounter++;
                            }

                            v1EventPrevious = v1Event;
                            v2EventPrevious = v2Event;
                        }

                        v1EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v1EventPrevious.FadeOut.Curve = CurveType.Smooth;
                        v2EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v2EventPrevious.FadeOut.Curve = CurveType.Smooth;

                        //MessageBox.Show("The value of initialStart is " + initialStart.ToString());
                        //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                        endPosition = cursorPosition;
                        //MessageBox.Show("I am ADDING A REGION THAT IS FROM " + initialStart + " to " + cursorPosition);
                        ScriptPortal.Vegas.Region region1 = new ScriptPortal.Vegas.Region(initialStart, cursorPosition - initialStart); //The second argument is LENGTH not just a timeframe
                        myVegas.Project.Regions.Add(region1);
                        cursorPosition = cursorPosition + Timecode.FromSeconds(timelineHighlight);
                        counter++;
                        dumbcounter = 0;

                        Thread.Sleep(sonyImport); // 30 second wait timer

                    }
                }
               else if(checkerIntro && !checkerOutro) // Intro is checked, but Outro is UNCHECKED
                {
                    Timecode cursorPosition = new Timecode();// new Timecode() I could just change this to 000000 usign the constuctor
                                                             //cursorPosition++;
                                                             //string imagePath = "E:\\Videos\\Highlights\\Highlights0\\Bridget.PNG";
                                                             //VideoEvent imageEvent = (VideoEvent)AddMedia(myVegas.Project, imagePath, 0, cursorPosition, Timecode.FromSeconds(10));
                    int counter = 0;
                    int dumbcounter = 0;
                    string basicPath = position2 + @"\" + "Highlights";
                    string intro = null;
                    if (!string.IsNullOrEmpty(entryIntro))
                    {
                        intro = Directory.GetFiles(entryIntro, "*.mp4").FirstOrDefault();
                    }


                    //Implement Outro later




                    while (counter < folderNumber+1) //NEEDS TO BE CHANGED
                    {
                        string currentFolder = basicPath + counter; // ie Highlights0 Highlights1 Highlights2 Highlights3 stops there but shouldn't
                        string[] fileNames = Directory.GetFiles(currentFolder, "*.mp4").Select(Path.GetFileName).ToArray();
                        Timecode initialStart = new Timecode();
                        Timecode endPosition = new Timecode();
                        VideoEvent v1EventPrevious = null;
                        AudioEvent v2EventPrevious = null;




                        foreach (string fileName in fileNames) //Need to only get files of mp4 because vegas will autocreate those terrible files. NOTE FOR THIS TO WORK A VIDEO TRACK AND AUDIO TRACK SHOULD ALREADY BE MADE
                        {


                            if (dumbcounter == 0) //Adds Intro to the beginning
                            {
                                initialStart = cursorPosition;
                                //MessageBox.Show("The value of intro is " + intro);
                                //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                                Media mediaIntro = Media.CreateInstance(myVegas.Project, intro);
                                Timecode lengthIntro = mediaIntro.Length;
                                VideoEvent v1EventIntro = (VideoEvent)AddMedia(myVegas.Project, intro, 0, cursorPosition, lengthIntro); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                                AudioEvent v2EventIntro = (AudioEvent)AddMedia(myVegas.Project, intro, 1, cursorPosition, lengthIntro);
                                cursorPosition = cursorPosition + lengthIntro;

                                dumbcounter++;
                            }





                            string specificFile = currentFolder + @"\" + fileName;
                            //MessageBox.Show("The value of specificFile is " + specificFile);
                            //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                            Media media = Media.CreateInstance(myVegas.Project, specificFile);
                            Timecode length = media.Length;
                            VideoEvent v1Event = (VideoEvent)AddMedia(myVegas.Project, specificFile, 0, cursorPosition, length); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                            AudioEvent v2Event = (AudioEvent)AddMedia(myVegas.Project, specificFile, 1, cursorPosition, length); //Adds audio but video has no audio file. Should work

                            cursorPosition = cursorPosition + length;
                            endPosition = cursorPosition;

                            if (dumbcounter == 1) //Adds fade to the clip after the intro
                            {
                                v1Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v1Event.FadeIn.Curve = CurveType.Smooth;
                                v2Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v2Event.FadeIn.Curve = CurveType.Smooth;
                                dumbcounter++;
                            }

                            v1EventPrevious = v1Event;
                            v2EventPrevious = v2Event;
                        }

                        v1EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v1EventPrevious.FadeOut.Curve = CurveType.Smooth;
                        v2EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v2EventPrevious.FadeOut.Curve = CurveType.Smooth;

                        //MessageBox.Show("The value of initialStart is " + initialStart.ToString());
                        //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                        endPosition = cursorPosition;
                        //MessageBox.Show("I am ADDING A REGION THAT IS FROM " + initialStart + " to " + cursorPosition);
                        ScriptPortal.Vegas.Region region1 = new ScriptPortal.Vegas.Region(initialStart, cursorPosition - initialStart); //The second argument is LENGTH not just a timeframe
                        myVegas.Project.Regions.Add(region1);
                        cursorPosition = cursorPosition + Timecode.FromSeconds(timelineHighlight);
                        counter++;
                        dumbcounter = 0;

                        Thread.Sleep(sonyImport); // 30 second wait timer

                    }
                }
               else if (!checkerIntro && checkerOutro) //Intro is UNCHECKED, but Outro is checked
                {
                    Timecode cursorPosition = new Timecode();// new Timecode() I could just change this to 000000 usign the constuctor
                                                             //cursorPosition++;
                                                             //string imagePath = "E:\\Videos\\Highlights\\Highlights0\\Bridget.PNG";
                                                             //VideoEvent imageEvent = (VideoEvent)AddMedia(myVegas.Project, imagePath, 0, cursorPosition, Timecode.FromSeconds(10));
                    int counter = 0;
                    int dumbcounter = 0;
                    string basicPath = position2 + @"\" + "Highlights";
                    
                    string outro = null;
                    
                    if (!string.IsNullOrEmpty(entryOutro))
                    {
                        outro = Directory.GetFiles(entryOutro, "*.mp4").FirstOrDefault();
                    }


                    //Implement Outro later




                    while (counter < folderNumber+1) //NEEDS TO BE CHANGED
                    {
                        string currentFolder = basicPath + counter; // ie Highlights0 Highlights1 Highlights2 Highlights3 stops there but shouldn't
                        string[] fileNames = Directory.GetFiles(currentFolder, "*.mp4").Select(Path.GetFileName).ToArray();
                        Timecode initialStart = new Timecode();
                        Timecode endPosition = new Timecode();
                        VideoEvent v1EventPrevious = null;
                        AudioEvent v2EventPrevious = null;




                        foreach (string fileName in fileNames) //Need to only get files of mp4 because vegas will autocreate those terrible files. NOTE FOR THIS TO WORK A VIDEO TRACK AND AUDIO TRACK SHOULD ALREADY BE MADE
                        {


                            //Might need to initialize cursorpostion to something





                            string specificFile = currentFolder + @"\" + fileName;
                            //MessageBox.Show("The value of specificFile is " + specificFile);
                            //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                            Media media = Media.CreateInstance(myVegas.Project, specificFile);
                            Timecode length = media.Length;
                            VideoEvent v1Event = (VideoEvent)AddMedia(myVegas.Project, specificFile, 0, cursorPosition, length); //The number after specificFile is the track index aka how many rows should the video have. Cursor position is point on the timeline.
                            AudioEvent v2Event = (AudioEvent)AddMedia(myVegas.Project, specificFile, 1, cursorPosition, length); //Adds audio but video has no audio file. Should work

                            cursorPosition = cursorPosition + length;
                            endPosition = cursorPosition;

                            if (dumbcounter == 1) //Adds fade to the clip after the intro
                            {
                                v1Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v1Event.FadeIn.Curve = CurveType.Smooth;
                                v2Event.FadeIn.Length = Timecode.FromSeconds(afterIntroFade);
                                v2Event.FadeIn.Curve = CurveType.Smooth;
                                dumbcounter++;
                            }

                            v1EventPrevious = v1Event;
                            v2EventPrevious = v2Event;
                        }

                        v1EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v1EventPrevious.FadeOut.Curve = CurveType.Smooth;
                        v2EventPrevious.FadeOut.Length = Timecode.FromSeconds(beforeOutroFade);
                        v2EventPrevious.FadeOut.Curve = CurveType.Smooth;

                        Media mediaOutro = Media.CreateInstance(myVegas.Project, outro);
                        Timecode lengthOutro = mediaOutro.Length;
                        VideoEvent v1EventOutro = (VideoEvent)AddMedia(myVegas.Project, outro, 0, cursorPosition, lengthOutro);
                        AudioEvent v2EventOutro = (AudioEvent)AddMedia(myVegas.Project, outro, 1, cursorPosition, lengthOutro);
                        cursorPosition = cursorPosition + lengthOutro;

                        //MessageBox.Show("The value of initialStart is " + initialStart.ToString());
                        //MessageBox.Show("The value of cursorPosition is " + cursorPosition.ToString());
                        endPosition = cursorPosition;
                        //MessageBox.Show("I am ADDING A REGION THAT IS FROM " + initialStart + " to " + cursorPosition);
                        ScriptPortal.Vegas.Region region1 = new ScriptPortal.Vegas.Region(initialStart, cursorPosition - initialStart); //The second argument is LENGTH not just a timeframe
                        myVegas.Project.Regions.Add(region1);
                        cursorPosition = cursorPosition + Timecode.FromSeconds(timelineHighlight);
                        counter++;
                        dumbcounter = 0;

                        Thread.Sleep(sonyImport); // 30 second wait timer

                    }
                }
                

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Specified argument was out of the range of valid values.
            }

        }

        TrackEvent[] GetSelectedEvents(Project project)
        {
            List<TrackEvent> selectedlist = new List<TrackEvent>();
            foreach (Track track in project.Tracks)
            {
                foreach (TrackEvent trackEvent in track.Events)
                {
                    selectedlist.Add(trackEvent);
                }
            }
            return selectedlist.ToArray();
        }

        TrackEvent AddMedia(Project project, string mediaPath, int trackIndex, Timecode start, Timecode length)
        {
            Media media = Media.CreateInstance(project, mediaPath);
            Track track = project.Tracks[trackIndex];
            
            


            if (track.MediaType == MediaType.Video)
            {
                VideoTrack videoTrack = (VideoTrack)track;
                VideoEvent videoEvent = videoTrack.AddVideoEvent(start, length);
                Take take = videoEvent.AddTake(media.GetVideoStreamByIndex(0));
                return videoEvent;
            }
            else if (track.MediaType == MediaType.Audio)
            {
                AudioTrack audioTrack = (AudioTrack)track;
                AudioEvent audioEvent = audioTrack.AddAudioEvent(start, length);
                Take take = audioEvent.AddTake(media.GetAudioStreamByIndex(0));
                return audioEvent;
            }

            //should be impossible
            return null;
        }





    }

    
    
}
