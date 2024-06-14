using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInteresseService
    {
        void Toggle(InteresseViewModel model, int usuarioLogadoId);
    }
}
