﻿using System;
using System.Collections.Generic;

namespace PollAndNomination.DataModel.Model
{
    public class User
    {
        public User()
        {
            Nominations = new List<Nomination>();
        }

        public Guid ID { get; set; }

        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public List<Nomination> Nominations { get; private set; }
    }
}