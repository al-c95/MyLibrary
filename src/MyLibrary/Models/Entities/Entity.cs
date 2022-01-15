using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    /// <summary>
    /// Base class representing a record that can be stored in the database.
    /// </summary>
    public abstract class Entity // abstract class to reduce code duplication of id property
    {
        public int Id { get; set; }
    }
}
