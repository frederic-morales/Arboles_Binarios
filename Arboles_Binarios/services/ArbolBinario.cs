using Arboles_Binarios.models;

namespace Arboles_Binarios.services
{
    public class ArbolBinario
    {
        private static int contador = 1;
        public int Id { get; private set; }
        public NodoArbol Raiz { get; private set; }


        public ArbolBinario() // Constructor
        {
            Id = contador++;
        }


        //Insertar Nodo al Arbol
        public void Insertar(InformacionNodo informacion)
        {
            Raiz = InsertarRecursivo(Raiz, informacion);
        }
        private NodoArbol InsertarRecursivo(NodoArbol nodo, InformacionNodo informacion)
        {
            if (nodo == null)
                return new NodoArbol(informacion);

            if (informacion.Edad < nodo.Informacion.Edad)
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, informacion);
            else if (informacion.Edad > nodo.Informacion.Edad)
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, informacion);

            return nodo;
        }


        //Eliminar Nodo del Arbol
        public void Eliminar(int edad)
        {
            Raiz = EliminarRecursivo(Raiz, edad);
        }
        private NodoArbol EliminarRecursivo(NodoArbol nodo, int edad)
        {
            if (nodo == null) return null;
            
            if (edad < nodo.Informacion.Edad)
                nodo.Izquierda = EliminarRecursivo(nodo.Izquierda, edad);
            else if (edad > nodo.Informacion.Edad)
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, edad);

            else
            {
                 if (nodo.Izquierda == null) return nodo.Derecha;
                 if (nodo.Derecha == null) return nodo.Izquierda;

                var sucesor = EncontrarMinimo(nodo.Derecha);
                nodo.Informacion = sucesor.Informacion;
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, sucesor.Informacion.Edad);   
            }

            return nodo;
        }
        private NodoArbol EncontrarMinimo(NodoArbol nodo)
        {
            while (nodo.Izquierda != null)
                nodo = nodo.Izquierda;
            return nodo;
        }


        //Buscar Nodo en el Arbol
        public NodoArbol Buscar(int edad)
        {
            return BuscarRecursivo(Raiz, edad);
        }
        private NodoArbol BuscarRecursivo(NodoArbol nodo, int edad)
        {
            if (nodo == null || nodo.Informacion.Edad == edad)
                return nodo;
            if (edad < nodo.Informacion.Edad)
                return BuscarRecursivo(nodo.Izquierda, edad);
            else
                return BuscarRecursivo(nodo.Derecha, edad);
        }
    }
}
