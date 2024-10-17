using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.User
{
    public class SystemUserId : EntityId
    {
        // Parameterless constructor for EF Core
        public SystemUserId() : base(null) // or you can choose to set it to null if appropriate
        {}
        
        public SystemUserId(long value) : base(value)
        {}
        
        public override string AsString()
        {
            return (string)base.ObjValue.ToString();
        }
        
        public long AsLong()
        {
            return (long)base.ObjValue;
        }

        protected override object createFromString(string text)
        {
            return long.Parse(text);
        }
    }
}
