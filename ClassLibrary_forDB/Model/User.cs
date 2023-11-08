using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary_forDB
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
