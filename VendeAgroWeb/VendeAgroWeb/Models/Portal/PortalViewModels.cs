﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace VendeAgroWeb.Models.Portal
{
    public class RegistroViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe de ser mínimo de 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmacion no son iguales.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [StringLength(14, ErrorMessage = "El celular no es válido.", MinimumLength = 10)]
        public string Celular { get; set; }
    }

    public class LoginViewModel {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe de ser mínimo de 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

    }

    public class OlvidasteContrasenaViewModel
    {
    }

    public class CrearAnuncioViewModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "La descripción no puede ser mayor de 250 caracteres.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Precio")]
        public double Precio { get; set; }
    }

    public class MisAnunciosViewModel
    {
        private ICollection<AnuncioViewModel> _anuncios;
        private ICollection<PaqueteViewModel> _paquetes;


        public ICollection<AnuncioViewModel> Anuncios
        {
            get
            {
                return _anuncios;
            }
        }

        public ICollection<PaqueteViewModel> Paquetes
        {
            get
            {
                return _paquetes;
            }
        }

        public MisAnunciosViewModel(ICollection<AnuncioViewModel> anuncios, ICollection<PaqueteViewModel> paquete) {
            _anuncios = anuncios;
            _paquetes = paquete;
        }
    }

    public class AnuncioViewModel
    {
        private int _id;
        private string _titulo;
        private string _usuario;
        private double _precio;
        private string _categoria;
        private string _subcategoria;
        private string _estado;
        private string _ciudad;
        private int? _clicks;
        private int _idPaquete;

        public string Titulo
        {
            get
            {
                return _titulo;
            }
        }

        public double Precio
        {
            get
            {
                return _precio;
            }
        }

        public string Categoria
        {
            get
            {
                return _categoria;
            }
        }

        public string Subcategoria
        {
            get
            {
                return _subcategoria;
            }
        }

        public string Estado
        {
            get
            {
                return _estado;
            }
        }

        public string Ciudad
        {
            get
            {
                return _ciudad;
            }
        }

        public int? Clicks
        {
            get
            {
                return _clicks;
            }
        }

        public string Usuario
        {
            get
            {
                return _usuario;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public int IdPaquete
        {
            get
            {
                return _idPaquete;
            }
        }

        public AnuncioViewModel(int id, string titulo, string usuario, double precio, string categoria, string subcategoria, string estado, string ciudad, int? clicks, int idPaquete)
        {
            _id = id;
            _titulo = titulo;
            _usuario = usuario;
            _precio = precio;
            _categoria = categoria;
            _subcategoria = subcategoria;
            _estado = estado;
            _ciudad = ciudad;
            _clicks = clicks;
            _idPaquete = idPaquete;
        }
    }

    public class PaqueteViewModel
    {
        private int _id;
        private string _nombre;
        private int _meses;

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }
        }

        public int Meses
        {
            get
            {
                return _meses;
            }
        }

        public PaqueteViewModel(int id, string nombre, int meses)
        {
            _id = id;
            _nombre = nombre;
            _meses = meses;
        }
    }

    public class PerfilViewModel
    {
        private int _id;
        private string _nombre;
        private string _apellidos;
        private string _telefono;
        private string _email;

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [StringLength(14, ErrorMessage = "El teléfono celular no es válido.", MinimumLength = 10)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe de ser mínimo de 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string PasswordNueva { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("PasswordNueva", ErrorMessage = "La contraseña y su confirmacion no son iguales.")]
        public string ConfirmPassword { get; set; }

        public PerfilViewModel() {
        }

    }


    //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class CustomPhoneNumberAttribute : ValidationAttribute
    //{

    //    public CustomPhoneNumberAttribute()
    //    {

    //    }

    //    public override bool IsValid(object value)
    //    {
    //        string strValue = value as string;
    //        Debug.WriteLine("String value:" + value);
    //        if (!string.IsNullOrEmpty(strValue))
    //        {
    //            int len = strValue.Length;
    //            return len == 10;
    //        }
    //        return true;
    //    }
    //}
}