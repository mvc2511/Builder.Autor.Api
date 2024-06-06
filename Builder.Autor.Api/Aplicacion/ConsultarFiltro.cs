﻿using Api.Microservice.Autor.Modelo;
using Api.Microservice.Autor.Persistencia;
using AutoMapper;
using Builder.Autor.Api.Aplicacion;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Microservice.Autor.Aplicacion
{
    public class ConsultarFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutoGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibros
                .Where(p => p.AutorLibroGuid == request.AutoGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontroal autor");

                }

                /////////////////////////////////////
                var builder = new AutorLibroBuilder()
                    .ConGuid(autor.AutorLibroGuid)
                    .ConNombre(autor.Nombre)
                    .ConApellido(autor.Apellido)
                    .ConFechaNacimiento(autor.FechaNacimiento)
                    .BuildFiltro();

                var autorDto = builder;

                return autorDto;
            }
        }


    }
}
