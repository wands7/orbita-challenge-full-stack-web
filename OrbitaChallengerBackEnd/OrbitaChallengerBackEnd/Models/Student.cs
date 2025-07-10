﻿using System.ComponentModel.DataAnnotations;

namespace OrbitaChallengerBackEnd.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [Key]
        public string RA { get; set; }

        public string CPF { get; set; }
    }
}
