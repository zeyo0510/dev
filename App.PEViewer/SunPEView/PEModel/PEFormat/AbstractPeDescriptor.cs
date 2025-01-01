using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    abstract class AbstractPeDescriptor
    {
        protected static Dictionary<string, string> descriptions;

        public string GetDescription(string elemName)
        {
            if (descriptions == null)
            {
                descriptions = new Dictionary<string, string>();
            }

            AddDescriptions();

            if (descriptions.ContainsKey(elemName))
            {
                return descriptions[elemName];
            }
            else
            {
                return "<No description available>";
                //throw new ArgumentException("Key " + elemName + " does not exist");
            }
        }

        protected void AddDescriptionEntry(string elemName, string desc)
        {
            if (!descriptions.ContainsKey(elemName))
            {
                descriptions.Add(elemName, desc);
            }

        }

        protected abstract void AddDescriptions();
    }
}
