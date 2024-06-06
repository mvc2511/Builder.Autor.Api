using Api.Microservice.Autor.Aplicacion;
using Api.Microservice.Autor.Modelo;

namespace Builder.Autor.Api.Aplicacion
{
    /*
    AutorLibroBuilder es una clase encargada de construir instancias de la clase AutorLibro 
    de manera flexible y paso a paso. Su función principal es encapsular la lógica de creación 
    de objetos AutorLibro, permitiendo configurar diferentes propiedades de manera individual 
    antes de construir el objeto final.
     */
    public class AutorLibroBuilder
    {
        private int _autorLibroId;
        private string _nombre;
        private string _apellido;
        private DateTime? _fechaNacimiento;
        private ICollection<GradoAcademico> _gradosAcademico = new List<GradoAcademico>();
        private string _autorLibroGuid;

        public AutorLibroBuilder ConId(int autorLibroId)
        {
            _autorLibroId = autorLibroId;
            return this;
        }

        public AutorLibroBuilder ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public AutorLibroBuilder ConApellido(string apellido)
        {
            _apellido = apellido;
            return this;
        }

        public AutorLibroBuilder ConFechaNacimiento(DateTime? fechaNacimiento)
        {
            _fechaNacimiento = fechaNacimiento;
            return this;
        }

        public AutorLibroBuilder ConGradosAcademico(ICollection<GradoAcademico> gradosAcademico)
        {
            _gradosAcademico = gradosAcademico;
            return this;
        }

        public AutorLibroBuilder ConGuid(string autorLibroGuid)
        {
            _autorLibroGuid = autorLibroGuid;
            return this;
        }

        public AutorLibro Build()
        {
            return new AutorLibro
            {
                AutorLibroId = _autorLibroId,
                Nombre = _nombre,
                Apellido = _apellido,
                FechaNacimiento = _fechaNacimiento,
                GradosAcademico = _gradosAcademico,
                AutorLibroGuid = _autorLibroGuid
            };
        }

        
        public AutorDto BuildFiltro()
        {
            return new AutorDto
            {
                AutorLibroId = _autorLibroId,
                Nombre = _nombre,
                Apellido = _apellido,
                FechaNacimiento = _fechaNacimiento,
                AutorLibroGuid = _autorLibroGuid
            };
        }

        public List<AutorDto> BuildList(List<AutorLibro> autores)
        {
            return autores.Select(autor => new AutorDto
            {
                AutorLibroId = autor.AutorLibroId,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido,
                FechaNacimiento = autor.FechaNacimiento,
                AutorLibroGuid = autor.AutorLibroGuid
            }).ToList();
        }
        
    }
}