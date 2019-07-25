using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace OpenCVtest
{
    public partial class MainForm : Form
    {
        private VideoCapture cap;
        private bool capturing = false,islastSeted = false;
        private Mat lastDescriptor = new Mat(),lastBuffer;
        private Point lastPoint;
        private KeyPoint[] lastkeyPoints;
        private float distanceStandard = 60;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private async void CaptureLoop()
        {
            while (capturing)
            {
                await Task.Run(() =>
                {
                    Mat img = new Mat();
                    GetImg(ref img);
                    Cv2.ImShow("CapturedImage", img);
                    Cv2.WaitKey(1);
                    img.Release();
                    img = null;
                });
            }
            cap.Release();
            Cv2.DestroyAllWindows();
        }

        private void CaptureLoopSync()
        {
            Mat img = new Mat();
            while (capturing)
            {
                GetImg(ref img);
                Cv2.ImShow("CapturedImage", img);
                Cv2.WaitKey(100);
            }
            img.Release();
            img = null;
            cap.Release();
            Cv2.DestroyAllWindows();
            islastSeted = false;
        }

        private void GetImg(ref Mat buffer)
        {
            cap.Read(buffer);
            buffer = Process(ref buffer);
            return;
        }

        private void distance_ValueChanged(object sender, EventArgs e)
        {
            distanceStandard = (float)distance.Value * 10.0F;
        }

        private Mat Process(ref Mat buffer)
        {
            Mat img = new Mat();
            AKAZE akaze = AKAZE.Create();
            akaze.Threshold = 0.0001;
            KeyPoint[] keyPoints;
            DMatch[] matches;
            List<DMatch> goodMatches = new List<DMatch>();
            Mat descriptor = new Mat();
            DescriptorMatcher matcher = DescriptorMatcher.Create("BruteForce");
            Cv2.CvtColor(buffer, buffer, ColorConversionCodes.BGR2GRAY);
            akaze.DetectAndCompute(buffer, null, out keyPoints, descriptor);
            Cv2.DrawKeypoints(buffer, keyPoints,img,Scalar.Black);
            Cv2.ImShow("keyps", img);
            if (islastSeted)
            {
                matches = matcher.Match(descriptor, lastDescriptor);
                for (int i = 0; i < matches.Length; i++)
                    if (matches[i].Distance < distanceStandard)
                        goodMatches.Add(matches[i]);
                //Cv2.DrawMatches(buffer, keyPoints, lastBuffer, lastkeyPoints, goodMatches, img);
                img = buffer;
                if (goodMatches.Count > 3)
                {
                    float[] average = new float[2];
                    average[0] = 0; average[1] = 0;
                    for (int i = 0; i < goodMatches.Count; i++)
                    {
                        average[0] += keyPoints[goodMatches[0].QueryIdx].Pt.X -
                        lastkeyPoints[goodMatches[0].TrainIdx].Pt.X;
                        average[1] += keyPoints[goodMatches[0].QueryIdx].Pt.Y -
                        lastkeyPoints[goodMatches[0].TrainIdx].Pt.Y;
                    }
                    lastPoint = new Point(lastPoint.X + average[0] / goodMatches.Count, lastPoint.Y + average[1] / goodMatches.Count);
                    lastBuffer = buffer;
                    lastDescriptor = descriptor;
                    lastkeyPoints = keyPoints;
                }
                Cv2.Circle(img, lastPoint, 15, Scalar.Red, 3);
            }
            else
            {
                islastSeted = true;
                img = buffer;
                lastPoint = new Point(buffer.Cols / 2, buffer.Rows / 2);
                lastBuffer = buffer;
                lastDescriptor = descriptor;
                lastkeyPoints = keyPoints;
            }
            
            return img;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capturing) capturing = false;
        }

        private void startstop_Click(object sender, EventArgs e)
        {
            if (capturing)
            {
                startstop.Text = "開始";
                capturing = false;
            }
            else
            {
                startstop.Text = "停止";
                capturing = true;
                cap = new VideoCapture(0);
                if (!cap.IsOpened()) return;
                if(isSync.Checked)CaptureLoopSync();
                else CaptureLoop();
            }
        }
    }
}
