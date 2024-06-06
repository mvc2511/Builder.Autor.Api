using Api.Microservice.Autor.Persistencia;
using Builder.Autor.Api.Aplicacion;
using FluentValidation;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Utiliza el Builder para crear una instancia de AutorLibro
                //permite configurar diferentes propiedades de manera individual antes de construir el objeto final
                //Para comenzar a construir un nuevo AutorLibro, se crea una instancia de AutorLibroBuilder.
                var autorLibro = new AutorLibroBuilder()
                    .ConNombre(request.Nombre)
                    .ConApellido(request.Apellido)
                    .ConFechaNacimiento(request.FechaNacimiento)
                    .ConGuid(Convert.ToString(Guid.NewGuid()))
                    .Build();

                _context.AutorLibros.Add(autorLibro);

                var respuesta = await _context.SaveChangesAsync(cancellationToken);
                if (respuesta > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
