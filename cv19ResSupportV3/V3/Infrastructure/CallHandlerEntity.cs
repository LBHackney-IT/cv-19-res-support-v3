using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("call_handlers")]
    public class CallHandlerEntity
    {
        public CallHandlerEntity()
        {

        }

        [Column("id")]
        [Key] public int Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("email")] public string Email { get; set; }
    }
}
