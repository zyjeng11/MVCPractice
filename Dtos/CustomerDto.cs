﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Min18Member]
        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MemberShipTypeId { get; set; }
    }
}