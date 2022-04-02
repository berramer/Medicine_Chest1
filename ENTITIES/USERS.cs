using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class USERS : EntityBase
    {

        string Name { get; set; }
        string Surname { get; set; }
        string IdentificationNo { get; set; }

        string Username { get; set; }
        string Address { get; set; }
        string Password { get; set; }
        string Phonenumber { get; set; }
        string MailAddress { get; set; }

    }
}

