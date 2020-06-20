using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedtoNewsLetter { get; set; }
        public MemberShipType MemberShipType { get; set; }
        public byte MemberShipTypeId { get; set; }
    }
}