﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class LoginViewModel
    {
        [DefaultValueAttribute("matheus@gmail.com")]
        public string Email { get; set; }
        [DefaultValueAttribute("senhamatheus")]
        public string Senha { get; set; }
    }
}
