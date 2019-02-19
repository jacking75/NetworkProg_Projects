using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCommunicate
{
    struct DefineData
    {
        public static int PacketHeaderIDSize = sizeof(short);
        public static int PacketHeaderBodySize = sizeof(short);
        public static int PacketHeaderSize = PacketHeaderIDSize + PacketHeaderBodySize;

        public static int BodySizePosAtHeader = sizeof(short);
    }

    //class PacketData
    //{
    //    public short PacketID;
    //    public short BodySize;
    //    public string JsonString;
    //}
}
