using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MemberDTO
    {
        public string Label { get; set; } = "M25";
        public string INode { get; set; } = "INode";
        public string JNode { get; set; } = "JNode";

        public string IRealese{ get; set; } = "IRealese";
        public string JRealese { get; set; } = "JRealese";

        public double IOffset { get; set; } = 100;
        public double JOffset { get; set; } = 100;

        public double length { get; set; } = 700;


        public double lengthloffset { get => (IOffset / length) * 100; }
        
        public double lengthroffset { get => 100 - (JOffset / length) * 100; }
        public bool IsINode { get => IRealese != null ? true : false; }
        public bool IsJNode { get => JRealese != null ? true : false; }

    }
}
