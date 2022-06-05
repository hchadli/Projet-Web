﻿using System.ComponentModel;
using Newtonsoft.Json;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Booking
{
    public int IdBooking { get; set; }
    public int IdFlight { get; set; }
    public int IdClient { get; set; }
    public int NbSeats { get; set; }

}

[Serializable]
public class BookingVm
{
    [JsonIgnore]
    public string Lastname { get; set; }
    [JsonIgnore]
    public string FirstName { get; set; }
    [JsonIgnore]
    public string Email { get; set; }

    [DisplayName("Itinerary")]
    public int IdItinerary { get; set; }

    [DisplayName("Available flights")]
    public int IdFlight { get; set; }

    [DisplayName("How many seats do you want to book ? ")]
    public int NbSeats { get; set; }

}

[Serializable]
public class Bookings
{
    public List<Booking> BookingsList { get; set; }
}