using System;
using Newtonsoft.Json;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.TodoItem
{
    public class TodoItemId : EntityId
    {
        [JsonConstructor]
        public TodoItemId(Guid value) : base(value)
        {
        }

        public TodoItemId(String value) : base(value)
        {
        }

        override
        protected  Object createFromString(String text){
            return text;
        }

        override
        public String AsString()
        {
            return ObjValue.ToString();
        }
    }
}