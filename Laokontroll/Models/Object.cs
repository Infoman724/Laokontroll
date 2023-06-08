using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laokontroll.Models
{
    [Table("Objekt")]
    public class Object
    {
        [PrimaryKey, AutoIncrement, Column("ObjektId")]
        public int ObjektId { get; set; }
        public string Nimetus { get; set; }
        public string Asukoht { get; set; }
        public int IdNumber { get; set; }
        [ForeignKey(typeof(Laos))]
        public int LaosId{ get; set;}
    }
}
