using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using Tracklist.Domain;
using log4net;

namespace ICYServer_Stress_Test_Harness
{
    public partial class Form1 : Form
    {
        static ILog logger = LogManager.GetLogger(typeof(Form1));

        private int _maxThreads = 4;
        private int _maxRequestsPerThread = 4;
        private int _burstThreshold = 7500;
        private int _burstChance = 50;
        private double _minRequestInterval = 45.0;
        private double _maxRequestInterval = 90.0;

        private long _outstandingRequests = 0;
        private long _errorCount = 0;
        private long _totalBytes = 0;
        private long _totalTime = 0;
        private long _workingThreadCount = 0;
        private long _requestCompleteCount = 0;

        private List<TrackInfo> _allTracks;

        private delegate void TextUpdateDelegate(string text);
        private delegate void ToolstripRefreshDelegate();

        private ManualResetEvent _stopEvent = new ManualResetEvent(false);
        public Form1()
        {
            InitializeComponent();

            _allTracks = DataAccess.GetAllTracks();

            refreshToolstrip();
        }

        private void logText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new TextUpdateDelegate(logText), new object[] { text });
            }
            else
            {
                textBox1.Text += text;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }

            logger.Info(text);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stopEvent.Reset();


            //_outstandingRequests = 0;
            //_errorCount = 0;
            //_totalBytes = 0;
            //_totalTime = 0;
            //_requestCompleteCount = 0;

            refreshToolstrip();

            for( int i=0; i< _maxThreads; i++ )
            {
                System.Threading.Thread thread = new System.Threading.Thread(ThreadFunction);
                thread.Start();

                // wait a few ms for proper spacing
                Thread.Sleep(100);
            }

            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }

        private void ThreadFunction()
        {
            Random random = new Random();

            logText(string.Format("Thread {0} starting.\r\n", Thread.CurrentThread.ManagedThreadId));

            // create semaphore to control amount of simulataneous requests on this thread
            Semaphore requestSemaphore = new Semaphore(_maxRequestsPerThread, _maxRequestsPerThread);

            // request counter so we can garbage collect once in a while
            int requestCount = 0;
            int burstCount = 0;

            DateTime lastRequestTime = DateTime.MinValue;

            Interlocked.Increment(ref _workingThreadCount);
            refreshToolstrip();

            while (!_stopEvent.WaitOne(1, true))
            {
                // check burst
                bool bursting = false;
                if (burstCount > _burstThreshold)
                {
                    int randomNumber = random.Next(100);
                    if (randomNumber <= _burstChance)
                    {
                        bursting = true;

                    }

                    burstCount = 0;
                }
                else
                {
                    burstCount++;
                }

                TimeSpan span = DateTime.Now - lastRequestTime;

                if (((double)span.Ticks / (double)TimeSpan.TicksPerSecond) > _minRequestInterval || bursting)
                {
                    // make sure we have a slot for a request
                    if (requestSemaphore.WaitOne(0, true))
                    {
                        // generate a random index
                        int randomIndex = random.Next(_allTracks.Count);

                        logText(string.Format("Thread {0} making request for {1}\r\n", Thread.CurrentThread.ManagedThreadId, _allTracks[randomIndex].Url));
                        if (!makeRequest(string.Format("http://zoom.servemp3.com/track/{0}.mp3", _allTracks[randomIndex].TrackId), requestSemaphore))
                        {
                            // have to release since request didn't occur due to exception thrown
                            requestSemaphore.Release();
                        }
                        else
                        {
                            lastRequestTime = DateTime.Now;
                        }

                        if (++requestCount > 10)
                        {
                            // do some garbage collecting
                            GC.Collect();

                            requestCount = 0;
                        }
                    }
                }
            }

            Interlocked.Decrement(ref _workingThreadCount);
            refreshToolstrip();
        }

        private bool makeRequest(string url, Semaphore requestSemaphore)
        {
            bool success = false;

            // do request
            WebClient client = new System.Net.WebClient();
            client.DownloadDataCompleted += new System.Net.DownloadDataCompletedEventHandler(client_DownloadDataCompleted);

            AsynchRequestData data = new AsynchRequestData();
            data.ResourceBlocker = requestSemaphore;
            data.RequestStartTime = DateTime.Now;
            data.ThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                client.DownloadDataAsync(new Uri(url), data);
                success = true;

                Interlocked.Increment(ref _outstandingRequests);
            }
            catch (Exception ex)
            {
                logText(string.Format("Thread {0} had an exception.\r\n{1}\r\n", Thread.CurrentThread.ManagedThreadId, ex.ToString()));
            }

            refreshToolstrip();

            return success;
        }

        private void refreshToolstrip()
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new ToolstripRefreshDelegate(refreshToolstrip));
            }
            else
            {
                long totalBytes = Interlocked.Read(ref _totalBytes);
                long totalTime = Interlocked.Read(ref _totalTime);
                long totalErrors = Interlocked.Read(ref _errorCount);
                long outstandingRequests = Interlocked.Read(ref _outstandingRequests);
                long requestsComplete = Interlocked.Read(ref _requestCompleteCount);
                long threadCount = Interlocked.Read(ref _workingThreadCount);

                requestsOutstandingStatusLabel.Text = string.Format("[Requests Outstanding: {0}]", outstandingRequests);
                requestCompleteStatusLabel.Text = string.Format("[Requests Complete: {0}]", requestsComplete);
                requestErrorCountLabel.Text = string.Format("[Errors: {0}]", totalErrors);
                requestTotalBytesStatusLabel.Text = string.Format("[Total Bytes: {0:0.00}MB]", (double)totalBytes / 1000000.0);
                requestTotalTimeStatusLabel.Text = string.Format("[Total Time: {0}]", new TimeSpan(totalTime));
                workingThreadCountStatusLabel.Text = string.Format("[Working Threads: {0}]", threadCount);


                if (totalTime > 0)
                {
                    throughputStatusLabel.Text = string.Format("[Average Bandwidth: {0:0.00}MB/s]", ((double)totalBytes / 1000000.0) / ((double)totalTime / TimeSpan.TicksPerSecond));
                }
                else
                {
                    throughputStatusLabel.Text = "[Average Bandwidth: Not Calculated]";
                }

                if (requestsComplete > 0)
                {
                    TimeSpan averageTime = new TimeSpan( totalTime / requestsComplete );
                    requestAverageTimeStatusLabel.Text = string.Format("[Average Time: {0}]", averageTime );
                }
                else
                {
                    requestAverageTimeStatusLabel.Text = "[Average Time: Not Calculated]";
                }
            }
        }

        void client_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            AsynchRequestData data = e.UserState as AsynchRequestData;
            if (data != null)
            {

                if (e.Error != null)
                {
                    logText(string.Format("Thread Id {0} had an exception.\r\n{1}\r\n", data.ThreadId, e.Error.ToString()));
                    Interlocked.Increment(ref _errorCount);
                }
                else
                {
                    TimeSpan connectionTime = DateTime.Now - data.RequestStartTime;
                    Interlocked.Add(ref _totalBytes, e.Result.Length);
                    Interlocked.Add(ref _totalTime, connectionTime.Ticks);
                    Interlocked.Increment(ref _requestCompleteCount);

                    logText(string.Format("Thread Id {0} request complete.\r\nTotal Bytes: {1}\r\nTime Connected: {2}ms\r\nTransfer rate: {3:0.00} KB/s\r\n", data.ThreadId, e.Result.Length, connectionTime.Ticks / TimeSpan.TicksPerMillisecond, (double)(e.Result.Length / 1024) / (double)(connectionTime.Ticks / TimeSpan.TicksPerSecond)));
                }

                // decrement outstanding requests
                Interlocked.Decrement(ref _outstandingRequests);

                refreshToolstrip();

                data.ResourceBlocker.Release(1);

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _stopEvent.Set();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stopEvent.Set();

            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadConfigDlg dlg = new ThreadConfigDlg();

            dlg.BurstChance = _burstChance;
            dlg.BurstThreshold = _burstThreshold;
            dlg.MaxThreads = _maxThreads;
            dlg.MaxRequestsPerThread = _maxRequestsPerThread;
            dlg.MinRequestInterval = _minRequestInterval;
            dlg.MaxRequestInterval = _maxRequestInterval;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _maxThreads = dlg.MaxThreads;
                _maxRequestsPerThread = dlg.MaxRequestsPerThread;
                _burstThreshold = dlg.BurstThreshold;
                _burstChance = dlg.BurstChance;
                _minRequestInterval = dlg.MinRequestInterval;
                _maxRequestInterval = dlg.MaxRequestInterval;
            }

        }
    }
}