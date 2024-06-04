using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Autenticacao
{
    public class TokenDto
    {
        [DefaultValueAttribute("cHXL7Uap2QH5woFCpi21pWkl49cTKgZ1wldOEoWzEpsXcG4V8fvsrXp1twxMgdgw")]
        public string AccessToken { get; set; }
        [DefaultValueAttribute("51200")]
        public long ExpiresIn { get; set; }
    }
}
