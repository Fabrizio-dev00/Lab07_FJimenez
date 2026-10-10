using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NPersona
    {
        private DPersona datos = new DPersona();

        public List<Persona> Listar()
        {
            return datos.Listar();
        }

        public List<Persona> Buscar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return datos.Listar();
            }

            return datos.Buscar(texto.Trim());
        }

        public void Insertar(Persona persona)
        {
            Validar(persona);
            datos.Insertar(persona);
        }

        public void Actualizar(Persona persona)
        {
            if (persona.IdPersona <= 0)
            {
                throw new Exception("Selecciona una persona de la lista para actualizar.");
            }

            Validar(persona);

            if (datos.Actualizar(persona) == 0)
            {
                throw new Exception("No se encontró la persona.");
            }
        }

        public void Eliminar(int idPersona)
        {
            if (idPersona <= 0)
            {
                throw new Exception("Selecciona una persona de la lista para eliminar.");
            }

            datos.Eliminar(idPersona);
        }

        private void Validar(Persona persona)
        {
            if (string.IsNullOrWhiteSpace(persona.Nombres))
            {
                throw new Exception("Los nombres son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(persona.Apellidos))
            {
                throw new Exception("Los apellidos son obligatorios.");
            }

            if (persona.Dni == null || persona.Dni.Length != 8)
            {
                throw new Exception("El DNI debe tener exactamente 8 dígitos.");
            }

            foreach (char c in persona.Dni)
            {
                if (!char.IsDigit(c))
                {
                    throw new Exception("El DNI solo debe contener números.");
                }
            }
        }
    }
}
