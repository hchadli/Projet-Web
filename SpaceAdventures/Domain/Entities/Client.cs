﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Client
    {
        public Client()
        {
            Bookings = new HashSet<Booking>();
        }

        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdMemberShipType { get; set; }

        public virtual MembershipType IdMemberShipTypeNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}