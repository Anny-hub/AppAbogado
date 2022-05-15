﻿using Abogado.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abogado.Application.UsersServices.GetAllUsersByName
{
    public class GetAllUsersByNameDTO 
    {
        public Role Role { get; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }
    }
}