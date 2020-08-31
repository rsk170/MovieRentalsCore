using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VidlyCore.Entities.Models;
using VidlyCore.Validators;

namespace VidlyCore.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        [Display(Name = "Date of Birth")]
        [MIn18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

    }
}
