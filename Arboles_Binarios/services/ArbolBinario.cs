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


        //---------------------
        // METODOS DE SALIDA
        //---------------------


        //Obtener los nodos hijos de un nodo x por su edad
        public List<NodoArbol> ObtenerHijos(int edad)
        {
            var nodo = Buscar(edad);
            var hijos = new List<NodoArbol>();
            if (nodo?.Izquierda != null) hijos.Add(nodo.Izquierda);
            if (nodo?.Derecha != null) hijos.Add(nodo.Derecha);
            return hijos;
        }


        //Obtener el antecesor de un nodo y
        public NodoArbol ObtenerPadre(int edad)
        {
            return BuscarPadreRecursivo(Raiz, null, edad);
        }
        private NodoArbol BuscarPadreRecursivo(NodoArbol actual, NodoArbol padre, int edad)
        {
            if (actual == null) return null;
            if (actual.Informacion.Edad == edad) return padre;

            if (edad < actual.Informacion.Edad)
                return BuscarPadreRecursivo(actual.Izquierda, actual, edad);
            else
                return BuscarPadreRecursivo(actual.Derecha, actual, edad);
        }


        //Obtener nodos interiores (con al menos un hijo)
        public List<NodoArbol> ObtenerNodosInteriores()
        {
            var interiores = new List<NodoArbol>();
            RecolectarInteriores(Raiz, interiores);
            return interiores;
        }
        private void RecolectarInteriores(NodoArbol nodo, List<NodoArbol> lista)
        {
            if (nodo == null) return;

            if (nodo.Izquierda != null || nodo.Derecha != null)
                lista.Add(nodo);

            RecolectarInteriores(nodo.Izquierda, lista);
            RecolectarInteriores(nodo.Derecha, lista);
        }


        //Obtener nodos hoja
        public List<NodoArbol> ObtenerNodosHoja()
        {
            var hojas = new List<NodoArbol>();
            RecolectarHojas(Raiz, hojas);
            return hojas;
        }
        private void RecolectarHojas(NodoArbol nodo, List<NodoArbol> lista)
        {
            if (nodo == null) return;

            if (nodo.Izquierda == null && nodo.Derecha == null)
                lista.Add(nodo);

            RecolectarHojas(nodo.Izquierda, lista);
            RecolectarHojas(nodo.Derecha, lista);
        }


        //Obtener el grado de un nodo
        public int GradoNodo(int edad)
        {
            var nodo = Buscar(edad);
            if (nodo == null) return -1;

            int grado = 0;
            if (nodo.Izquierda != null) grado++;
            if (nodo.Derecha != null) grado++;
            return grado;
        }


        //Obtener nivel de un nodo
        public int NivelNodo(int edad)
        {
            return ObtenerNivel(Raiz, edad, 0);
        }
        private int ObtenerNivel(NodoArbol nodo, int edad, int nivel)
        {
            if (nodo == null) return -1;
            if (nodo.Informacion.Edad == edad) return nivel;

            if (edad < nodo.Informacion.Edad)
                return ObtenerNivel(nodo.Izquierda, edad, nivel + 1);
            else
                return ObtenerNivel(nodo.Derecha, edad, nivel + 1);
        }


        //Longitud de camino interno
        public int LongitudCaminoInterno()
        {
            return SumarCaminoInterno(Raiz, 0);
        }
        private int SumarCaminoInterno(NodoArbol nodo, int nivel)
        {
            if (nodo == null) return 0;

            // Es interior si tiene al menos un hijo
            int suma = (nodo.Izquierda != null || nodo.Derecha != null) ? nivel : 0;

            suma += SumarCaminoInterno(nodo.Izquierda, nivel + 1);
            suma += SumarCaminoInterno(nodo.Derecha, nivel + 1);

            return suma;
        }


        //Longitud de camino externo
        public int LongitudCaminoExterno()
        {
            return SumarCaminoExterno(Raiz, 0);
        }
        private int SumarCaminoExterno(NodoArbol nodo, int nivel)
        {
            if (nodo == null) return 0;

            if (nodo.Izquierda == null && nodo.Derecha == null)
                return nivel;

            return SumarCaminoExterno(nodo.Izquierda, nivel + 1) +
                   SumarCaminoExterno(nodo.Derecha, nivel + 1);
        }


        //Eliminar todas las hojas del arbol
        public void EliminarHojas()
        {
            Raiz = EliminarHojasRecursivo(Raiz);
        }

        private NodoArbol EliminarHojasRecursivo(NodoArbol nodo)
        {
            if (nodo == null) return null;

            if (nodo.Izquierda == null && nodo.Derecha == null)
                return null;

            nodo.Izquierda = EliminarHojasRecursivo(nodo.Izquierda);
            nodo.Derecha = EliminarHojasRecursivo(nodo.Derecha);

            return nodo;
        }



        //Intercarbio de los valores de un arbol izquierdo y derecho
        public void IntercambiarSubarboles()
        {
            IntercambiarRecursivo(Raiz);
        }

        private void IntercambiarRecursivo(NodoArbol nodo)
        {
            if (nodo == null) return;

            // Intercambia los hijos
            var temp = nodo.Izquierda;
            nodo.Izquierda = nodo.Derecha;
            nodo.Derecha = temp;

            // Llama recursivamente en ambos subárboles
            IntercambiarRecursivo(nodo.Izquierda);
            IntercambiarRecursivo(nodo.Derecha);
        }


        //---------------------
        // METODOS DE SALIDAñ
        //---------------------


        //Comparar si los arboles son similares
        public static bool SonSimilares(NodoArbol nodo1, NodoArbol nodo2)
        {
            if (nodo1 == null && nodo2 == null) return true;
            if (nodo1 == null || nodo2 == null) return false;

            return SonSimilares(nodo1.Izquierda, nodo2.Izquierda) &&
                   SonSimilares(nodo1.Derecha, nodo2.Derecha);
        }


        //Comparar si los arboles son equivalentes
        public static bool SonEquivalentes(NodoArbol nodo1, NodoArbol nodo2)
        {
            if (nodo1 == null && nodo2 == null) return true;
            if (nodo1 == null || nodo2 == null) return false;

            bool mismaInfo = nodo1.Informacion.Edad == nodo2.Informacion.Edad &&
                             nodo1.Informacion.Nombre == nodo2.Informacion.Nombre &&
                             nodo1.Informacion.Apellido == nodo2.Informacion.Apellido &&
                             nodo1.Informacion.Telefono == nodo2.Informacion.Telefono;

            return mismaInfo &&
                   SonEquivalentes(nodo1.Izquierda, nodo2.Izquierda) &&
                   SonEquivalentes(nodo1.Derecha, nodo2.Derecha);
        }


        //Comparar si son distintos
        public static bool SonDistintos(NodoArbol nodo1, NodoArbol nodo2)
        {
            return !SonSimilares(nodo1, nodo2) && !SonEquivalentes(nodo1, nodo2);
        }

    }
}
