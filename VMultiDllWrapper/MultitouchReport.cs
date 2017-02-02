using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMultiDllWrapper
{
    
    public class MultitouchPointerInfo
    {
        private const byte MULTI_TIPSWITCH_BIT = 1;
        private const byte MULTI_IN_RANGE_BIT = 2;
        private const byte MULTI_CONFIDENCE_BIT = 4;

        private const uint MULTI_MIN_COORDINATE = 0x0000;
        private const uint MULTI_MAX_COORDINATE = 0x7FFF;

        private const uint MULTI_MAX_COUNT = 20;

        public bool Down = false;
        public bool Hover = false;
        public byte ID = 0;
        public double X = 0;
        public double Y = 0;

        public MultitouchPointerInfoRaw getPointerInfoRaw()
        {
            MultitouchPointerInfoRaw info = new MultitouchPointerInfoRaw();
            if (Down)
            {
                info.Status = MULTI_TIPSWITCH_BIT | MULTI_IN_RANGE_BIT | MULTI_CONFIDENCE_BIT;
            }
            else if(Hover)
            {
                info.Status = MULTI_IN_RANGE_BIT | MULTI_CONFIDENCE_BIT;
            }
            else
            {
                info.Status = 0;
            }

            info.ContactID = this.ID;
            info.XValue = (ushort)(X * (double)MULTI_MAX_COORDINATE);
            info.YValue = (ushort)(Y * (double)MULTI_MAX_COORDINATE);
            info.Width = 20;
            info.Height = 30;

            return info;
        }
    }

    public struct MultitouchPointerInfoRaw
    {
        public byte Status;
        public byte ContactID;
        public ushort XValue;
        public ushort YValue;
        public ushort Width;
        public ushort Height;
    }


    public class MultitouchReport
    {
        private List<MultitouchPointerInfo> touches;

        public MultitouchReport(List<MultitouchPointerInfo> touches)
        {
            this.touches = touches;
        }

        public MultitouchPointerInfoRaw[] getTouchesRaw()
        {
            MultitouchPointerInfoRaw[] allTouches = new MultitouchPointerInfoRaw[touches.Count];
            for (int i = 0; i < touches.Count; i++ )
            {
                allTouches[i] = touches[i].getPointerInfoRaw();
            }
            return allTouches;
        }

        public byte getTouchesCountRaw()
        {
            return (byte)touches.Count;
        }

        public byte getRequestType()
        {
            return 0x01; //REPORTID_MTOUCH
        }

        public byte getReportControlId()
        {
            return 0x40; //REPORTID_CONTROL
        }
    }
}
