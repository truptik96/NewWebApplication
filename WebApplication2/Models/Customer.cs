using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Cusomer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        [Min18yearsIfAMember]
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedtoNewsLetter { get; set; }
        public MemberShipType MemberShipType { get; set; }
        public byte MemberShipTypeId { get; set; }
    }
}