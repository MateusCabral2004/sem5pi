
using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.ConfirmationToken
{
    public class ConfirmationTokenId : EntityId
    {
        
        [JsonConstructor]
        public ConfirmationTokenId(Guid value) : base(value)
        {
        }

        override
            protected  Object createFromString(String text){
            return new Guid(text);
        }
        
        override
            public String AsString(){
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }
        public Guid AsGuid(){
            return (Guid) base.ObjValue;
        }
    }
}