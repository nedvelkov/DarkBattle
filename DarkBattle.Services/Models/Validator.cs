namespace DarkBattle.Services.Models
{

    using System.Collections.Generic;
    using DarkBattle.Services.Interface;

    public class Validator : IValidator
    {
        public Dictionary<string, string> Errors(object dto)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValid(object dto)
        {
            return dto != null;
        }
    }
}
