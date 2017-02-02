using System;
using System.IO;
using System.Runtime.InteropServices;

namespace VMultiDllWrapper
{
    public class VMulti
    {
        
        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MyHelloWorld();

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MySum(int a, int b);
        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vmulti_alloc();
        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vmulti_free(IntPtr vmulti);

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vmulti_connect(IntPtr vmulti, int i);

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vmulti_disconnect(IntPtr vmulti);

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vmulti_update_joystick(IntPtr vmulti, ushort buttons, byte hat, byte x, byte y, byte z, byte rz, byte throttle);

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vmulti_update_multitouch(IntPtr vmulti, MultitouchPointerInfoRaw[] pTouch, byte actualCount, byte request_type, byte report_control_id);

        [DllImport("VMultiDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vmulti_update_keyboard(IntPtr vmulti, byte shiftKeyFlags, byte[] keyCodes);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int GetProcAddress(int handle, string funcName);
        [DllImport("Kernel32.dll", SetLastError = true, CharSet =CharSet.Unicode)]
        private static extern int LoadLibrary(string dllPath);
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int FreeLibrary(int handle);


        /*
        public delegate void HelloWorld();
        private HelloWorld Pro_HelloWorld { get; set; }

        public delegate IntPtr vmulti_alloc();
        private vmulti_alloc Pro_vmulti_alloc { get; set; }
        public delegate void vmulti_free(IntPtr vmulti);
        private vmulti_free Pro_vmulti_free { get; set; }
        public delegate bool vmulti_connect(IntPtr vmulti, int i);
        private vmulti_connect Pro_vmulti_connect { get; set; }
        public delegate void vmulti_disconnect(IntPtr vmulti);
        private vmulti_disconnect Pro_vmulti_disconnect { get; set; }
        public delegate bool vmulti_update_joystick(IntPtr vmulti, ushort buttons, byte hat, byte x, byte y, byte z, byte rz, byte throttle);
        private vmulti_update_joystick Pro_vmulti_update_joystick { get; set; }
        public delegate bool vmulti_update_multitouch(IntPtr vmulti, MultitouchPointerInfoRaw[] pTouch, byte actualCount, byte request_type, byte report_control_id);
        private vmulti_update_multitouch Pro_vmulti_update_multitouch { get; set; }
        public delegate bool vmulti_update_keyboard(IntPtr vmulti, byte shiftKeyFlags, byte[] keyCodes);
        private vmulti_update_keyboard Pro_vmulti_update_keyboard { get; set; }
        */
        private IntPtr vmulti;
        private bool connected;
        private int nDllHandle = 0;

        /*
        private static Delegate GetAddress(int dllMoule,string functionName, Type t)
        {
            int Addr = GetProcAddress(dllMoule, functionName);
            if (Addr == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(Addr), t);
        }
        private void MyFreeLibrary()
        {
            if (this.nDllHandle >= 32)
                FreeLibrary(this.nDllHandle);
        }
        private bool MyLoadLibaray(string dllPath)
        {
            this.nDllHandle = LoadLibrary(dllPath);
            if(this.nDllHandle >= 32)
            {
                this.Pro_HelloWorld = (HelloWorld)GetAddress(nDllHandle, "HelloWorld", typeof(HelloWorld));
                this.Pro_vmulti_alloc = (vmulti_alloc)GetAddress(nDllHandle, "vmulti_alloc", typeof(vmulti_alloc));
                this.Pro_vmulti_free = (vmulti_free)GetAddress(nDllHandle, "vmulti_free", typeof(vmulti_free));
                this.Pro_vmulti_connect = (vmulti_connect)GetAddress(nDllHandle, "vmulti_connect", typeof(vmulti_connect));
                this.Pro_vmulti_disconnect = (vmulti_disconnect)GetAddress(nDllHandle, "vmulti_disconnect", typeof(vmulti_disconnect));
                this.Pro_vmulti_update_joystick = (vmulti_update_joystick)GetAddress(nDllHandle, "vmulti_update_joystick", typeof(vmulti_update_joystick));
                this.Pro_vmulti_update_multitouch = (vmulti_update_multitouch)GetAddress(nDllHandle, "vmulti_update_multitouch", typeof(vmulti_update_multitouch));
                this.Pro_vmulti_update_keyboard = (vmulti_update_keyboard)GetAddress(nDllHandle, "vmulti_update_keyboard", typeof(vmulti_update_keyboard));
                return true;
            }
            else
            {
                string strError = Marshal.GetLastWin32Error().ToString();
                throw new Exception("LoadLibrary failed, error code = " + strError);
            }
        }
        */

        public VMulti(string dllPath)
        {
            /*
            int a = 1; int b = 2;
            int c = MySum(a, b);
            */
            MyHelloWorld();
            //MyLoadLibaray(dllPath);
            vmulti = vmulti_alloc();
        }

        ~VMulti()
        {
            //MyFreeLibrary();
        }

        public virtual bool isConnected()
        {
            return this.connected;
        }

        public virtual bool connect()
        {
            return this.connected = vmulti_connect(vmulti, 1);
        }

        public virtual void disconnect()
        {
            if (connected)
            {
                vmulti_disconnect(vmulti);
            }
        }

        public virtual bool updateJoystick(JoystickReport report)
        {
            if (connected)
            {
                return vmulti_update_joystick(vmulti, report.getButtonsRaw(),
                    report.getPOVRaw(), report.getJoystickXRaw(), report.getJoystickYRaw(), 0, 128, 0);
            }
            else
            {
                return false;
            }
        }

        public virtual bool updateMultitouch(MultitouchReport report)
        {
            if (connected)
            {
                MultitouchPointerInfoRaw[] touches = report.getTouchesRaw();
                return vmulti_update_multitouch(vmulti, touches, 
                    report.getTouchesCountRaw(), report.getRequestType(), report.getReportControlId());
            }
            else
            {
                return false;
            }
        }

        public virtual bool updateKeyboard(KeyboardReport report)
        {
            if (connected)
            {
                return vmulti_update_keyboard(vmulti, 
                    report.getRawShiftKeyFlags(), report.getRawKeyCodes());
            }
            else
            {
                return false;
            }
        }

    }
}
