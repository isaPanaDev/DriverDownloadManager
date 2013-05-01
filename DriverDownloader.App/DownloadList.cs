using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DriverDownloader.Core;
using System.Collections;
using System.Diagnostics;
using System.IO;
using DriverDownloader.Core.Common;
using System.Web;
using System.Threading;
using System.Collections.ObjectModel;
using System.Timers;

namespace DriverDownloader
{
    public partial class DownloadList : UserControl
    {
        public delegate void ChangedEventHandler(int newOwner);
        public event ChangedEventHandler ExtractionCompleted;

        delegate void ActionDownloader(Downloader d, ListViewItem item);

        Hashtable mapItemToDownload = new Hashtable();
        Hashtable mapDownloadToItem = new Hashtable();
        Hashtable mapItemToCompleted = new Hashtable();
        Hashtable mapCompletedToItem = new Hashtable();
        
        ListViewItem lastSelection = null;
        List<BlockedProgressBar> progressList;

        // This boolean if set true shows the error image - if set to false shows a blank image
        // The timer class shifs it alternatively giving a blinking effect
        bool showErrorImage = true;

        // timerRunning - state of the Timer. true - if running, false otherwise to avoid unecessary cales on timer.Start()
        bool timerRunning = false;

        // Timer object that manages the blinking animation
        System.Timers.Timer blinker_timer;

        // Interface to 7z.exe      
        SevenZipInterface extractor;

        // whether to extract
        bool EXTRACT_FILES = true;
       
        public DownloadList()
        {
            InitializeComponent();
            progressList = new List<BlockedProgressBar>();
            DisableMenu();

            DownloadManager.Instance.DownloadAdded += new EventHandler<DownloaderEventArgs>(Instance_DownloadAdded);
            DownloadManager.Instance.DownloadRemoved += new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);
            DownloadManager.Instance.DownloadEnded += new EventHandler<DownloaderEventArgs>(Instance_DownloadEnded);
            //DownloadManager.Instance.DownloadEndedWithError += new EventHandler<DownloaderEventArgs>(Instance_DownloadEnded);

            //Remove to clear the list!
             for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
            {
                AddDownload(DownloadManager.Instance.Downloads[i]);
            }
            
            progressListView.SmallImageList = FileTypeImageList.GetSharedInstance();

            extractor = new SevenZipInterface();
      
            //Create ImageList
            ImageList iconsList = new ImageList();
            iconsList.TransparentColor = Color.Blue;
            iconsList.ColorDepth = ColorDepth.Depth32Bit;
            iconsList.ImageSize = new Size(13, 13);
            iconsList.Images.Add(DriverDownloader.Properties.Resources.error);
            iconsList.Images.Add(DriverDownloader.Properties.Resources.blank);
            //Pass the imagelist to TabControl
            tabSegmentsLogs.ImageList = iconsList;
            //Timer init
            blinker_timer = new System.Timers.Timer(500);
            blinker_timer.Elapsed += new ElapsedEventHandler(blinker_timer_Elapsed);
        }

        public void StartSelections()
        {
            DownloadsAction(
                delegate(Downloader d, ListViewItem item)
                {
                    d.Start();
                }
            );
        }   

        /*public void Pause()
        {
            DownloadsAction(
                delegate(Downloader d, ListViewItem item)
                {
                        if (d.RemoteFileInfo.AcceptRanges || Notify.proceedPause(this.ParentForm, d.LocalFile))
                        {
                            if (d.State != DownloaderState.Ended && d.State != DownloaderState.EndedWithError)
                            {
                                Console.WriteLine("Successfully Paused: " + d.LocalFile);
                                d.Pause();
                            }
                        }
                }
            );
        }*/

        bool allPaused = false;

        public bool getAllPaused_var() { return allPaused; }

        /*public void setAllPaused_var(bool state) { allPaused = state; }*/
       
