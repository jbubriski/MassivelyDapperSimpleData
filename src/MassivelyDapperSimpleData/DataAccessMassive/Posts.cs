using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MassivelyDapperSimpleData.DataAccessMassive;

namespace MassivelyDapperSimpleData.DataAccessMassive
{
    public class Posts : DynamicModel
    {
        public Posts()
            : base("DefaultConnection")
        {
            this.PrimaryKeyField = "Id";
        }
    }
}