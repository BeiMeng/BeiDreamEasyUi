using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Common.PetaPoco
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {
            ForceToUtc = false;
        }

        public ColumnAttribute(string Name)
        {
            this.Name = Name;
            ForceToUtc = false;
        }

        public string Name
        {
            get;
            set;
        }

        public bool ForceToUtc
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ResultColumnAttribute : ColumnAttribute
    {
        public ResultColumnAttribute()
        {
        }

        public ResultColumnAttribute(string name)
            : base(name)
        {
        }
    }
}
