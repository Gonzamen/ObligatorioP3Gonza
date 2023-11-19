using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class ManejoArchivos
    {
        public ManejoArchivos() { }

        private static string ArchivoUsuarios = AppDomain.CurrentDomain.BaseDirectory + "//Archivos/UsuariosGuardados.txt";
        private static string ArchivoVacunas = AppDomain.CurrentDomain.BaseDirectory + "//Archivos/VacunasGuardadas.txt";
        private static string ArchivoLaboratorios = AppDomain.CurrentDomain.BaseDirectory + "//Archivos/LaboratoriosGuardados.txt";
        private static string ArchivoTiposVacuna = AppDomain.CurrentDomain.BaseDirectory + "//Archivos/TiposVacunaGuardados.txt";
        private static string ArchivoLabVac = AppDomain.CurrentDomain.BaseDirectory + "//Archivos/LabVacGuardados.txt";

        public static List<string> GenerarUsuarios()
        {
            List<string> ret = new List<string>();
            string fecha = DateTime.Today.DayOfWeek.ToString();
            StreamReader sr = null;
            RepositorioUsuario repo = new RepositorioUsuario();
            using (sr = new StreamReader(ArchivoUsuarios))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] vecDatos = linea.Split("|".ToCharArray());
                    if (repo.FindByCedula(vecDatos[0]) == null)
                    {
                        Usuario nuevo = new Usuario
                        {
                            Cedula = vecDatos[0],
                            Password = ""
                        };
                        if (nuevo.ValidarSoloCedula())
                        {
                            string nuevacontraseña = vecDatos[0].Substring(0, 3) + "/" + fecha.Substring(0, 3);
                            nuevo.Password = Seguridad.Encriptar(nuevacontraseña);
                            if (!repo.Add(nuevo))
                            {
                                ret.Add("ERROR; Usuario de cedula: " + vecDatos[0] + " generó error en la base de datos.");
                            }
                            else
                            {
                                ret.Add(nuevo.Cedula + "|" + nuevacontraseña);
                            }
                        }
                        else
                        {
                            ret.Add("ERROR; Usuario de cedula: " + vecDatos[0] + " es invalido.");
                        }
                    }
                    else
                    {
                        ret.Add("ERROR; Usuario de cedula: " + vecDatos[0] + " ya fue registrado");
                    }
                }
            }
            return ret;
        }

        public static string GenerarTiposVacuna()
        {
            string errores = "";
            StreamReader sr = null;
            RepositorioTipoVacuna repo = new RepositorioTipoVacuna();
            using (sr = new StreamReader(ArchivoTiposVacuna))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] vecDatos = linea.Split("|".ToCharArray());
                    if (repo.FindByCodigo(vecDatos[0]) == null)
                    {
                        TipoVacuna nuevo = new TipoVacuna
                        {
                            Codigo = vecDatos[0],
                            Descripcion = vecDatos[1]
                        };
                        if (nuevo.Validar())
                        {
                            if (!repo.Add(nuevo))
                            {
                                errores += "Tipo de Vacuna: " + vecDatos[0] + " generó error en la base de datos." + "\n\r";
                            }
                        }
                        else
                        {
                            errores += "Tipo de Vacuna: " + vecDatos[0] + " es invalido." + "\n\r";
                        }
                    }
                    else
                    {
                        errores += "Tipo de Vacuna: " + vecDatos[0] + " ya está registrado." + "\n\r";
                    }
                }
            }
            return errores;
        }

        public static string GenerarLaboratorios()
        {
            string errores = "";
            bool experienciabool = false;
            StreamReader sr = null;
            RepositorioLaboratorio repo = new RepositorioLaboratorio();
            using (sr = new StreamReader(ArchivoLaboratorios))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] vecDatos = linea.Split("|".ToCharArray());
                    if (repo.FindById(Int32.Parse(vecDatos[0])) == null && repo.FindByNombre(vecDatos[1]) == null)
                    {
                        if (vecDatos[2] == "SI")
                        {
                            experienciabool = true;
                        }
                        Laboratorio nuevo = new Laboratorio
                        {
                            Id = Int32.Parse(vecDatos[0]),
                            Nombre = vecDatos[1],
                            Experiencia = experienciabool,
                            Pais = vecDatos[3]
                        };
                        if (!repo.Add(nuevo))
                        {
                            errores += "Laboratorio: " + vecDatos[1] + " generó error en la base de datos." + "\n\r";
                        }
                    }
                    else
                    {
                        errores += "Laboratorio: " + vecDatos[1] + " ya está registrado." + "\n\r";
                    }
                }
            }
            return errores;
        }

        public static ICollection<Laboratorio> TraerLabxVacuna(int idvac)
        {
            ICollection<Laboratorio> ret = new List<Laboratorio>();
            StreamReader sr = null;
            RepositorioLaboratorio repo = new RepositorioLaboratorio();
            using(sr = new StreamReader(ArchivoLabVac))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] vecDatos = linea.Split("|".ToCharArray());
                    if (Int32.Parse(vecDatos[0]) == idvac)
                    {
                        if(repo.FindById(Int32.Parse(vecDatos[1])) != null)
                        {
                            ret.Add(repo.FindById(Int32.Parse(vecDatos[1])));
                        }                      
                    }
                }
            }
            return ret;
        }

        public static List<string> GenerarVacunas()
        {
            List<string> ret = new List<string>();
            bool boolapro = false;
            bool boolcovax = false;
            StreamReader sr = null;
            RepositorioVacuna repo = new RepositorioVacuna();
            RepositorioTipoVacuna repotipo = new RepositorioTipoVacuna();
            using (sr = new StreamReader(ArchivoVacunas))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] vecDatos = linea.Split("|".ToCharArray());
                    if(repo.FindById(Int32.Parse(vecDatos[0])) == null && repo.FindByNombre(vecDatos[1]) == null)
                    {
                        if (vecDatos[13] == "SI")
                        {
                            boolapro = true;
                        }
                        if(vecDatos[17] == "SI")
                        {
                            boolcovax = true;
                        }
                        Vacuna nueva = new Vacuna
                        {
                            Id = Int32.Parse(vecDatos[0]),
                            Nombre = vecDatos[1],
                            Laboratorios = TraerLabxVacuna(Int32.Parse(vecDatos[0])),
                            VacunaTipo = vecDatos[2],
                            Dosis = Int32.Parse(vecDatos[3]),
                            LapsoDosis = Int32.Parse(vecDatos[4]),
                            RangoEdadMin = Int32.Parse(vecDatos[5]),
                            RangoEdadMax = Int32.Parse(vecDatos[6]),
                            EfiCovid = Convert.ToDecimal(vecDatos[7]),
                            EfiHosp = Convert.ToDecimal(vecDatos[8]),
                            EfiCti = Convert.ToDecimal(vecDatos[9]),
                            TempMin = Int32.Parse(vecDatos[10]),
                            TempMax = Int32.Parse(vecDatos[11]),
                            Fase = Int32.Parse(vecDatos[12]),
                            AprobacionEmergencia = boolapro,
                            EfectosAdv = vecDatos[14],
                            Precio = Convert.ToDecimal(vecDatos[15]),
                            DosisAnual = Int32.Parse(vecDatos[16]),
                            Covax = boolcovax,
                            Cedula = vecDatos[18],
                            FechaRegistro = Convert.ToDateTime(vecDatos[19]),
                            EstatusPaises = vecDatos[20],
                        };
                        if (nueva.Validar())
                        {
                            if (!repo.Add(nueva))
                            {
                                ret.Add("ERROR; Vacuna: " + vecDatos[1] + " generó error en la base de datos.");
                            }
                            else
                            {
                                ret.Add(nueva.Nombre + "|" + nueva.VacunaTipo + "|" + nueva.Precio);
                            }
                        }
                        else
                        {
                            ret.Add("ERROR; Vacuna: " + vecDatos[1] + " es invalida.");
                        }                       
                    }
                    else
                    {
                        ret.Add("ERROR; Vacuna: " + vecDatos[1] + " ya está registrada.");
                    }
                }
            }
            return ret;
        }
    }
}

