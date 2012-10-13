using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massive;

namespace StackUnderToe.Models
{
    public class BadgesTable : DynamicModel
    {
        public BadgesTable(): base("Default", "Badges", "Id")
        {

        }
    }
}