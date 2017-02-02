using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using VMultiDllWrapper;

namespace VMultiTest
{
    class Program
    {
        static void Main(string[] args)
        {

            VMulti vmulti = new VMulti("D:\\Work\\CSharpWork\\VMultiDll-master\\VMultiDll-master\\Debug\\VMultiDll.dll");
            Console.WriteLine("Connect: " + vmulti.connect());

            System.Threading.Thread.Sleep(3000);

            keyboardTest(vmulti);

            vmulti.disconnect();

            Console.ReadKey();
        }

        [DllImport("USER32.dll")]
        static extern short GetKeyState(int nVirtKey);

        private static void keyboardTest(VMulti vmulti)
        {
            System.Diagnostics.Process.Start("notepad.exe");

            System.Threading.Thread.Sleep(3000);

            KeyboardReport report = new KeyboardReport();

            report.keyDown(KeyboardKey.H);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardKey.H);
            report.keyDown(KeyboardKey.E);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardKey.E);
            report.keyDown(KeyboardKey.L);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardKey.L);
            vmulti.updateKeyboard(report);
            report.keyDown(KeyboardKey.L);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardKey.L);
            report.keyDown(KeyboardKey.O);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardKey.O);
            report.keyDown(KeyboardModifier.LShift);
            report.keyDown(KeyboardKey.Number1);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardModifier.LShift);
            report.keyUp(KeyboardKey.Number1);
            vmulti.updateKeyboard(report);

            System.Threading.Thread.Sleep(4000);
            report.keyDown(KeyboardModifier.LWin);
            report.keyDown(KeyboardKey.D);
            vmulti.updateKeyboard(report);
            report.keyUp(KeyboardModifier.LWin);
            report.keyUp(KeyboardKey.D);
            vmulti.updateKeyboard(report);
        }

        private void joystickTest(VMulti vmulti)
        {
            double i = 0;
            bool running = true;
            while (running)
            {
                JoystickButtonState joyButtonState = new JoystickButtonState();
                joyButtonState.A = false;
                joyButtonState.X = false;
                joyButtonState.Left = false;

                double x = Math.Sin(i);
                double y = Math.Cos(i);

                Console.WriteLine("x: " + x + " y: " + y);

                JoystickReport joystickReport = new JoystickReport(joyButtonState, x, y);

                Console.WriteLine("Update Joystick: " + vmulti.updateJoystick(joystickReport));

                i += 0.1;

                System.Threading.Thread.Sleep(100);
            }
        }

        private void multitouchTest(VMulti vmulti)
        {
            double x = 500;
            double y = 500;

            while (true)
            {
                List<MultitouchPointerInfo> touches = new List<MultitouchPointerInfo>();
                bool spacePressed = Convert.ToBoolean(GetKeyState(0x20) & 0x8000);
                MultitouchPointerInfo pointer = new MultitouchPointerInfo();

                bool rightPressed = Convert.ToBoolean(GetKeyState(0x27) & 0x8000);
                if (rightPressed)
                    x += 10;
                bool downPressed = Convert.ToBoolean(GetKeyState(0x28) & 0x8000);
                if (downPressed)
                    y += 10;
                bool leftPressed = Convert.ToBoolean(GetKeyState(0x25) & 0x8000);
                if (leftPressed)
                    x -= 10;
                bool upPressed = Convert.ToBoolean(GetKeyState(0x26) & 0x8000);
                if (upPressed)
                    y -= 10;


                if (spacePressed)
                {
                    Console.WriteLine("pressed");
                    pointer.Down = true;
                }
                else
                {
                    pointer.Down = false;
                }

                Point mousePos = Control.MousePosition;
                Console.WriteLine(mousePos);
                pointer.X = x / Screen.PrimaryScreen.Bounds.Width;
                pointer.Y = y / Screen.PrimaryScreen.Bounds.Height;

                Console.WriteLine("X: " + pointer.X);
                Console.WriteLine("Y: " + pointer.Y);

                touches.Add(pointer);

                MultitouchReport report = new MultitouchReport(touches);

                if (!vmulti.updateMultitouch(report))
                {
                    Console.WriteLine("fail");
                }

                System.Threading.Thread.Sleep(10);
            }
        }



    }
}
