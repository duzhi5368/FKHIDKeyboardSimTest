using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMultiDllWrapper
{
    public struct JoystickButtonState
    {
        public bool A;
        public bool B;
        public bool X;
        public bool Y;
        public bool L;
        public bool R;
        public bool L2;
        public bool R2;
        public bool Start;
        public bool Select;

        public bool Extra1;
        public bool Extra2;
        public bool Extra3;
        public bool Extra4;
        public bool Extra5;
        public bool Extra6;

        public bool Left;
        public bool Right;
        public bool Up;
        public bool Down;
    }

    public class JoystickReport
    {
        private JoystickButtonState buttonState;

        private double joystickX;
        private double joystickY;

        public JoystickReport(JoystickButtonState buttonState, double joystickX, double joystickY)
        {
            this.buttonState = buttonState;
            if (joystickX > 1)
            {
                joystickX = 1.0;
            }
            else if (joystickX < -1)
            {
                joystickX = -1.0;
            }
            if (joystickY > 1)
            {
                joystickY = 1.0;
            }
            else if (joystickY < -1)
            {
                joystickY = -1.0;
            }
            this.joystickX = joystickX;
            this.joystickY = joystickY;
        }

        public ushort getButtonsRaw()
        {
            ushort result = 0;
            if (buttonState.A)
            {
                result |= 1 << 1;
            }
            if (buttonState.B)
            {
                result |= 1 << 2;
            }
            if (buttonState.X)
            {
                result |= 1 << 3;
            }
            if (buttonState.Y)
            {
                result |= 1 << 4;
            }
            if (buttonState.L)
            {
                result |= 1 << 5;
            }
            if (buttonState.R)
            {
                result |= 1 << 6;
            }

            return result;
        }

        public byte getPOVRaw()
        {
            byte result = 0;

            /*
            if (buttonState.Left)
            {
                result = 129;
            }*/

            return result;
        }

        public byte getJoystickXRaw()
        {
            return (byte)(joystickX * 127);
        }

        public byte getJoystickYRaw()
        {
            return (byte)(joystickY * 127);
        }
    }
}
