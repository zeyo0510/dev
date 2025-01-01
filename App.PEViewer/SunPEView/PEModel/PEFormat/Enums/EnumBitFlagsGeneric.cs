using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    public abstract class EnumBitFlagsGeneric
    {
        public long Value { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public EnumBitFlagsGeneric(long value, string name, string description)
        {
            this.Value = value;
            this.Description = description;
            this.Name = name;
        }

        public abstract IEnumerable<EnumBitFlagsGeneric> GetValues();
    }
}
