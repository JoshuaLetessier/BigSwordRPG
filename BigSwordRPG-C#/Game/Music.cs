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

        [DllImport("winmm.dll", SetLastError = true)]
        public static extern bool mciGetErrorString([In] int error, [In, Out] char[] buffer, [In] int bufferCount);

        [DllImport("winmm.dll", SetLastError = true)]
        public static extern int mciSendString([In] string command, [Optional, In, Out] char[] returnBuffer, [Optional, In] int returnBufferCount, [Optional, In] IntPtr hNotifyWindow);


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


        public void Play(string fileName)
        {
            //Close();

            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                int error = mciSendString($"open \"{fileName}\" type mpegvideo alias RandomAudio", null, 0, IntPtr.Zero);

                if (error != 0)
                {
                    error = mciSendString($"open \"{fileName}\" alias RandomAudio", null, 0, IntPtr.Zero);

                    if (error != 0)
                    {
                        throw new MciException(error);
                    }
                }
                error = mciSendString($"play RandomAudio", null, 0, IntPtr.Zero);

                if (error != 0)
                {
                    Close();

                    throw new MciException(error);
                }
            }
        }

        static void Close()
        {
            var error = mciSendString($"close RandomAudio", null, 0, IntPtr.Zero);

            if (error != 0)
            {
                throw new MciException(error);
            }
        }

        class MciException : SystemException
        {
            public MciException(int error)
            {
                var buffer = new char[128];

                if (mciGetErrorString(error, buffer, 128))
                {
                    _message = new string(buffer);

                    return;
                }
                _message = "An unknown error has occured.";
            }

            public override string Message
            {
                get
                {
                    return _message;
                }
            }

            private string _message;
        }
    }
}
