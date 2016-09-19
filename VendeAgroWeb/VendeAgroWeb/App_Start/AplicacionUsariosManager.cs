﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VendeAgroWeb.Models;

namespace VendeAgroWeb
{
    public class AplicacionUsuariosManager
    {
        private AdministradorUsuario _usuarioAdministradorActual;
        private PortalUsuario _usuarioPortalActual;

        public AdministradorUsuario UsuarioAdministradorActual
        {
            get
            {
                return _usuarioAdministradorActual;
            }
        }

        public PortalUsuario UsuarioPortalActual
        {
            get
            {
                return _usuarioPortalActual;
            }
        }

        public async Task<LoginStatus> LoginAdministradorAsync(string email, string password)
        {
            HttpResponse response = HttpContext.Current.Response;
            return await Task.Run(() =>
            {
                using (var _dbContext = new VendeAgroEntities())
                {
                    var usuario = _dbContext.Usuario_Administrador.Where(u => u.email == email).FirstOrDefault();
                    if (usuario == null)
                    {
                        return LoginStatus.Incorrecto;
                    }

                    if (!usuario.confirmaEmail)
                    {
                        //TO DO: Reenviar token
                        return LoginStatus.ConfirmacionMail;
                    }

                    if (!usuario.activo || usuario.password.CompareTo(password) != 0)
                    {
                        return LoginStatus.Incorrecto;
                    }

                    _usuarioAdministradorActual = new AdministradorUsuario(usuario.id, usuario.email, usuario.nombre);
                    usuario.tokenSesion = getToken();
                    setCookie("AdminVendeAgro", usuario.tokenSesion, response);
                    _dbContext.SaveChanges();
                    return LoginStatus.Exitoso;
                }

            });

        }

        public async Task<RegistroStatus> RegistroUsuarioAsync(Models.Portal.RegistroViewModel model)
        {
            HttpResponse response = HttpContext.Current.Response;
            return await Task.Run(() =>
            {
                using (var _dbContext = new VendeAgroEntities())
                {
                    var usuario = _dbContext.Usuarios.Where(u => u.email == model.Email).FirstOrDefault();
                    if (usuario != null)
                    {
                        return RegistroStatus.MailOcupado;
                    }

                    usuario = _dbContext.Usuarios.Where(u => u.telefono == model.Celular).FirstOrDefault();

                    if (usuario != null)
                    {
                        return RegistroStatus.TelefonoOcupado;
                    }

                    string tokenSesion = getToken();
                    string tokenEmail = getToken();
                    _dbContext.Usuarios.Add(new Usuario
                    {
                        nombre = model.Nombre,
                        apellidos = model.Apellidos,
                        telefono = model.Celular,
                        password = Hash(model.Password),
                        email = model.Email,
                        confirmaEmail = true,
                        tokenSesion = tokenSesion,
                        tokenEmail = tokenEmail,

                    });

                    _dbContext.SaveChanges();
                    setCookie("VendeAgroUser", tokenSesion, response);

                    var usuarioRegistrado = _dbContext.Usuarios.Where(u => u.email == model.Email).FirstOrDefault();
                    _usuarioPortalActual = new PortalUsuario(usuarioRegistrado.id, model.Email, model.Nombre, model.Apellidos, model.Celular.ToString());
                    string mailMensaje = "<p>Estimado {0} gracias por registrarte en vendeagro.com</p>" +
                    "<p>Para completar tu registro y poder hacer login da click <a href=\'http://localhost:50827/Portal/ConfirmarMail?token=" + "{1}\'>AQUÍ</a></p>";

                    var result = Startup.GetServicioEmail().SendAsync(string.Format(mailMensaje, model.Nombre + " " + model.Apellidos, tokenEmail), "Registro VendeAgro", model.Email);
                    return RegistroStatus.Exitoso;
                }

            });

        }

        public async Task<LoginStatus> VerificarAdminSesionAsync()
        {
            if (_usuarioAdministradorActual != null) return LoginStatus.Exitoso;

            HttpRequest request = HttpContext.Current.Request;
            return await Task.Run(() =>
            {
                if (request.Cookies["AdminVendeAgro"] != null)
                {
                    var token = request.Cookies["AdminVendeAgro"]["token"];
                    if (token != null)
                    {
                        using (var _dbContext = new VendeAgroEntities())
                        {
                            var usuario = _dbContext.Usuario_Administrador.Where(u => u.tokenSesion == token).FirstOrDefault();
                            if (usuario == null)
                            {
                                return LoginStatus.Incorrecto;
                            }

                            if (!usuario.activo)
                            {
                                return LoginStatus.Incorrecto;
                            }

                            _usuarioAdministradorActual = new AdministradorUsuario(usuario.id, usuario.email, usuario.nombre);
                            return LoginStatus.Exitoso;
                        }

                    }
                }
                return LoginStatus.Incorrecto;
            });
        }

        public LoginStatus VerificarPortalSesion()
        {
            if (_usuarioPortalActual != null) return LoginStatus.Exitoso;

            HttpRequest request = HttpContext.Current.Request;
            if (request.Cookies["VendeAgroUser"] != null)
            {
                var token = request.Cookies["VendeAgroUser"]["token"];
                if (token != null)
                {
                    using (var _dbContext = new VendeAgroEntities())
                    {
                        var usuario = _dbContext.Usuarios.Where(u => u.tokenSesion == token).FirstOrDefault();
                        if (usuario == null)
                        {
                            return LoginStatus.Incorrecto;
                        }

                        _usuarioPortalActual = new PortalUsuario(usuario.id, usuario.email, usuario.nombre, usuario.apellidos, usuario.telefono.ToString());
                        return LoginStatus.Exitoso;
                    }

                }
            }

            return LoginStatus.Incorrecto;
        }

        public void LogoutPortal()
        {
            _usuarioPortalActual = null;
            HttpRequest request = HttpContext.Current.Request;
            if (request.Cookies["VendeAgroUser"] != null)
            {
                var token = request.Cookies["VendeAgroUser"]["token"];
                if (token != null)
                {
                    using (var _dbContext = new VendeAgroEntities())
                    {
                        var usuario = _dbContext.Usuarios.Where(u => u.tokenSesion == token).FirstOrDefault();
                        if (usuario == null)
                        {
                            return;
                        }

                        usuario.tokenSesion = "";
                        _dbContext.SaveChanges();
                        
                    }

                }
                borrarCookie(HttpContext.Current.Response, "VendeAgroUser");
            }
        }

        private string getToken()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                int opcion = random.Next(0, 9);
                if (opcion < 5)
                {
                    sb.Append(random.Next(0, 9));
                }
                else
                {
                    sb.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
                }
            }

            return sb.ToString();
        }

        private void setCookie(string name, string value, HttpResponse response)
        {
            HttpCookie myCookie = new HttpCookie(name);
            myCookie["token"] = value;
            myCookie.Expires = DateTime.Now.AddDays(5d);
            response.Cookies.Add(myCookie);
        }

        private void borrarCookie(HttpResponse response, string nombre)
        {
            HttpCookie temp = response.Cookies[nombre];
            temp.Expires = DateTime.Now.AddDays(-1D);
            response.Cookies.Add(temp);
        }

        public static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

    }

    public enum LoginStatus
    {
        Exitoso,
        ConfirmacionMail,
        Incorrecto
    }

    public enum RegistroStatus
    {
        Exitoso,
        MailOcupado,
        TelefonoOcupado,
        Incorrecto
    }
}