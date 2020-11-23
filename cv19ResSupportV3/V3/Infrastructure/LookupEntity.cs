using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("i_need_help_resident_support_v3")]
    public class LookupEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("lookup_group")]
        public string LookupGroup { get; set; }

        [Column("lookup")]
        public string Lookup { get; set; }
    }
}
