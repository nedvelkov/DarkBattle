using System.Collections.Generic;

namespace DarkBattle.Services
{

  public class Validator:IValidator
    {
        public Dictionary<string, string> Errors(object dto)
        {
            throw new System.NotImplementedException();
        }

        public  bool IsValid(object dto)
        {
            return dto != null;
        }
    }
}
