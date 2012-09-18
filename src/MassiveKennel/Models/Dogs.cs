using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MassiveKennel.Models
{
    public class Dogs : DynamicModel
    {
        public Dogs() : base("kennel_db")
        {
            PrimaryKeyField = "id";
        }
    }
}