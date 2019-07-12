using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.WindowsMediaFormat;
using System.Diagnostics;
using NAudio.Lame;
using Alvas.Audio;

namespace Konwerter
{
    public partial class Konwerter : Form
    {
        private const int SampleSize = 1024;
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        int sekundy;
        int minuty;
        int liczba = 1;
        string sciezka;
        Stopwatch sw = new Stopwatch();
        double x = 0;
        double y = 0;
        double z = 0;
        int wynik = 0;

        public Konwerter()
        {
            InitializeComponent();

        }

        private void buttonotworz_Click(object sender, EventArgs e)
        {
            otworzplik();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonkonwertuj_Click(object sender, EventArgs e)
        {

            if (textBox1.TextLength != 0)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        WavtoMP3();
                        break;


                    case 1:
                        WAVtoMP4();
                        break;

                    case 2:
                        WAVtoWMA();
                        break;
                }
                sw.Stop();

            }
            else
            {
                MessageBox.Show("Nie wybrano pliku.");
            }
        }

        private void WavtoMP3()
        {
            sw.Reset();
            sw.Start();
            string plik;
            do
            {
                do
                {
                    plik = textBox1.Text;
                }
                while (!System.IO.File.Exists(plik));
            }
            while (!plik.EndsWith(".wav"));
            string plikmp3 = Path.GetFileNameWithoutExtension(plik) + 2 + Path.GetExtension(plik);
            string nowyplik = Path.GetFileNameWithoutExtension(plik) + 1 + Path.GetExtension(plik);
            var wavRdr = new WaveFileReader(nowyplik);
            {
                using (var mp3Writer = new LameMP3FileWriter(plikmp3, wavRdr.WaveFormat, 64))
                {
                    wavRdr.CopyTo(mp3Writer);
                }
            }

            string popliku = Path.GetFileNameWithoutExtension(plikmp3) + ".mp3";
            File.Move(plikmp3, Path.ChangeExtension(plikmp3, ".mp3"));

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(popliku);
            if (fileInfo.Exists)
            {
                long bytes = 0;
                double kilobytes = 0;
                double megabytes = 0;

                bytes = fileInfo.Length;
                kilobytes = (double)bytes / 1024;
                megabytes = kilobytes / 1024;
                y = Math.Round(megabytes, 3);
                label3.Text = "Po konwersji: " + y + " MB";
                string format1 = fileInfo.Extension;
                label4.Text = Path.GetFileNameWithoutExtension(popliku) + "  Typ pliku: " + format1;

                z = (100 - ((100 * y) / x));
                wynik = Convert.ToInt32(System.Math.Floor(z));
                label11.Text = "Stopień konwersji: " + wynik + "%";

            }
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            czas.Text = String.Format("{0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            MessageBox.Show("Konwersja zakończona. Plik zapisany w: " + popliku + ".\nNaciśnij dowolny klawisz.");
            Console.In.ReadLine();
        }

        private void otworzplik()
        {
            label3.Text = "";
            label4.Text = "";

            openFileDialog1.Filter = "WAV(*.wav)|*.wav| MP3(*.mp3)|*.mp3| WMA(*.wma)|*.wma| MP4(*.mp4)|*.mp4| Wszystkie pliki(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.AutoUpgradeEnabled = false;
            openFileDialog1.Title = "Konwersja";
            pictureBox1.Image = null;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                textBox1.Text = Path.GetFullPath(openFileDialog1.FileName);
                string path = Path.GetFileName(openFileDialog1.FileName);

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
                if (fileInfo.Exists)
                {
                    long bytes = 0;
                    double kilobytes = 0;
                    double megabytes = 0;

                    bytes = fileInfo.Length;
                    kilobytes = (double)bytes / 1024;
                    megabytes = kilobytes / 1024;
                    x = Math.Round(megabytes, 3);
                    label2.Text = "Przed konwersja: " + x + " MB";
                    string format = fileInfo.Extension;
                    label1.Text = Path.GetFileNameWithoutExtension(path) + "  Typ pliku: " + format;
                }
                sr.Close();
            }
        }

