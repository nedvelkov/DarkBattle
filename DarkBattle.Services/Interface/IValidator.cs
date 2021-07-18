namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;

    public interface IValidator
    {
        public bool IsValid(object dto);
        public Dictionary<string, string> Errors(object dto);
    }
}
