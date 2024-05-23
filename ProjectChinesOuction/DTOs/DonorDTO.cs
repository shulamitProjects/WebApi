﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models.DTOs
{
    public class DonorDTO
    {
        [Key]
        [NotNull]
        public int DonorId { get; set; }

        [NotNull]
        public string FirstName { get; set; }
        [NotNull]

        public string LastName { get; set; }

        [MaxLength(15)]
        public string PhonNumber { get; set; }

        public string Email { get; set; }

        //public ICollection<PresentDTO> Gifts { get; set; }

    }
}