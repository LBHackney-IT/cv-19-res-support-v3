using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("help_request_calls")]
    public class HelpRequestCallEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("help_request_id")]
        [ForeignKey("HelpRequestEntity")]
        public int HelpRequestId { get; set; }

        [Column("call_type")]
        public string CallType { get; set; }

        [Column("call_direction")]
        public string CallDirection { get; set; }
        [Column("call_outcome")]
        public string CallOutcome { get; set; }

        [Column("call_date_time")]
        public DateTime CallDateTime { get; set; }

        [Column("caller")]
        public string Caller { get; set; }

        public HelpRequestEntity HelpRequestEntity { get; set; }

    }
}
