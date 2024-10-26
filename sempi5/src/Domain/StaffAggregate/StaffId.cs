using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.StaffAggregate
{
    public class StaffId : EntityId
    {

        public StaffId(string value) : base(value)
        {
        }
        
        override
        public string AsString()
        {
            return (string)ObjValue;
        }

        protected override object createFromString(string text)
        {
            return text;
        }
        
        public bool Equals(StaffId other)
        {
            
            if (other == null)
            {
                return false;
            }
            
            return ObjValue.Equals(other.ObjValue);
        }

        public string ToString()
        {
            return (string)ObjValue;
        }
    }
}
