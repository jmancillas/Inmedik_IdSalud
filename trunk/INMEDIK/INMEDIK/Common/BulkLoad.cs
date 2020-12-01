using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Helpers;
using INMEDIK.Models.Entity;
using System.Globalization;
using System.Data.Entity;

namespace INMEDIK.Common
{
    public class PatientLoadAux
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string NSS { get; set; }
        public string Referencia { get; set; }
    }

    public class BulkLoad
    {
        public static GenericResult UploadPatients(List<PatientLoadAux> patients)
        {
            GenericResult result = new GenericResult();
            List<PatientAux> processedPatients = new List<PatientAux>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    processedPatients = ProcessPatients(patients, db);
                    result = PatientHelper.CreateBulkPatients(processedPatients);
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }

            return result;
        }
        public static List<PatientAux> ProcessPatients(List<PatientLoadAux> patients, dbINMEDIK dbConnection)
        {
            List<PatientAux> result = new List<PatientAux>();
            
            DateTime tempDate;
            try
            {
                foreach (PatientLoadAux patient in patients)
                {
                    PatientAux temp = new PatientAux();
                    temp.personAux.name = string.IsNullOrEmpty(patient.Nombre) ? "" : patient.Nombre;
                    temp.personAux.lastName = string.IsNullOrEmpty(patient.Apellido) ? "" : patient.Apellido;
                    temp.personAux.sex = string.IsNullOrEmpty(patient.Sexo) ? "" : patient.Sexo;
                    if (DateTime.TryParseExact(patient.FechaNacimiento, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out tempDate))
                    {
                        temp.personAux.birthDate = tempDate;
                    }
                    else
                    {
                        temp.personAux.birthDate = DateTime.Now.AddYears(-30);
                    }
                    temp.personAux.email = string.IsNullOrEmpty(patient.Correo) ? "" : patient.Correo;
                    temp.personAux.phoneNumber = string.IsNullOrEmpty(patient.Telefono) ? "" : patient.Telefono;
                    temp.personAux.mobile = string.IsNullOrEmpty(patient.Movil) ? "" : patient.Movil;
                    temp.personAux.addressAux.addressLine = string.IsNullOrEmpty(patient.Direccion) ? "" : patient.Direccion;
                    temp.personAux.addressAux.postalCode = string.IsNullOrEmpty(patient.CodigoPostal) ? "" : patient.CodigoPostal;
                    temp.personAux.socialSecurity = string.IsNullOrEmpty(patient.NSS) ? "" : patient.NSS;
                    temp.reference = string.IsNullOrEmpty(patient.Referencia) ? "" : patient.Referencia;
                    if (string.IsNullOrEmpty(patient.Estado))
                    {
                        County tempCounty = dbConnection.County.Where(c => c.City.State.Name == "Nuevo León" && c.City.Name == "Monterrey" && c.Name == "OTRO").FirstOrDefault();
                        temp.personAux.addressAux.countyAux.id = tempCounty != null ? tempCounty.id : 1;
                    }
                    else
                    {
                        State tempState = dbConnection.State.Where(s => s.Name == patient.Estado).FirstOrDefault();
                        City tempCity;
                        if (string.IsNullOrEmpty(patient.Municipio))
                        {
                            tempCity = tempState.City.FirstOrDefault();
                            temp.personAux.addressAux.countyAux.id = tempCity != null ? (tempCity.County.FirstOrDefault() != null ? tempCity.County.FirstOrDefault().id : 1) : 1;
                        }
                        else
                        {
                            tempCity = tempState.City.Where(c => c.Name == patient.Municipio).FirstOrDefault();

                            if (string.IsNullOrEmpty(patient.Colonia))
                            {
                                temp.personAux.addressAux.countyAux.id = tempCity != null ? (tempCity.County.FirstOrDefault() != null ? tempCity.County.FirstOrDefault().id : 1) : 1;
                            }
                            else
                            {
                                County tempCounty = dbConnection.County.Where(c => c.Name == patient.Colonia).FirstOrDefault();
                                temp.personAux.addressAux.countyAux.id = tempCounty != null ? tempCounty.id : 1;
                            }
                        }
                    }
                    result.Add(temp);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}