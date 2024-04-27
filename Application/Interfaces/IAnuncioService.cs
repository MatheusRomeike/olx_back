﻿using Application.ViewModels;
using Domain.Anuncio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAnuncioService
    {
        void Add(AnuncioViewModel anuncioViewModel);
        Anuncio LoadById(int anuncioId);

        void Update(AnuncioViewModel anuncioViewModel);

        void Delete(int anuncioId);
    }
}
