﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransAction.Data.Models
{
    public class UserActivityCreateDto
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public DateTime ActivityTimestamp { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int EventId { get; set; }
        public int TeamId { get; set; }
        public long ConcurrencyControlNumber { get; set; }
    }
}
