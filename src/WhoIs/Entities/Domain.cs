using System;

namespace WhoIs.Entities
{
    public class Domain
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string NameServer { get; set; }
        public string DnsSec { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Contact Registrant { get; set; }
        public Contact Admin { get; set; }
        public Contact Tech { get; set; }
    }
}
