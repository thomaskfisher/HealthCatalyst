using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthCatalyst.Models
{
    [Table("People")]
    public class People
    {
        [Key]
        public int personID { get; set; }

        [Required(ErrorMessage = "Your first name is required")]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "The maximum length is 50")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Your last name is required")]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "The maximum length is 50")]
        public string lastName { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Your street address is required")]
        [DisplayName("Street Address")]
        [StringLength(50, ErrorMessage = "The maximum length is 50")]
        public string address { get; set; }

        [Required(ErrorMessage = "Your city is required")]
        [DisplayName("City")]
        [StringLength(50, ErrorMessage = "The maximum length is 50")]
        public string city { get; set; }

        [Required(ErrorMessage = "Your state is required")]
        [DisplayName("State")]
        [StringLength(50, ErrorMessage = "The maximum length is 50")]
        public string state { get; set; }

        [Required(ErrorMessage = "Your zipcode is required")]
        [DisplayName("Zipcode")]
        public int zipcode { get; set; }

        [Required(ErrorMessage = "Your age is required")]
        [DisplayName("Age")]
        public int age { get; set; }

        [Required(ErrorMessage = "A description of your interests is required")]
        [DisplayName("Interests")]
        public string interests { get; set; }

        //[Required(ErrorMessage = "A picture is required")]
        [DisplayName("Picture")]
        public byte[] picture { get; set; }
    }
}