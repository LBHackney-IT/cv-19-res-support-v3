using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("help_request_calls")]
    public class HelpRequestCallEntity
    {
        [Column("id")] [Key] public int Id { get; set; }

        [Column("help_request_id")] public bool? helpRequestId { get; set; }

        [Column("call_type")] public bool? callType { get; set; }

        [Column("call_outcome")] public string callOutcome { get; set; }

        [Column("call_date_time")] public string callDateTime { get; set; }
    }
}
