using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;
using System.Data.Entity;
using System.Web.Configuration;
using System.Web.Hosting;
using System.IO;
using System.Text;

namespace INMEDIK.Models.Helpers
{
    public class vwElectronicFileAux
    {
        public int fileId { get; set; }
        public int patientId { get; set; }
        public string fullName { get; set; }
        public int clinicId { get; set; }
        public string clinicName { get; set; }
        public string curp { get; set; }
        public DateTime birthDate { get; set; }
        public string sex { get; set; }
        public string nationality { get; set; }
        public bool deleted { get; set; }
        public string companyName { get; set; }

        public string sex_string
        {
            get
            {
                string s;

                if (sex == "M")
                {
                    s = "Masculino";
                }
                else if (sex == "F")
                {
                    s = "Femenino";
                }
                else
                {
                    s = "No definido";
                }
                return s;
            }
        }
    }
    public class vwElectronicFileAuxResult : Result
    {
        public vwElectronicFileAux data { get; set; }
        public List<vwElectronicFileAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwElectronicFileAuxResult()
        {
            data = new vwElectronicFileAux();
            data_list = new List<vwElectronicFileAux>();
            total = new NumericResult();
        }
    }

    public class vwElectronicFilesUpdatesAux
    {
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(UpdatedDate).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string Source { get; set; }
        public string UserUpdated { get; set; }
    }

    public class vwElectronicFilesUpdatesAuxResult : Result
    {
        public vwElectronicFilesUpdatesAux data { get; set; }
        public List<vwElectronicFilesUpdatesAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwElectronicFilesUpdatesAuxResult()
        {
            data = new vwElectronicFilesUpdatesAux();
            data_list = new List<vwElectronicFilesUpdatesAux>();
            total = new NumericResult();
        }
    }

    public class IdentificationAux
    {
        public string created_date { get; set; }
        public string expFolio { get; set; }
        public int medicId { get; set; }
        public string medicName { get; set; }
        public string patientFullName { get; set; }
        public string curp { get; set; }
        public string birthdate_string { get; set; }
        public string sex_string { get; set; }
        public string nationality_string { get; set; }
        public string age_string { get; set; }
        public string clinicName { get; set; }
        public int clinicId { get; set; }
        public int consultId { get; set; }
    }

    public class ElectronicFileAux
    {
        //public ConsultAux consultAux { get; set; }
        public PatientAux patientAux { get; set; }
        public AHFAux ahfaux { get; set; }
        public ClinicAux clinicAux { get; set; }
        public List<AHFDiseaseAux> diseaseAux { get; set; }
        public DeviceAndSystemsAux deviceAndSystemsAux { get; set; }
        public CurrentConditionAux currentConditionAux { get; set; }
        public ExplorationAux explorationAux { get; set; }
        public List<PreviousResultAux> previousResultAux { get; set; }
        public List<DiagnosticAux> diagnosticAux { get; set; }
        public List<EvolutionNoteAux> evolutionNotesAux { get; set; }
        public List<InterconsultAux> interconsultAux { get; set; }
        public List<ReferencenoteAux> referencenoteAux { get; set; }
        public List<ServiceNoteAux> servicenoteAux { get; set; }
        public List<ExamNoteAux> examnoteAux { get; set; }
        public List<MedicNoteAux> medicnoteAux { get; set; }
        public IdentificationAux identificationAux { get; set; }


        public int id { get; set; }

        #region Ficha de identificación

        public string created_date { get; set; }
        public string expFolio { get; set; }
        public string medicName { get; set; }
        public string patientFullName { get; set; }
        public string curp { get; set; }
        public string birthdate_string { get; set; }
        public string sex_string { get; set; }
        public string nationality_string { get; set; }
        public string age_string { get; set; }
        public string clinicName { get; set; }
        public int consultId { get; set; }

        #endregion

        public ElectronicFileAux()
        {
            ahfaux = new AHFAux();
            patientAux = new PatientAux();
            clinicAux = new ClinicAux();
            diseaseAux = new List<AHFDiseaseAux>();
            deviceAndSystemsAux = new DeviceAndSystemsAux();
            currentConditionAux = new CurrentConditionAux();
            explorationAux = new ExplorationAux();
            previousResultAux = new List<PreviousResultAux>();
            diagnosticAux = new List<DiagnosticAux>();
            evolutionNotesAux = new List<EvolutionNoteAux>();
            interconsultAux = new List<InterconsultAux>();
            referencenoteAux = new List<ReferencenoteAux>();
            identificationAux = new IdentificationAux();
            servicenoteAux = new List<ServiceNoteAux>();
            examnoteAux = new List<ExamNoteAux>();
            medicnoteAux = new List<MedicNoteAux>();
        }
    }

    public class ElectronicFileResult : Result
    {
        public ElectronicFileAux data { get; set; }
        public List<ElectronicFileAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ElectronicFileResult()
        {
            data = new ElectronicFileAux();
            data_list = new List<ElectronicFileAux>();
            total = new NumericResult();
        }
    }

    #region Nota de servicio
    public class ServiceNoteAux
    {
        public int id { get; set; }
        public string notes { get; set; }
        public ConceptAux conceptAux { get; set; }
        public string createdby { get; set; }
        public List<FilesAux> filesAux { get; set; }
        public List<DiagnosticAux> diagnosticAux { get; set; }
        public RecipeAux recipeAux { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string age { get; set; }

        public ServiceNoteAux()
        {
            filesAux = new List<FilesAux>();
            conceptAux = new ConceptAux();
            diagnosticAux = new List<DiagnosticAux>();
            recipeAux = new RecipeAux();
        }

    }
    #endregion

    #region solicitud de examen 
    public class ExamNoteAux
    {
        public int id { get; set; }
        public string notes { get; set; }
        public ConceptAux conceptAux { get; set; }
        public RecipeAux recipeAux { get; set; }
        public List<FilesAux> filesAux { get; set; }
        public string createdby { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public ExamNoteAux()
        {
            filesAux = new List<FilesAux>();
            conceptAux = new ConceptAux();
            recipeAux = new RecipeAux();
        }
    }
    #endregion

    #region nota de Medicamento
    public class MedicNoteAux
    {
        public int id { get; set; }
        public string notes { get; set; }
        public string createdby { get; set; }
        public ConceptAux conceptAux { get; set; }
        public int quantity { get; set; }
        public int instock { get; set; }
        public DateTime created { get; set; }
        public List<AhoritaLoCambias> medicamstock { get; set; }
        public List<DiagnosticAux> diagnosticAux { get; set; }
        public RecipeAux recipeAux { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public MedicNoteAux()
        {
            medicamstock = new List<AhoritaLoCambias>();
            conceptAux = new ConceptAux();
            diagnosticAux = new List<DiagnosticAux>();
            recipeAux = new RecipeAux();
        }
    }

    public class MedicamStock
    {
        public List<AhoritaLoCambias> medicamstock { get; set; }
        public List<DiagnosticAux> diagnosticAux { get; set; }
        public RecipeAux recipeAux { get; set; }
        public int quantity { get; set; }
        public string recipetext { get; set; }

        public MedicamStock()
        {
            medicamstock = new List<AhoritaLoCambias>();
            diagnosticAux = new List<DiagnosticAux>();
            recipeAux = new RecipeAux();
        }
    }


    public class AhoritaLoCambias
    {
        public ConceptAux medicamento { get; set; }
        public RecipeAux recipeAux { get; set; }
        public int quantity { get; set; }
        public int stock { get; set; }
        public string recipetext { get; set; }

        public AhoritaLoCambias(){
            medicamento = new ConceptAux();
            recipeAux = new RecipeAux();
        }

    }
    #endregion

    #region Nota de interconsulta
    public class InterconsultAux
    {
        public int id { get; set; }
        public SpecialtyAux specialityAux { get; set; }
        public string shippingReason { get; set; }
        public string diagnosticCriteria { get; set; }
        public string studyPlan { get; set; }
        public string suggestions { get; set; }
        public string counterReference { get; set; }
        public bool IsCancelled { get; set; }
        public int MyProperty { get; set; }
        public DateTime Created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string CreatedBy { get; set; }

        public InterconsultAux()
        {
            specialityAux = new SpecialtyAux();
        }
    }

