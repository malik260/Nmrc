namespace Mortgage.Ecosystem.BusinessLogic.Layer.Enums
{
    public enum NameFormat
    {
        // E.g. Fintrak, Software Limited
        Default,
        // E.g. Mr Software Limited Fintrak
        FullName,
        // E.g. Mr S L Fintrak
        FullNameAbbreviated,
        // E.g. Software Limited Fintrak
        FullNameNoTitle,
        // E.g. SLF
        Initials
    }
}
