using System.Collections.Generic;

namespace DarkBattle.Services
{
  public interface IValidator
    {
        public bool IsValid(object dto);
        public Dictionary<string,string> Errors(object dto);
    }
}
