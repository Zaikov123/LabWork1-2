using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKozl
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firm { get; set; }
        public string Brand { get; set; }
        public string BodyType { get; set; }
        public string LicensePlates { get; set; }
        public string Owner { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Driver
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public uint Exp { get; set; }
        public string Certificate { get; set; }
    }
    public class DTP
    {
        public DateTime DateDTP { get; set; }
        public int Id { get; set; }
        public string Plase { get; set; }
        public uint СasualtyRate { get; set; }
        public ulong ActNumber { get; set; }
        public string DTPCause { get; set; }
    }
    public class Member
    {
        public DTP DTPId { get; set; }
        public Driver DriverId { get; set; }
        public Car CarId { get; set; }
    }
}