        private void WAVtoMP4()
        {
            sw.Reset();
            sw.Start();
            string plik;
            do
            {
                do
                {
                    plik = textBox1.Text;
                }
                while (!System.IO.File.Exists(plik));
            }
            while (!plik.EndsWith(".wav"));
            string plikwav = plik.Replace(".wav", ".mp4");
            string pliksciezka = plikwav;

            int index = plikwav.LastIndexOf("\\");
            string nazwawav = plikwav.Substring(index + 1, plikwav.Length - index - 1);
            index = plik.LastIndexOf("\\");
            string nazwamp3 = plik.Substring(index + 1, plik.Length - index - 1);

            using (var reader = new MediaFoundationReader(textBox1.Text))
            {
                MediaFoundationEncoder.EncodeToAac(reader, plikwav);
            }

            string path1 = Path.GetFileName(plikwav);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path1);
            if (fileInfo.Exists)
            {
                long bytes = 0;
                double kilobytes = 0;
                double megabytes = 0;

                bytes = fileInfo.Length;
                kilobytes = (double)bytes / 1024;
                megabytes = kilobytes / 1024;
                y = megabytes;
                label3.Text = "Po konwersji: " + Convert.ToString(Math.Round(megabytes, 3)) + " MB";
                string format1 = fileInfo.Extension;
                label4.Text = Path.GetFileNameWithoutExtension(path1) + "  Typ pliku: " + format1;

                z = 100 - (y / x) * 100;
                wynik = Convert.ToInt32(System.Math.Floor(z));
                label11.Text = "Stopień konwersji: " + wynik + "%";
            }
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            czas.Text = String.Format("{0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            MessageBox.Show("Konwersja zakończona. Plik zapisany w: " + plikwav + ".\nNaciśnij dowolny klawisz.");
            Console.In.ReadLine();

        }

        private void WAVtoWMA()
        {
            sw.Reset();
            sw.Start();
            string plik;
            do
            {
                do
                {
                    plik = textBox1.Text;
                }
                while (!System.IO.File.Exists(plik));
            }
            while (!plik.EndsWith(".wav"));
            string plikwav = plik.Replace(".wav", ".wma");
            string pliksciezka = plikwav;

            int index = plikwav.LastIndexOf("\\");
            string nazwawav = plikwav.Substring(index + 1, plikwav.Length - index - 1);
            index = plik.LastIndexOf("\\");
            string nazwamp3 = plik.Substring(index + 1, plik.Length - index - 1);

            string pokompresji = Path.GetFileNameWithoutExtension(plik) + "-encoded" + ".wma";

            using (var reader = new MediaFoundationReader(textBox1.Text))
            {
                MediaFoundationEncoder.EncodeToAac(reader, plikwav);
            }

            using (var reader2 = new MediaFoundationReader(plikwav))
            {
                MediaFoundationEncoder.EncodeToWma(reader2, pokompresji, 192000);
            }

            File.Delete(plikwav);

            FileInfo fileInfo = new FileInfo(pokompresji);
            if (fileInfo.Exists)
            {
                long bytes = 0;
                double kilobytes = 0;
                double megabytes = 0;

                bytes = fileInfo.Length;
                kilobytes = (double)bytes / 1024;
                megabytes = kilobytes / 1024;
                y = megabytes;
                label3.Text = "Po konwersji: " + Convert.ToString(Math.Round(megabytes, 3)) + " MB";
                string format1 = fileInfo.Extension;
                label4.Text = Path.GetFileNameWithoutExtension(pokompresji) + "  Typ pliku: " + format1;

                z = (100 - ((100 * y) / x));
                wynik = Convert.ToInt32(System.Math.Floor(z));
                label11.Text = "Stopień konwersji: " + wynik + "%";
            }
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            czas.Text = String.Format("{0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            MessageBox.Show("Konwersja zakończona. Plik zapisany w: " + plikwav + ".\nNaciśnij dowolny klawisz.");
            Console.In.ReadLine();

        }
        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            stop.Enabled = true;

            waveSource = new WaveIn();
            waveSource.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            sciezka = @"C:\Users\" + Environment.UserName + "\\Music\\Nagranie" + liczba + ".wav";
            liczba++;
            waveFile = new WaveFileWriter(sciezka, waveSource.WaveFormat);

            waveSource.StartRecording();
            label8.Text = "Trwa nagrywanie!";
            label9.Text = "";
            minuty = 0;
            sekundy = 0;
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void stop_Click(object sender, EventArgs e)

        {
            stop.Enabled = false;
            waveSource.StopRecording();
            label8.Text = "";
            label9.Text = sciezka;
            timer1.Stop();
            label6.Text = "";
            timer1.Enabled = false;
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
            start.Enabled = true;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            sekundy++;
            if (sekundy > 59)
            {
                minuty++;
                sekundy = 0;
            }

            if (sekundy < 10 & minuty < 10)
            {
                label6.Text = "0" + minuty + ":0" + sekundy;
            }
            else if (sekundy < 10 & minuty >= 10)
            {
                label6.Text = minuty + ":0" + sekundy;
            }
            else if (sekundy >= 10 & minuty >= 10)
            {
                label6.Text = minuty + ":" + sekundy;
            }
            else
            {
                label6.Text = "0" + minuty + ":" + sekundy;
            }
        }
    }
 }