    public class InterconsultResult : Result
    {
        public InterconsultAux data { get; set; }
        public List<InterconsultAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public InterconsultResult()
        {
            data = new InterconsultAux();
            data_list = new List<InterconsultAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Nota de referencia
    public class ReferencenoteAux
    {
        public int id { get; set; }
        public ClinicAux clinicAux { get; set; }
        public string establishmentThatReceives { get; set; }
        public string diagnosticCriteria { get; set; }
        public string studyPlan { get; set; }
        public string suggestions { get; set; }
        public string counterReference { get; set; }
        public string shippingReason { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime Created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string CreatedBy { get; set; }
        public ReferencenoteAux()
        {
            clinicAux = new ClinicAux();
        }
    }

    public class ReferenceNoteResult : Result
    {
        public ReferencenoteAux data { get; set; }
        public List<ReferencenoteAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ReferenceNoteResult()
        {
            data = new ReferencenoteAux();
            data_list = new List<ReferencenoteAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Obtener vias de administración
    public class WayOfAdministrationAux
    {
        public int id { get; set; }
        public string wayOfAdministration { get; set; }
    }

    public class WayOfAdministrationResult : Result
    {
        public WayOfAdministrationAux data { get; set; }
        public List<WayOfAdministrationAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public WayOfAdministrationResult()
        {
            data = new WayOfAdministrationAux();
            data_list = new List<WayOfAdministrationAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Obtener sustancia activa
    public class vwSubstanceActiveAux
    {
        public string genericName { get; set; }
    }

    public class vwSubstanceActiveResult : Result
    {
        public vwSubstanceActiveAux data { get; set; }
        public List<vwSubstanceActiveAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwSubstanceActiveResult()
        {
            data = new vwSubstanceActiveAux();
            data_list = new List<vwSubstanceActiveAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Obtener Unidad
    public class vwPharmaceuticalFormAux
    {
        public string PharmaceuticalForm { get; set; }
        public string Presentation { get; set; }
    }

    public class vwPharmaceuticalFormResult : Result
    {
        public vwPharmaceuticalFormAux data { get; set; }
        public List<vwPharmaceuticalFormAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwPharmaceuticalFormResult()
        {
            data = new vwPharmaceuticalFormAux();
            data_list = new List<vwPharmaceuticalFormAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Obtener Forma Farmaceutica
    public class PharmaceuticalFormAux
    {
        public string pharmaceuticalForm { get; set; }
    }

    public class PharmaceuticalFormResult : Result
    {
        public PharmaceuticalFormAux data { get; set; }
        public List<PharmaceuticalFormAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PharmaceuticalFormResult()
        {
            data = new PharmaceuticalFormAux();
            data_list = new List<PharmaceuticalFormAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Obtener Presentacion
    public class PresentationAux
    {
        public string presentation { get; set; }
    }

    public class PresentationResult : Result
    {
        public PresentationAux data { get; set; }
        public List<PresentationAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PresentationResult()
        {
            data = new PresentationAux();
            data_list = new List<PresentationAux>();
            total = new NumericResult();
        }
    }
    #endregion

    public class ActiveSubstanceAux
    {
        public string ActiveSubstance { get; set; }
    }

    public class ActiveSubstanceResult : Result
    {
        public ActiveSubstanceAux data { get; set; }
        public List<ActiveSubstanceAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public ActiveSubstanceResult()
        {
            data = new ActiveSubstanceAux();
            data_list = new List<ActiveSubstanceAux>();
            total = new NumericResult();
        }
    }

    #region Comentarios
    public class CommentsAux
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public string notes { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
    #endregion


    #region Nota de evolución
    public class EvolutionNoteAux
    {
        public int id { get; set; }
        public CurrentConditionAux evolutionConditionAux { get; set; }
        public ExplorationAux evolutionExplorationAux { get; set; }
        public List<PreviousResultAux> evolutionPreviousAux { get; set; }
        public List<RecipeAux> recipeAux { get; set; }
        public RecipeAux objRecipe { get; set; }
        public List<FilesAux> filesAux { get; set; }
        public List<DiagnosticAux> evolutionDiagnosticAux { get; set; }
        public CommentsAux commentsAux { get; set; }
        public UserAux userAux { get; set; }
        public List<AhoritaLoCambias> medicamstock { get; set; }
        public int currentIndex { get; set; }
        public DateTime created { get; set; }
        public string diagnostic_string { get; set; }
        public string userCreated { get; set; }
        public DateTime updated { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public bool IsCancelled { get; set; }

        public EvolutionNoteAux()
        {
            evolutionConditionAux = new CurrentConditionAux();
            evolutionExplorationAux = new ExplorationAux();
            evolutionPreviousAux = new List<PreviousResultAux>();
            evolutionDiagnosticAux = new List<DiagnosticAux>();
            userAux = new UserAux();
            medicamstock = new List<AhoritaLoCambias>();
            commentsAux = new CommentsAux();
            recipeAux = new List<RecipeAux>();
            filesAux = new List<FilesAux>();
            objRecipe = new RecipeAux();
        }
    }

    public class EvolutionNoteResult : Result
    {
        public EvolutionNoteAux data { get; set; }
        public List<EvolutionNoteAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public EvolutionNoteResult()
        {
            data = new EvolutionNoteAux();
            data_list = new List<EvolutionNoteAux>();
            total = new NumericResult();
        }
    }
    #endregion

    #region Imagenes
    public class FilesAux
    {
        public int id { get; set; }
        public int evolutionNoteId { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
    }
    #endregion

    #region Diagnostico
    public class DiagnosticAux
    {
        public int id { get; set; }
        public CIE10Aux cie10Aux { get; set; }
        public string pronostic { get; set; }
        public int dose { get; set; }
        public string presentacion { get; set; }
        public string wayOfAdministration { get; set; }
        public string frequency { get; set; }
        public int daysOfThreatment { get; set; }
        public string activeSubstance { get; set; }
        public string commercialBrand { get; set; }
        public string unit { get; set; }
        public int consecutivo { get; set; }
        public string name { get; set; }
        public string recipeText { get; set; }

        public DiagnosticAux()
        {
            cie10Aux = new CIE10Aux();
        }
    }
    #endregion

    #region Receta
    public class RecipeAux
    {
        public int id { get; set; }
        public int consecutivo { get; set; }
        public string nombre { get; set; }
        public string recipe { get; set; }
        public string descripcion_capitulo { get; set; }
        public List<IndicationsAux> indicationsAux { get; set; }
        public CIE10Aux cie10Aux { get; set; }

        public RecipeAux()
        {
            indicationsAux = new List<IndicationsAux>();
            cie10Aux = new CIE10Aux();
        }
    }

    public class IndicationsAux
    {
        public int id { get; set; }
        public string activeSubstance { get; set; }
        public string commercialBrand { get; set; }
        public int daysOfThreatment { get; set; }
        public int dose { get; set; }
        public string frequency { get; set; }
        public string presentation { get; set; }
        public string pronostic { get; set; }
        public string unit { get; set; }
        public string wayOfAdministration { get; set; }
    }
    #endregion

    #region Resultados Previos
    public class PreviousResultAux
    {
        public int id { get; set; }
        public int? cholesterol { get; set; }
        public DateTime? dateOfTaking_Cholesterol { get; set; }
        public int? triglycerides { get; set; }
        public DateTime? dateOfTaking_Triglycerides { get; set; }
        public int? glucose { get; set; }
        public DateTime? dateOfTaking_Glucose { get; set; }
        public string glucose_category { get; set; }
        public string comments { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
    }
    #endregion

    #region Exploracion

    public class ExplorationAux
    {
        public int id { get; set; }
        public string habitus { get; set; }
        public int? temperature { get; set; }
        public int? ta_sistolica { get; set; }
        public int? ta_diastolica { get; set; }
        public int? heartRate { get; set; }
        public int? breathingFrequency { get; set; }
        public int? oxygenSaturation { get; set; }
        public int? weight { get; set; }
        public int? size { get; set; }
        public int? hipCircumference { get; set; }
        public int? waistCircumference { get; set; }
        public decimal? imc { get; set; }
        public string painScale { get; set; }
        public int painScaleAdult { get; set; }
        public bool saveVitalSigns { get; set; }
    }

    #endregion

    #region Padecimiento Actual

    public class CurrentConditionAux
    {
        public int id { get; set; }
        public string reasonForConsultation { get; set; }
        public string subjective { get; set; }
        public string objective { get; set; }
        public string analysis { get; set; }
        public string tipeConsult { get; set; }
        public List<CourseThreatmentAux> coursethreatmentAux { get; set; }

        public CurrentConditionAux()
        {
            coursethreatmentAux = new List<CourseThreatmentAux>();
        }

    }

    public class CourseThreatmentAux
    {
        public int id { get; set; }
        public int currentConditionId { get; set; }
        public int consecutivo { get; set; }
        public CIE10Aux cie10Aux { get; set; }
        public string medicine { get; set; }
        public string dose { get; set; }
        public string generalIndication { get; set; }

        public CourseThreatmentAux()
        {
            cie10Aux = new CIE10Aux();
        }
    }

    #endregion

    #region Aparatos y sistemas

    public class DeviceAndSystemsAux
    {
        public int id { get; set; }
        public string respiratory { get; set; }
        public string cardiovascular { get; set; }
        public string digestive { get; set; }
        public string urinary { get; set; }
        public string musculoskeletal { get; set; }
        public string genital { get; set; }
        public string hematological { get; set; }
        public string endocrine { get; set; }
        public string nervous { get; set; }
        public string psychosomatic { get; set; }
        public string others { get; set; }
    }

    #endregion

    #region Antecedentes Personales No Patologicos

    public class APNPAux
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public string livingPlace { get; set; }
        public string feeding { get; set; }
        public string hygiene { get; set; }
        public string physicalActivity { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    #endregion

    #region Antecedentes Personales Patologicos

    public class APPAux
    {
        public int id { get; set; }
        //public int employeeId { get; set; }
        public bool disease { get; set; }
        public bool hta { get; set; }
        public bool dm { get; set; }
        public string currentDisease { get; set; }
        public bool surgical { get; set; }
        public string surgical_string { get; set; }
        public bool transfuctional { get; set; }
        public string transfunctional_string { get; set; }
        public bool trauma { get; set; }
        public string trauma_string { get; set; }
        public bool previousInternals { get; set; }
        public string previousInternals_string { get; set; }
        public bool allergic { get; set; }
        public string allergic_string { get; set; }
        public string tobacco_question { get; set; }
        public string tobacco_string { get; set; }
        public string alcohol_question { get; set; }
        public string alcohol_string { get; set; }
        public string drugs_question { get; set; }
        public string drugs_string { get; set; }
        //public DateTime created { get; set; }
        //public DateTime updated { get; set; }
    }

    #endregion

    #region Antecedentes Heredo Familiares

    public class AHFAux
    {
        public int id { get; set; }
        public AHFCommentsAux commentsAux { get; set; }
        public List<AHFFamilyAux> familyAux { get; set; }

        public AHFAux()
        {
            this.commentsAux = new AHFCommentsAux();
            this.familyAux = new List<AHFFamilyAux>();
        }
    }

    public class AHFCommentsAux
    {
        public int id { get; set; }
        public string comments { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class AHFDiseaseAux
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class AhfDiseaseResult : Result
    {
        public AHFDiseaseAux data { get; set; }
        public List<AHFDiseaseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AhfDiseaseResult()
        {
            data = new AHFDiseaseAux();
            data_list = new List<AHFDiseaseAux>();
            total = new NumericResult();
        }
    }

    public class AHFFamilyAux
    {
        public int id { get; set; }
        public bool grandfather { get; set; }
        public bool grandmother { get; set; }
        public bool father { get; set; }
        public bool mother { get; set; }
        public bool brothers { get; set; }
        public bool uncles { get; set; }
        public List<AHFDiseaseAux> diseaseAux { get; set; }

        public AHFFamilyAux()
        {
            diseaseAux = new List<AHFDiseaseAux>();
        }

    }

    #endregion

    public class ElectronicFileHelper
    {
        public static AhfDiseaseResult GetDiseases()
        {
            AhfDiseaseResult result = new AhfDiseaseResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.AHFDisease;
                    foreach (var Diseasedata in query)
                    {
                        AHFDiseaseAux aux = new AHFDiseaseAux();
                        DataHelper.fill(aux, Diseasedata);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static vwElectronicFileAuxResult GetElectronicFiles(DTParameterModel filter)
        {
            vwElectronicFileAuxResult result = new vwElectronicFileAuxResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwElectronicFile> query = db.vwElectronicFile;

                    foreach (DTColumn column in filter.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "patientId":
                                query = columnHasValue ? query.Where(u => u.PatientId.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "fileId":
                                query = columnHasValue ? query.Where(u => u.FileId.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "fullName":
                                query = columnHasValue ? query.Where(u => u.FullName.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                        if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
                        {
                            string order = filter.Order.First().Dir;
                            string orderColumn = filter.Order.First().Data;

                            switch (orderColumn)
                            {
                                case "patientId":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.PatientId) : query.OrderByDescending(q => q.PatientId);
                                    break;
                                case "fileId":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.FileId) : query.OrderByDescending(q => q.FileId);
                                    break;
                                case "fullName":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.FullName) : query.OrderByDescending(q => q.FullName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (vwElectronicFile file in query.ToList())
                    {
                        vwElectronicFileAux aux = new vwElectronicFileAux();
                        DataHelper.fill(aux, file);
                        result.data_list.Add(aux);
                    }

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static vwElectronicFilesUpdatesAuxResult GetElectronicFileUpdates(ElectronicFileAux ConsultFile)
        {
            vwElectronicFilesUpdatesAuxResult result = new vwElectronicFilesUpdatesAuxResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {

                    var query = db.vwElectronicFilesUpdates.Where(e => e.UpdatedDate != null && e.ElectronicFileId == ConsultFile.id).ToList();
                    foreach (var Update in query.ToList())
                    {
                        vwElectronicFilesUpdatesAux aux = new vwElectronicFilesUpdatesAux();
                        DataHelper.fill(aux, Update);
                        Employee employeeDB = db.Employee.Where(e => e.UserId == Update.UpdatedBy).FirstOrDefault();
                        if (employeeDB != null)
                        {
                            Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                            aux.UserUpdated = personDB.Name + " " + personDB.LastName;
                        }
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

        public static WayOfAdministrationResult GetWayOfAdministration(string data)
        {
            WayOfAdministrationResult result = new WayOfAdministrationResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.RouteOfAdministration.Where(p => p.WayOfAdministration.Contains(data));
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (RouteOfAdministration routeDb in query.ToList())
                    {
                        WayOfAdministrationAux aux = new WayOfAdministrationAux();
                        DataHelper.fill(aux, routeDb);
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

        public static vwSubstanceActiveResult LoadActiveSubstance(string data)
        {
            vwSubstanceActiveResult result = new vwSubstanceActiveResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(data)).Select(p => p.GenericName).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var genericName in query.ToList())
                    {
                        vwSubstanceActiveAux aux = new vwSubstanceActiveAux();
                        aux.genericName = genericName;
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

        public static vwPharmaceuticalFormResult GetUnit(string data)
        {
            vwPharmaceuticalFormResult result = new vwPharmaceuticalFormResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(data)).Select(p => p.PharmaceuticalForm).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var pharmaceuticalForm in query.ToList())
                    {
                        vwPharmaceuticalFormAux aux = new vwPharmaceuticalFormAux();
                        aux.PharmaceuticalForm = pharmaceuticalForm;
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

        public static vwPharmaceuticalFormResult LoadPresentation(string data)
        {
            vwPharmaceuticalFormResult result = new vwPharmaceuticalFormResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(data)).Select(p => p.Presentation).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var Presentation in query.ToList())
                    {
                        vwPharmaceuticalFormAux aux = new vwPharmaceuticalFormAux();
                        aux.Presentation = Presentation;
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

        public static PharmaceuticalFormResult GetPharmaceuticalForm(vwSubstanceActiveAux item)
        {
            PharmaceuticalFormResult result = new PharmaceuticalFormResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(item.genericName)).Select(p => p.PharmaceuticalForm).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var pharmaceuticalForm in query.ToList())
                    {
                        PharmaceuticalFormAux aux = new PharmaceuticalFormAux();
                        aux.pharmaceuticalForm = pharmaceuticalForm;
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

        public static PresentationResult GetPresentation(PharmaceuticalFormAux unit, vwSubstanceActiveAux substance)
        {
            PresentationResult result = new PresentationResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(substance.genericName) && p.PharmaceuticalForm.Contains(unit.pharmaceuticalForm)).Select(p => p.Presentation).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var presentation in query.ToList())
                    {
                        PresentationAux aux = new PresentationAux();
                        aux.presentation = presentation;
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

        public static ActiveSubstanceResult GetActiveSubstance(vwSubstanceActiveAux item)
        {
            ActiveSubstanceResult result = new ActiveSubstanceResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Medicines.Where(p => p.GenericName.Contains(item.genericName)).Select(p => p.GenericName).Distinct();
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (var ActiveSubstance in query.ToList())
                    {
                        ActiveSubstanceAux aux = new ActiveSubstanceAux();
                        aux.ActiveSubstance = ActiveSubstance;
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

        public static ReferenceNoteResult SaveCurrentReferenceNote(ReferencenoteAux reference, int idx, int electronicFileId)
        {
            ReferenceNoteResult result = new ReferenceNoteResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    ElectronicFile elecFileDb = db.ElectronicFile.Where(e => e.Id == electronicFileId).FirstOrDefault();
                    if (elecFileDb != null)
                    {
                        if (reference.id > 0)
                        {
                            ReferenceNote refeDb = db.ReferenceNote.Where(i => i.Id == reference.id).FirstOrDefault();
                            refeDb.EstablishmentThatSends = reference.clinicAux.id;
                            refeDb.EstablishmentThatReceives = reference.establishmentThatReceives;
                            refeDb.DiagnosticCriteria = reference.diagnosticCriteria;
                            refeDb.StudyPlan = reference.studyPlan;
                            refeDb.Suggestions = reference.suggestions;
                            refeDb.CounterReference = reference.counterReference;
                            refeDb.ShippingReason = reference.shippingReason;
                            refeDb.Updated = DateTime.UtcNow;
                            refeDb.UpdatedBy = res.User.id.Value;

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de referencia";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            db.SaveChanges();
                            result.success = true;
                        }
                        else
                        {
                            ReferenceNote refeData = db.ReferenceNote.Create();
                            refeData.ElectronicFileId = electronicFileId;
                            refeData.EstablishmentThatSends = reference.clinicAux.id;
                            refeData.EstablishmentThatReceives = reference.establishmentThatReceives;
                            refeData.DiagnosticCriteria = reference.diagnosticCriteria;
                            refeData.StudyPlan = reference.studyPlan;
                            refeData.Suggestions = reference.suggestions;
                            refeData.CounterReference = reference.counterReference;
                            refeData.ShippingReason = reference.shippingReason;
                            refeData.Created = DateTime.UtcNow;
                            refeData.CreatedBy = res.User.id.Value;

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de referencia";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            db.ReferenceNote.Add(refeData);
                            db.SaveChanges();
                            result.success = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static InterconsultResult SaveCurrentInterconsult(InterconsultAux interconsult, int idx, int electronicFileId)
        {
            InterconsultResult result = new InterconsultResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    ElectronicFile elecFileDb = db.ElectronicFile.Where(e => e.Id == electronicFileId).FirstOrDefault();
                    if (elecFileDb != null)
                    {
                        if (interconsult.id > 0)
                        {
                            InterconsultNote interDb = db.InterconsultNote.Where(i => i.Id == interconsult.id).FirstOrDefault();
                            interDb.SpecialityId = interconsult.specialityAux.id;
                            interDb.ShippingReason = interconsult.shippingReason;
                            interDb.DiagnosticCriteria = interconsult.diagnosticCriteria;
                            interDb.StudyPlan = interconsult.studyPlan;
                            interDb.Suggestions = interconsult.suggestions;
                            interDb.CounterReference = interconsult.counterReference;
                            interDb.Updated = DateTime.UtcNow;
                            interDb.UpdatedBy = res.User.id.Value;

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = elecFileDb.Created;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de interconsulta";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            db.SaveChanges();
                            result.success = true;
                            result.data.id = interconsult.id;
                        }
                        else
                        {
                            InterconsultNote interData = db.InterconsultNote.Create();
                            interData.ElectronicFileId = electronicFileId;
                            interData.SpecialityId = interconsult.specialityAux.id;
                            interData.ShippingReason = interconsult.shippingReason;
                            interData.DiagnosticCriteria = interconsult.diagnosticCriteria;
                            interData.StudyPlan = interconsult.studyPlan;
                            interData.Suggestions = interconsult.suggestions;
                            interData.CounterReference = interconsult.counterReference;
                            interData.Created = DateTime.UtcNow;
                            interData.CreatedBy = res.User.id.Value;
                            db.InterconsultNote.Add(interData);

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = elecFileDb.Created;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de interconsulta";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            db.SaveChanges();
                            result.success = true;
                            result.data.id = interData.Id;
                        }
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static EvolutionNoteResult SaveCurrentNote(ConceptAux concept, EvolutionNoteAux evolution, int idx, int electronicFileId, bool toPrint, ConceptAux tipec, bool toSigns)
        {
            EvolutionNoteResult result = new EvolutionNoteResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (evolution.id > 0)
                        { 
                            
                            ElectronicFile elecFileDb = db.ElectronicFile.Where(e => e.Id == electronicFileId).FirstOrDefault();
                            Patient patientdb = db.Patient.Where(p => p.id == elecFileDb.PatientId).FirstOrDefault();

                            PatientAux aux = new PatientAux();
                            DataHelper.fill(aux, patientdb);
                            DataHelper.fill(aux.personAux, patientdb.Person);
                            EvolutionNote evolutionDb = db.EvolutionNote.Where(p => p.Id == evolution.id).FirstOrDefault();

                            if(evolutionDb != null && concept.id != 0) evolutionDb.ConceptId = concept.id;

                            ElectronicFileUpdate electronicFileUpdate = new ElectronicFileUpdate();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = evolutionDb.Created;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de evolución";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);

                            db.SaveChanges();

                            EvolCurrentCondition conditionDb = db.EvolCurrentCondition.Where(e => e.Id == evolutionDb.EvolCurrentConditionId).FirstOrDefault();
                            if (conditionDb != null)
                            {
                                conditionDb.TipeConsult = evolution.evolutionConditionAux.tipeConsult;
                                conditionDb.ReasonForConsultation = evolution.evolutionConditionAux.reasonForConsultation;
                                conditionDb.Subjective = evolution.evolutionConditionAux.subjective;
                                conditionDb.Objective = evolution.evolutionConditionAux.objective;
                                conditionDb.Analysis = evolution.evolutionConditionAux.analysis;

                                db.SaveChanges();
                            }
                            else
                            {
                                EvolCurrentCondition conditionData = db.EvolCurrentCondition.Create();
                                conditionData.TipeConsult = evolution.evolutionConditionAux.tipeConsult;
                                conditionData.ReasonForConsultation = evolution.evolutionConditionAux.reasonForConsultation;
                                conditionData.Subjective = evolution.evolutionConditionAux.subjective;
                                conditionData.Objective = evolution.evolutionConditionAux.objective;
                                conditionData.Analysis = evolution.evolutionConditionAux.analysis;
                                db.EvolCurrentCondition.Add(conditionData);

                                db.SaveChanges();

                                evolutionDb.EvolCurrentConditionId = conditionData.Id;
                                result.data.evolutionConditionAux.id = conditionData.Id;
                            }
                            EvolExploration explorationDb = db.EvolExploration.Where(e => e.Id == evolutionDb.EvolExplorationId).FirstOrDefault();
                            if (explorationDb != null)
                            {
                                explorationDb.Habitus = evolution.evolutionExplorationAux.habitus;
                                explorationDb.Temperature = evolution.evolutionExplorationAux.temperature;
                                explorationDb.TA_Sistolica = evolution.evolutionExplorationAux.ta_sistolica;
                                explorationDb.TA_Diastolica = evolution.evolutionExplorationAux.ta_diastolica;
                                explorationDb.HeartRate = evolution.evolutionExplorationAux.heartRate;
                                explorationDb.BreathingFrequency = evolution.evolutionExplorationAux.breathingFrequency;
                                explorationDb.OxygenSaturation = evolution.evolutionExplorationAux.oxygenSaturation;
                                explorationDb.Weight = evolution.evolutionExplorationAux.weight;
                                explorationDb.Size = evolution.evolutionExplorationAux.size;
                                explorationDb.HipCircumference = evolution.evolutionExplorationAux.hipCircumference;
                                explorationDb.WaistCircumference = evolution.evolutionExplorationAux.waistCircumference;
                                explorationDb.IMC = evolution.evolutionExplorationAux.imc;
                                if (aux.personAux.age >= 8)
                                {
                                    explorationDb.PainScale = evolution.evolutionExplorationAux.painScaleAdult.ToString();
                                    explorationDb.PainScaleAdult = evolution.evolutionExplorationAux.painScaleAdult;
                                }
                                else
                                {
                                    explorationDb.PainScale = evolution.evolutionExplorationAux.painScale;
                                    explorationDb.PainScaleAdult = Convert.ToInt32(evolution.evolutionExplorationAux.painScale);
                                }

                                if (!toSigns) result.data.evolutionExplorationAux.saveVitalSigns = false;
                                db.SaveChanges();
                            }
                            else
                            {
                                EvolExploration explorationData = db.EvolExploration.Create();
                                explorationData.Habitus = evolution.evolutionExplorationAux.habitus;
                                explorationData.Temperature = evolution.evolutionExplorationAux.temperature;
                                explorationData.TA_Sistolica = evolution.evolutionExplorationAux.ta_sistolica;
                                explorationData.TA_Diastolica = evolution.evolutionExplorationAux.ta_diastolica;
                                explorationData.HeartRate = evolution.evolutionExplorationAux.heartRate;
                                explorationData.BreathingFrequency = evolution.evolutionExplorationAux.breathingFrequency;
                                explorationData.OxygenSaturation = evolution.evolutionExplorationAux.oxygenSaturation;
                                explorationData.Weight = evolution.evolutionExplorationAux.weight;
                                explorationData.Size = evolution.evolutionExplorationAux.size;
                                explorationData.HipCircumference = evolution.evolutionExplorationAux.hipCircumference;
                                explorationData.WaistCircumference = evolution.evolutionExplorationAux.waistCircumference;
                                explorationData.IMC = evolution.evolutionExplorationAux.imc;
                                if (aux.personAux.age >= 8)
                                {
                                    explorationDb.PainScale = evolution.evolutionExplorationAux.painScaleAdult.ToString();
                                    explorationDb.PainScaleAdult = evolution.evolutionExplorationAux.painScaleAdult;
                                }
                                else
                                {
                                    explorationDb.PainScale = evolution.evolutionExplorationAux.painScale;
                                    explorationDb.PainScaleAdult = Convert.ToInt32(evolution.evolutionExplorationAux.painScale);
                                }

                                db.EvolExploration.Add(explorationData);
                                db.SaveChanges();
                                evolutionDb.EvolExplorationId = explorationData.Id;
                                result.data.evolutionExplorationAux.id = explorationData.Id;
                                if (toSigns) result.data.evolutionExplorationAux.saveVitalSigns = true;
                            }

                            EvolComments commentsDb = db.EvolComments.Where(e => e.Id == evolutionDb.EvolCommentsId).FirstOrDefault();
                            if (commentsDb != null)
                            {
                                commentsDb.Notes = evolution.commentsAux.notes;
                                db.SaveChanges();
                            }
                            else
                            {
                                EvolComments commentsData = db.EvolComments.Create();
                                commentsData.Notes = evolution.commentsAux.notes;

                                db.EvolComments.Add(commentsData);
                                db.SaveChanges();

                                evolutionDb.EvolCommentsId = commentsData.Id;
                                result.data.commentsAux.id = commentsData.Id;
                            }

                            Recipe recipeDb = db.Recipe.Create();
                            recipeDb.EvolutionNoteId = evolution.id;
                            recipeDb.RecipeText = evolution.objRecipe.recipe;

                            db.Recipe.Add(recipeDb);
                            db.SaveChanges();

                            if (recipeDb.Id > 0)
                            {
                                result.data.objRecipe.id = recipeDb.Id;
                            }

                            foreach (var medicaments in evolution.medicamstock)
                            {
                                var consept = db.Concept.FirstOrDefault(c => c.id == medicaments.medicamento.id);
                                Product products = db.Product.Create();
                                products.ConceptId = medicaments.medicamento.id;
                                products.Quantity = medicaments.quantity;
                                products.CreatedBy = res.User.id.Value;
                                products.Created = DateTime.UtcNow;

                                evolutionDb.Product.Add(products);

                                var stock = db.Stock.FirstOrDefault(s => s.ConceptId == medicaments.medicamento.id);
                                stock.InStock -= medicaments.quantity;
                            }
                            evolutionDb.Updated = DateTime.UtcNow;
                            evolutionDb.UpdatedBy = res.User.id.Value;
                            result.data.id = evolution.id;
                            result.data.currentIndex = idx;
                            
                        }
                        else
                        {
                            ElectronicFile elecFileDb = db.ElectronicFile.Where(e => e.Id == electronicFileId).FirstOrDefault();
                            Patient patientdb = db.Patient.Where(p => p.id == elecFileDb.PatientId).FirstOrDefault();

                            PatientAux aux = new PatientAux();
                            DataHelper.fill(aux, patientdb);
                            DataHelper.fill(aux.personAux, patientdb.Person);

                            EvolCurrentCondition conditionData = db.EvolCurrentCondition.Create();
                            conditionData.TipeConsult = evolution.evolutionConditionAux.tipeConsult;
                            conditionData.ReasonForConsultation = evolution.evolutionConditionAux.reasonForConsultation;
                            conditionData.Subjective = evolution.evolutionConditionAux.subjective;
                            conditionData.Objective = evolution.evolutionConditionAux.objective;
                            conditionData.Analysis = evolution.evolutionConditionAux.analysis;
                            db.EvolCurrentCondition.Add(conditionData);

                            db.SaveChanges();

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = elecFileDb.Created;
                            electronicFileUpdate.ElectronicFileId = elecFileDb.Id;
                            electronicFileUpdate.Source = "Nota de evolución";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            EvolExploration explorationData = db.EvolExploration.Create();
                            explorationData.Habitus = evolution.evolutionExplorationAux.habitus;
                            explorationData.Temperature = evolution.evolutionExplorationAux.temperature;
                            explorationData.TA_Sistolica = evolution.evolutionExplorationAux.ta_sistolica;
                            explorationData.TA_Diastolica = evolution.evolutionExplorationAux.ta_diastolica;
                            explorationData.HeartRate = evolution.evolutionExplorationAux.heartRate;
                            explorationData.BreathingFrequency = evolution.evolutionExplorationAux.breathingFrequency;
                            explorationData.OxygenSaturation = evolution.evolutionExplorationAux.oxygenSaturation;
                            explorationData.Weight = evolution.evolutionExplorationAux.weight;
                            explorationData.Size = evolution.evolutionExplorationAux.size;
                            explorationData.HipCircumference = evolution.evolutionExplorationAux.hipCircumference;
                            explorationData.WaistCircumference = evolution.evolutionExplorationAux.waistCircumference;
                            explorationData.IMC = evolution.evolutionExplorationAux.imc;
                            if (aux.personAux.age >= 8)
                            {
                                explorationData.PainScale = evolution.evolutionExplorationAux.painScaleAdult.ToString();
                                explorationData.PainScaleAdult = evolution.evolutionExplorationAux.painScaleAdult;
                            }
                            else
                            {
                                explorationData.PainScale = evolution.evolutionExplorationAux.painScale;
                                explorationData.PainScaleAdult = Convert.ToInt32(evolution.evolutionExplorationAux.painScale);
                            }

                            db.EvolExploration.Add(explorationData);

                            db.SaveChanges();
                            result.data.evolutionExplorationAux.id = explorationData.Id;
                            if (!toSigns) result.data.evolutionExplorationAux.saveVitalSigns = false;

                            EvolComments commentsData = db.EvolComments.Create();
                            commentsData.Notes = evolution.commentsAux.notes;
                            db.EvolComments.Add(commentsData);

                            db.SaveChanges();
                            result.data.commentsAux.id = commentsData.Id;

                            EvolutionNote evolutionData = db.EvolutionNote.Create();
                            evolutionData.ConceptId = concept.id != 0 ? concept.id : (int?)null;
                            evolutionData.ElectronicFileId = electronicFileId;
                            evolutionData.EvolCurrentConditionId = conditionData.Id;
                            evolutionData.EvolExplorationId = explorationData.Id;
                            evolutionData.EvolCommentsId = commentsData.Id;
                            evolutionData.Created = DateTime.UtcNow;
                            evolutionData.CreatedBy = res.User.id.Value;
                            db.EvolutionNote.Add(evolutionData);

                            db.SaveChanges();

                            Recipe recipeDb = db.Recipe.Create();
                            recipeDb.EvolutionNoteId = evolutionData.Id;
                            recipeDb.RecipeText = evolution.objRecipe.recipe;

                            db.Recipe.Add(recipeDb);
                            db.SaveChanges();

                            if (recipeDb.Id > 0)
                            {
                                result.data.objRecipe.id = recipeDb.Id;
                            }

                            foreach (var DiagnosticData in evolution.evolutionDiagnosticAux)
                            {
                                var cie10db = db.CIE10.FirstOrDefault(c => c.Consecutivo == DiagnosticData.consecutivo);
                                string diagnosticNamedb = cie10db.Nombre;
                                EvolDiagnostic EvolDiDB = db.EvolDiagnostic.Create();
                                EvolDiDB.CIE10Id = DiagnosticData.consecutivo;
                                EvolDiDB.EvolutionNoteId = evolutionData.Id;
                                EvolDiDB.Pronostic = DiagnosticData.pronostic;
                                EvolDiDB.Dose = DiagnosticData.dose;
                                EvolDiDB.Presentation = DiagnosticData.presentacion;
                                EvolDiDB.WayOfAdministration = DiagnosticData.wayOfAdministration;
                                EvolDiDB.Frequency = DiagnosticData.frequency;
                                EvolDiDB.DaysOfThreatment = DiagnosticData.daysOfThreatment;
                                EvolDiDB.ActiveSubstance = DiagnosticData.activeSubstance;
                                EvolDiDB.CommercialBrand = DiagnosticData.commercialBrand;
                                EvolDiDB.Unit = DiagnosticData.unit;
                                EvolDiDB.DiagnosticName = diagnosticNamedb;
                                db.EvolDiagnostic.Add(EvolDiDB);
                                db.SaveChanges();
                            }

                            foreach (var medicaments in evolution.medicamstock)
                            {
                                var consept = db.Concept.FirstOrDefault(c => c.id == medicaments.medicamento.id);
                                Product products = db.Product.Create();
                                products.ConceptId = medicaments.medicamento.id;
                                products.Quantity = medicaments.quantity;
                                products.CreatedBy = res.User.id.Value;
                                products.Created = DateTime.UtcNow;

                                evolutionData.Product.Add(products);

                                var stock = db.Stock.FirstOrDefault(s => s.ConceptId == medicaments.medicamento.id);
                                stock.InStock -= medicaments.quantity;
                            }
                            result.data.id = evolutionData.Id;
                            result.data.evolutionConditionAux.id = conditionData.Id;
                            result.data.evolutionExplorationAux.id = explorationData.Id;
                        }

                        db.SaveChanges();

                        dbTransaction.Commit();
                        result.success = true;
                        result.data.currentIndex = idx;
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }

        public static EvolutionNoteResult CancelEvolutionNote(int Evid)
        {
            EvolutionNoteResult result = new EvolutionNoteResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    EvolutionNote EvDB = db.EvolutionNote.Where(c => c.Id == Evid).FirstOrDefault();
                    if (EvDB != null)
                    {
                        EvDB.IsCancelled = true;
                        EvDB.Updated = DateTime.UtcNow;
                        EvDB.UpdatedBy = res.User.id.Value;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontro la nota.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static InterconsultResult CancelInterconsultNote(int InId)
        {
            InterconsultResult result = new InterconsultResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    InterconsultNote InDB = db.InterconsultNote.Where(c => c.Id == InId).FirstOrDefault();
                    if (InDB != null)
                    {
                        InDB.IsCancelled = true;
                        InDB.Updated = DateTime.UtcNow;
                        InDB.UpdatedBy = res.User.id.Value;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontro la nota.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static ReferenceNoteResult CancelReferenceNote(int RefId)
        {
            ReferenceNoteResult result = new ReferenceNoteResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    ReferenceNote RefDB = db.ReferenceNote.Where(c => c.Id == RefId).FirstOrDefault();
                    if (RefDB != null)
                    {
                        RefDB.IsCancelled = true;
                        RefDB.Updated = DateTime.UtcNow;
                        RefDB.UpdatedBy = res.User.id.Value;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No se encontro la nota.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static ElectronicFileResult GetConsultFile(int consultId)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //Consult consultDb = db.Consult.Where(c => c.id == consultId).FirstOrDefault();
                    ElectronicFile fileDb = db.ElectronicFile.Where(e => e.Id == consultId).FirstOrDefault();
                    Patient patientDb = db.Patient.Where(p => p.id == fileDb.PatientId).FirstOrDefault();



                    AHF ahfDb = db.AHF.Where(a => a.PatientId == patientDb.id).FirstOrDefault();

                    ElectronicFileAux electronicAux = new ElectronicFileAux();
                    ConsultAux consultAux = new ConsultAux();
                    PatientAux patientAux = new PatientAux();

                    List<AHFDisease> diseasesDB = db.AHFDisease.ToList();

                    foreach (var diseases in diseasesDB)
                    {
                        AHFDiseaseAux diseasesAux = new AHFDiseaseAux();
                        DataHelper.fill(diseasesAux, diseases);
                        result.data.diseaseAux.Add(diseasesAux);
                    }

                    if (fileDb != null)
                    {
                        DataHelper.fill(result.data, fileDb);

                        #region Exploracion
                        if (fileDb.Exploration != null)
                        {
                            DataHelper.fill(result.data.explorationAux, fileDb.Exploration);
                        }
                        #endregion

                        #region Aparatos y sistemas
                        if (fileDb.DeviceAndSystems != null)
                        {
                            DataHelper.fill(result.data.deviceAndSystemsAux, fileDb.DeviceAndSystems);
                        }
                        #endregion
                        #region Clinica
                        if (fileDb.ClinicId != null)
                        {
                            DataHelper.fill(result.data.clinicAux, fileDb.Clinic);
                        }
                        #endregion
                        #region Padecimiento Actual
                        if (fileDb.CurrentCondition != null)
                        {
                            DataHelper.fill(result.data.currentConditionAux, fileDb.CurrentCondition);
                            if (fileDb.CurrentCondition.CourseThreatment.Count > 0)
                            {
                                foreach (var courseT in fileDb.CurrentCondition.CourseThreatment)
                                {
                                    CourseThreatmentAux temp = new CourseThreatmentAux();
                                    DataHelper.fill(temp, courseT);
                                    DataHelper.fill(temp.cie10Aux, courseT.CIE10);
                                    result.data.currentConditionAux.coursethreatmentAux.Add(temp);
                                }
                            }
                        }
                        #endregion
                        #region Exploracion Fisica
                        if (fileDb.Exploration != null)
                        {
                            DataHelper.fill(result.data.explorationAux, fileDb.Exploration);
                        }
                        #endregion
                        #region Resultados Previos
                        if (fileDb.PreviousResult.Count > 0)
                        {
                            foreach (var previous in fileDb.PreviousResult)
                            {
                                PreviousResultAux temp = new PreviousResultAux();
                                DataHelper.fill(temp, previous);
                                Employee employeeDB = db.Employee.Where(e => e.UserId == previous.CreatedBy).FirstOrDefault();
                                if (employeeDB != null)
                                {
                                    Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                                    if (personDB != null)
                                    {
                                        temp.CreatedBy = personDB.Name + " " + personDB.LastName;
                                    }
                                    else
                                    {
                                        temp.CreatedBy = previous.User.UserAccount;
                                    }
                                }
                                else
                                {
                                    temp.CreatedBy = previous.User.UserAccount;
                                }
                                result.data.previousResultAux.Add(temp);
                            }
                        }
                        #endregion
                        #region Diagnostico
                        if (fileDb.Diagnostic.Count > 0)
                        {
                            foreach (var Diag in fileDb.Diagnostic)
                            {
                                DiagnosticAux temp = new DiagnosticAux();
                                DataHelper.fill(temp, Diag);
                                DataHelper.fill(temp.cie10Aux, Diag.CIE10);
                                result.data.diagnosticAux.Add(temp);
                            }
                        }
                        #endregion
                        if (fileDb.Service.Count > 0)
                        {
                            foreach (var service in fileDb.Service)
                            {
                                ServiceNoteAux serviceNoteAux = new ServiceNoteAux();
                                serviceNoteAux.createdby = service.Employee.Person.Name;
                                serviceNoteAux.created = service.Created;
                                serviceNoteAux.notes = service.Notes;
                                serviceNoteAux.id = service.id;
                                serviceNoteAux.conceptAux.name = service.Concept.Name;
                                serviceNoteAux.recipeAux.recipe = service.Recipe.RecipeText;
                                serviceNoteAux.recipeAux.id = service.Recipe.Id;
                                result.data.servicenoteAux.Add(serviceNoteAux);

                                foreach (var Diag in service.Diagnostic)
                                {
                                    DiagnosticAux temp = new DiagnosticAux();
                                    temp.pronostic = Diag.Pronostic;
                                    temp.cie10Aux.consecutivo = Diag.CIE10.Consecutivo;
                                    temp.cie10Aux.nombre = Diag.CIE10.Nombre;
                                    temp.name = Diag.CIE10.Nombre;
                                    serviceNoteAux.diagnosticAux.Add(temp);
                                }
                                foreach (var files in service.EvFile)
                                {
                                    FilesAux tempFile = new FilesAux();
                                    DataHelper.fill(tempFile, files);
                                    serviceNoteAux.filesAux.Add(tempFile);
                                }
                            }
                        }

                        if (fileDb.ProductNote.Count > 0)
                        {
                            foreach (var product in fileDb.ProductNote)
                            {
                                MedicNoteAux medicNoteAux = new MedicNoteAux();
                                medicNoteAux.createdby = fileDb.Employee.Person.Name;
                                medicNoteAux.created = (DateTime)product.Created;
                                medicNoteAux.notes = product.Notes;
                                medicNoteAux.recipeAux.recipe = product.Recipe.RecipeText;
                                medicNoteAux.recipeAux.id = product.Recipe.Id;
                                result.data.medicnoteAux.Add(medicNoteAux);

                                foreach (var Diag in product.Diagnostic)
                                {
                                    DiagnosticAux temp = new DiagnosticAux();
                                    temp.pronostic = Diag.Pronostic;
                                    temp.cie10Aux.consecutivo = Diag.CIE10.Consecutivo;
                                    temp.consecutivo = Diag.CIE10.Consecutivo;
                                    temp.cie10Aux.nombre = Diag.CIE10.Nombre;
                                    temp.name = Diag.CIE10.Nombre;
                                    medicNoteAux.diagnosticAux.Add(temp);
                                }
                                foreach (var Prod in product.Product)
                                {
                                    var stock = db.Stock.FirstOrDefault(s=>s.ConceptId == Prod.ConceptId);
                                    AhoritaLoCambias temp = new AhoritaLoCambias();
                                    temp.quantity = (int)Prod.Quantity;
                                    temp.stock = stock.InStock;
                                    temp.medicamento.inStock = stock.InStock;
                                    temp.medicamento.name = Prod.Concept.Name;
                                    temp.medicamento.id = Prod.Concept.id;
                                    medicNoteAux.medicamstock.Add(temp);
                                }
                            }
                        }
                        if (fileDb.Exam.Count > 0)
                        {
                            foreach (var exam in fileDb.Exam)
                            {
                                ExamNoteAux examNoteAux = new ExamNoteAux();
                                examNoteAux.createdby = fileDb.Employee.Person.Name;
                                examNoteAux.created = (DateTime)exam.Created;
                                examNoteAux.conceptAux.name = exam.Concept.Name;
                                examNoteAux.recipeAux.recipe = exam.Recipe.RecipeText;
                                examNoteAux.recipeAux.id = exam.Recipe.Id;
                                result.data.examnoteAux.Add(examNoteAux);

                            }
                        }
                        #region Nota de evolución
                        if (fileDb.EvolutionNote.Count > 0)
                        {
                            var EvolutionList = fileDb.EvolutionNote.OrderBy(f => f.Created).Reverse();
                            foreach (var Evol in EvolutionList)
                            {
                                EvolutionNoteAux temp = new EvolutionNoteAux();
                                DataHelper.fill(temp, Evol);
                                DataHelper.fill(temp.evolutionConditionAux, Evol.EvolCurrentCondition);
                                DataHelper.fill(temp.evolutionExplorationAux, Evol.EvolExploration);
                                
                                temp.commentsAux.notes = Evol.EvolComments.Notes;
                                foreach (var files in Evol.EvFile)
                                {
                                    FilesAux tempFile = new FilesAux();
                                    DataHelper.fill(tempFile, files);
                                    temp.filesAux.Add(tempFile);
                                }
                                foreach (var threatment in Evol.EvolCurrentCondition.EvolCourseThreatment)
                                {
                                    CourseThreatmentAux tempThreat = new CourseThreatmentAux();
                                    DataHelper.fill(tempThreat, threatment);
                                    DataHelper.fill(tempThreat.cie10Aux, threatment.CIE10);
                                }
                                foreach (var evolPrevious in Evol.EvolPreviousResult)
                                {
                                    PreviousResultAux tempPrevious = new PreviousResultAux();
                                    DataHelper.fill(tempPrevious, evolPrevious);
                                    Employee EMPLOYEEdb = db.Employee.Where(e => e.UserId == evolPrevious.CreatedBy).FirstOrDefault();
                                    if (EMPLOYEEdb != null)
                                    {
                                        Person personDB = db.Person.Where(p => p.id == EMPLOYEEdb.PersonId).FirstOrDefault();
                                        if (personDB != null)
                                        {
                                            tempPrevious.CreatedBy = personDB.Name + " " + personDB.LastName;
                                        }
                                        else
                                        {
                                            tempPrevious.CreatedBy = evolPrevious.User.UserAccount;
                                        }
                                    }
                                    else
                                    {
                                        if (evolPrevious.User != null)
                                        {
                                            tempPrevious.CreatedBy = evolPrevious.User.UserAccount;
                                        }
                                        else
                                        {
                                            tempPrevious.CreatedBy = "Usuario no encontrado";
                                        }
                                    }
                                    temp.evolutionPreviousAux.Add(tempPrevious);
                                }
                                foreach (var recipe in Evol.Recipe)
                                {
                                    RecipeAux tempRecipe = new RecipeAux();
                                    tempRecipe.recipe = recipe.RecipeText;
                                    DataHelper.fill(tempRecipe, recipe);
                                    if (recipe.Indication.Count > 0)
                                    {
                                        foreach (var indication in recipe.Indication)
                                        {
                                            IndicationsAux tempIndication = new IndicationsAux();
                                            DataHelper.fill(tempIndication, indication);
                                            tempRecipe.indicationsAux.Add(tempIndication);
                                        }
                                    }
                                    temp.recipeAux.Add(tempRecipe);
                                    temp.objRecipe.recipe = recipe.RecipeText;
                                    temp.objRecipe.id = recipe.Id;
                                }
                                foreach (var Diagnostic in Evol.EvolDiagnostic)
                                {
                                    DiagnosticAux tempDiag = new DiagnosticAux();
                                    tempDiag.presentacion = Diagnostic.Presentation;
                                    DataHelper.fill(tempDiag, Diagnostic);
                                    tempDiag.name = Diagnostic.DiagnosticName;
                                    DataHelper.fill(tempDiag.cie10Aux, Diagnostic.CIE10);
                                    tempDiag.consecutivo = tempDiag.cie10Aux.consecutivo;
                                    temp.evolutionDiagnosticAux.Add(tempDiag);
                                }
                                foreach (var Prod in Evol.Product)
                                {
                                    var stock = db.Stock.FirstOrDefault(s => s.ConceptId == Prod.ConceptId);
                                    AhoritaLoCambias tempo = new AhoritaLoCambias();
                                    tempo.quantity = (int)Prod.Quantity;
                                    tempo.stock = stock.InStock;
                                    tempo.medicamento.inStock = stock.InStock;
                                    tempo.medicamento.name = Prod.Concept.Name;
                                    tempo.medicamento.id = Prod.Concept.id;
                                    temp.medicamstock.Add(tempo);
                                }

                                Employee employeeDB = db.Employee.Where(e => e.UserId == Evol.CreatedBy).FirstOrDefault();
                                if (employeeDB != null)
                                {
                                    Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                                    if (personDB != null)
                                    {
                                        temp.userCreated = personDB.Name + " " + personDB.LastName;
                                    }
                                    else
                                    {
                                        temp.userCreated = Evol.User.UserAccount;
                                    }
                                }
                                else
                                {
                                    temp.userCreated = Evol.User.UserAccount;
                                }

                                result.data.evolutionNotesAux.Add(temp);
                            }
                        }
                        #endregion
                        #region Nota de interconsulta
                        if (fileDb.InterconsultNote.Count > 0)
                        {
                            foreach (var interconsult in fileDb.InterconsultNote)
                            {
                                InterconsultAux tempInter = new InterconsultAux();
                                DataHelper.fill(tempInter, interconsult);
                                if (interconsult.Specialty != null)
                                {
                                    DataHelper.fill(tempInter.specialityAux, interconsult.Specialty);
                                }
                                Employee employeeDB = db.Employee.Where(e => e.UserId == interconsult.CreatedBy).FirstOrDefault();
                                if (employeeDB != null)
                                {
                                    Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                                    if (personDB != null)
                                    {
                                        tempInter.CreatedBy = personDB.Name + " " + personDB.LastName;
                                    }
                                    else
                                    {
                                        tempInter.CreatedBy = interconsult.User.UserAccount;
                                    }
                                }
                                else
                                {
                                    tempInter.CreatedBy = interconsult.User.UserAccount;
                                }
                                result.data.interconsultAux.Add(tempInter);
                            }
                        }
                        #endregion
                        #region Nota de referencia
                        if (fileDb.ReferenceNote.Count > 0)
                        {
                            foreach (var reference in fileDb.ReferenceNote)
                            {
                                ReferencenoteAux tempReference = new ReferencenoteAux();
                                DataHelper.fill(tempReference, reference);
                                if (reference.Clinic != null)
                                {
                                    DataHelper.fill(tempReference.clinicAux, reference.Clinic);
                                }
                                Employee employeeDB = db.Employee.Where(e => e.UserId == reference.CreatedBy).FirstOrDefault();
                                if (employeeDB != null)
                                {
                                    Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                                    if (personDB != null)
                                    {
                                        tempReference.CreatedBy = personDB.Name + " " + personDB.LastName;
                                    }
                                    else
                                    {
                                        tempReference.CreatedBy = reference.User.UserAccount;
                                    }
                                }
                                else
                                {
                                    tempReference.CreatedBy = reference.User.UserAccount;
                                }
                                result.data.referencenoteAux.Add(tempReference);
                            }
                        }
                        #endregion
                    }

                    if (patientDb != null)
                    {
                        DataHelper.fill(patientAux, patientDb);
                        DataHelper.fill(patientAux.personAux, patientDb.Person);
                        if (patientDb.Person.Nationality != null)
                        {
                            //DataHelper.fill(patientAux.personAux.nationalityAux, patientDb.Person.Nationality);
                        }
                        #region Antecedentes Heredos Familiares
                        if (patientDb.AHF.Count > 0)
                        {
                            DataHelper.fill(patientAux.ahfaux, patientDb.AHF.Last());
                            DataHelper.fill(patientAux.ahfaux.commentsAux, patientDb.AHF.First().AHFComments);
                            foreach (var family in patientDb.AHF.Last().AHFFamily)
                            {
                                AHFFamilyAux temp = new AHFFamilyAux();
                                DataHelper.fill(temp, family);

                                foreach (var disease in family.AHFDisease)
                                {
                                    AHFDiseaseAux diseasetemp = new AHFDiseaseAux();
                                    DataHelper.fill(diseasetemp, disease);
                                    temp.diseaseAux.Add(diseasetemp);
                                }
                                patientAux.ahfaux.familyAux.Add(temp);
                            }
                        }
                        #endregion
                        #region Antecedentes Personales Patologicos
                        if (patientDb.APP != null)
                        {
                            var ChronicDisease = db.ChronicDisease.ToList();
                            //ChronicDisease = ChronicDisease.Where(c => c.ElectronicFile.Contains(fileDb)).ToList();
                            if (ChronicDisease.Count() > 0)
                            {
                                foreach (var item in ChronicDisease)
                                {
                                    if (item.Code == "DM")
                                    {
                                        patientAux.appAux.dm = true;
                                    }
                                    if (item.Code == "HTA")
                                    {
                                        patientAux.appAux.hta = true;
                                    }
                                }
                            }

                            DataHelper.fill(patientAux.appAux, patientDb.APP);
                        }
                        #endregion
                        #region Antecedentes Personales no Patologicos
                        if (patientDb.APNP != null)
                        {
                            DataHelper.fill(patientAux.apnpAux, patientDb.APNP);
                        }
                        #endregion
                        result.data.patientAux = patientAux;
                    }

                    #region Ficha de identificación

                    if (fileDb.Created != null)
                    {
                        result.data.identificationAux.created_date = fileDb.Created.Value.ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
                    }
                    result.data.identificationAux.expFolio = fileDb.Id.ToString();
                    if (fileDb.Employee != null)
                    {
                        result.data.identificationAux.medicName = fileDb.Employee.Person.Name + " " + fileDb.Employee.Person.LastName + " " + fileDb.Employee.Person.SecondLastName;
                        result.data.identificationAux.medicId = fileDb.MedicId.Value;
                    }
                    result.data.identificationAux.patientFullName = patientAux.personAux.name + " " + patientAux.personAux.lastName + " " + patientAux.personAux.secondlastname;
                    result.data.identificationAux.curp = patientAux.personAux.curp;
                    result.data.identificationAux.birthdate_string = patientAux.personAux.birthDate.ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
                    if (patientAux.personAux.sex == "M")
                    {
                        result.data.identificationAux.sex_string = "Masculino";
                    }
                    else if (patientAux.personAux.sex == "F")
                    {
                        result.data.identificationAux.sex_string = "Femenino";
                    }
                    else
                    {
                        result.data.identificationAux.sex_string = "No definido";
                    }
                    //if (patientAux.personAux.nationalityAux != null)
                    //{
                    //    result.data.identificationAux.nationality_string = patientAux.personAux.nationalityAux.name;
                    //}
                    //else
                    //{
                    //    result.data.identificationAux.nationality_string = "S/N";
                    //}
                    result.data.identificationAux.age_string = patientAux.personAux.age.ToString();
                    if (fileDb.Clinic != null)
                    {
                        result.data.identificationAux.clinicName = fileDb.Clinic.Name;
                        result.data.identificationAux.clinicId = fileDb.Clinic.id;
                    }
                    else
                    {
                        result.data.identificationAux.clinicName = "S/N";
                        result.data.identificationAux.clinicId = 1;
                    }
                    result.data.identificationAux.consultId = consultAux.id;

                    #endregion


                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        #region Vieja función SaveElements
        //public static ElectronicFileResult SaveElements(ElectronicFileAux ConsultFile, List<EvolutionNoteAux> DataDeletedEvolutionNote)
        //{
        //    ElectronicFileResult result = new ElectronicFileResult();
        //    UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
        //    if (!res.success)
        //    {
        //        result.success = false;
        //        result.message = "La sesión ha finalizado.";
        //        return result;
        //    }

        //    using (dbINMEDIK db = new dbINMEDIK())
        //    {
        //        using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (ConsultFile.id > 0)
        //                {
        //                    ElectronicFile fileDb = db.ElectronicFile.Where(e => e.Id == ConsultFile.id).FirstOrDefault();
        //                    Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
        //                    PatientAux aux = new PatientAux();
        //                    DataHelper.fill(aux, patientDb);
        //                    DataHelper.fill(aux.personAux, patientDb.Person);

        //                    if (DataDeletedEvolutionNote != null)
        //                    {
        //                        foreach (var DeletedData in DataDeletedEvolutionNote)
        //                        {
        //                            EvolutionNote toDelete = db.EvolutionNote.Where(d => d.Id == DeletedData.id).FirstOrDefault();
        //                            EvolCurrentCondition ConditionDelete = db.EvolCurrentCondition.Where(c => c.Id == toDelete.EvolCurrentConditionId).FirstOrDefault();
        //                            EvolExploration explorationDelete = db.EvolExploration.Where(e => e.Id == toDelete.EvolExplorationId).FirstOrDefault();
        //                            db.EvolutionNote.Remove(toDelete);
        //                            db.EvolCurrentCondition.Remove(ConditionDelete);
        //                            db.EvolExploration.Remove(explorationDelete);
        //                        }
        //                    }

        //                    AHF ahfDb = db.AHF.Where(a => a.Id == ConsultFile.patientAux.ahfaux.id).FirstOrDefault();

        //                    if (ConsultFile.patientAux.ahfaux.commentsAux != null)
        //                    {
        //                        AHFComments commentsDb = db.AHFComments.Where(c => c.Id == ConsultFile.patientAux.ahfaux.commentsAux.id).FirstOrDefault();

        //                        if (commentsDb != null)
        //                        {
        //                            commentsDb.Comments = ConsultFile.patientAux.ahfaux.commentsAux.comments;
        //                            db.SaveChanges();
        //                        }
        //                    }

        //                    List<AHFFamily> familylist = db.AHFFamily.Where(f => f.AHFId == ConsultFile.patientAux.ahfaux.id).ToList();

        //                    if (familylist.Count > 0)
        //                    {
        //                        foreach (var family in familylist)
        //                        {
        //                            family.AHFDisease.Clear();
        //                            db.AHFFamily.Remove(family);
        //                        }
        //                        db.SaveChanges();
        //                    }

        //                    foreach (var FamilyDisease in ConsultFile.patientAux.ahfaux.familyAux)
        //                    {
        //                        AHFFamily family = db.AHFFamily.Create();
        //                        family.AHFId = ConsultFile.patientAux.ahfaux.id;
        //                        family.Grandfather = FamilyDisease.grandfather;
        //                        family.Grandmother = FamilyDisease.grandmother;
        //                        family.Father = FamilyDisease.father;
        //                        family.Mother = FamilyDisease.mother;
        //                        family.Brothers = FamilyDisease.brothers;
        //                        family.Uncles = FamilyDisease.uncles;
        //                        foreach (var Diseases in FamilyDisease.diseaseAux)
        //                        {
        //                            AHFDisease temp = db.AHFDisease.Where(d => d.Id == Diseases.id).FirstOrDefault();
        //                            family.AHFDisease.Add(temp);
        //                        }
        //                        ahfDb.AHFFamily.Add(family);
        //                    }
        //                    db.SaveChanges();

        //                    #region Antecedentes Personales Patologicos                            
        //                    APP appDb = db.APP.Where(p => p.Id == ConsultFile.patientAux.appAux.id).FirstOrDefault();

        //                    if (appDb != null)
        //                    {
        //                        appDb.Disease = ConsultFile.patientAux.appAux.disease;
        //                        appDb.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
        //                        appDb.Surgical = ConsultFile.patientAux.appAux.surgical;
        //                        appDb.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
        //                        appDb.Transfuctional = ConsultFile.patientAux.appAux.transfunctional;
        //                        appDb.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
        //                        appDb.Trauma = ConsultFile.patientAux.appAux.trauma;
        //                        appDb.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
        //                        appDb.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
        //                        appDb.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
        //                        appDb.Allergic = ConsultFile.patientAux.appAux.allergic;
        //                        appDb.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
        //                        appDb.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
        //                        appDb.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
        //                        appDb.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
        //                        appDb.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
        //                        appDb.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
        //                        appDb.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        APP appData = db.APP.Create();
        //                        appData.Disease = ConsultFile.patientAux.appAux.disease;
        //                        appData.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
        //                        appData.Surgical = ConsultFile.patientAux.appAux.surgical;
        //                        appData.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
        //                        appData.Transfuctional = ConsultFile.patientAux.appAux.transfunctional;
        //                        appData.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
        //                        appData.Trauma = ConsultFile.patientAux.appAux.trauma;
        //                        appData.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
        //                        appData.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
        //                        appData.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
        //                        appData.Allergic = ConsultFile.patientAux.appAux.allergic;
        //                        appData.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
        //                        appDb.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
        //                        appData.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
        //                        appDb.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
        //                        appDb.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
        //                        appDb.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
        //                        appData.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
        //                        db.APP.Add(appData);
        //                        db.SaveChanges();

        //                        patientDb.AppId = appData.Id;
        //                        result.data.patientAux.appAux.id = appData.Id;

        //                    }
        //                    #endregion

        //                    #region Antecedentes Personales no Patologicos
        //                    APNP apnpDb = db.APNP.Where(a => a.Id == ConsultFile.patientAux.apnpAux.id).FirstOrDefault();
        //                    if (apnpDb != null)
        //                    {
        //                        apnpDb.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
        //                        apnpDb.Feeding = ConsultFile.patientAux.apnpAux.feeding;
        //                        apnpDb.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
        //                        apnpDb.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        APNP apnpData = db.APNP.Create();
        //                        apnpData.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
        //                        apnpData.Feeding = ConsultFile.patientAux.apnpAux.feeding;
        //                        apnpData.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
        //                        apnpData.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
        //                        db.APNP.Add(apnpData);
        //                        db.SaveChanges();

        //                        patientDb.APNPId = apnpData.Id;
        //                        result.data.patientAux.apnpAux.id = apnpData.Id;
        //                    }
        //                    #endregion

        //                    #region Padecimiento Actual

        //                    CurrentCondition currentDb = db.CurrentCondition.Where(c => c.Id == ConsultFile.currentConditionAux.id).FirstOrDefault();

        //                    if (currentDb != null)
        //                    {
        //                        currentDb.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
        //                        currentDb.Subjective = ConsultFile.currentConditionAux.subjective;
        //                        currentDb.Objective = ConsultFile.currentConditionAux.objective;
        //                        currentDb.Analysis = ConsultFile.currentConditionAux.analysis;

        //                        if (currentDb.CourseThreatment.Count() > 0)
        //                        {
        //                            List<CourseThreatment> threatmentDB = db.CourseThreatment.Where(c => c.CurrentConditionId == fileDb.CurrentCondition.Id).ToList();
        //                            foreach (var previousPlan in threatmentDB)
        //                            {
        //                                db.CourseThreatment.Remove(previousPlan);
        //                            }

        //                            foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
        //                            {
        //                                CourseThreatment threatmentData = db.CourseThreatment.Create();
        //                                threatmentData.CurrentConditionId = currentDb.Id;
        //                                threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
        //                                threatmentData.Medicine = currentThreatmen.medicine;
        //                                threatmentData.Dose = currentThreatmen.dose;
        //                                threatmentData.GeneralIndication = currentThreatmen.generalIndication;

        //                                db.CourseThreatment.Add(threatmentData);
        //                            }
        //                            db.SaveChanges();
        //                        }
        //                        else
        //                        {
        //                            foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
        //                            {
        //                                CourseThreatment threatmentData = db.CourseThreatment.Create();
        //                                threatmentData.CurrentConditionId = currentDb.Id;
        //                                threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
        //                                threatmentData.Medicine = currentThreatmen.medicine;
        //                                threatmentData.Dose = currentThreatmen.dose;
        //                                threatmentData.GeneralIndication = currentThreatmen.generalIndication;

        //                                db.CourseThreatment.Add(threatmentData);
        //                            }
        //                            db.SaveChanges();
        //                        }

        //                    }
        //                    else
        //                    {
        //                        CurrentCondition currentData = db.CurrentCondition.Create();
        //                        currentData.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
        //                        currentData.Subjective = ConsultFile.currentConditionAux.subjective;
        //                        currentData.Objective = ConsultFile.currentConditionAux.objective;
        //                        currentData.Analysis = ConsultFile.currentConditionAux.analysis;
        //                        db.SaveChanges();

        //                        foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
        //                        {
        //                            CourseThreatment threatmentData = db.CourseThreatment.Create();
        //                            threatmentData.CurrentConditionId = currentData.Id;
        //                            threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
        //                            threatmentData.Medicine = currentThreatmen.medicine;
        //                            threatmentData.Dose = currentThreatmen.dose;
        //                            threatmentData.GeneralIndication = currentThreatmen.generalIndication;

        //                            db.CourseThreatment.Add(threatmentData);
        //                        }
        //                        db.SaveChanges();
        //                    }

        //                    #endregion

        //                    #region Aparatos y sistemas
        //                    DeviceAndSystems deviceDb = db.DeviceAndSystems.Where(d => d.Id == ConsultFile.deviceAndSystemsAux.id).FirstOrDefault();
        //                    if (deviceDb != null)
        //                    {
        //                        deviceDb.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
        //                        deviceDb.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
        //                        deviceDb.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
        //                        deviceDb.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
        //                        deviceDb.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
        //                        deviceDb.Genital = ConsultFile.deviceAndSystemsAux.genital;
        //                        deviceDb.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
        //                        deviceDb.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
        //                        deviceDb.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
        //                        deviceDb.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
        //                        deviceDb.Others = ConsultFile.deviceAndSystemsAux.others;
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        DeviceAndSystems deviceData = db.DeviceAndSystems.Create();
        //                        deviceData.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
        //                        deviceData.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
        //                        deviceData.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
        //                        deviceData.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
        //                        deviceData.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
        //                        deviceData.Genital = ConsultFile.deviceAndSystemsAux.genital;
        //                        deviceData.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
        //                        deviceData.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
        //                        deviceData.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
        //                        deviceData.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
        //                        deviceData.Others = ConsultFile.deviceAndSystemsAux.others;

        //                        db.DeviceAndSystems.Add(deviceData);
        //                        db.SaveChanges();

        //                        fileDb.DeviceAndSystemsId = deviceData.Id;
        //                    }
        //                    #endregion

        //                    #region Exploracion
        //                    Exploration explorationDb = db.Exploration.Where(e => e.Id == ConsultFile.explorationAux.id).FirstOrDefault();
        //                    if (explorationDb != null)
        //                    {
        //                        explorationDb.Habitus = ConsultFile.explorationAux.habitus;
        //                        explorationDb.Temperature = ConsultFile.explorationAux.temperature;
        //                        explorationDb.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
        //                        explorationDb.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
        //                        explorationDb.HeartRate = ConsultFile.explorationAux.heartRate;
        //                        explorationDb.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
        //                        explorationDb.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
        //                        explorationDb.Weight = ConsultFile.explorationAux.weight;
        //                        explorationDb.Size = ConsultFile.explorationAux.size;
        //                        explorationDb.HipCircumference = ConsultFile.explorationAux.hipCircumference;
        //                        explorationDb.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
        //                        explorationDb.IMC = ConsultFile.explorationAux.imc;
        //                        if (aux.personAux.age >= 8)
        //                        {
        //                            explorationDb.PainScale = ConsultFile.explorationAux.painScaleAdult.ToString();
        //                            explorationDb.PainScaleAdult = ConsultFile.explorationAux.painScaleAdult;
        //                        }
        //                        else
        //                        {
        //                            explorationDb.PainScale = ConsultFile.explorationAux.painScale;
        //                            explorationDb.PainScaleAdult = Convert.ToInt32(ConsultFile.explorationAux.painScale);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        Exploration explorationData = db.Exploration.Create();
        //                        explorationData.Habitus = ConsultFile.explorationAux.habitus;
        //                        explorationData.Temperature = ConsultFile.explorationAux.temperature;
        //                        explorationData.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
        //                        explorationData.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
        //                        explorationData.HeartRate = ConsultFile.explorationAux.heartRate;
        //                        explorationData.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
        //                        explorationData.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
        //                        explorationData.Weight = ConsultFile.explorationAux.weight;
        //                        explorationData.Size = ConsultFile.explorationAux.size;
        //                        explorationData.HipCircumference = ConsultFile.explorationAux.hipCircumference;
        //                        explorationData.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
        //                        explorationData.IMC = ConsultFile.explorationAux.imc;
        //                        explorationData.PainScale = ConsultFile.explorationAux.painScale;

        //                        db.Exploration.Add(explorationData);
        //                        db.SaveChanges();
        //                        fileDb.ExplorationId = explorationData.Id;
        //                    }
        //                    #endregion

        //                    #region Resultados Previos
        //                    if (fileDb.PreviousResult.Count > 0)
        //                    {
        //                        List<PreviousResult> previousList = db.PreviousResult.Where(p => p.ElectronicFileId == ConsultFile.id).ToList();
        //                        foreach (var previous in previousList)
        //                        {
        //                            db.PreviousResult.Remove(previous);
        //                        }
        //                        foreach (var NewsResults in ConsultFile.previousResultAux)
        //                        {
        //                            PreviousResult previousData = db.PreviousResult.Create();
        //                            previousData.ElectronicFileId = ConsultFile.id;
        //                            previousData.Cholesterol = NewsResults.cholesterol;
        //                            previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
        //                            previousData.Triglycerides = NewsResults.triglycerides;
        //                            previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
        //                            previousData.Glucose = NewsResults.glucose;
        //                            previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
        //                            previousData.Glucose_Category = NewsResults.glucose_category;
        //                            db.PreviousResult.Add(previousData);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        foreach (var NewsResults in ConsultFile.previousResultAux)
        //                        {
        //                            PreviousResult previousData = db.PreviousResult.Create();
        //                            previousData.ElectronicFileId = ConsultFile.id;
        //                            previousData.Cholesterol = NewsResults.cholesterol;
        //                            previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
        //                            previousData.Triglycerides = NewsResults.triglycerides;
        //                            previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
        //                            previousData.Glucose = NewsResults.glucose;
        //                            previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
        //                            previousData.Glucose_Category = NewsResults.glucose_category;
        //                            db.PreviousResult.Add(previousData);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    #endregion

        //                    #region Diagnostico
        //                    if (fileDb.Diagnostic.Count > 0)
        //                    {
        //                        List<Diagnostic> diagList = db.Diagnostic.Where(d => d.ElectronicFileId == ConsultFile.id).ToList();
        //                        foreach (var diag in diagList)
        //                        {
        //                            db.Diagnostic.Remove(diag);
        //                        }

        //                        foreach (var Diags in ConsultFile.diagnosticAux)
        //                        {
        //                            Diagnostic diagnosticData = db.Diagnostic.Create();
        //                            diagnosticData.CIE10Id = Diags.cie10Aux.consecutivo;
        //                            diagnosticData.Pronostic = Diags.pronostic;
        //                            diagnosticData.ElectronicFileId = ConsultFile.id;
        //                            diagnosticData.Dose = Diags.dose;
        //                            diagnosticData.Presentation = Diags.presentation;
        //                            diagnosticData.WayOfAdministration = Diags.wayOfAdministration;
        //                            diagnosticData.Frequency = Diags.frequency;
        //                            diagnosticData.DaysOfThreatment = Diags.daysOfThreatment;
        //                            diagnosticData.ActiveSubstance = Diags.activeSubstance;
        //                            diagnosticData.CommercialBrand = Diags.commercialBrand;
        //                            diagnosticData.Unit = Diags.unit;
        //                            db.Diagnostic.Add(diagnosticData);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        foreach (var Diags in ConsultFile.diagnosticAux)
        //                        {
        //                            Diagnostic diagnosticData = db.Diagnostic.Create();
        //                            diagnosticData.CIE10Id = Diags.cie10Aux.consecutivo;
        //                            diagnosticData.Pronostic = Diags.pronostic;
        //                            diagnosticData.ElectronicFileId = ConsultFile.id;
        //                            diagnosticData.Dose = Diags.dose;
        //                            diagnosticData.Presentation = Diags.presentation;
        //                            diagnosticData.WayOfAdministration = Diags.wayOfAdministration;
        //                            diagnosticData.Frequency = Diags.frequency;
        //                            diagnosticData.DaysOfThreatment = Diags.daysOfThreatment;
        //                            diagnosticData.ActiveSubstance = Diags.activeSubstance;
        //                            diagnosticData.CommercialBrand = Diags.commercialBrand;
        //                            diagnosticData.Unit = Diags.unit;
        //                            db.Diagnostic.Add(diagnosticData);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    #endregion



        //                    db.SaveChanges();
        //                    dbTransaction.Commit();

        //                }
        //                else
        //                {
        //                    Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
        //                    PatientAux aux = new PatientAux();
        //                    DataHelper.fill(aux, patientDb);
        //                    DataHelper.fill(aux.personAux, patientDb.Person);


        //                    if (patientDb.AHF.Count == 0)
        //                    {
        //                        AHFComments ahfCommentsDb = db.AHFComments.Create();
        //                        ahfCommentsDb.Comments = ConsultFile.patientAux.ahfaux.commentsAux.comments;
        //                        db.AHFComments.Add(ahfCommentsDb);
        //                        db.SaveChanges();

        //                        AHF ahfDb = db.AHF.Create();
        //                        ahfDb.PatientId = ConsultFile.patientAux.id;
        //                        ahfDb.AHFCommentsId = ahfCommentsDb.Id;
        //                        db.AHF.Add(ahfDb);
        //                        db.SaveChanges();
        //                        result.data.patientAux.ahfaux.id = ahfDb.Id;

        //                        foreach (var FamilyDisease in ConsultFile.patientAux.ahfaux.familyAux)
        //                        {
        //                            AHFFamily family = db.AHFFamily.Create();
        //                            family.AHFId = ahfDb.Id;
        //                            family.Grandfather = FamilyDisease.grandfather;
        //                            family.Grandmother = FamilyDisease.grandmother;
        //                            family.Father = FamilyDisease.father;
        //                            family.Mother = FamilyDisease.mother;
        //                            family.Brothers = FamilyDisease.brothers;
        //                            family.Uncles = FamilyDisease.uncles;
        //                            foreach (var Diseases in FamilyDisease.diseaseAux)
        //                            {
        //                                AHFDisease temp = db.AHFDisease.Where(d => d.Id == Diseases.id).FirstOrDefault();
        //                                family.AHFDisease.Add(temp);
        //                            }
        //                            ahfDb.AHFFamily.Add(family);
        //                        }
        //                        db.SaveChanges();
        //                    }

        //                    #region Antecedentes Personales Patologicos
        //                    APP appData = db.APP.Create();
        //                    appData.Disease = ConsultFile.patientAux.appAux.disease;
        //                    appData.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
        //                    appData.Surgical = ConsultFile.patientAux.appAux.surgical;
        //                    appData.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
        //                    appData.Transfuctional = ConsultFile.patientAux.appAux.transfunctional;
        //                    appData.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
        //                    appData.Trauma = ConsultFile.patientAux.appAux.trauma;
        //                    appData.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
        //                    appData.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
        //                    appData.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
        //                    appData.Allergic = ConsultFile.patientAux.appAux.allergic;
        //                    appData.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
        //                    appData.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
        //                    appData.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
        //                    appData.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
        //                    appData.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
        //                    appData.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
        //                    appData.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
        //                    db.APP.Add(appData);
        //                    db.SaveChanges();

        //                    patientDb.AppId = appData.Id;
        //                    result.data.patientAux.appAux.id = appData.Id;
        //                    #endregion

        //                    #region Antecedentes Personales no 
        //                    APNP apnpData = db.APNP.Create();
        //                    apnpData.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
        //                    apnpData.Feeding = ConsultFile.patientAux.apnpAux.feeding;
        //                    apnpData.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
        //                    apnpData.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
        //                    db.APNP.Add(apnpData);
        //                    db.SaveChanges();

        //                    patientDb.APNPId = apnpData.Id;
        //                    result.data.patientAux.apnpAux.id = apnpData.Id;
        //                    #endregion

        //                    #region Padecimiento Actual
        //                    CurrentCondition currentData = db.CurrentCondition.Create();
        //                    currentData.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
        //                    currentData.Subjective = ConsultFile.currentConditionAux.subjective;
        //                    currentData.Objective = ConsultFile.currentConditionAux.objective;
        //                    currentData.Analysis = ConsultFile.currentConditionAux.analysis;
        //                    db.CurrentCondition.Add(currentData);
        //                    db.SaveChanges();

        //                    foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
        //                    {
        //                        CourseThreatment threatmentData = db.CourseThreatment.Create();
        //                        threatmentData.CurrentConditionId = currentData.Id;
        //                        threatmentData.CIE10Id = currentThreatmen.consecutivo;
        //                        threatmentData.Medicine = currentThreatmen.medicine;
        //                        threatmentData.Dose = currentThreatmen.dose;
        //                        threatmentData.GeneralIndication = currentThreatmen.generalIndication;

        //                        db.CourseThreatment.Add(threatmentData);
        //                    }
        //                    db.SaveChanges();
        //                    #endregion

        //                    #region Aparatos y sistemas
        //                    DeviceAndSystems deviceData = db.DeviceAndSystems.Create();
        //                    deviceData.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
        //                    deviceData.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
        //                    deviceData.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
        //                    deviceData.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
        //                    deviceData.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
        //                    deviceData.Genital = ConsultFile.deviceAndSystemsAux.genital;
        //                    deviceData.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
        //                    deviceData.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
        //                    deviceData.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
        //                    deviceData.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
        //                    deviceData.Others = ConsultFile.deviceAndSystemsAux.others;

        //                    db.DeviceAndSystems.Add(deviceData);
        //                    db.SaveChanges();
        //                    #endregion

        //                    #region Exploracion
        //                    Exploration explorationData = db.Exploration.Create();
        //                    explorationData.Habitus = ConsultFile.explorationAux.habitus;
        //                    explorationData.Temperature = ConsultFile.explorationAux.temperature;
        //                    explorationData.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
        //                    explorationData.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
        //                    explorationData.HeartRate = ConsultFile.explorationAux.heartRate;
        //                    explorationData.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
        //                    explorationData.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
        //                    explorationData.Weight = ConsultFile.explorationAux.weight;
        //                    explorationData.Size = ConsultFile.explorationAux.size;
        //                    explorationData.HipCircumference = ConsultFile.explorationAux.hipCircumference;
        //                    explorationData.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
        //                    explorationData.IMC = ConsultFile.explorationAux.imc;
        //                    if (aux.personAux.age >= 8)
        //                    {
        //                        explorationData.PainScale = ConsultFile.explorationAux.painScaleAdult.ToString();
        //                        explorationData.PainScaleAdult = ConsultFile.explorationAux.painScaleAdult;
        //                    }
        //                    else
        //                    {
        //                        explorationData.PainScale = ConsultFile.explorationAux.painScale;
        //                        explorationData.PainScaleAdult = Convert.ToInt32(ConsultFile.explorationAux.painScale);
        //                    }

        //                    db.Exploration.Add(explorationData);
        //                    db.SaveChanges();
        //                    #endregion

        //                    ElectronicFile consultFileInDb = db.ElectronicFile.Create();
        //                    consultFileInDb.PatientId = ConsultFile.patientAux.id;
        //                    consultFileInDb.ClinicId = ConsultFile.identificationAux.clinicId;
        //                    consultFileInDb.DeviceAndSystemsId = deviceData.Id;
        //                    consultFileInDb.ExplorationId = explorationData.Id;
        //                    if (ConsultFile.identificationAux.medicId == 0)
        //                    {
        //                        consultFileInDb.MedicId = null;
        //                    }
        //                    else
        //                    {
        //                        consultFileInDb.MedicId = ConsultFile.identificationAux.medicId;
        //                    }
        //                    consultFileInDb.Created = DateTime.UtcNow;

        //                    db.ElectronicFile.Add(consultFileInDb);
        //                    db.SaveChanges();

        //                    result.data.id = consultFileInDb.Id;

        //                    #region Diagnostico
        //                    foreach (var Diags in ConsultFile.diagnosticAux)
        //                    {
        //                        Diagnostic diagnosticData = db.Diagnostic.Create();
        //                        diagnosticData.CIE10Id = Diags.cie10Aux.consecutivo;
        //                        diagnosticData.Pronostic = Diags.pronostic;
        //                        diagnosticData.ElectronicFileId = consultFileInDb.Id;
        //                        diagnosticData.Dose = Diags.dose;
        //                        diagnosticData.Presentation = Diags.presentation;
        //                        diagnosticData.WayOfAdministration = Diags.wayOfAdministration;
        //                        diagnosticData.Frequency = Diags.frequency;
        //                        diagnosticData.DaysOfThreatment = Diags.daysOfThreatment;
        //                        diagnosticData.ActiveSubstance = Diags.activeSubstance;
        //                        diagnosticData.CommercialBrand = Diags.commercialBrand;
        //                        diagnosticData.Unit = Diags.unit;
        //                        db.Diagnostic.Add(diagnosticData);
        //                    }
        //                    db.SaveChanges();
        //                    #endregion

        //                    #region Resultados previos
        //                    foreach (var NewsResults in ConsultFile.previousResultAux)
        //                    {
        //                        PreviousResult previousData = db.PreviousResult.Create();
        //                        previousData.ElectronicFileId = consultFileInDb.Id;
        //                        previousData.Cholesterol = NewsResults.cholesterol;
        //                        previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
        //                        previousData.Triglycerides = NewsResults.triglycerides;
        //                        previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
        //                        previousData.Glucose = NewsResults.glucose;
        //                        previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
        //                        previousData.Glucose_Category = NewsResults.glucose_category;
        //                        db.PreviousResult.Add(previousData);
        //                    }
        //                    db.SaveChanges();
        //                    #endregion

        //                    #region Nota de evolución
        //                    foreach (var evolution in ConsultFile.evolutionNotesAux)
        //                    {
        //                        EvolCurrentCondition conditionData = db.EvolCurrentCondition.Create();
        //                        conditionData.ReasonForConsultation = evolution.evolutionConditionAux.reasonForConsultation;
        //                        conditionData.Subjective = evolution.evolutionConditionAux.subjective;
        //                        conditionData.Objective = evolution.evolutionConditionAux.objective;
        //                        conditionData.Analysis = evolution.evolutionConditionAux.analysis;
        //                        db.EvolCurrentCondition.Add(conditionData);

        //                        db.SaveChanges();

        //                        EvolutionNote evolutionData = db.EvolutionNote.Create();
        //                        evolutionData.ElectronicFileId = consultFileInDb.Id;
        //                        evolutionData.EvolCurrentConditionId = conditionData.Id;
        //                        //evolutionData.EvolExplorationId = ;
        //                        //evolutionData.EvolPreviousResultId = ;
        //                        //evolutionData.EvolDiagnosticId = ;
        //                        evolutionData.Created = DateTime.UtcNow;
        //                        evolutionData.CreatedBy = res.User.id.Value;
        //                        db.EvolutionNote.Add(evolutionData);

        //                        db.SaveChanges();
        //                    }
        //                    #endregion

        //                    dbTransaction.Commit();
        //                    result.success = true;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                dbTransaction.Rollback();
        //                result.success = false;
        //                result.exception = e;
        //                result.message = "Ocurrió un error inesperado. " + result.exception_message;
        //            }
        //        }
        //    }
        //    return result;
        //}
        #endregion

        public static ElectronicFileResult SaveClinicHistoryElements(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        List<ChronicDisease> chronicDisease = new List<ChronicDisease>();
                        if (ConsultFile.id > 0)
                        {
                            ElectronicFile fileDb = db.ElectronicFile.Where(e => e.Id == ConsultFile.id).FirstOrDefault();
                            Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
                            PatientAux aux = new PatientAux();
                            aux.ahfaux.commentsAux = ConsultFile.patientAux.ahfaux.commentsAux;
                            DataHelper.fill(aux, patientDb);
                            DataHelper.fill(aux.personAux, patientDb.Person);


                            if (ConsultFile.patientAux.appAux.dm)
                            {
                                var dm = db.ChronicDisease.FirstOrDefault(c => c.Code == "DM");
                                chronicDisease.Add(dm);
                            }
                            if (ConsultFile.patientAux.appAux.hta)
                            {
                                var hta = db.ChronicDisease.FirstOrDefault(c => c.Code == "HTA");
                                chronicDisease.Add(hta);
                            }

                            //foreach (ChronicDisease item in chronicDisease)
                            //{
                            //    fileDb.ChronicDisease.Add(item);
                            //}

                            db.SaveChanges();

                            AHF ahfDb = db.AHF.Where(a => a.Id == ConsultFile.patientAux.ahfaux.id).FirstOrDefault();
                            AHFAux ahfAux = new AHFAux();
                            ahfAux.id = ahfDb.Id;

                            if (ConsultFile.patientAux.ahfaux.commentsAux != null)
                            {
                                AHFComments commentsDb = db.AHFComments.Where(c => c.Id == ConsultFile.patientAux.ahfaux.commentsAux.id).FirstOrDefault();

                                if (commentsDb != null)
                                {
                                    commentsDb.Comments = ConsultFile.patientAux.ahfaux.commentsAux.comments;
                                    commentsDb.Updated = DateTime.UtcNow;
                                    commentsDb.UpdatedBy = res.User.id.Value;
                                    db.SaveChanges();
                                }
                            }

                            List<AHFFamily> familylist = db.AHFFamily.Where(f => f.AHFId == ConsultFile.patientAux.ahfaux.id).ToList();

                            if (familylist.Count > 0)
                            {
                                foreach (var family in familylist)
                                {
                                    family.AHFDisease.Clear();
                                    db.AHFFamily.Remove(family);
                                }
                                db.SaveChanges();
                            }

                            foreach (var FamilyDisease in ConsultFile.patientAux.ahfaux.familyAux)
                            {
                                AHFFamily family = db.AHFFamily.Create();
                                family.AHFId = ConsultFile.patientAux.ahfaux.id;
                                family.Grandfather = FamilyDisease.grandfather;
                                family.Grandmother = FamilyDisease.grandmother;
                                family.Father = FamilyDisease.father;
                                family.Mother = FamilyDisease.mother;
                                family.Brothers = FamilyDisease.brothers;
                                family.Uncles = FamilyDisease.uncles;
                                //family.Updated = DateTime.UtcNow;
                                foreach (var Diseases in FamilyDisease.diseaseAux)
                                {
                                    AHFDisease temp = db.AHFDisease.Where(d => d.Id == Diseases.id).FirstOrDefault();
                                    family.AHFDisease.Add(temp);
                                }
                                ahfDb.AHFFamily.Add(family);
                            }

                            db.SaveChanges();

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                            electronicFileUpdate.ElectronicFileId = fileDb.Id;
                            electronicFileUpdate.Source = "Historia Clínica";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            #region Antecedentes Personales Patologicos                            
                            APP appDb = db.APP.Where(p => p.Id == ConsultFile.patientAux.appAux.id).FirstOrDefault();

                            if (appDb != null)
                            {
                                appDb.Disease = ConsultFile.patientAux.appAux.disease;
                                appDb.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
                                appDb.Surgical = ConsultFile.patientAux.appAux.surgical;
                                appDb.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
                                appDb.Transfuctional = ConsultFile.patientAux.appAux.transfuctional;
                                appDb.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
                                appDb.Trauma = ConsultFile.patientAux.appAux.trauma;
                                appDb.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
                                appDb.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
                                appDb.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
                                appDb.Allergic = ConsultFile.patientAux.appAux.allergic;
                                appDb.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
                                appDb.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
                                appDb.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
                                appDb.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
                                appDb.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
                                appDb.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
                                appDb.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
                                appDb.Updated = DateTime.UtcNow;
                                appDb.UpdatedBy = res.User.id.Value;
                                db.SaveChanges();
                            }
                            else
                            {
                                APP appData = db.APP.Create();
                                appData.Disease = ConsultFile.patientAux.appAux.disease;
                                appData.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
                                appData.Surgical = ConsultFile.patientAux.appAux.surgical;
                                appData.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
                                appData.Transfuctional = ConsultFile.patientAux.appAux.transfuctional;
                                appData.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
                                appData.Trauma = ConsultFile.patientAux.appAux.trauma;
                                appData.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
                                appData.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
                                appData.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
                                appData.Allergic = ConsultFile.patientAux.appAux.allergic;
                                appData.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
                                appDb.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
                                appData.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
                                appDb.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
                                appDb.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
                                appDb.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
                                appData.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
                                appDb.Created = DateTime.UtcNow;
                                db.APP.Add(appData);
                                db.SaveChanges();

                                patientDb.AppId = appData.Id;
                                result.data.patientAux.appAux.id = appData.Id;

                            }
                            #endregion

                            #region Antecedentes Personales no Patologicos
                            APNP apnpDb = db.APNP.Where(a => a.Id == ConsultFile.patientAux.apnpAux.id).FirstOrDefault();
                            if (apnpDb != null)
                            {
                                apnpDb.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
                                apnpDb.Feeding = ConsultFile.patientAux.apnpAux.feeding;
                                apnpDb.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
                                apnpDb.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
                                apnpDb.Updated = DateTime.UtcNow;
                                apnpDb.UpdatedBy = res.User.id.Value;
                                db.SaveChanges();
                            }
                            else
                            {
                                APNP apnpData = db.APNP.Create();
                                apnpData.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
                                apnpData.Feeding = ConsultFile.patientAux.apnpAux.feeding;
                                apnpData.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
                                apnpData.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
                                apnpData.Created = DateTime.UtcNow;
                                db.APNP.Add(apnpData);
                                db.SaveChanges();

                                patientDb.APNPId = apnpData.Id;
                                result.data.patientAux.apnpAux.id = apnpData.Id;
                            }
                            #endregion

                            #region Padecimiento Actual

                            CurrentCondition currentDb = db.CurrentCondition.Where(c => c.Id == ConsultFile.currentConditionAux.id).FirstOrDefault();

                            if (currentDb != null)
                            {
                                currentDb.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
                                currentDb.Subjective = ConsultFile.currentConditionAux.subjective;
                                currentDb.Objective = ConsultFile.currentConditionAux.objective;
                                currentDb.Analysis = ConsultFile.currentConditionAux.analysis;

                                if (currentDb.CourseThreatment.Count() > 0)
                                {
                                    List<CourseThreatment> threatmentDB = db.CourseThreatment.Where(c => c.CurrentConditionId == fileDb.CurrentCondition.Id).ToList();
                                    foreach (var previousPlan in threatmentDB)
                                    {
                                        db.CourseThreatment.Remove(previousPlan);
                                    }

                                    foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
                                    {
                                        CourseThreatment threatmentData = db.CourseThreatment.Create();
                                        threatmentData.CurrentConditionId = currentDb.Id;
                                        threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
                                        threatmentData.Medicine = currentThreatmen.medicine;
                                        threatmentData.Dose = currentThreatmen.dose;
                                        threatmentData.GeneralIndication = currentThreatmen.generalIndication;

                                        db.CourseThreatment.Add(threatmentData);
                                    }
                                    db.SaveChanges();
                                }
                                else
                                {
                                    foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
                                    {
                                        CourseThreatment threatmentData = db.CourseThreatment.Create();
                                        threatmentData.CurrentConditionId = currentDb.Id;
                                        threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
                                        threatmentData.Medicine = currentThreatmen.medicine;
                                        threatmentData.Dose = currentThreatmen.dose;
                                        threatmentData.GeneralIndication = currentThreatmen.generalIndication;

                                        db.CourseThreatment.Add(threatmentData);
                                    }
                                    db.SaveChanges();
                                }

                            }
                            else
                            {
                                CurrentCondition currentData = db.CurrentCondition.Create();
                                currentData.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
                                currentData.Subjective = ConsultFile.currentConditionAux.subjective;
                                currentData.Objective = ConsultFile.currentConditionAux.objective;
                                currentData.Analysis = ConsultFile.currentConditionAux.analysis;
                                db.SaveChanges();

                                foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
                                {
                                    CourseThreatment threatmentData = db.CourseThreatment.Create();
                                    threatmentData.CurrentConditionId = currentData.Id;
                                    threatmentData.CIE10Id = currentThreatmen.cie10Aux.consecutivo;
                                    threatmentData.Medicine = currentThreatmen.medicine;
                                    threatmentData.Dose = currentThreatmen.dose;
                                    threatmentData.GeneralIndication = currentThreatmen.generalIndication;

                                    db.CourseThreatment.Add(threatmentData);
                                }
                                db.SaveChanges();

                            }

                            #endregion

                            db.SaveChanges();
                            dbTransaction.Commit();

                        }
                        else
                        {
                            Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
                            PatientAux aux = new PatientAux();
                            DataHelper.fill(aux, patientDb);
                            DataHelper.fill(aux.personAux, patientDb.Person);

                            if (ConsultFile.patientAux.appAux.dm)
                            {
                                var dm = db.ChronicDisease.FirstOrDefault(c => c.Code == "DM");
                                chronicDisease.Add(dm);
                            }
                            if (ConsultFile.patientAux.appAux.hta)
                            {
                                var hta = db.ChronicDisease.FirstOrDefault(c => c.Code == "HTA");
                                chronicDisease.Add(hta);
                            }

                            db.SaveChanges();


                            if (ConsultFile.patientAux.ahfaux.familyAux.Count != 0)
                            {
                                AHFComments ahfCommentsDb = db.AHFComments.Create();
                                ahfCommentsDb.Comments = ConsultFile.patientAux.ahfaux.commentsAux.comments;
                                ahfCommentsDb.Created = DateTime.UtcNow;
                                db.AHFComments.Add(ahfCommentsDb);
                                db.SaveChanges();

                                AHF ahfDb = db.AHF.Create();
                                ahfDb.PatientId = ConsultFile.patientAux.id;
                                ahfDb.AHFCommentsId = ahfCommentsDb.Id;
                                db.AHF.Add(ahfDb);
                                db.SaveChanges();
                                result.data.patientAux.ahfaux.id = ahfDb.Id;

                                foreach (var FamilyDisease in ConsultFile.patientAux.ahfaux.familyAux)
                                {
                                    AHFFamily family = db.AHFFamily.Create();
                                    family.AHFId = ahfDb.Id;
                                    family.Grandfather = FamilyDisease.grandfather;
                                    family.Grandmother = FamilyDisease.grandmother;
                                    family.Father = FamilyDisease.father;
                                    family.Mother = FamilyDisease.mother;
                                    family.Brothers = FamilyDisease.brothers;
                                    family.Uncles = FamilyDisease.uncles;
                                    //family.Created = DateTime.UtcNow;
                                    foreach (var Diseases in FamilyDisease.diseaseAux)
                                    {
                                        AHFDisease temp = db.AHFDisease.Where(d => d.Id == Diseases.id).FirstOrDefault();
                                        family.AHFDisease.Add(temp);
                                    }
                                    ahfDb.AHFFamily.Add(family);
                                }
                                db.SaveChanges();
                            }

                            #region Antecedentes Personales Patologicos
                            APP appData = db.APP.Create();
                            appData.Disease = ConsultFile.patientAux.appAux.disease;
                            appData.CurrentDisease = ConsultFile.patientAux.appAux.currentDisease;
                            appData.Surgical = ConsultFile.patientAux.appAux.surgical;
                            appData.Surgical_string = ConsultFile.patientAux.appAux.surgical_string;
                            appData.Transfuctional = ConsultFile.patientAux.appAux.transfuctional;
                            appData.Transfunctional_string = ConsultFile.patientAux.appAux.transfunctional_string;
                            appData.Trauma = ConsultFile.patientAux.appAux.trauma;
                            appData.Trauma_string = ConsultFile.patientAux.appAux.trauma_string;
                            appData.PreviousInternals = ConsultFile.patientAux.appAux.previousInternals;
                            appData.PreviousInternals_string = ConsultFile.patientAux.appAux.previousInternals_string;
                            appData.Allergic = ConsultFile.patientAux.appAux.allergic;
                            appData.Allergic_string = ConsultFile.patientAux.appAux.allergic_string;
                            appData.Tobacco_question = ConsultFile.patientAux.appAux.tobacco_question;
                            appData.Tobacco_string = ConsultFile.patientAux.appAux.tobacco_string;
                            appData.Alcohol_question = ConsultFile.patientAux.appAux.alcohol_question;
                            appData.Alcohol_string = ConsultFile.patientAux.appAux.alcohol_string;
                            appData.Drugs_question = ConsultFile.patientAux.appAux.drugs_question;
                            appData.Drugs_string = ConsultFile.patientAux.appAux.drugs_string;
                            appData.Created = DateTime.UtcNow;
                            db.APP.Add(appData);
                            db.SaveChanges();

                            patientDb.AppId = appData.Id;
                            result.data.patientAux.appAux.id = appData.Id;
                            #endregion

                            #region Antecedentes Personales no 
                            APNP apnpData = db.APNP.Create();
                            apnpData.LivingPlace = ConsultFile.patientAux.apnpAux.livingPlace;
                            apnpData.Feeding = ConsultFile.patientAux.apnpAux.feeding;
                            apnpData.Hygiene = ConsultFile.patientAux.apnpAux.hygiene;
                            apnpData.PhysicalActivity = ConsultFile.patientAux.apnpAux.physicalActivity;
                            apnpData.Created = DateTime.UtcNow;
                            db.APNP.Add(apnpData);
                            db.SaveChanges();

                            patientDb.APNPId = apnpData.Id;
                            result.data.patientAux.apnpAux.id = apnpData.Id;
                            #endregion

                            #region Padecimiento Actual
                            CurrentCondition currentData = db.CurrentCondition.Create();
                            currentData.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
                            currentData.Subjective = ConsultFile.currentConditionAux.subjective;
                            currentData.Objective = ConsultFile.currentConditionAux.objective;
                            currentData.Analysis = ConsultFile.currentConditionAux.analysis;
                            db.CurrentCondition.Add(currentData);
                            db.SaveChanges();

                            foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
                            {
                                CourseThreatment threatmentData = db.CourseThreatment.Create();
                                threatmentData.CurrentConditionId = currentData.Id;
                                threatmentData.CIE10Id = currentThreatmen.consecutivo;
                                threatmentData.Medicine = currentThreatmen.medicine;
                                threatmentData.Dose = currentThreatmen.dose;
                                threatmentData.GeneralIndication = currentThreatmen.generalIndication;

                                db.CourseThreatment.Add(threatmentData);
                            }
                            db.SaveChanges();
                            #endregion

                            ElectronicFile consultFileInDb = db.ElectronicFile.Create();
                            consultFileInDb.PatientId = ConsultFile.patientAux.id;
                            consultFileInDb.ClinicId = ConsultFile.identificationAux.clinicId;
                            if (ConsultFile.identificationAux.medicId == 0)
                            {
                                consultFileInDb.MedicId = null;
                            }
                            else
                            {
                                consultFileInDb.MedicId = ConsultFile.identificationAux.medicId;
                            }
                            consultFileInDb.Created = DateTime.UtcNow;


                            db.ElectronicFile.Add(consultFileInDb);

                            //foreach (ChronicDisease item in chronicDisease)
                            //{
                            //    consultFileInDb.ChronicDisease.Add(item);
                            //}
                            db.SaveChanges();

                            result.data.id = consultFileInDb.Id;

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                            electronicFileUpdate.ElectronicFileId = result.data.id;
                            electronicFileUpdate.Source = "Historia Clínica";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();

                            dbTransaction.Commit();

                            result.success = true;
                        }
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }

        public static ElectronicFileResult SaveExplorationElements(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ConsultFile.id > 0)
                        {
                            ElectronicFile fileDb = db.ElectronicFile.Where(e => e.Id == ConsultFile.id).FirstOrDefault();
                            Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
                            PatientAux aux = new PatientAux();
                            DataHelper.fill(aux, patientDb);
                            DataHelper.fill(aux.personAux, patientDb.Person);



                            #region Aparatos y sistemas
                            DeviceAndSystems deviceDb = db.DeviceAndSystems.Where(d => d.Id == ConsultFile.deviceAndSystemsAux.id).FirstOrDefault();
                            if (deviceDb != null)
                            {
                                deviceDb.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
                                deviceDb.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
                                deviceDb.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
                                deviceDb.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
                                deviceDb.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
                                deviceDb.Genital = ConsultFile.deviceAndSystemsAux.genital;
                                deviceDb.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
                                deviceDb.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
                                deviceDb.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
                                deviceDb.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
                                deviceDb.Others = ConsultFile.deviceAndSystemsAux.others;

                                ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                                electronicFileUpdate.UpdatedBy = res.User.id.Value;
                                electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                                electronicFileUpdate.ElectronicFileId = ConsultFile.id;
                                electronicFileUpdate.Source = "Exploración física";
                                db.ElectronicFileUpdate.Add(electronicFileUpdate);
                                db.SaveChanges();
                            }
                            else
                            {
                                DeviceAndSystems deviceData = db.DeviceAndSystems.Create();
                                deviceData.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
                                deviceData.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
                                deviceData.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
                                deviceData.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
                                deviceData.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
                                deviceData.Genital = ConsultFile.deviceAndSystemsAux.genital;
                                deviceData.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
                                deviceData.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
                                deviceData.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
                                deviceData.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
                                deviceData.Others = ConsultFile.deviceAndSystemsAux.others;

                                db.DeviceAndSystems.Add(deviceData);

                                ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                                electronicFileUpdate.UpdatedBy = res.User.id.Value;
                                electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                                electronicFileUpdate.ElectronicFileId = ConsultFile.id;
                                electronicFileUpdate.Source = "Exploración física";
                                db.ElectronicFileUpdate.Add(electronicFileUpdate);
                                db.SaveChanges();

                                fileDb.DeviceAndSystemsId = deviceData.Id;
                            }
                            #endregion

                            #region Exploracion
                            Exploration explorationDb = db.Exploration.Where(e => e.Id == ConsultFile.explorationAux.id).FirstOrDefault();
                            if (explorationDb != null)
                            {
                                explorationDb.Habitus = ConsultFile.explorationAux.habitus;
                                explorationDb.Temperature = ConsultFile.explorationAux.temperature;
                                explorationDb.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
                                explorationDb.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
                                explorationDb.HeartRate = ConsultFile.explorationAux.heartRate;
                                explorationDb.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
                                explorationDb.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
                                explorationDb.Weight = ConsultFile.explorationAux.weight;
                                explorationDb.Size = ConsultFile.explorationAux.size;
                                explorationDb.HipCircumference = ConsultFile.explorationAux.hipCircumference;
                                explorationDb.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
                                explorationDb.IMC = ConsultFile.explorationAux.imc;
                                if (aux.personAux.age >= 8)
                                {
                                    explorationDb.PainScale = ConsultFile.explorationAux.painScaleAdult.ToString();
                                    explorationDb.PainScaleAdult = ConsultFile.explorationAux.painScaleAdult;
                                }
                                else
                                {
                                    explorationDb.PainScale = ConsultFile.explorationAux.painScale;
                                    explorationDb.PainScaleAdult = Convert.ToInt32(ConsultFile.explorationAux.painScale);
                                }
                                db.SaveChanges();
                            }
                            else
                            {
                                Exploration explorationData = db.Exploration.Create();
                                explorationData.Habitus = ConsultFile.explorationAux.habitus;
                                explorationData.Temperature = ConsultFile.explorationAux.temperature;
                                explorationData.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
                                explorationData.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
                                explorationData.HeartRate = ConsultFile.explorationAux.heartRate;
                                explorationData.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
                                explorationData.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
                                explorationData.Weight = ConsultFile.explorationAux.weight;
                                explorationData.Size = ConsultFile.explorationAux.size;
                                explorationData.HipCircumference = ConsultFile.explorationAux.hipCircumference;
                                explorationData.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
                                explorationData.IMC = ConsultFile.explorationAux.imc;
                                explorationData.PainScale = ConsultFile.explorationAux.painScale;

                                db.Exploration.Add(explorationData);
                                db.SaveChanges();
                                fileDb.ExplorationId = explorationData.Id;
                            }
                            #endregion

                            #region Resultados Previos
                            //if (fileDb.PreviousResult.Count > 0)
                            //{
                            //    List<PreviousResult> previousList = db.PreviousResult.Where(p => p.ElectronicFileId == ConsultFile.id).ToList();
                            //    foreach (var previous in previousList)
                            //    {
                            //        db.PreviousResult.Remove(previous);
                            //    }
                            //    foreach (var NewsResults in ConsultFile.previousResultAux)
                            //    {
                            //        PreviousResult previousData = db.PreviousResult.Create();
                            //        previousData.ElectronicFileId = ConsultFile.id;
                            //        previousData.Cholesterol = NewsResults.cholesterol;
                            //        previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
                            //        previousData.Triglycerides = NewsResults.triglycerides;
                            //        previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
                            //        previousData.Glucose = NewsResults.glucose;
                            //        previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
                            //        previousData.Glucose_Category = NewsResults.glucose_category;
                            //        previousData.Created = DateTime.UtcNow;
                            //        previousData.CreatedBy = res.User.id.Value;
                            //        db.PreviousResult.Add(previousData);
                            //    }

                            //    ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                            //    electronicFileUpdated.UpdatedBy = res.User.id.Value;
                            //    electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                            //    electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                            //    electronicFileUpdated.Source = "Resultados previos de laboratorio";
                            //    db.ElectronicFileUpdate.Add(electronicFileUpdated);
                            //    db.SaveChanges();
                            //}
                            //else
                            //{
                            //    foreach (var NewsResults in ConsultFile.previousResultAux)
                            //    {
                            //        PreviousResult previousData = db.PreviousResult.Create();
                            //        previousData.ElectronicFileId = ConsultFile.id;
                            //        previousData.Cholesterol = NewsResults.cholesterol;
                            //        previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
                            //        previousData.Triglycerides = NewsResults.triglycerides;
                            //        previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
                            //        previousData.Glucose = NewsResults.glucose;
                            //        previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
                            //        previousData.Glucose_Category = NewsResults.glucose_category;
                            //        previousData.Created = DateTime.UtcNow;
                            //        previousData.CreatedBy = res.User.id.Value;
                            //        db.PreviousResult.Add(previousData);
                            //    }

                            //    ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                            //    electronicFileUpdated.UpdatedBy = res.User.id.Value;
                            //    electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                            //    electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                            //    electronicFileUpdated.Source = "Resultados previos de laboratorio";
                            //    db.ElectronicFileUpdate.Add(electronicFileUpdated);
                            //    db.SaveChanges();
                            //}
                            #endregion


                            db.SaveChanges();
                            dbTransaction.Commit();

                        }
                        else
                        {
                            Patient patientDb = db.Patient.Where(p => p.id == ConsultFile.patientAux.id).FirstOrDefault();
                            PatientAux aux = new PatientAux();
                            DataHelper.fill(aux, patientDb);
                            DataHelper.fill(aux.personAux, patientDb.Person);

                            #region Aparatos y sistemas
                            DeviceAndSystems deviceData = db.DeviceAndSystems.Create();
                            deviceData.Respiratory = ConsultFile.deviceAndSystemsAux.respiratory;
                            deviceData.Cardiovascular = ConsultFile.deviceAndSystemsAux.cardiovascular;
                            deviceData.Digestive = ConsultFile.deviceAndSystemsAux.digestive;
                            deviceData.Urinary = ConsultFile.deviceAndSystemsAux.urinary;
                            deviceData.Musculoskeletal = ConsultFile.deviceAndSystemsAux.musculoskeletal;
                            deviceData.Genital = ConsultFile.deviceAndSystemsAux.genital;
                            deviceData.Hematological = ConsultFile.deviceAndSystemsAux.hematological;
                            deviceData.Endocrine = ConsultFile.deviceAndSystemsAux.endocrine;
                            deviceData.Nervous = ConsultFile.deviceAndSystemsAux.nervous;
                            deviceData.Psychosomatic = ConsultFile.deviceAndSystemsAux.psychosomatic;
                            deviceData.Others = ConsultFile.deviceAndSystemsAux.others;

                            db.DeviceAndSystems.Add(deviceData);

                            ElectronicFileUpdate electronicFileUpdate = db.ElectronicFileUpdate.Create();
                            electronicFileUpdate.UpdatedBy = res.User.id.Value;
                            electronicFileUpdate.UpdatedDate = DateTime.UtcNow;
                            electronicFileUpdate.ElectronicFileId = result.data.id;
                            electronicFileUpdate.Source = "Exploración física";
                            db.ElectronicFileUpdate.Add(electronicFileUpdate);
                            db.SaveChanges();
                            #endregion

                            #region Exploracion
                            Exploration explorationData = db.Exploration.Create();
                            explorationData.Habitus = ConsultFile.explorationAux.habitus;
                            explorationData.Temperature = ConsultFile.explorationAux.temperature;
                            explorationData.TA_Sistolica = ConsultFile.explorationAux.ta_sistolica;
                            explorationData.TA_Diastolica = ConsultFile.explorationAux.ta_diastolica;
                            explorationData.HeartRate = ConsultFile.explorationAux.heartRate;
                            explorationData.BreathingFrequency = ConsultFile.explorationAux.breathingFrequency;
                            explorationData.OxygenSaturation = ConsultFile.explorationAux.oxygenSaturation;
                            explorationData.Weight = ConsultFile.explorationAux.weight;
                            explorationData.Size = ConsultFile.explorationAux.size;
                            explorationData.HipCircumference = ConsultFile.explorationAux.hipCircumference;
                            explorationData.WaistCircumference = ConsultFile.explorationAux.waistCircumference;
                            explorationData.IMC = ConsultFile.explorationAux.imc;
                            if (aux.personAux.age >= 8)
                            {
                                explorationData.PainScale = ConsultFile.explorationAux.painScaleAdult.ToString();
                                explorationData.PainScaleAdult = ConsultFile.explorationAux.painScaleAdult;
                            }
                            else
                            {
                                explorationData.PainScale = ConsultFile.explorationAux.painScale;
                                explorationData.PainScaleAdult = Convert.ToInt32(ConsultFile.explorationAux.painScale);
                            }

                            db.Exploration.Add(explorationData);
                            db.SaveChanges();
                            #endregion

                            ElectronicFile consultFileInDb = db.ElectronicFile.Create();
                            consultFileInDb.PatientId = ConsultFile.patientAux.id;
                            consultFileInDb.ClinicId = ConsultFile.identificationAux.clinicId;
                            consultFileInDb.DeviceAndSystemsId = deviceData.Id;
                            consultFileInDb.ExplorationId = explorationData.Id;
                            if (ConsultFile.identificationAux.medicId == 0)
                            {
                                consultFileInDb.MedicId = null;
                            }
                            else
                            {
                                consultFileInDb.MedicId = ConsultFile.identificationAux.medicId;
                            }
                            consultFileInDb.Created = DateTime.UtcNow;

                            db.ElectronicFile.Add(consultFileInDb);
                            db.SaveChanges();

                            result.data.id = consultFileInDb.Id;

                            #region Resultados previos
                            //foreach (var NewsResults in ConsultFile.previousResultAux)
                            //{
                            //    PreviousResult previousData = db.PreviousResult.Create();
                            //    previousData.ElectronicFileId = consultFileInDb.Id;
                            //    previousData.Cholesterol = NewsResults.cholesterol;
                            //    previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
                            //    previousData.Triglycerides = NewsResults.triglycerides;
                            //    previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
                            //    previousData.Glucose = NewsResults.glucose;
                            //    previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
                            //    previousData.Glucose_Category = NewsResults.glucose_category;
                            //    db.PreviousResult.Add(previousData);
                            //}

                            //ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                            //electronicFileUpdated.UpdatedBy = res.User.id.Value;
                            //electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                            //electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                            //electronicFileUpdated.Source = "Resultados previos de laboratorio";
                            //db.ElectronicFileUpdate.Add(electronicFileUpdated);
                            //db.SaveChanges();
                            #endregion

                            dbTransaction.Commit();
                            result.success = true;
                        }
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }

        public static ElectronicFileResult SavePreviousResult(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    ElectronicFile fileDb = db.ElectronicFile.Where(e => e.Id == ConsultFile.id).FirstOrDefault();
                    if (fileDb.PreviousResult.Count > 0)
                    {
                        List<PreviousResult> previousList = db.PreviousResult.Where(p => p.ElectronicFileId == ConsultFile.id).ToList();
                        foreach (var previous in previousList)
                        {
                            db.PreviousResult.Remove(previous);
                        }
                        foreach (var NewsResults in ConsultFile.previousResultAux)
                        {
                            PreviousResult previousData = db.PreviousResult.Create();
                            previousData.ElectronicFileId = ConsultFile.id;
                            previousData.Cholesterol = NewsResults.cholesterol;
                            previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
                            previousData.Triglycerides = NewsResults.triglycerides;
                            previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
                            previousData.Glucose = NewsResults.glucose;
                            previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
                            previousData.Glucose_Category = NewsResults.glucose_category;
                            previousData.Comments = NewsResults.comments;
                            previousData.Created = DateTime.UtcNow;
                            previousData.CreatedBy = res.User.id.Value;
                            db.PreviousResult.Add(previousData);
                        }

                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Resultados previos de laboratorio";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();
                    }
                    else
                    {
                        foreach (var NewsResults in ConsultFile.previousResultAux)
                        {
                            PreviousResult previousData = db.PreviousResult.Create();
                            previousData.ElectronicFileId = ConsultFile.id;
                            previousData.Cholesterol = NewsResults.cholesterol;
                            previousData.DateOfTaking_Cholesterol = NewsResults.dateOfTaking_Cholesterol;
                            previousData.Triglycerides = NewsResults.triglycerides;
                            previousData.DateOfTaking_Triglycerides = NewsResults.dateOfTaking_Triglycerides;
                            previousData.Glucose = NewsResults.glucose;
                            previousData.DateOfTaking_Glucose = NewsResults.dateOfTaking_Glucose;
                            previousData.Glucose_Category = NewsResults.glucose_category;
                            previousData.Comments = NewsResults.comments;
                            previousData.Created = DateTime.UtcNow;
                            previousData.CreatedBy = res.User.id.Value;
                            db.PreviousResult.Add(previousData);
                        }

                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Resultados previos de laboratorio";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();
                    }

                }
                catch
                {

                }
            }
            return result;
        }

        public static ElectronicFileResult SaveServices(ElectronicFileAux ConsultFile, ServiceNoteAux services, string name, int idx, int electronicFileId, bool toPrint)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var dbElectronicFile = db.ElectronicFile.FirstOrDefault(e=> e.Id == ConsultFile.id);
                    List<Service> serviceList = db.Service.Where(p => p.ElectronicFileId == ConsultFile.id).ToList();
                    if (serviceList.Count > 0)
                    {
                        foreach (var service in serviceList)
                        {
                            List<Diagnostic> serviceDiagnostic = db.Diagnostic.Where(d => d.Service.id == service.id).ToList();
                            List<EvFile> serviceFile = db.EvFile.Where(s => s.ServiceId == service.id).ToList();
                            service.EvFile.Clear();
                            service.Diagnostic.Clear();
                            db.Service.Remove(service);
                            foreach (var item in serviceDiagnostic)
                            {
                                db.Diagnostic.Remove(item);
                            }
                            foreach (var item in serviceFile)
                            {
                                db.EvFile.Remove(item);
                            }

                        }
                        foreach (var NewService in ConsultFile.servicenoteAux)
                        {
                            var conseptdb = db.Concept.FirstOrDefault(c => c.Name == NewService.conceptAux.name);
                            Recipe recipe = new Recipe();
                            Service service;
                            Diagnostic diagnostic;


                            if (NewService.recipeAux.recipe != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = NewService.recipeAux.recipe;
                            }

                            service = db.Service.Create();
                            service.ElectronicFileId = ConsultFile.id;
                            service.PatientId = ConsultFile.patientAux.id;
                            service.ClinicId = ConsultFile.clinicAux.id;
                            service.ConceptId = conseptdb.id;
                            service.Notes = NewService.notes;
                            service.CreatedBy = ConsultFile.identificationAux.medicId;
                            service.Created = DateTime.UtcNow;
                            service.StatusId = 3;

                         
                            foreach (var Diagnostics in NewService.diagnosticAux)
                            {
                                diagnostic = db.Diagnostic.Create();
                                diagnostic.Pronostic = Diagnostics.pronostic;
                                diagnostic.CIE10Id = Diagnostics.cie10Aux.consecutivo != 0 ? Diagnostics.cie10Aux.consecutivo : Diagnostics.consecutivo;

                                service.Diagnostic.Add(diagnostic);
                            }
                            recipe.Service.Add(service);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();

                            foreach (var item in NewService.filesAux)
                            {
                                var serviceId = db.Service.FirstOrDefault(s => s.id == service.id);
                                ServiceNoteAux serviceNoteAux = new ServiceNoteAux();
                                serviceNoteAux.id = serviceId.id;
                                result.data.servicenoteAux.Add(serviceNoteAux);
                            }
                        }


                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Atenciones";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();

                        result.success = true;
                    }
                    else
                    {
                        foreach (var NewService in ConsultFile.servicenoteAux)
                        {
                            var conseptdb = db.Concept.FirstOrDefault(c => c.Name == NewService.conceptAux.name);
                            Recipe recipe = new Recipe();
                            Service service;
                            Diagnostic diagnostic;


                            if (NewService.recipeAux.recipe != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = NewService.recipeAux.recipe;
                            }

                            service = db.Service.Create();
                            service.ElectronicFileId = ConsultFile.id;
                            service.PatientId = ConsultFile.patientAux.id;
                            service.ClinicId = ConsultFile.clinicAux.id;
                            service.ConceptId = conseptdb.id;
                            service.Notes = NewService.notes;
                            service.CreatedBy = ConsultFile.identificationAux.medicId;
                            service.Created = DateTime.UtcNow;
                            service.StatusId = 3;


                            foreach (var Diagnostics in NewService.diagnosticAux)
                            {
                                diagnostic = db.Diagnostic.Create();
                                diagnostic.Pronostic = Diagnostics.pronostic;
                                diagnostic.CIE10Id = Diagnostics.cie10Aux.consecutivo != 0 ? Diagnostics.cie10Aux.consecutivo : Diagnostics.consecutivo;

                                service.Diagnostic.Add(diagnostic);
                                db.SaveChanges();
                            }
                            recipe.Service.Add(service);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();

                            foreach (var item in NewService.filesAux)
                            {
                                var serviceId = db.Service.FirstOrDefault(s => s.id == service.id);
                                ServiceNoteAux serviceNoteAux = new ServiceNoteAux();
                                serviceNoteAux.id = serviceId.id;
                                result.data.servicenoteAux.Add(serviceNoteAux);
                            }
                        }
                        

                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Atenciones";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();

                        result.success = true;
                    }
                }
                catch(Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                }
            }
            return result;
        }

        public static ElectronicFileResult SaveMedicaments(ElectronicFileAux ConsultFile, MedicamStock medicament)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var dbElectronicFile = db.ElectronicFile.FirstOrDefault(e => e.Id == ConsultFile.id);
                    List<ProductNote> productList = db.ProductNote.Where(p => p.ElectroniFileId == ConsultFile.id).ToList();
                    if (productList.Count > 0)
                    {

                        foreach (var product in productList)
                        {
                            List<Diagnostic> productDiagnostic = db.Diagnostic.Where(d => d.ProductId == product.Id).ToList();
                            List<Product> productDb = db.Product.Where(p => p.ProductNoteId == product.Id).ToList();
                            
                            db.ProductNote.Remove(product);
                            foreach (var item in productDiagnostic)
                            {
                                db.Diagnostic.Remove(item);
                            }
                            foreach (var item in productDb)
                            {
                                db.Product.Remove(item);
                            }
                            db.ProductNote.Remove(product);
                        }
                        foreach (var Newproduct in ConsultFile.medicnoteAux)
                        {
                            Recipe recipe = new Recipe();
                            ProductNote product;
                            Diagnostic diagnostic;
                            Product products;


                            if (Newproduct.recipeAux != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = Newproduct.notes;
                            }

                            product = db.ProductNote.Create();
                            product.ElectroniFileId = ConsultFile.id;
                            product.ClinicId = ConsultFile.identificationAux.clinicId;
                            product.Notes = Newproduct.notes;
                            product.CreatedBy = ConsultFile.identificationAux.medicId;
                            product.Created = DateTime.UtcNow;


                            foreach (var Diagnostics in Newproduct.diagnosticAux)
                            {
                                diagnostic = db.Diagnostic.Create();
                                diagnostic.Pronostic = Diagnostics.pronostic;
                                diagnostic.CIE10Id = Diagnostics.consecutivo;

                                product.Diagnostic.Add(diagnostic);
                            }
                            foreach (var medicaments in Newproduct.medicamstock)
                            {
                                var consept = db.Concept.FirstOrDefault(c => c.id == medicaments.medicamento.id);
                                products = db.Product.Create();
                                products.ConceptId = medicaments.medicamento.id;
                                products.Quantity = medicaments.quantity;
                                products.CreatedBy = ConsultFile.identificationAux.medicId;
                                products.Created = DateTime.UtcNow;

                                product.Product.Add(products);

                                var stock = db.Stock.FirstOrDefault(s=>s.ConceptId == medicaments.medicamento.id);
                                stock.InStock -= medicaments.quantity;
                            }
                            recipe.ProductNote.Add(product);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();
                        }


                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Atenciones";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();

                        result.success = true;
                    }
                    else
                    {
                        foreach (var NewProduct in ConsultFile.medicnoteAux)
                        {
                            var conseptdb = db.Concept.FirstOrDefault(c => c.Name == NewProduct.conceptAux.name);
                            Recipe recipe = new Recipe();
                            ProductNote product;
                            Diagnostic diagnostic;
                            Product products;


                            if (NewProduct.recipeAux != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = NewProduct.notes;
                            }

                            product = db.ProductNote.Create();
                            product.ElectroniFileId = ConsultFile.id;
                            //product.ConseptId = conseptdb.id;
                            
                            product.Notes = NewProduct.notes;
                            product.CreatedBy = ConsultFile.identificationAux.medicId;
                            product.Created = DateTime.UtcNow;


                            foreach (var Diagnostics in NewProduct.diagnosticAux)
                            {
                                diagnostic = db.Diagnostic.Create();
                                diagnostic.Pronostic = Diagnostics.pronostic;
                                diagnostic.CIE10Id = Diagnostics.consecutivo;
                                product.Diagnostic.Add(diagnostic);
                                db.SaveChanges();
                            }
                            foreach (var medicaments in NewProduct.medicamstock)
                            {
                                var consept = db.Concept.FirstOrDefault(c => c.id == medicaments.medicamento.id);
                                products = db.Product.Create();
                                products.ConceptId = consept.id;
                                products.Quantity = medicaments.quantity;
                                products.CreatedBy = ConsultFile.identificationAux.medicId;
                                products.Created = DateTime.UtcNow;

                                product.Product.Add(products);

                                var stock = db.Stock.FirstOrDefault(s => s.ConceptId == medicaments.medicamento.id);
                                stock.InStock -= medicaments.quantity;
                            }
                            recipe.ProductNote.Add(product);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();
                        }


                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Atenciones";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();

                        result.success = true;
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                }
            }
            return result;
        }

        public static ElectronicFileResult SaveExamns(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = new ElectronicFileResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var dbElectronicFile = db.ElectronicFile.FirstOrDefault(e => e.Id == ConsultFile.id);
                    List<Exam> examList = db.Exam.Where(p => p.ElectronicFileId == ConsultFile.id).ToList();
                    if (examList.Count > 0)
                    {
                        foreach (var exam in examList)
                        {
                            
                            foreach (var item in examList)
                            {
                                db.Exam.Remove(item);
                            }

                        }
                        foreach (var NewExam in ConsultFile.examnoteAux)
                        {
                            var conseptdb = db.Concept.FirstOrDefault(c => c.Name == NewExam.conceptAux.name);
                            Recipe recipe = new Recipe();
                            Exam exam;


                            if (NewExam.recipeAux != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = NewExam.recipeAux.recipe;
                            }

                            exam = db.Exam.Create();
                            exam.ElectronicFileId = ConsultFile.id;
                            exam.PatientId = ConsultFile.patientAux.id;
                            exam.ClinicId = ConsultFile.clinicAux.id;
                            exam.MedicId = ConsultFile.identificationAux.medicId;
                            exam.StatusId = 3;
                            exam.ConceptId = conseptdb.id;
                            exam.CreatedBy = ConsultFile.identificationAux.medicId;
                            exam.Created = DateTime.UtcNow;
                            exam.StatusId = 3;

                            recipe.Exam.Add(exam);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();
                        }


                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Examenes";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();
                    }
                    else
                    {
                        foreach (var NewExam in ConsultFile.examnoteAux)
                        {
                            var conseptdb = db.Concept.FirstOrDefault(c => c.Name == NewExam.conceptAux.name);
                            Recipe recipe = new Recipe();
                            Exam exam;


                            if (NewExam.recipeAux != null)
                            {
                                recipe = db.Recipe.Create();
                                recipe.RecipeText = NewExam.recipeAux.recipe;
                            }

                            exam = db.Exam.Create();
                            exam.ElectronicFileId = ConsultFile.id;
                            exam.PatientId = ConsultFile.patientAux.id;
                            exam.ClinicId = ConsultFile.clinicAux.id;
                            exam.MedicId = ConsultFile.identificationAux.medicId;
                            exam.StatusId = 3;
                            exam.ConceptId = conseptdb.id;
                            exam.CreatedBy = ConsultFile.identificationAux.medicId;
                            exam.Created = DateTime.UtcNow;
                            exam.StatusId = 3;

                            recipe.Exam.Add(exam);
                            db.Recipe.Add(recipe);
                            db.SaveChanges();
                        }


                        ElectronicFileUpdate electronicFileUpdated = db.ElectronicFileUpdate.Create();
                        electronicFileUpdated.UpdatedBy = res.User.id.Value;
                        electronicFileUpdated.UpdatedDate = DateTime.UtcNow;
                        electronicFileUpdated.ElectronicFileId = ConsultFile.id;
                        electronicFileUpdated.Source = "Examenes";
                        db.ElectronicFileUpdate.Add(electronicFileUpdated);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.exception = ex;
                }
            }
            return result;
        }

        public static ElectronicFileResult PreSavePlan(ElectronicFileAux ConsultFile)
        {
            ElectronicFileResult result = new ElectronicFileResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ConsultFile.currentConditionAux.coursethreatmentAux.Count() > 0)
                        {
                            var alreadySave = false;
                            foreach (var threatment in ConsultFile.currentConditionAux.coursethreatmentAux)
                            {
                                if (threatment.id == 0)
                                {
                                    alreadySave = false;
                                    break;
                                }
                                else
                                {
                                    alreadySave = true;
                                }
                            }

                            if (alreadySave)
                            {
                                result.success = true;
                            }
                            else
                            {
                                ElectronicFile fileDb = db.ElectronicFile.Where(f => f.Id == ConsultFile.id).FirstOrDefault();
                                if (fileDb.CurrentCondition != null)
                                {
                                    List<CourseThreatment> threatmentDB = db.CourseThreatment.Where(c => c.CurrentConditionId == fileDb.CurrentCondition.Id).ToList();
                                    foreach (var previousPlan in threatmentDB)
                                    {
                                        db.CourseThreatment.Remove(previousPlan);
                                    }

                                    foreach (var Plan in ConsultFile.currentConditionAux.coursethreatmentAux)
                                    {
                                        CourseThreatment courseData = db.CourseThreatment.Create();
                                        courseData.CurrentConditionId = ConsultFile.currentConditionAux.id;
                                        courseData.CIE10Id = Plan.consecutivo;
                                        courseData.Medicine = Plan.medicine;
                                        courseData.Dose = Plan.dose;
                                        courseData.GeneralIndication = Plan.generalIndication;
                                        db.CourseThreatment.Add(courseData);

                                    }

                                    db.SaveChanges();

                                    dbTransaction.Commit();
                                    result.success = true;
                                }
                                else
                                {
                                    CurrentCondition currentData = db.CurrentCondition.Create();
                                    currentData.ReasonForConsultation = ConsultFile.currentConditionAux.reasonForConsultation;
                                    currentData.Subjective = ConsultFile.currentConditionAux.subjective;
                                    currentData.Objective = ConsultFile.currentConditionAux.objective;
                                    currentData.Analysis = ConsultFile.currentConditionAux.analysis;

                                    db.CurrentCondition.Add(currentData);
                                    db.SaveChanges();

                                    foreach (var currentThreatmen in ConsultFile.currentConditionAux.coursethreatmentAux)
                                    {
                                        CourseThreatment threatmentData = db.CourseThreatment.Create();
                                        threatmentData.CurrentConditionId = currentData.Id;
                                        threatmentData.CIE10Id = currentThreatmen.consecutivo;
                                        threatmentData.Medicine = currentThreatmen.medicine;
                                        threatmentData.Dose = currentThreatmen.dose;
                                        threatmentData.GeneralIndication = currentThreatmen.generalIndication;

                                        db.CourseThreatment.Add(threatmentData);
                                    }
                                    fileDb.CurrentConditionId = currentData.Id;
                                    db.SaveChanges();

                                    dbTransaction.Commit();
                                    result.success = true;
                                }
                            }
                        }
                        else
                        {
                            result.success = false;
                            result.message = "No se encontraron elementos a guardar";
                        }
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }

        public static GenericResult BuildPlan(int evolutionNoteId, string userName)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK model = new dbINMEDIK())
            {
                try
                {
                    //var mConsult = model.Consult.Where(cons => cons.id == evolutionNoteId).FirstOrDefault();
                    var mUser = model.User.Where(user => user.UserAccount == userName).FirstOrDefault();
                    //var mEmployee = model.Employee.Where(e => e.UserId == mUser.id).FirstOrDefault();
                    var mRoleUser = UserHelper.GetUser(userName);
                    var evolutionDb = model.EvolutionNote.Where(e => e.Id == evolutionNoteId).FirstOrDefault();
                    var electronicFiledb = model.ElectronicFile.Where(c => c.Id == evolutionDb.ElectronicFileId).FirstOrDefault();
                    var mEmployee = model.Employee.Where(e => e.id == electronicFiledb.MedicId).FirstOrDefault();
                    var patientDb = model.Patient.Where(p => p.id == electronicFiledb.PatientId).FirstOrDefault();


                    if (mUser != null)
                    {
                        if (evolutionDb != null)
                        {
                            if (electronicFiledb.Employee.UserId == mUser.id || mRoleUser.User.rolAux.name == "Admin")
                            {
                                PersonAux employee = new PersonAux();
                                SpecialtyAux specialty = new SpecialtyAux();
                                DataHelper.fill(employee, electronicFiledb.Employee.Person);
                                DataHelper.fill(specialty, electronicFiledb.Employee.Specialty);

                                string templateName = "Receta Medica";
                                string doctorName = electronicFiledb.Employee.Person.Name + " " + electronicFiledb.Employee.Person.LastName + " " + electronicFiledb.Employee.Person.SecondLastName;
                                string licenseDoctor = electronicFiledb.Employee.Person.License;
                                string universityDoctor = electronicFiledb.Employee.Person.University;
                                string specialityDoctor = specialty.Name;
                                string patientName = patientDb.Person.Name + " " + patientDb.Person.LastName + " " + patientDb.Person.SecondLastName;
                                string consultDate = TimeZoneInfo.ConvertTimeFromUtc(
                                                     new DateTime(electronicFiledb.Created.Value.Ticks, DateTimeKind.Utc),
                                                     TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                                                     ).ToString("dd/MM/yyyy", new CultureInfo("es-MX"));

                                string clinicName = electronicFiledb.Clinic.Name;
                                string patientExp = patientDb.Person.id.ToString();
                                string patientAge = PatientHelper.GetPatientAge(electronicFiledb.PatientId).string_value;

                                string plan_strings = "";

                                foreach (var plan in evolutionDb.Recipe)
                                {
                                    foreach (var indication in plan.Indication)
                                    {
                                        plan_strings = plan_strings + indication.ActiveSubstance + ", " + indication.CommercialBrand + ", " + indication.Presentation + ", " +
                                            indication.Dose + ", " + indication.Unit + ", " + indication.Frequency + ", " + indication.WayOfAdministration + ", " + indication.DaysOfThreatment + "\n";
                                    }
                                }
                                string treatment = plan_strings;



                                if (!string.IsNullOrEmpty(treatment))
                                {
                                    treatment = treatment.Replace("\n", "<br/>");
                                }

                                string clinicCounty = electronicFiledb.Clinic.Address.County.Name;
                                string clinicAdressLine = electronicFiledb.Clinic.Address.AddressLine;
                                string clinicAdress = clinicAdressLine + " Col. " + clinicCounty;
                                string clinicEmail = electronicFiledb.Clinic.Email;
                                string clinicPhone = electronicFiledb.Clinic.PhoneNumber;
                                string logoName = "varaEsculapio.png";

                                var template = model.Template.Where(tem => tem.Name == "Receta Medica");
                                List<KeyValuePair<string, string>> ListKey_Value = new List<KeyValuePair<string, string>>();
                                ListKey_Value.Add(new KeyValuePair<string, string>("$logo", HostingEnvironment.MapPath("~/Content/Images/" + logoName)));
                                if (WebConfigurationManager.AppSettings.AllKeys.Contains("Settings.RecipeEmail"))
                                {
                                    ListKey_Value.Add(new KeyValuePair<string, string>("$pageInmedika", WebConfigurationManager.AppSettings["Settings.RecipeEmail"]));
                                }
                                else
                                {
                                    ListKey_Value.Add(new KeyValuePair<string, string>("$pageInmedika", ""));
                                }
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorName", doctorName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorLicense", licenseDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorUniversity", universityDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorSpeciality", specialityDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patientName", patientName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$consultDate", consultDate));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patientExp", patientExp));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patienAge", patientAge));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$treatment", treatment));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicName", clinicName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicAdress", clinicAdress));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicEmail", clinicEmail));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicPhone", clinicPhone));


                                result.string_value = BuildTemplateHelper.GenerateTemplate(templateName, ListKey_Value).string_value;
                                result.success = true;
                            }
                            else
                            {
                                result.string_value = BuildTemplateHelper.GenerateTemplateError("No se puede acceder a esta información").string_value;
                                return result;
                            }
                        }
                        else
                        {
                            result.string_value = BuildTemplateHelper.GenerateTemplateError("Consulta inexistente").string_value;
                            return result;
                        }
                    }
                    else
                    {
                        throw new Exception("La sesión ha caducado");
                    }


                }
                catch (Exception error)
                {
                    result.exception = error;
                    result.string_value = error.Message;
                }
            }
            return result;
        }

        public static ElectronicFileResult PreSaveRecipe(EvolutionNoteAux evolutionnote)
        {
            ElectronicFileResult result = new ElectronicFileResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (evolutionnote.recipeAux.Count() > 0)
                        {
                            var alreadySave = false;
                            foreach (var diagnostic in evolutionnote.recipeAux)
                            {
                                if (diagnostic.id == 0)
                                {
                                    alreadySave = false;
                                    break;
                                }
                                else
                                {
                                    alreadySave = true;
                                }
                            }

                            if (alreadySave)
                            {
                                result.success = true;
                            }
                            else
                            {
                                ElectronicFile fileDb = db.ElectronicFile.Where(f => f.Id == evolutionnote.id).FirstOrDefault();
                                if (fileDb.Diagnostic.Count > 0)
                                {
                                    List<Diagnostic> diagList = db.Diagnostic.Where(d => d.ElectronicFileId == evolutionnote.id).ToList();
                                    foreach (var diag in diagList)
                                    {
                                        db.Diagnostic.Remove(diag);
                                    }

                                    foreach (var Diags in evolutionnote.recipeAux)
                                    {
                                        //Diagnostic diagnosticData = db.Diagnostic.Create();
                                        //diagnosticData.CIE10Id = Diags.consecutivo;
                                        //diagnosticData.Pronostic = Diags.pronostic;
                                        //diagnosticData.ElectronicFileId = evolutionnote.id;
                                        //diagnosticData.Dose = Diags.dose;
                                        //diagnosticData.Presentation = Diags.presentation;
                                        //diagnosticData.WayOfAdministration = Diags.wayOfAdministration;
                                        //diagnosticData.Frequency = Diags.frequency;
                                        //diagnosticData.DaysOfThreatment = Diags.daysOfThreatment;
                                        //diagnosticData.ActiveSubstance = Diags.activeSubstance;
                                        //diagnosticData.CommercialBrand = Diags.commercialBrand;
                                        //diagnosticData.Unit = Diags.unit;
                                        //db.Diagnostic.Add(diagnosticData);
                                    }
                                    db.SaveChanges();

                                    dbTransaction.Commit();
                                    result.success = true;
                                }
                                else
                                {
                                    //foreach (var Diags in ConsultFile.diagnosticAux)
                                    //{
                                    //    Diagnostic diagnosticData = db.Diagnostic.Create();
                                    //    diagnosticData.CIE10Id = Diags.consecutivo;
                                    //    diagnosticData.Pronostic = Diags.pronostic;
                                    //    diagnosticData.ElectronicFileId = ConsultFile.id;
                                    //    diagnosticData.Dose = Diags.dose;
                                    //    diagnosticData.Presentation = Diags.presentation;
                                    //    diagnosticData.WayOfAdministration = Diags.wayOfAdministration;
                                    //    diagnosticData.Frequency = Diags.frequency;
                                    //    diagnosticData.DaysOfThreatment = Diags.daysOfThreatment;
                                    //    diagnosticData.ActiveSubstance = Diags.activeSubstance;
                                    //    diagnosticData.CommercialBrand = Diags.commercialBrand;
                                    //    diagnosticData.Unit = Diags.unit;
                                    //    db.Diagnostic.Add(diagnosticData);
                                    //}
                                    db.SaveChanges();

                                    dbTransaction.Commit();
                                    result.success = true;
                                }
                            }
                        }
                        else
                        {
                            result.success = false;
                            result.message = "No se encontraron elementos a guardar";
                        }
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }
            return result;
        }

        public static GenericResult BuildRecipe(int consultId, string userName)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK model = new dbINMEDIK())
            {
                try
                {
                    var mConsult = model.Consult.Where(cons => cons.id == consultId).FirstOrDefault();
                    var mUser = model.User.Where(user => user.UserAccount == userName).FirstOrDefault();
                    var mRoleUser = UserHelper.GetUser(userName);
                    var electronicFiledb = model.ElectronicFile.Where(c => c.Id == consultId).FirstOrDefault();

                    if (mUser != null)
                    {
                        if (mConsult != null)
                        {
                            if (mConsult.Employee.UserId == mUser.id || mRoleUser.User.rolAux.name == "Admin")
                            {
                                PersonAux employee = new PersonAux();
                                SpecialtyAux specialty = new SpecialtyAux();
                                DataHelper.fill(employee, mConsult.Employee.Person);
                                DataHelper.fill(specialty, mConsult.Employee.Specialty);

                                string templateName = "Receta Medica";
                                string doctorName = mConsult.Employee.Person.Name + " " + mConsult.Employee.Person.LastName;
                                string licenseDoctor = mConsult.Employee.Person.License;
                                string universityDoctor = mConsult.Employee.Person.University;
                                string specialityDoctor = specialty.Name;
                                string patientName = mConsult.Patient.Person.Name + " " + mConsult.Patient.Person.LastName;
                                string consultDate = TimeZoneInfo.ConvertTimeFromUtc(
                                                     new DateTime(mConsult.Updated.Ticks, DateTimeKind.Utc),
                                                     TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                                                     ).ToString("dd/MM/yyyy", new CultureInfo("es-MX"));

                                string clinicName = mConsult.Clinic.Name;
                                string patientExp = mConsult.Patient.Person.id.ToString();
                                string patientAge = PatientHelper.GetPatientAge(mConsult.PatientId).string_value;

                                string diagnostic_strings = "";

                                foreach (var diagnostic in electronicFiledb.Diagnostic)
                                {
                                    diagnostic_strings = diagnostic_strings + diagnostic.ActiveSubstance + ", " + diagnostic.CommercialBrand + ", " + diagnostic.Presentation + diagnostic.Dose + ", " + diagnostic.Unit + ", " + diagnostic.Frequency + ", " + diagnostic.WayOfAdministration + "Por(Días): " + ", " + diagnostic.DaysOfThreatment + "\n";
                                }
                                string treatment = diagnostic_strings;

                                if (!string.IsNullOrEmpty(treatment))
                                {
                                    treatment = treatment.Replace("\n", "<br/>");
                                }

                                string clinicCounty = mConsult.Clinic.Address.County.Name;
                                string clinicAdressLine = mConsult.Clinic.Address.AddressLine;
                                string clinicAdress = clinicAdressLine + " Col. " + clinicCounty;
                                string clinicEmail = mConsult.Clinic.Email;
                                string clinicPhone = mConsult.Clinic.PhoneNumber;
                                string logoName = "varaEsculapio.png";

                                var template = model.Template.Where(tem => tem.Name == "Receta Medica");
                                List<KeyValuePair<string, string>> ListKey_Value = new List<KeyValuePair<string, string>>();
                                ListKey_Value.Add(new KeyValuePair<string, string>("$logo", HostingEnvironment.MapPath("~/Content/Images/" + logoName)));
                                if (WebConfigurationManager.AppSettings.AllKeys.Contains("Settings.RecipeEmail"))
                                {
                                    ListKey_Value.Add(new KeyValuePair<string, string>("$pageInmedika", WebConfigurationManager.AppSettings["Settings.RecipeEmail"]));
                                }
                                else
                                {
                                    ListKey_Value.Add(new KeyValuePair<string, string>("$pageInmedika", ""));
                                }
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorName", doctorName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorLicense", licenseDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorUniversity", universityDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$doctorSpeciality", specialityDoctor));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patientName", patientName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$consultDate", consultDate));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patientExp", patientExp));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$patienAge", patientAge));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$treatment", treatment));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicName", clinicName));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicAdress", clinicAdress));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicEmail", clinicEmail));
                                ListKey_Value.Add(new KeyValuePair<string, string>("$clinicPhone", clinicPhone));


                                result.string_value = BuildTemplateHelper.GenerateTemplate(templateName, ListKey_Value).string_value;
                                result.success = true;
                            }
                            else
                            {
                                result.string_value = BuildTemplateHelper.GenerateTemplateError("No se puede acceder a esta información").string_value;
                                return result;
                            }
                        }
                        else
                        {
                            result.string_value = BuildTemplateHelper.GenerateTemplateError("Consulta inexistente").string_value;
                            return result;
                        }
                    }
                    else
                    {
                        throw new Exception("La sesión ha caducado");
                    }


                }
                catch (Exception error)
                {
                    result.exception = error;
                    result.string_value = error.Message;
                }
            }
            return result;
        }

    }
}