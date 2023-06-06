using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laokontroll.Models
{
    [Table("Laos")]
    public class Laos
    {
        [PrimaryKey, AutoIncrement , Column("LaosId")]
        public int LaosId { get; set; }
        [ForeignKey(typeof(Object))]
        public int ObjektId { get; set; }
        public string Nimetus { get; set; }
    }
}