        public void Pause()
        {
            DownloadsAction(
                delegate(Downloader d, ListViewItem item)
                {
                    if (d.State != DownloaderState.NeedToPrepare && 
                        d.State != DownloaderState.Pausing && 
                        d.State != DownloaderState.Paused &&
                        d.State != DownloaderState.Ended && 
                        d.State != DownloaderState.EndedWithError)
                    {
                        Console.WriteLine("Successfully Paused: " + d.LocalFile);
                        d.Pause();
                    }
                }
            );
            UpdateList();
        }

        public void PauseAll()
        {
            allPaused = false;
            bool allResumables = true;
            ReadOnlyCollection<Downloader> downloads = DownloadManager.Instance.Downloads;
            List<string> nonResumables = new List<string>();
            foreach (Downloader d in downloads)
            {
                if (d.State != DownloaderState.NeedToPrepare && d.State != DownloaderState.Pausing && d.State != DownloaderState.Paused)
                {
                    if (d.RemoteFileInfo != null)
                    {
                        if (!d.RemoteFileInfo.AcceptRanges)
                        {
                            nonResumables.Add(d.LocalFile);
                            allResumables = false;
                        }
                    }
                    else
                    {
                        nonResumables.Add(d.LocalFile);
                    }
                }
            }

            /*if (TempSettings.isClearAll || allResumables || nonResumables.Count==0 || Notify.proceedPause(this.ParentForm, nonResumables))
            {
                allPaused = true;
            }*/
            allPaused = true;
            if (allPaused)
            {
                //This foreach only tries to pause downloads that have not ended.
                // If DownloadManager.PauseAll does the same then can call that method instead
                // @note - PauseAll doesn't do this check
                //DownloadManager.Instance.PauseAll();
                foreach (Downloader d in downloads)
                {
                    if (d.State != DownloaderState.NeedToPrepare && 
                        d.State != DownloaderState.Pausing && 
                        d.State != DownloaderState.Paused &&
                        d.State != DownloaderState.Ended && 
                        d.State != DownloaderState.EndedWithError)
                    {
                            Console.WriteLine("Successfully Paused(All): " + d.LocalFile);
                            d.Pause();
                    }
                }
            }
            UpdateList();
        }

        public void ResumeStarted()
        {
            if (progressListView.SelectedItems.Count == 0)
            {
                DownloadManager.Instance.ResumeStarted();
                //allPaused = false;
            }
            else
            {
                for (int i = 0; i < progressListView.SelectedItems.Count; i++)
                {
                    Downloader d = mapItemToDownload[progressListView.SelectedItems[i]] as Downloader;
                    if (d.State == DownloaderState.Paused ||
                            d.State == DownloaderState.Pausing ||
                            d.State == DownloaderState.NeedToPrepare ||
                            d.State == DownloaderState.Prepared)
                    {
                        DownloadManager.Instance.ResumeStarted(d);
                    }
                }
            }
            UpdateList();
        }

