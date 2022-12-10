using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CathodeRayTube
{
    public class ComsDevice
    {
        private int _ClockCycle;
        public int ClockCycle { get { return _ClockCycle; } }

        private int XRegister;
        private List<int> SignalStrengths;

        private const int FirstSignalCycle = 20; // send the first signal at 20 cycles
        private const int SignalPeriod = 40;     // poll every 40 cycles after the first signal
        private const int NoopCycles = 1;
        private const int AddxCycles = 2;

        private char[] Display;

        public ComsDevice()
        {
            _ClockCycle = 0;
            XRegister = 1;
            SignalStrengths = new List<int>();
            Display = new char[240];
        }

        public void AddX(int number)
        {
            for(int i = 0; i < AddxCycles; i++)
            {
                TickCycle();
            }

            // complese the add X instruction
            XRegister += number;
        }

        public void Noop()
        {
            TickCycle();
        }

        public int SignalStrengthSum()
        {
            return SignalStrengths.Sum(x => x);
        }

        public void DrawDisplay()
        {
            const int pixelsPerLine = 40;
            for(var i = 0; i < Display.Length; i++)
            {
                if (i % pixelsPerLine == 0)
                    Console.Write("\n");

                Console.Write(Display[i]);
            }
        }

        private void TickCycle()
        {
            var lineWidth = 40;
            if (XRegister == ClockCycle % lineWidth || XRegister == (ClockCycle - 1) % lineWidth || XRegister == (ClockCycle + 1) % lineWidth)
                Display[ClockCycle] = '#';
            else
                Display[ClockCycle] = '.';

            _ClockCycle++;
            SendSignalIfNeeded();
        }

        private void SendSignalIfNeeded()
        {
            if(ClockCycle == (FirstSignalCycle + (SignalPeriod * SignalStrengths.Count)))
            {
                SignalStrengths.Add(XRegister * ClockCycle);
            }
        }
    }
}
