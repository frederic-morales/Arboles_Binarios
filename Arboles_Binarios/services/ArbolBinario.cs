using Arboles_Binarios.models;

namespace Arboles_Binarios.services
{
    public class ArbolBinario
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public NodoArbol Raiz { get; private set; }

        public void Insertar(InformacionNodo informacion)
        {
            Raiz = InsertarRecursivo(Raiz, informacion);
        }
        private NodoArbol InsertarRecursivo(NodoArbol nodo, InformacionNodo informacion)
        {
            //Si llegamos a un nodo null, significa que encontramos un lugar vacío, y allí insertamos el nuevo nodo.
            if (nodo == null)
                return new NodoArbol(informacion);

            if (informacion.Edad < nodo.Informacion.Edad)
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, informacion);
            else if (informacion.Edad > nodo.Informacion.Edad)
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, informacion);

            return nodo;
        }

    }
}
