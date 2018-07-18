using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MemberDTO
    {
        public string MemberLabel { get; set; } = "M25";
        public string INode { get; set; } = "INode";
        public string JNode { get; set; } = "JNode";

        public string IRealese{ get; set; } = "IRealese";
        public string JRealese { get; set; } = "JRealese";

        public double IOffset { get; set; } = 100;
        public double JOffset { get; set; } = 100;

        public double length { get; set; } = 1000;


        public double lengthloffset { get => (IOffset / length) * 100; }
        
        public double lengthroffset { get => 100 - (JOffset / length) * 100; }

        public double IOffsetRelative { get; set; } = 350;
        public double JOffsetRelative { get; set; } = 750;
        public bool IsICircleVisible { get; set; }
        public bool IsJCircleVisible { get; set; }
        public bool IsIEllipseVisible { get; set; }
        public bool IsJEllipseVisible { get; set; }
        public bool IsINode { get => IRealese != null ? true : false; }
        public bool IsJNode { get => JRealese != null ? true : false; }

    }
}