        public void RemoveSelections()
        {
            ListView.SelectedIndexCollection indexes = progressListView.SelectedIndices;
            if (indexes.Count > 0)
            {
                if (MessageBox.Show(Resources.common.DownloadList_Messages_Confirm,
                    this.ParentForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DownloadManager.Instance.DownloadRemoved -= new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);

                        DownloadsAction(
                            delegate(Downloader d, ListViewItem item)
                            {
                                if (d.State != DownloaderState.NeedToPrepare &&
                                        d.State != DownloaderState.Pausing &&
                                        d.State != DownloaderState.Paused &&
                                        d.State != DownloaderState.Ended &&
                                        d.State != DownloaderState.EndedWithError)
                                {
                                    d.Pause();
                                }
                                progressListView.Items.RemoveAt(item.Index);
                                DownloadManager.Instance.RemoveDownload(d);
                                FileManager.DeleteFile(d.LocalFile);
                            }
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to remove");
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        DownloadManager.Instance.DownloadRemoved += new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);
                    }
                }
                this.UpdateList();
            }
        }

        public void SelectAll()
        {
            using (DownloadManager.Instance.LockDownloadList(false))
            {
                progressListView.BeginUpdate();
                try
                {
                    for (int i = 0; i < progressListView.Items.Count; i++)
                    {
                        progressListView.Items[i].Selected = true;
                    }
                }
                finally
                {
                    progressListView.EndUpdate();
                }
            }
        }
        internal void ClearList()
        {
            if (progressList != null)
            {
                progressList.Clear();
                progressList = null;
            }
            progressListView.Items.Clear();
            FileManager.DeleteFiles(DownloadManager.Instance.getAllFilePaths());
            DownloadManager.Instance.ClearAll(); 
        }

        public void LoadSettingsView()
        {
          // For when we want to load last saved settings!
        }

        public void UpdateUI()
        {
            if (progressListView.Items.Count == 0)
            {
                this.DisableMenu();
            }
            else
            {
                toolClearAll.Enabled = true;
                //UpdateUI_All();
                if (progressListView.SelectedItems.Count > 0)
                {
                    //UpdateUI_SelectedItems();
                    toolRemove.Enabled = true;
                }
                else
                {
                    toolRemove.Enabled = false;
                }
            }
            OnSelectionChange();
        }

        private void UpdateUI_SelectedItems()
        {
            //Init states
            bool enablePause = false;
            bool enableResume = false;
            for (int i = 0; i < progressListView.SelectedItems.Count; i++)
            {
                Downloader d = mapItemToDownload[progressListView.SelectedItems[i]] as Downloader;
                //Validate whether to enable pause
                if (d.State == DownloaderState.NeedToPrepare || 
                    d.State == DownloaderState.Pausing || 
                    d.State == DownloaderState.Paused || 
                    d.State == DownloaderState.Prepared)
                {
                    enablePause = enablePause || false;
                    enableResume = enableResume || true;
                }
                else
                {
                    enablePause = enablePause || true;
                    enableResume = enableResume || false;
                }
            }
            //Apply states
            toolPause.Enabled = enablePause;
            toolResumeStarted.Enabled = enableResume;
            toolResumeStarted.Text = Resources.common.DownloadList_UI_Start;
        }

        private void UpdateUI_All()
        {
            //Init states
            bool enablePauseAll = false;
            for (int i = 0; i < progressListView.Items.Count; i++)
            {
                Downloader d = mapItemToDownload[progressListView.Items[i]] as Downloader;
                //Validate whether to enable pauseAll
                if (d.State != DownloaderState.NeedToPrepare && 
                    d.State != DownloaderState.Pausing && 
                    d.State != DownloaderState.Paused && 
                    d.State != DownloaderState.Prepared)
                {
                    enablePauseAll = true;
                    break;
                }
            }
            //Apply states
            toolPauseAll.Enabled = enablePauseAll;
            toolResumeStarted.Enabled = !enablePauseAll;
            toolResumeStarted.Text = Resources.common.DownloadList_UI_StartAll;
        }

        public event EventHandler SelectionChange;

        protected virtual void OnSelectionChange()
        {
            if (SelectionChange != null)
            {
                SelectionChange(this, EventArgs.Empty);
            }
        }

        private void copyURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SelectedDownloaders[0].ResourceLocation.URL);
        }

        private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", String.Format("/select,{0}", SelectedDownloaders[0].LocalFile));
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(SelectedDownloaders[0].LocalFile);
            }
            catch (Exception)
            {
            }
        }

        public int SelectedCount
        {
            get
            {
                return progressListView.SelectedItems.Count;
            }
        }

        public Downloader[] SelectedDownloaders
        {
            get
            {
                if (progressListView.SelectedItems.Count > 0)
                {
                    Downloader[] downloaders = new Downloader[progressListView.SelectedItems.Count];
                    for (int i = 0; i < downloaders.Length; i++)
                    {
                        downloaders[i] = mapItemToDownload[progressListView.SelectedItems[i]] as Downloader;
                    }
                    return downloaders;
                }

                return null;
            }
        }

        void Instance_DownloadRemoved(object sender, DownloaderEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
                {
                    ListViewItem item = mapDownloadToItem[e.Downloader] as ListViewItem;

                    if (item != null)
                    {
                        if (item.Selected)
                        {
                            lastSelection = null;

                            //lvwCompleted.Items.Clear();
                            //progressListView.SelectedItems.Clear();
                        }

                        mapDownloadToItem[e.Downloader] = null;
                        mapItemToDownload[item] = null;

                        item.Remove();
                    }
                }
            );
        }

        void Instance_DownloadAdded(object sender, DownloaderEventArgs e)
        {
            if (IsHandleCreated)
            {
                this.BeginInvoke((MethodInvoker)delegate() { AddDownload(e.Downloader); });
            }
            else
            {
                AddDownload(e.Downloader);
            }
        }

        public void AddDownload(Downloader d)
        {
            d.RestartingSegment += new EventHandler<SegmentEventArgs>(download_RestartingSegment);
            d.SegmentStoped += new EventHandler<SegmentEventArgs>(download_SegmentEnded);
            d.SegmentFailed += new EventHandler<SegmentEventArgs>(download_SegmentFailed);
            d.SegmentStarted += new EventHandler<SegmentEventArgs>(download_SegmentStarted);
            d.InfoReceived += new EventHandler(download_InfoReceived);
            d.SegmentStarting += new EventHandler<SegmentEventArgs>(Downloader_SegmentStarting);

            string ext = Path.GetExtension(d.LocalFile);

            ListViewItem owner = new ListViewItem();
            owner.ImageIndex = FileTypeImageList.GetImageIndexByExtention(ext);
            owner.Text = d.FilePath;

            // size
            ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem(owner, ByteFormatter.ToString(d.FileSize));
            item.Name = "Size";
            owner.SubItems.Add(item);
            // completed
            item = new ListViewItem.ListViewSubItem(owner, ByteFormatter.ToString(d.Transfered));
            item.Name = "Done";
            owner.SubItems.Add(item);
             // state
            item = new ListViewItem.ListViewSubItem(owner, d.State.ToString());
            item.Name = "State";
            owner.SubItems.Add(item);
            // progressbar
            item = new ListViewItem.ListViewSubItem(owner, "");
            item.Name = "Progressbar";
            owner.SubItems.Add(item);
        
            // Finalize things before adding progressbar!!
            mapDownloadToItem[d] = owner;
            mapItemToDownload[owner] = d;
            progressListView.Items.Add(owner);

            // Add the progressbar!  
            if (progressList == null)
              progressList = new List<BlockedProgressBar>();
            
            int i = progressList.Count + 1;
            BlockedProgressBar pb = new BlockedProgressBar();
            pb.ForeColor = Color.Red;

            progressListView.AddEmbeddedControl(pb, 4, owner.Index); // 4 is column number for progressbar

            // Save into the progressList the added progressbar
            progressList.Add(pb);

            //toolPauseAll.Enabled = true;
        }
    
        private static string GetResumeStr(Downloader d)
        {
            return (d.RemoteFileInfo != null && d.RemoteFileInfo.AcceptRanges ? "Yes" : "No");
        }

        public void UpdateList()
        {
            for (int i = 0; i < progressListView.Items.Count; i++)
            {
                ListViewItem item = progressListView.Items[i];
                if (item == null) return;

                Downloader d = mapItemToDownload[item] as Downloader;
                if (d == null) return;

                DownloaderState state;
             
                if (item.Tag == null) state = DownloaderState.Working;
                else state = (DownloaderState)item.Tag;

                if (state != d.State ||
                    state == DownloaderState.Working ||
                    state == DownloaderState.WaitingForReconnect || state == DownloaderState.Paused)
                {
                    item.SubItems[0].Text = d.FilePath;
                    item.SubItems[1].Text = ByteFormatter.ToString(d.FileSize);
                    item.SubItems[2].Text = ByteFormatter.ToString(d.Transfered);

                    //@hack
                    string fileStatus = d.State.ToString();
                    try
                    {
                        if (!d.RemoteFileInfo.AcceptRanges && d.State==DownloaderState.Paused)
                            fileStatus = "Stopped";

                    }
                    catch (Exception ex)
                    {
                        fileStatus = d.State.ToString();
                    }

                    //item.SubItems[3].Text = d.State.ToString();
                    item.SubItems[3].Text = fileStatus;
                    //item.SubItems[4].Text = String.Format("{0:0.##}", d.Rate / 1024.0);
                    
                    this.progressListView.BeginUpdate();
                    // Update the progressbar for this particular Downloader instance!
                    List<Block> blocks = new List<Block>();
                    for (int p = 0; p < d.Segments.Count; p++)
                    {
                        blocks.Add(new Block(d.Segments[p].TotalToTransfer, (float)d.Segments[p].Progress));
                    }
                    try
                    {
                        BlockedProgressBar currentProgressBar = progressList[i];
                        currentProgressBar.BlockList = blocks;
                        currentProgressBar.Refresh();

                        // Create the text on top of the progressbar!!
                        if (d.Progress != 0)
                        {
                            string percent = String.Format("{0:0.##}%", d.Progress);
                            currentProgressBar.CreateGraphics().DrawString(percent, new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black,
                                new PointF(currentProgressBar.Width / 2 - 10, currentProgressBar.Height / 2 - 7));
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        this.progressListView.EndUpdate();
                    }
                    item.Tag = d.State;
                }
            }
      
            UpdateSegments();
            UpdateUI();
        }
        
        private void UpdateSegments()
        {
            try
            {
                progressListView.BeginUpdate();

                if (progressListView.SelectedItems.Count == 1)
                {
                    ListViewItem newSelection = progressListView.SelectedItems[0];
                    Downloader d = mapItemToDownload[newSelection] as Downloader;
                    UpdateFullProgressbar(d, newSelection); // update progressbar for the selected segment!
                }
                else
                {
                    lastSelection = null;

                   // blockedProgressBar.BlockList.Clear();
                   // blockedProgressBar.Refresh();
                }
            }
            finally
            {
                progressListView.EndUpdate();
            }
        }
        // Calculates and updates the segment and increases the progressbar 
        // Note: not used currently, but maybe for future when overall progressbar added!
        private void UpdateFullProgressbar(Downloader d, ListViewItem newSelection)
        {
            lastSelection = newSelection;

            List<Block> blocks = new List<Block>();

            for (int i = 0; i < d.Segments.Count; i++)
            {
                blocks.Add(new Block(d.Segments[i].TotalToTransfer, (float)d.Segments[i].Progress));
            }

           // this.blockedProgressBar.BlockList = blocks;
        }

        private void DownloadsAction(ActionDownloader action)
        {
            if (progressListView.SelectedItems.Count > 0)
            {
                try
                {
                    progressListView.BeginUpdate();

                    for (int i = progressListView.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = progressListView.SelectedItems[i];
                        action((Downloader)mapItemToDownload[item], item);
                    }
                }
                finally
                {
                    progressListView.EndUpdate();
                    UpdateSegments();
                }
            }            
        }
    
        void download_InfoReceived(object sender, EventArgs e)
        {
            Downloader d = (Downloader)sender;

            Log(
                d,
                String.Format(
                "Connected to: {2}. File size = {0}, Resume = {1}",
                ByteFormatter.ToString(d.FileSize),
                d.RemoteFileInfo.AcceptRanges,
                d.LocalFile),
                LogMode.Information);
        }

        void Downloader_SegmentStarting(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Starting segment for {3}, start position = {0}, end position {1}, segment size = {2}",
                ByteFormatter.ToString(e.Segment.InitialStartPosition),
                ByteFormatter.ToString(e.Segment.EndPosition),
                ByteFormatter.ToString(e.Segment.TotalToTransfer),
                e.Downloader.LocalFile),
                LogMode.Information);
        }

        void download_SegmentStarted(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Started segment for {3}, start position = {0}, end position {1}, segment size = {2}",
                ByteFormatter.ToString(e.Segment.InitialStartPosition),
                ByteFormatter.ToString(e.Segment.EndPosition),
                ByteFormatter.ToString(e.Segment.TotalToTransfer),
                e.Downloader.LocalFile),
                LogMode.Information);
        }

        void download_SegmentFailed(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) failed for {2}, reason = {1}",
                e.Segment.Index,
                e.Segment.LastError.Message,
                e.Downloader.LocalFile),
                LogMode.Error);
        }

        void download_SegmentEnded(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) ended for {1}",
                e.Segment.Index,
                e.Downloader.LocalFile),
                LogMode.Information);
        }

        void download_RestartingSegment(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) is restarting for {1}",
                e.Segment.Index,
                e.Downloader.LocalFile),
                LogMode.Information);
        }
        
        public void RemoveCompleted()
        {
            try
            {
                this.BeginInvoke(
                    (MethodInvoker)
                  delegate()
                  {
                      progressListView.BeginUpdate();
                      try
                      {
                          int iCompleted = DownloadManager.Instance.CompletedIndex();
                          Downloader dCompleted = DownloadManager.Instance.Downloads[iCompleted];
                          dCompleted = null;

                          DownloadManager.Instance.ClearEnded();
                          UpdateList();
                      }
                      finally
                      {
                          progressListView.EndUpdate();
                      }
                  }
              );
            }
            catch { }
        }

        private void MoveErrored()
        {
            try
            {
                this.BeginInvoke(
                    (MethodInvoker)
                  delegate()
                  {
                      progressListView.BeginUpdate();
                      errorListView.BeginUpdate();
                      try
                      {
                          int iCompletedError = DownloadManager.Instance.CompletedWithErrorIndex();
                          // Show in the "Completed" tab
                          if (iCompletedError != -1)
                          {
                              Downloader dCompleted = DownloadManager.Instance.Downloads[iCompletedError];
                              AddToErrorTab(dCompleted);
                              if (!timerRunning)
                              {
                                  Console.WriteLine("Start blinking");
                                  blinker_timer.Start();
                              }

                              // Remove it from the "Job List"
                              DownloadManager.Instance.ClearEndedWithError();
                              progressList.RemoveAt(iCompletedError);  // ERROR ON THIS LINE!!!!!
                              UpdateList();
                          }
                      }
                      finally
                      {
                          progressListView.EndUpdate();
                          errorListView.EndUpdate();
                      }
                  }
              );
            }
            catch { }

        }
        // Add the ended jobs to the error tab
        public void AddToErrorTab(Downloader d)
        {
            ListViewItem owner = new ListViewItem();
            string ext = Path.GetExtension(d.LocalFile);
            owner.ImageIndex = FileTypeImageList.GetImageIndexByExtention(ext);
            owner.Text = d.FilePath;
            // size
            ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem(owner, ByteFormatter.ToString(d.FileSize));
            item.Name = "Size";
            owner.SubItems.Add(item);
            // state
            item = new ListViewItem.ListViewSubItem(owner, d.State.ToString());
            item.Name = "State";
            owner.SubItems.Add(item);

           // mapCompletedToItem[d] = owner;
           // mapItemToCompleted[owner] = d;
            errorListView.Items.Add(owner);
        }
        // Move the instance from "Job List" tab to the "Completed" tab
        private void MoveCompleted()
        {
            try
            {
                this.BeginInvoke(
                    (MethodInvoker)
                  delegate()
                  {
                      progressListView.BeginUpdate();
                      completedListView.BeginUpdate();
                      try
                      {
                              int iCompleted = DownloadManager.Instance.CompletedIndex();
                              // Show in the "Completed" tab
                              if (iCompleted != -1)
                              {
                                  Downloader dCompleted = DownloadManager.Instance.Downloads[iCompleted];
                                  AddDownloadCompleted(dCompleted);

                                  // Remove it from the "Job List"
                                  DownloadManager.Instance.ClearEnded();
                                  progressList.RemoveAt(iCompleted);
                                  UpdateList();
                              }
                      }
                      finally
                      {
                          progressListView.EndUpdate();
                          completedListView.EndUpdate();
                      }
                  }
              );
            }
            catch { }
        }
        // Add the ended jobs to the completed tab
        public void AddDownloadCompleted(Downloader d)
        {
            ListViewItem owner = new ListViewItem(); 
            string ext = Path.GetExtension(d.LocalFile);
            owner.ImageIndex = FileTypeImageList.GetImageIndexByExtention(ext);
            owner.Text = d.FilePath;
            // size
            ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem(owner, ByteFormatter.ToString(d.FileSize));
            item.Name = "Size";
            owner.SubItems.Add(item);
            // state
            item = new ListViewItem.ListViewSubItem(owner, d.State.ToString());
            item.Name = "State";
            owner.SubItems.Add(item);
            
            mapCompletedToItem[d] = owner;
            mapItemToCompleted[owner] = d;
            completedListView.Items.Add(owner);
        }

        // When a download is completed!
        void Instance_DownloadEnded(object sender, DownloaderEventArgs e)
        {
            LogMode mode;
            // Move completed to either completed or error tabs!
            if (e.Downloader.State == DownloaderState.EndedWithError)
            {
                mode = LogMode.Error;
                //tabSegmentsLogs.SelectedTab = tabError;
                this.MoveErrored();
            }
            else
            {   // Extraction Completed
                mode = LogMode.Information;
                if (EXTRACT_FILES && extractor.extractorExists())
                {
                    // Fix the bug the Installation Instructions PDF gets deleted
                    // 8/3/2012 by George
                    // skip unzipping for PDF files
                    if (Path.GetExtension(e.Downloader.LocalFile).ToUpper() != ".PDF")
                    {
                        if (extractor.Extract(e.Downloader))
                            FileManager.DeleteFile(e.Downloader.LocalFile); //delete sfx files
                    }

                    // Notify extraction completed so that driver list can update its view/colors
                   this.BeginInvoke(
                           (MethodInvoker)
                         delegate()
                         {
                             OnExtractionEnded(e.Downloader.ExtractedCompletedIndex);
                         });
                }
                // Move the completed job from the "Job list" tab and put to the "Completed" tab
                this.MoveCompleted();
            }

            Log(
                e.Downloader,
                String.Format(
                "Download ended {0}",
                e.Downloader.LocalFile),
                mode);

            // Disable menu tools if no item in the list
            if (progressListView.Items.Count == 0)
                DisableMenu();
        }

        // Pass to main the resource location so we can get the driver that
        // is done extracting - in order to notify driverlist to update color!
        private void OnExtractionEnded(int completedIndex)
        {
            if (this.ExtractionCompleted != null)
                this.ExtractionCompleted(completedIndex);
        }

        enum LogMode
        {
            Error,
            Information
        }

        void Log(Downloader downloader, string msg, LogMode m)
        {
            try
            {
                this.BeginInvoke(
                    (MethodInvoker)
                  delegate()
                  {
                      /*int len = txtboxLog.Text.Length;
                      if (len > 0)
                      {
                          txtboxLog.SelectionStart = len;
                      }

                      if (m == LogMode.Error)
                      {
                          txtboxLog.SelectionColor = Color.Red;
                      }
                      else
                      {
                          txtboxLog.SelectionColor = Color.Blue;
                      }

                      txtboxLog.AppendText(msg + Environment.NewLine);*/
                  }
              );
            }
            catch { }
        }
        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // txtboxLog.Clear();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSelections();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelections();
        }

        private void popUpContextMenu_Opening(object sender, CancelEventArgs e)
        {
            UpdateUI();
        }

        private void lvwDownloads_DoubleClick(object sender, EventArgs e)
        {
            UpdateUI();

            openFileToolStripMenuItem_Click(sender, e);
        }
               
        private void lvwDownloads_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateList();
        }

        // Tool Menu Methods
        private void DisableMenu () {
            try
            {
                toolResumeStarted.Enabled = false;
                toolRemove.Enabled = false;
                toolPause.Enabled = false;
                toolPauseAll.Enabled = false;
                toolClearAll.Enabled = false;
            }
            catch (Exception e)
            {
                //do nothing. Code isn't ThreadSafe
            }
        }
        private void toolPauseAll_Click(object sender, EventArgs e)
        {
            this.PauseAll();
            /*if (allPaused)
            {
                toolResumeStarted.Enabled = true;
                toolPauseAll.Enabled = false;
                toolPause.Enabled = false;
            }*/
        }

        private void toolPause_Click(object sender, EventArgs e)
        {
            bool allResumables = true;
            List<string> nonResumables = new List<string>();
            for (int i = 0; i < progressListView.SelectedItems.Count; i++)
            {
                Downloader d = mapItemToDownload[progressListView.SelectedItems[i]] as Downloader;
                if (d.State != DownloaderState.NeedToPrepare && d.State != DownloaderState.Pausing && d.State != DownloaderState.Paused)
                {
                    if (d.RemoteFileInfo != null)
                    {
                        if (!d.RemoteFileInfo.AcceptRanges)
                        {
                            nonResumables.Add(d.LocalFile);
                            allResumables = false;
                        }
                    }
                    else
                    {
                        nonResumables.Add(d.LocalFile);
                    }
                }   
            }
            if (nonResumables.Count > 0)
            {
                if (allResumables || Notify.proceedPause(this, nonResumables))
                {
                    this.Pause();
                    //@note - below statments are commented, because UI elements are automatically
                    //        updated when a state of any downloader is changed
                    //toolPause.Enabled = false;
                    //toolResumeStarted.Enabled = true;
                }
            }
        }

        // Clear All clicked on "Job List" tab
        private void toolClearAll_Click(object sender, EventArgs e)
        {
            if (Notify.proceedClearAll(this))
            {
                TempSettings.isClearAll = true;
                this.PauseAll();
                this.ClearList();
                //DisableMenu();
                //toolPauseAll.Enabled = false;
                //toolClearAll.Enabled = false;
            }
            TempSettings.isClearAll = false;
            UpdateList();
        }

        private void toolRemove_Click(object sender, EventArgs e)
        {
            this.RemoveSelections();
        }

        private void toolResumeStarted_Click(object sender, EventArgs e)
        {
            if (DownloadManager.Instance.GetActiveJobsCount() >= Settings.Default.concurrentMaxJobs)
                Notify.maxJobsReached(this, Settings.Default.concurrentMaxJobs);
            else
                this.ResumeStarted();
            //UpdateList();
            //toolResumeStarted.Enabled = false;
            //toolPauseAll.Enabled = true;
        }
        // "Completed" tab: "Open File" clicked
        private void toolOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                Downloader d = mapItemToCompleted[completedListView.SelectedItems[0]] as Downloader;
                Process.Start(d.LocalFile);
            }
            catch (Exception)
            {
            }
        }
        // "Completed" tab: Clear All clicked
        private void toolClearCompleted_Click(object sender, EventArgs e)
        {
            if (Notify.proceedClearAllCompleted(this))
            {
                completedListView.Items.Clear();
            }
        }
        // "Completed" tab: double click
        private void completedListView_DoubleClick(object sender, EventArgs e)
        {
            Downloader d = mapItemToCompleted[completedListView.SelectedItems[0]] as Downloader;
            if (File.Exists(d.LocalFile))
                Process.Start(d.LocalFile);
        }

        private void progressListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void tabSegmentsLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSegmentsLogs.SelectedIndex == 2)
            {
                blinker_timer.Stop();
                timerRunning = false;
                tabError.ImageIndex = -1;
            }
        }

        public delegate void process();

        private void blinker_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke(new process(blink), null);
        }

        private void blink()
        {
            //Console.WriteLine("blinking");
            timerRunning = true;
            if (showErrorImage)
                tabError.ImageIndex = 0;
            else
                tabError.ImageIndex = 1;
            showErrorImage = !showErrorImage;
        }

        private void toolClearError_Click(object sender, EventArgs e)
        {
            if (Notify.proceedClearAllCompleted(this))
            {
                errorListView.Items.Clear();
            }
        }

    }
}
