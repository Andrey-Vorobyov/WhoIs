using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhoIs.Entities
{
    [Table("contacts")]
    public class Contact
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("organization")]
        public string Organization { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("postalcode")]
        public string PostalCode { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("phoneext")]
        public string PhoneExt { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("faxext")]
        public string FaxExt { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}
