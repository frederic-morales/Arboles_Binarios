using Arboles_Binarios.models;
namespace Arboles_Binarios.models
{
    public class NodoArbol
    {
        public InformacionNodo Informacion { get; set; }
        public NodoArbol Izquierda { get; set; }
        public NodoArbol Derecha { get; set; }

        public NodoArbol(InformacionNodo informacion)
        {
            Informacion = informacion;
        }
    }
}
