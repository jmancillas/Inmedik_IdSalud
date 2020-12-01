using System;
using INMEDIK.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class FormAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int modifiedById { get; set; }
        public UserAux userAux { get; set; }
        public EmployeeAux employeeAux { get; set; }
        public bool deleted { get; set; }
        public DateTime? created { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }

        public List<ElementAux> elementList { get; set; }

        public FormAux()
        {
            this.userAux = new UserAux();
            this.elementList = new List<ElementAux>();
            this.employeeAux = new EmployeeAux();
        }
    }
    public class vwFormAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int modifiedById { get; set; }
        public string fullName { get; set; }
        public bool deleted { get; set; }
        public string createdString { get; set; }
        public string updatedString { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
    }
    public class FormDataAux
    {
        public int id { get; set; }
        public int examId { get; set; }
        public ExamAux examAux { get; set; }
        public int userId { get; set; }
        public UserAux userAux { get; set; }
        public DateTime? created { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public string JsonString { get; set; }

        public FormDataAux()
        {
            this.userAux = new UserAux();
            this.examAux = new ExamAux();
        }
    }

    public class FieldOptionAux
    {
        public int id { get; set; }
        public int fieldId { get; set; }
        public string value { get; set; }
    }
    public class FieldTypeAux
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class FieldAux
    {
        public int id { get; set; }
        public string tag { get; set; }
        public string description { get; set; }
        public int fieldTypeId { get; set; }
        public FieldTypeAux fieldTypeAux { get; set; }
        public string unit { get; set; }
        public bool required { get; set; }
        public string reference { get; set; }
        public decimal upperLimit { get; set; }
        public decimal lowerLimit { get; set; }
        public bool deleted { get; set; }
        public int modifiedById { get; set; }
        public UserAux userAux { get; set; }
        public DateTime? created { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }

        public List<FieldOptionAux> fieldOptionAux { get; set; }

        public FieldAux()
        {
            this.userAux = new UserAux();
            this.fieldTypeAux = new FieldTypeAux();
            this.fieldOptionAux = new List<FieldOptionAux>();
        }

        public void fillDB(ref Field dbField)
        {
            dbField.Tag = this.tag;
            dbField.Description = this.description;
            dbField.FieldTypeId = this.fieldTypeAux.id;
            dbField.Unit = this.unit;
            dbField.Reference = this.reference;
            dbField.UpperLimit = this.upperLimit;
            dbField.LowerLimit = this.lowerLimit;
            dbField.Deleted = this.deleted;
            dbField.ModifiedById = this.modifiedById;
        }
    }
    public class ModuleAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool deleted { get; set; }
        public int modifiedById { get; set; }
        public UserAux userAux { get; set; }
        public DateTime? created { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }

        public List<FieldAux> fieldList { get; set; }

        public ModuleAux()
        {
            this.userAux = new UserAux();
            this.fieldList = new List<FieldAux>();
        }

        public void fillDB(ref Module dbModule)
        {
            dbModule.Name = this.name;
            dbModule.Description = this.description;
            dbModule.Deleted = this.deleted;
            dbModule.ModifiedById = this.modifiedById;
        }
    }
    public class ElementAux
    {
        public FieldAux field { get; set; }
        public ModuleAux module { get; set; }
        public int order { get; set; }
    }

    public class vwFieldAux
    {
        public int id { get; set; }
        public string fieldName { get; set; }
        public string fieldDescription { get; set; }
        public string fieldTypeName { get; set; }
        public string unit { get; set; }
        public string modifiedBy { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public bool deleted { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
    }
    public class vwModuleAux
    {
        public int id { get; set; }
        public string moduleName { get; set; }
        public string moduleDescription { get; set; }
        public string modifiedBy { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public bool deleted { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                : "";
            }
        }
    }

    public class FormResult : Result
    {
        public FormAux data { get; set; }
        public List<FormAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public FormResult()
        {
            this.data = new FormAux();
            this.data_list = new List<FormAux>();
            this.total = new NumericResult();
        }
    }
    public class vwFormResult : Result
    {
        public vwFormAux data { get; set; }
        public List<vwFormAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwFormResult()
        {
            this.data = new vwFormAux();
            this.data_list = new List<vwFormAux>();
            this.total = new NumericResult();
        }
    }
    public class FormDataResult : Result
    {
        public FormDataAux data { get; set; }
        public List<FormDataAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public FormDataResult()
        {
            this.data = new FormDataAux();
            this.data_list = new List<FormDataAux>();
            this.total = new NumericResult();
        }
    }
    public class ModuleResult : Result
    {
        public ModuleAux data { get; set; }
        public List<ModuleAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public ModuleResult()
        {
            this.data = new ModuleAux();
            this.data_list = new List<ModuleAux>();
            this.total = new NumericResult();
        }
    }
    public class FieldResult : Result
    {
        public FieldAux data { get; set; }
        public List<FieldAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public FieldResult()
        {
            this.data = new FieldAux();
            this.data_list = new List<FieldAux>();
            this.total = new NumericResult();
        }
    }
    public class ElementResult : Result
    {
        public ElementAux data { get; set; }
        public List<ElementAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public ElementResult()
        {
            this.data = new ElementAux();
            this.data_list = new List<ElementAux>();
            this.total = new NumericResult();
        }
    }
    public class FieldTypeResult : Result
    {
        public FieldTypeAux data { get; set; }
        public List<FieldTypeAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public FieldTypeResult()
        {
            this.data = new FieldTypeAux();
            this.data_list = new List<FieldTypeAux>();
            this.total = new NumericResult();
        }
    }
    public class vwFieldResult : Result
    {
        public vwFieldAux data { get; set; }
        public List<vwFieldAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwFieldResult()
        {
            this.data = new vwFieldAux();
            this.data_list = new List<vwFieldAux>();
            this.total = new NumericResult();
        }
    }
    public class vwModuleResult : Result
    {
        public vwModuleAux data { get; set; }
        public List<vwModuleAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public vwModuleResult()
        {
            this.data = new vwModuleAux();
            this.data_list = new List<vwModuleAux>();
            this.total = new NumericResult();
        }
    }

    public class FormHelper
    {
        public static GenericResult SaveForm(FormAux form)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (form.id > 0)
                    {
                        Form formDB = db.Form.FirstOrDefault(f => f.id == form.id);
                        if (formDB != null)
                        {
                            formDB.Description = form.description;
                            formDB.Deleted = form.deleted;
                            formDB.ModifiedById = form.modifiedById;
                            formDB.Name = form.name;
                            db.RelFormField.RemoveRange(formDB.RelFormField);
                            db.RelFormModule.RemoveRange(formDB.RelFormModule);

                            foreach (ElementAux element in form.elementList)
                            {
                                if (element.field != null)
                                {
                                    Field temp;
                                    RelFormField newRel = db.RelFormField.Create();
                                    temp = db.Field.FirstOrDefault(f => f.id == element.field.id);
                                    newRel.Field = temp;
                                    newRel.Form = formDB;
                                    newRel.Required = element.field.required;
                                    newRel.Position = form.elementList.IndexOf(element);
                                    db.RelFormField.Add(newRel);
                                }
                                else
                                {
                                    Module temp;
                                    RelFormModule newRel = db.RelFormModule.Create();
                                    temp = db.Module.FirstOrDefault(f => f.id == element.module.id);

                                    newRel.Module = temp;
                                    newRel.Form = formDB;
                                    newRel.Position = form.elementList.IndexOf(element);
                                    db.RelFormModule.Add(newRel);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Formulario no encontrado.");
                        }
                    }
                    else
                    {
                        Form formDB = db.Form.Create();
                        formDB.Description = form.description;
                        formDB.Deleted = form.deleted;
                        formDB.ModifiedById = form.modifiedById;
                        formDB.Name = form.name;
                        foreach (ElementAux element in form.elementList)
                        {
                            if (element.field != null)
                            {
                                Field temp;
                                RelFormField newRel = db.RelFormField.Create();

                                temp = db.Field.FirstOrDefault(f => f.id == element.field.id);

                                newRel.Field = temp;
                                newRel.Form = formDB;
                                newRel.Required = element.field.required;
                                newRel.Position = form.elementList.IndexOf(element);
                                db.RelFormField.Add(newRel);
                            }
                            else
                            {
                                Module temp;
                                RelFormModule newRel = db.RelFormModule.Create();
                                temp = db.Module.FirstOrDefault(f => f.id == element.module.id);

                                newRel.Module = temp;
                                newRel.Form = formDB;
                                newRel.Position = form.elementList.IndexOf(element);
                                db.RelFormModule.Add(newRel);
                            }
                        }

                        db.Form.Add(formDB);
                    }
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.bool_value = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static GenericResult SaveFormData(FormDataAux data)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    FormData formData;
                    if (data.id > 0)
                    {
                        formData = db.FormData.FirstOrDefault();
                        if (formData != null)
                        {
                            formData.ExamId = data.examId;
                            formData.JsonString = data.JsonString;
                            formData.UserId = data.userId;
                        }
                        else
                        {
                            throw new Exception("Formulario no encontrado.");
                        }
                    }
                    else
                    {
                        formData = db.FormData.Create();
                        formData.ExamId = data.examId;
                        formData.JsonString = data.JsonString;
                        formData.UserId = data.userId;
                        db.FormData.Add(formData);
                    }
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.bool_value = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static GenericResult SaveField(FieldAux field)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (field.id > 0)
                    {
                        Field dbField = db.Field.FirstOrDefault(f => f.id == field.id);
                        if (dbField != null)
                        {
                            field.fillDB(ref dbField);
                            db.FieldOption.RemoveRange(dbField.FieldOption);
                            foreach (FieldOptionAux option in field.fieldOptionAux)
                            {
                                FieldOption temp = db.FieldOption.Create();
                                temp.Value = option.value;
                                dbField.FieldOption.Add(temp);
                            }
                        }
                        else
                        {
                            throw new Exception("Campo no encontrado.");
                        }
                    }
                    else
                    {
                        Field dbField = db.Field.Create();
                        field.fillDB(ref dbField);
                        foreach (FieldOptionAux option in field.fieldOptionAux)
                        {
                            dbField.FieldOption.Add(new FieldOption()
                            {
                                Value = option.value
                            });
                        }
                        db.Field.Add(dbField);
                    }
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.bool_value = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static GenericResult SaveModule(ModuleAux module)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (module.id > 0)
                    {
                        Module dbModule = db.Module.FirstOrDefault(f => f.id == module.id);
                        if (dbModule != null)
                        {
                            module.fillDB(ref dbModule);
                            db.RelModuleField.RemoveRange(dbModule.RelModuleField);
                            foreach (FieldAux field in module.fieldList)
                            {
                                Field fieldTemp;
                                RelModuleField newFieldRel = db.RelModuleField.Create();

                                fieldTemp = db.Field.FirstOrDefault(f => f.id == field.id);

                                newFieldRel.Field = fieldTemp;
                                newFieldRel.Required = field.required;
                                newFieldRel.Module = dbModule;
                                newFieldRel.Position = module.fieldList.IndexOf(field);
                                db.RelModuleField.Add(newFieldRel);
                            }
                        }
                        else
                        {
                            throw new Exception("Módulo no encontrado.");
                        }
                    }
                    else
                    {
                        Module dbModule = db.Module.Create();
                        module.fillDB(ref dbModule);
                        foreach (FieldAux field in module.fieldList)
                        {
                            Field fieldTemp;
                            RelModuleField newFieldRel = db.RelModuleField.Create();
                            fieldTemp = db.Field.FirstOrDefault(f => f.id == field.id);

                            newFieldRel.Field = fieldTemp;
                            newFieldRel.Required = field.required;
                            newFieldRel.Module = dbModule;
                            newFieldRel.Position = module.fieldList.IndexOf(field);
                            db.RelModuleField.Add(newFieldRel);
                        }
                        db.Module.Add(dbModule);
                    }
                    db.SaveChanges();
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.bool_value = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static FormResult GetForm(int id)
        {
            FormResult result = new FormResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Form formDB = db.Form.FirstOrDefault(f => f.id == id);
                    if (formDB != null)
                    {
                        DataHelper.fill(result.data, formDB);
                        foreach (RelFormModule relModule in formDB.RelFormModule.Where(rm => !rm.Module.Deleted))
                        {
                            ModuleAux tempModule = new ModuleAux();
                            DataHelper.fill(tempModule, relModule.Module);
                            foreach (RelModuleField relField in relModule.Module.RelModuleField.Where(rm => !rm.Field.Deleted).OrderBy(rm => rm.Position))
                            {
                                FieldAux tempField = new FieldAux();
                                DataHelper.fill(tempField, relField.Field);
                                tempField.required = relField.Required;
                                DataHelper.fill(tempField.fieldTypeAux, relField.Field.FieldType);
                                foreach (FieldOption option in relField.Field.FieldOption)
                                {
                                    FieldOptionAux temp = new FieldOptionAux();
                                    DataHelper.fill(temp, option);
                                    tempField.fieldOptionAux.Add(temp);
                                }
                                tempModule.fieldList.Add(tempField);
                            }
                            result.data.elementList.Add(new ElementAux()
                            {
                                module = tempModule,
                                order = relModule.Position
                            });
                        }
                        foreach (RelFormField relField in formDB.RelFormField.Where(rm => !rm.Field.Deleted))
                        {
                            FieldAux tempField = new FieldAux();
                            DataHelper.fill(tempField, relField.Field);
                            tempField.required = relField.Required;
                            DataHelper.fill(tempField.fieldTypeAux, relField.Field.FieldType);
                            foreach (FieldOption option in relField.Field.FieldOption)
                            {
                                FieldOptionAux temp = new FieldOptionAux();
                                DataHelper.fill(temp, option);
                                tempField.fieldOptionAux.Add(temp);
                            }
                            result.data.elementList.Add(new ElementAux()
                            {
                                field = tempField,
                                order = relField.Position
                            });
                        }
                        result.data.elementList.Sort((x, y) => x.order.CompareTo(y.order));
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Formulario no encontrado.";
                    }
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
        public static FormDataResult GetFormData(int id)
        {
            FormDataResult result = new FormDataResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    FormData dataDB = db.FormData.FirstOrDefault(f => f.id == id);
                    if (dataDB != null)
                    {
                        DataHelper.fill(result.data, dataDB);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Datos no encontrados.";
                    }
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
        public static FormDataResult GetFormDataByExam(int id)
        {
            FormDataResult result = new FormDataResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    FormData dataDB = db.FormData.FirstOrDefault(f => f.ExamId == id);
                    if (dataDB != null)
                    {
                        DataHelper.fill(result.data, dataDB);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Datos no encontrados.";
                    }
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
        public static FieldResult GetField(int id)
        {
            FieldResult result = new FieldResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Field fieldDB = db.Field.FirstOrDefault(f => f.id == id);
                    if (fieldDB != null)
                    {
                        DataHelper.fill(result.data, fieldDB);
                        DataHelper.fill(result.data.fieldTypeAux, fieldDB.FieldType);
                        foreach (FieldOption option in fieldDB.FieldOption)
                        {
                            FieldOptionAux temp = new FieldOptionAux();
                            DataHelper.fill(temp, option);
                            result.data.fieldOptionAux.Add(temp);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Campo no encontrado.";
                    }
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
        public static ModuleResult GetModule(int id)
        {
            ModuleResult result = new ModuleResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Module dataDB = db.Module.FirstOrDefault(f => f.id == id);
                    if (dataDB != null)
                    {
                        DataHelper.fill(result.data, dataDB);
                        foreach (RelModuleField relField in dataDB.RelModuleField.Where(rm => !rm.Field.Deleted).OrderBy(rm => rm.Position))
                        {
                            FieldAux tempField = new FieldAux();
                            DataHelper.fill(tempField, relField.Field);
                            tempField.required = relField.Required;
                            DataHelper.fill(tempField.fieldTypeAux, relField.Field.FieldType);
                            foreach (FieldOption option in relField.Field.FieldOption)
                            {
                                FieldOptionAux temp = new FieldOptionAux();
                                DataHelper.fill(temp, option);
                                tempField.fieldOptionAux.Add(temp);
                            }
                            result.data.fieldList.Add(tempField);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Módulo no encontrado.";
                    }
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

        public static GenericResult DeleteForm(int id, int userId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Form dbForm = db.Form.Where(f => f.id == id).FirstOrDefault();
                    dbForm.Deleted = true;
                    dbForm.ModifiedById = userId;
                    db.SaveChanges();
                    result.success = true;
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
        public static GenericResult DeleteModule(int id, int userId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Module dbModule = db.Module.Where(f => f.id == id).FirstOrDefault();
                    dbModule.Deleted = true;
                    dbModule.ModifiedById = userId;
                    db.SaveChanges();
                    result.success = true;
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
        public static GenericResult DeleteField(int id, int userId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Field dbField = db.Field.Where(f => f.id == id).FirstOrDefault();
                    dbField.Deleted = true;
                    dbField.ModifiedById = userId;
                    db.SaveChanges();
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

        public static vwFormResult GetForms(DTParameterModel filter)
        {
            vwFormResult result = new vwFormResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwForm> query = db.vwForm.Where(f => !f.Deleted);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "description" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Description.Contains(column.Search.Value));
                        }
                        if (column.Data == "fullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.fullName.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name);
                            }
                        }
                        if (orderColumn == "description")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Description);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Description);
                            }
                        }
                        if (orderColumn == "fullName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.fullName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.fullName);
                            }
                        }
                        if (orderColumn == "sCreated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                        if (orderColumn == "sUpdated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwForm formDB in query.ToList())
                    {
                        vwFormAux aux = new vwFormAux();
                        DataHelper.fill(aux, formDB);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
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
        public static ModuleResult GetModules(DTParameterModel filter)
        {
            ModuleResult result = new ModuleResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<Module> query = db.Module.Where(f => !f.Deleted);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "description" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Description.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name);
                            }
                        }
                        if (orderColumn == "description")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Description);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Description);
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Module moduleDB in query.ToList())
                    {
                        ModuleAux aux = new ModuleAux();
                        DataHelper.fill(aux, moduleDB);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
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
        public static FieldResult GetFields(DTParameterModel filter)
        {
            FieldResult result = new FieldResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<Field> query = db.Field.Where(f => !f.Deleted);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "tag" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Tag.Contains(column.Search.Value));
                        }
                        if (column.Data == "description" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Description.Contains(column.Search.Value));
                        }
                        if (column.Data == "fieldTypeAux.name")
                        {
                            query = query.Where(q => q.FieldType.Name.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "tag")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Tag);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Tag);
                            }
                        }
                        if (orderColumn == "description")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Description);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Description);
                            }
                        }
                        if (orderColumn == "fieldTypeAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.FieldType.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.FieldType.Name);
                            }
                        }

                        result.total.value = query.Count();

                        query = query.Skip(filter.Start).Take(filter.Length);
                        foreach (Field fieldDB in query.ToList())
                        {
                            FieldAux aux = new FieldAux();
                            DataHelper.fill(aux, fieldDB);
                            DataHelper.fill(aux.fieldTypeAux, fieldDB.FieldType);
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
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
        public static vwFieldResult SearchFields(DTParameterModel filter)
        {
            vwFieldResult result = new vwFieldResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwField> query = db.vwField.Where(f => !f.Deleted);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "fieldName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.fieldName.Contains(column.Search.Value));
                        }
                        if (column.Data == "fieldDescription" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.fieldDescription.Contains(column.Search.Value));
                        }
                        if (column.Data == "fieldTypeName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.fieldTypeName.Contains(column.Search.Value));
                        }
                        if (column.Data == "unit" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Unit.Contains(column.Search.Value));
                        }
                        if (column.Data == "modifiedBy" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.modifiedBy.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "fieldName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.fieldName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.fieldName);
                            }
                        }
                        if (orderColumn == "fieldDescription")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.fieldDescription);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.fieldDescription);
                            }
                        }
                        if (orderColumn == "fieldTypeName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.fieldTypeName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.fieldTypeName);
                            }
                        }
                        if (orderColumn == "unit")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Unit);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Unit);
                            }
                        }
                        if (orderColumn == "modifiedBy")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.modifiedBy);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.modifiedBy);
                            }
                        }
                        if (orderColumn == "sCreated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                        if (orderColumn == "sUpdated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }
                        result.total.value = query.Count();

                        query = query.Skip(filter.Start).Take(filter.Length);
                        foreach (vwField fieldDB in query.ToList())
                        {
                            vwFieldAux aux = new vwFieldAux();
                            DataHelper.fill(aux, fieldDB);
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
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
        public static vwModuleResult SearchModules(DTParameterModel filter)
        {
            vwModuleResult result = new vwModuleResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwModule> query = db.vwModule.Where(f => !f.Deleted);

                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "moduleName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.moduleName.Contains(column.Search.Value));
                        }
                        if (column.Data == "moduleDescription" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.moduleDescription.Contains(column.Search.Value));
                        }
                        if (column.Data == "modifiedBy")
                        {
                            query = query.Where(q => q.modifiedBy.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "moduleName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.moduleName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.moduleName);
                            }
                        }
                        if (orderColumn == "moduleDescription")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.moduleDescription);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.moduleDescription);
                            }
                        }
                        if (orderColumn == "modifiedBy")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.modifiedBy);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.modifiedBy);
                            }
                        }
                        if (orderColumn == "sCreated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                        if (orderColumn == "sUpdated")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }

                        result.total.value = query.Count();

                        query = query.Skip(filter.Start).Take(filter.Length);
                        foreach (vwModule fieldDB in query.ToList())
                        {
                            vwModuleAux aux = new vwModuleAux();
                            DataHelper.fill(aux, fieldDB);
                            result.data_list.Add(aux);
                        }
                        result.success = true;
                    }
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
        public static FieldTypeResult GetFieldTypeSelect()
        {
            FieldTypeResult result = new FieldTypeResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    foreach (FieldType type in db.FieldType.AsQueryable())
                    {
                        FieldTypeAux temp = new FieldTypeAux();
                        DataHelper.fill(temp, type);
                        result.data_list.Add(temp);
                    }
                    result.success = true;
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
    }
}