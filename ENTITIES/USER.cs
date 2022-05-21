using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class USER : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentificationNo { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string MailAddress { get; set; }

    }
}
