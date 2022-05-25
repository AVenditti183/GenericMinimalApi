using System.ComponentModel.DataAnnotations;

namespace GenericMinimalApi.Infrastructures
{
    public static class ValidationModel
{
    public static (bool, Dictionary<string, string[]>) Validate(object obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(obj, validationContext, validationResults);
        if (!isValid)
        {
            var errors = validationResults.SelectMany(o => o.MemberNames.Select(m => new { Member = m, Error = o.ErrorMessage }))
                .GroupBy(s => s.Member)
                .Select(o => new
                {
                    Member = o.Key,
                    Errros = o.Select(s => s.Error).ToArray()
                }).ToDictionary(k => k.Member, v => v.Errros);

            return (false, errors);
        }

        return (true, null);
    }
}
    public static class ErrorExtensions
    {
        public static Dictionary<string, string[]> Map(this ArgumentException e)
            => new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> { new KeyValuePair<string, string[]>(e.ParamName, new string[] { e.Message }) });
    }
}