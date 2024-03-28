using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Game
{
    public class Music
    {
        private string test;
        private int[] frequencies;
        private int[] durations;
        static WaveOutEvent waveOut;


        public Music()
        {

        }

        public void PlayBeepSelectMenu()
        {
            Console.Beep(392, 100);
        }

        public void PlayEnterMenu()
        {
            Console.Beep(262, 100);
        }


        public void ImporterMP3(string mp3FilePath)
        {
            try
            {
                using (var reader = new Mp3FileReader(mp3FilePath))
                {

                    waveOut = new WaveOutEvent();
                    waveOut.Init(reader);
                    waveOut.Volume = 0.1f;

                    waveOut.PlaybackStopped += (sender, e) => {
                        waveOut.Stop();
                        waveOut.Dispose();
                        ImporterMP3(mp3FilePath); 
                    };

                    waveOut.Play();

                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue lors de l'importation du fichier MP3 : " + ex.Message);
            }
            finally
            {
                waveOut?.Dispose();
            }
        }

        public void CloseMusic()
        {
            waveOut.Stop();
        }
    }
}
