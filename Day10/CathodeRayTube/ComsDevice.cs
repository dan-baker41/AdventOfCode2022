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

        public ComsDevice()
        {
            _ClockCycle = 0;
            XRegister = 1;
            SignalStrengths = new List<int>();
        }

        public void AddX(int number)
        {
            for(int i = 0; i < AddxCycles; i++)
            {
                // increment 1 clock cycle and send a signal if needed
                _ClockCycle++;
                SendSignalIfNeeded();
            }

            // complese the add X instruction
            XRegister += number;
        }

        public void Noop()
        {
            // increment 1 clock cycle and send a signal if needed
            _ClockCycle++;
            SendSignalIfNeeded();
        }

        public int SignalStrengthSum()
        {
            return SignalStrengths.Sum(x => x);
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
