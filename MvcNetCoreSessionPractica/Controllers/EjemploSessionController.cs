using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSessionPractica.Extension;
using MvcNetCoreSessionPractica.Helpers;
using MvcNetCoreSessionPractica.Models;

namespace MvcNetCoreSessionPractica.Controllers
{
    public class EjemploSessionController : Controller
    {
        HelperSessionContextAccesor helper;

        public EjemploSessionController(HelperSessionContextAccesor helper)
        {
            this.helper = helper;
        }
        public IActionResult Index()
        {
            List<Mascota> mascotas = this.helper.GetMascotasSession();
            return View(mascotas);
        }

        public IActionResult SessionMascotaCollection(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota {Nombre = "Patricio", Raza = "Estrella de mar", Edad = 4},
                        new Mascota {Nombre = "Apu", Raza = "Monito", Edad = 10},
                        new Mascota {Nombre = "Donald", Raza = "Pato", Edad = 50},
                        new Mascota {Nombre = "Pluto", Raza = "Perro", Edad = 60}
                    };
                    HttpContext.Session.SetObject("MASCOTAS", mascotas);
                    ViewData["MENSAJE"] = "Coleccion Mascotas almacenada";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas = HttpContext.Session.GetObject<List<Mascota>>("MASCOTAS");
                    return View(mascotas);
                }
            }
            return View();
        }

        public IActionResult SessionMascotaObject(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Olaf",
                        Raza = "Muñeco",
                        Edad = 19
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota como Object almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota;
                    ViewData["MENSAJE"] = "Mascota recuperada de la session";
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJSON(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Simba",
                        Raza = "Leon",
                        Edad = 9
                    };
                    string jsonMascota = HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "MASCOTA JSON ALMACENADA";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string json = HttpContext.Session.GetString("MASCOTA");

                    if (json != null)
                    {
                        Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(json);
                        ViewData["MASCOTA"] = mascota;
                    }
                    else
                    {
                        ViewData["MENSAJE"] = "No se encontró la mascota en la sesión.";
                    }
                }
            }
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Flounder";
                    mascota.Raza = "Pex";
                    mascota.Edad = 5;
                    byte[] data = HelpperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en session";
                }else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    Mascota mascota =(Mascota) HelpperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }

            }
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                    {
                        List<Mascota> mascotas = new List<Mascota>
                        {
                            new Mascota {Nombre = "Nala", Raza = "Leona", Edad = 10},
                            new Mascota {Nombre = "Olaf", Raza = "Nieve", Edad = 14},
                            new Mascota {Nombre = "Rafiki", Raza = "Mono", Edad = 20},
                            new Mascota {Nombre = "Sebastian", Raza = "Cangrejo", Edad = 12}
                        };
                        byte[] data = HelpperBinarySession.ObjectToByte(mascotas);
                        HttpContext.Session.Set("MASCOTAS", data);
                        ViewData["MENSAJE"] = "Coleccion almacenada en Session";
                        return View();
                    }
                    else if (accion.ToLower() == "mostrar")
                    {
                        byte[] data = HttpContext.Session.Get("MASCOTAS");
                        List<Mascota> mascotas = (List<Mascota>)HelpperBinarySession.ByteToObject(data);
                        return View(mascotas);
                    }
            }
            
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados session";
                }else if (accion.ToLower() == "mostrar")
                {
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
    }
}
