using System;

namespace DemoConsole.MyGroup
{
    public class AnalysisSource
    {
        public long Id { get; set; }
        public int No { get; set; }

        public long DelayMs { get; set; }

        public long RecTimestamp { get; set; }

        public DateTime CreateTime { get; set; }

        public string MessageFile { get; set; }
    }
}
